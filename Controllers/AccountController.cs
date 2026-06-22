using Microsoft.AspNetCore.Mvc;
using AtencionesApp.Models.Data;
using AtencionesApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using AtencionesApp.Models.ViewModels;

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
            .FirstOrDefaultAsync(u => u.Email == model.Email && !u.IsDeleted);

        if (usuario == null)
        {
            ViewBag.Error = "Email o contraseña incorrectos";
            return View(model);
        }

        var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash,
    model.Password);
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

        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}