using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AtencionesApp.Models.Data;
using AtencionesApp.Models.Dtos;
using AtencionesApp.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;

namespace AtencionesApp.Controllers.Api;

[ApiController]
[Route("api/auth")]
public class AuthApiController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<Usuario> _passwordHasher;
    private readonly IConfiguration _config;
    private readonly IMemoryCache _cache;

    private const int MaxIntentos = 5;
    private static readonly TimeSpan VentanaLockout = TimeSpan.FromMinutes(15);

    public AuthApiController(AppDbContext context, IPasswordHasher<Usuario> passwordHasher,
        IConfiguration config, IMemoryCache cache)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _config = config;
        _cache = cache;
    }

    [HttpPost("login")]
    [EnableRateLimiting("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Email) || string.IsNullOrWhiteSpace(req.Password))
            return BadRequest(new { error = "Email y contraseña son obligatorios." });

        var emailKey = "api-login-fails:" + req.Email.Trim().ToLowerInvariant();
        if (_cache.TryGetValue<int>(emailKey, out var fallos) && fallos >= MaxIntentos)
            return StatusCode(429, new { error = "Demasiados intentos fallidos. Esperá unos minutos." });

        var usuario = await _context.Usuarios
            .Include(u => u.Rol)
            .Include(u => u.Instituciones)
            .FirstOrDefaultAsync(u => u.Email == req.Email && !u.IsDeleted);

        if (usuario == null ||
            _passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, req.Password) == PasswordVerificationResult.Failed)
        {
            _cache.Set(emailKey, fallos + 1, VentanaLockout);
            return Unauthorized(new { error = "Email o contraseña incorrectos." });
        }

        _cache.Remove(emailKey);

        var instituciones = usuario.Instituciones.ToList();
        if (!instituciones.Any())
            return StatusCode(403, new { error = "Tu cuenta no tiene instituciones asignadas. Contactá al administrador." });

        // 1 institución → token con institución ya elegida; >1 → token sin institución
        var unica = instituciones.Count == 1 ? instituciones[0] : null;
        var (token, expira) = GenerarToken(usuario, unica?.Id, unica?.Nombre);

        return Ok(new LoginResponse
        {
            Token = token,
            ExpiraUtc = expira,
            UsuarioId = usuario.Id,
            NombreCompleto = $"{usuario.Nombre} {usuario.Apellido}",
            Email = usuario.Email,
            Rol = usuario.Rol.Nombre,
            RequiereSeleccion = unica == null,
            InstitucionActivaId = unica?.Id,
            Instituciones = instituciones
                .Select(i => new InstitucionDto { Id = i.Id, Nombre = i.Nombre })
                .ToList()
        });
    }

    [HttpPost("institucion")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> SeleccionarInstitucion([FromBody] SeleccionInstitucionRequest req)
    {
        var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var usuario = await _context.Usuarios
            .Include(u => u.Rol)
            .Include(u => u.Instituciones)
            .FirstOrDefaultAsync(u => u.Id == usuarioId && !u.IsDeleted);

        if (usuario == null) return Unauthorized();

        // Validar pertenencia (protección IDOR): solo una institución asignada al usuario
        var inst = usuario.Instituciones.FirstOrDefault(i => i.Id == req.InstitucionId);
        if (inst == null) return Forbid();

        var (token, expira) = GenerarToken(usuario, inst.Id, inst.Nombre);

        return Ok(new LoginResponse
        {
            Token = token,
            ExpiraUtc = expira,
            UsuarioId = usuario.Id,
            NombreCompleto = $"{usuario.Nombre} {usuario.Apellido}",
            Email = usuario.Email,
            Rol = usuario.Rol.Nombre,
            RequiereSeleccion = false,
            InstitucionActivaId = inst.Id,
            Instituciones = usuario.Instituciones
                .Select(i => new InstitucionDto { Id = i.Id, Nombre = i.Nombre })
                .ToList()
        });
    }

    // ── Helper: genera el JWT, con institución opcional como claims ──
     private (string token, DateTime expira) GenerarToken(Usuario usuario, int? institucionId, string? institucionNombre)
    {
        var jwt = _config.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expira = DateTime.UtcNow.AddHours(int.Parse(jwt["ExpireHours"] ?? "12"));

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellido}"),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Role, usuario.Rol.Nombre)
        };

        if (institucionId.HasValue)
        {
            claims.Add(new Claim("institucionId", institucionId.Value.ToString()));
            claims.Add(new Claim("institucionNombre", institucionNombre ?? ""));
        }

        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: expira,
            signingCredentials: creds);

        return (new JwtSecurityTokenHandler().WriteToken(token), expira);
    }
}
