using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AtencionesApp.Models.Data;
using AtencionesApp.Models.Entities;
using AtencionesApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AtencionesApp.Controllers;

[Authorize(Roles = "Administrador")]
public class AdministracionController : Controller
{
    private readonly AppDbContext _db;
    private readonly IPasswordHasher<Usuario> _hasher;

    public AdministracionController(AppDbContext db, IPasswordHasher<Usuario> hasher)
    {
        _db = db;
        _hasher = hasher;
    }

    // ─── USUARIOS ──────────────────────────────────────────────────────────────

    public async Task<IActionResult> Usuarios()
    {
        var usuarios = await _db.Usuarios
            .Include(u => u.Rol)
            .Include(u => u.Instituciones)
            .OrderBy(u => u.Apellido).ThenBy(u => u.Nombre)
            .ToListAsync();
        return View(usuarios);
    }

    public async Task<IActionResult> CrearUsuario()
    {
        ViewBag.Roles = await _db.Roles.OrderBy(r => r.Nombre).ToListAsync();
        ViewBag.Instituciones = await _db.Instituciones
            .OrderBy(i => i.Nombre).ToListAsync();
        return View(new UsuarioFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CrearUsuario(UsuarioFormViewModel vm)
    {
        if (string.IsNullOrWhiteSpace(vm.Contrasena))
            ModelState.AddModelError("Contrasena", "La contraseña es requerida");

        if (!ModelState.IsValid)
        {
            ViewBag.Roles = await _db.Roles.OrderBy(r => r.Nombre).ToListAsync();
            ViewBag.Instituciones = await _db.Instituciones.OrderBy(i => i.Nombre).ToListAsync();
            return View(vm);
        }

        if (await _db.Usuarios.AnyAsync(u => u.Email == vm.Email))
        {
            ModelState.AddModelError("Email", "Ya existe un usuario con ese email");
            ViewBag.Roles = await _db.Roles.OrderBy(r => r.Nombre).ToListAsync();
            ViewBag.Instituciones = await _db.Instituciones.OrderBy(i => i.Nombre).ToListAsync();
            return View(vm);
        }

        var usuario = new Usuario
        {
            Nombre = vm.Nombre,
            Apellido = vm.Apellido,
            Email = vm.Email,
            Telefono = vm.Telefono,
            RolId = vm.RolId,
            PasswordHash = ""
        };
        usuario.PasswordHash = _hasher.HashPassword(usuario, vm.Contrasena!);

        if (vm.InstitucionIds.Any())
        {
            var instituciones = await _db.Instituciones
                .Where(i => vm.InstitucionIds.Contains(i.Id))
                .ToListAsync();
            foreach (var inst in instituciones)
                usuario.Instituciones.Add(inst);
        }

        _db.Usuarios.Add(usuario);
        await _db.SaveChangesAsync();
        TempData["Exito"] = "Usuario creado correctamente";
        return RedirectToAction(nameof(Usuarios));
    }

    public async Task<IActionResult> EditarUsuario(int id)
    {
        var usuario = await _db.Usuarios
            .Include(u => u.Instituciones)
            .FirstOrDefaultAsync(u => u.Id == id);
        if (usuario == null) return NotFound();

        var vm = new UsuarioFormViewModel
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Email = usuario.Email,
            Telefono = usuario.Telefono,
            RolId = usuario.RolId,
            InstitucionIds = usuario.Instituciones.Select(i => i.Id).ToList()
        };

        ViewBag.Roles = await _db.Roles.OrderBy(r => r.Nombre).ToListAsync();
        ViewBag.Instituciones = await _db.Instituciones.OrderBy(i => i.Nombre).ToListAsync();
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditarUsuario(int id, UsuarioFormViewModel vm)
    {
        // Contraseña es opcional en edición
        ModelState.Remove("Contrasena");
        ModelState.Remove("ConfirmarContrasena");

        if (!ModelState.IsValid)
        {
            ViewBag.Roles = await _db.Roles.OrderBy(r => r.Nombre).ToListAsync();
            ViewBag.Instituciones = await _db.Instituciones.OrderBy(i => i.Nombre).ToListAsync();
            return View(vm);
        }

        var usuario = await _db.Usuarios
            .Include(u => u.Instituciones)
            .FirstOrDefaultAsync(u => u.Id == id);
        if (usuario == null) return NotFound();

        if (await _db.Usuarios.AnyAsync(u => u.Email == vm.Email && u.Id != id))
        {
            ModelState.AddModelError("Email", "Ya existe un usuario con ese email");
            ViewBag.Roles = await _db.Roles.OrderBy(r => r.Nombre).ToListAsync();
            ViewBag.Instituciones = await _db.Instituciones.OrderBy(i => i.Nombre).ToListAsync();
            return View(vm);
        }

        usuario.Nombre = vm.Nombre;
        usuario.Apellido = vm.Apellido;
        usuario.Email = vm.Email;
        usuario.Telefono = vm.Telefono;
        usuario.RolId = vm.RolId;

        if (!string.IsNullOrWhiteSpace(vm.Contrasena))
            usuario.PasswordHash = _hasher.HashPassword(usuario, vm.Contrasena);

        // Actualizar instituciones (limpiar y reasignar)
        usuario.Instituciones.Clear();
        if (vm.InstitucionIds.Any())
        {
            var instituciones = await _db.Instituciones
                .Where(i => vm.InstitucionIds.Contains(i.Id))
                .ToListAsync();
            foreach (var inst in instituciones)
                usuario.Instituciones.Add(inst);
        }

        await _db.SaveChangesAsync();
        TempData["Exito"] = "Usuario actualizado correctamente";
        return RedirectToAction(nameof(Usuarios));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EliminarUsuario(int id)
    {
        var usuario = await _db.Usuarios.FindAsync(id);
        if (usuario == null) return NotFound();
        usuario.IsDeleted = true;
        await _db.SaveChangesAsync();
        TempData["Exito"] = "Usuario eliminado";
        return RedirectToAction(nameof(Usuarios));
    }

    // ─── PRESTACIONES ENFERMERÍA ────────────────────────────────────────────────

    public async Task<IActionResult> PrestacionesEnfermeria()
    {
        var prestaciones = await _db.TiposPrestacionEnfermeria
            .OrderBy(p => p.Grupo).ThenBy(p => p.NombrePrestacion)
            .ToListAsync();
        return View(prestaciones);
    }

    public IActionResult CrearPrestacionEnfermeria()
    {
        return View(new PrestacionEnfermeriaFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CrearPrestacionEnfermeria(PrestacionEnfermeriaFormViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        _db.TiposPrestacionEnfermeria.Add(new TipoPrestacionEnfermeria
        {
            Grupo = vm.Grupo.Trim(),
            NombrePrestacion = vm.NombrePrestacion.Trim()
        });
        await _db.SaveChangesAsync();
        TempData["Exito"] = "Prestación creada correctamente";
        return RedirectToAction(nameof(PrestacionesEnfermeria));
    }

    public async Task<IActionResult> EditarPrestacionEnfermeria(int id)
    {
        var p = await _db.TiposPrestacionEnfermeria.FindAsync(id);
        if (p == null) return NotFound();
        return View(new PrestacionEnfermeriaFormViewModel
        {
            Id = p.Id,
            Grupo = p.Grupo,
            NombrePrestacion = p.NombrePrestacion
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditarPrestacionEnfermeria(int id, PrestacionEnfermeriaFormViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var p = await _db.TiposPrestacionEnfermeria.FindAsync(id);
        if (p == null) return NotFound();

        p.Grupo = vm.Grupo.Trim();
        p.NombrePrestacion = vm.NombrePrestacion.Trim();
        await _db.SaveChangesAsync();
        TempData["Exito"] = "Prestación actualizada";
        return RedirectToAction(nameof(PrestacionesEnfermeria));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EliminarPrestacionEnfermeria(int id)
    {
        var p = await _db.TiposPrestacionEnfermeria.FindAsync(id);
        if (p == null) return NotFound();
        p.IsDeleted = true;
        await _db.SaveChangesAsync();
        TempData["Exito"] = "Prestación eliminada";
        return RedirectToAction(nameof(PrestacionesEnfermeria));
    }

    // ─── PRESTACIONES ODONTOLOGÍA ───────────────────────────────────────────────

    public async Task<IActionResult> PrestacionesOdontologia()
    {
        var prestaciones = await _db.TiposPrestacionOdontologia
            .OrderBy(p => p.NombrePrestacion)
            .ToListAsync();
        return View(prestaciones);
    }

    public IActionResult CrearPrestacionOdontologia()
    {
        return View(new PrestacionOdontologiaFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CrearPrestacionOdontologia(PrestacionOdontologiaFormViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        _db.TiposPrestacionOdontologia.Add(new TipoPrestacionOdontologia
        {
            NombrePrestacion = vm.NombrePrestacion.Trim()
        });
        await _db.SaveChangesAsync();
        TempData["Exito"] = "Prestación creada correctamente";
        return RedirectToAction(nameof(PrestacionesOdontologia));
    }

    public async Task<IActionResult> EditarPrestacionOdontologia(int id)
    {
        var p = await _db.TiposPrestacionOdontologia.FindAsync(id);
        if (p == null) return NotFound();
        return View(new PrestacionOdontologiaFormViewModel
        {
            Id = p.Id,
            NombrePrestacion = p.NombrePrestacion
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditarPrestacionOdontologia(int id, PrestacionOdontologiaFormViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var p = await _db.TiposPrestacionOdontologia.FindAsync(id);
        if (p == null) return NotFound();

        p.NombrePrestacion = vm.NombrePrestacion.Trim();
        await _db.SaveChangesAsync();
        TempData["Exito"] = "Prestación actualizada";
        return RedirectToAction(nameof(PrestacionesOdontologia));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EliminarPrestacionOdontologia(int id)
    {
        var p = await _db.TiposPrestacionOdontologia.FindAsync(id);
        if (p == null) return NotFound();
        p.IsDeleted = true;
        await _db.SaveChangesAsync();
        TempData["Exito"] = "Prestación eliminada";
        return RedirectToAction(nameof(PrestacionesOdontologia));
    }
}
