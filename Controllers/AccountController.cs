using Microsoft.AspNetCore.Mvc;
using AtencionesApp.Models.Data;
using AtencionesApp.Models.Entities;
using AtencionesApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AtencionesApp.Controllers;

public class AccountController : Controller
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<Usuario> _passwordHasher;

    public AccountController(AppDbContext context, IPasswordHasher<Usuario> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var usuario = await _context.Usuarios
            .Include(u => u.Rol)
            .Include(u => u.Instituciones)
            .FirstOrDefaultAsync(u => u.Email == model.Email && !u.IsDeleted);

        if (usuario == null)
        {
            ViewBag.Error = "Email o contraseña incorrectos";
            return View(model);
        }

        var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, model.Password);
        if (resultado == PasswordVerificationResult.Failed)
        {
            ViewBag.Error = "Email o contraseña incorrectos";
            return View(model);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellido}"),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Role, usuario.Rol.Nombre)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
            new AuthenticationProperties { IsPersistent = model.Recordarme });

        // Administrador: puede operar sin institución (vista global)
        if (usuario.Rol.Nombre == "Administrador")
        {
            HttpContext.Session.Remove("InstitucionActivaId");
            HttpContext.Session.Remove("InstitucionActivaNombre");
            return RedirectToAction("Index", "Home");
        }

        // Otros roles: necesitan al menos una institución
        var instituciones = usuario.Instituciones.ToList();
        if (!instituciones.Any())
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.Error = "Tu cuenta no tiene instituciones asignadas. Contactá al administrador.";
            return View(model);
        }

        if (instituciones.Count == 1)
        {
            // Auto-seleccionar la única institución
            HttpContext.Session.SetInt32("InstitucionActivaId", instituciones[0].Id);
            HttpContext.Session.SetString("InstitucionActivaNombre", instituciones[0].Nombre);
            return RedirectToAction("Index", "Home");
        }

        // Múltiples instituciones: mostrar selector
        return RedirectToAction("SeleccionarInstitucion");
    }

    [Authorize]
    public async Task<IActionResult> SeleccionarInstitucion()
    {
        var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var usuario = await _context.Usuarios
            .Include(u => u.Instituciones)
            .FirstOrDefaultAsync(u => u.Id == usuarioId);

        if (usuario == null) return RedirectToAction("Login");

        return View(usuario.Instituciones.OrderBy(i => i.Nombre).ToList());
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmarInstitucion(int institucionId)
    {
        // Admin puede elegir cualquier institución; otros roles sólo las asignadas
        Institucion? inst;
        if (User.IsInRole("Administrador"))
        {
            inst = await _context.Instituciones.FindAsync(institucionId);
        }
        else
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var usuario = await _context.Usuarios
                .Include(u => u.Instituciones)
                .FirstOrDefaultAsync(u => u.Id == usuarioId);
            inst = usuario?.Instituciones.FirstOrDefault(i => i.Id == institucionId);
        }

        if (inst == null) return Forbid();

        HttpContext.Session.SetInt32("InstitucionActivaId", inst.Id);
        HttpContext.Session.SetString("InstitucionActivaNombre", inst.Nombre);
        return RedirectToAction("Index", "Home");
    }

    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> SeleccionarInstitucionAdmin()
    {
        var instituciones = await _context.Instituciones
            .OrderBy(i => i.Nombre)
            .ToListAsync();
        return View(instituciones);
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult VolverAVistaGlobal()
    {
        HttpContext.Session.Remove("InstitucionActivaId");
        HttpContext.Session.Remove("InstitucionActivaNombre");
        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CambiarInstitucion(int institucionId)
    {
        // Mismo control: validar pertenencia antes de mutar la sesión
        Institucion? inst;
        if (User.IsInRole("Administrador"))
        {
            inst = await _context.Instituciones.FindAsync(institucionId);
        }
        else
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            inst = await _context.Usuarios
                .Where(u => u.Id == usuarioId)
                .SelectMany(u => u.Instituciones)
                .FirstOrDefaultAsync(i => i.Id == institucionId);
        }

        if (inst == null) return Forbid();

        HttpContext.Session.SetInt32("InstitucionActivaId", inst.Id);
        HttpContext.Session.SetString("InstitucionActivaNombre", inst.Nombre);
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        HttpContext.Session.Clear();
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult AccessDenied() => View();
}
