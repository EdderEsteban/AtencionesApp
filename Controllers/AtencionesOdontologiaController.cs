using AtencionesApp.Models.Data;
using AtencionesApp.Models.Entities;
using AtencionesApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AtencionesApp.Controllers;

[Authorize]
public class AtencionesOdontologiaController : Controller
{
    private readonly AppDbContext _db;

    public AtencionesOdontologiaController(AppDbContext db)
    {
        _db = db;
    }

    // GET /AtencionesOdontologia
    public async Task<IActionResult> Index(string? q, DateTime? fecha)
    {
        var query = _db.AtencionesOdontologia
            .Include(a => a.Paciente)
            .Include(a => a.Usuario)
            .Include(a => a.Diagnostico)
            .AsQueryable();

        var rol = User.FindFirstValue(ClaimTypes.Role);
        if (rol == "Odontólogo")
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            query = query.Where(a => a.UsuarioId == userId);
        }

        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(a =>
                a.Paciente.Apellido.Contains(q) ||
                a.Paciente.Nombre.Contains(q) ||
                a.Paciente.DNI.Contains(q));

        if (fecha.HasValue)
            query = query.Where(a => a.Fecha.Date == fecha.Value.Date);

        var atenciones = await query
            .OrderByDescending(a => a.Fecha)
            .Take(100)
            .ToListAsync();

        ViewBag.Q = q;
        ViewBag.Fecha = fecha?.ToString("yyyy-MM-dd");
        return View(atenciones);
    }

    // GET /AtencionesOdontologia/Create?pacienteId=X
    [Authorize(Roles = "Administrador,Odontólogo")]
    public async Task<IActionResult> Create(int? pacienteId)
    {
        if (!pacienteId.HasValue)
            return RedirectToAction("Index", "Pacientes");

        var paciente = await _db.Pacientes.FindAsync(pacienteId.Value);
        if (paciente == null) return NotFound();

        var vm = new AtencionOdontologiaFormViewModel
        {
            Fecha = DateTime.Today,
            PacienteId = paciente.Id,
            PacienteNombre = $"{paciente.Apellido}, {paciente.Nombre}",
            PacienteFechaNacimiento = paciente.FechaNacimiento.ToString("yyyy-MM-dd"),
            PacienteSexo = paciente.Sexo,
            PacienteTieneObraSocial = !string.IsNullOrEmpty(paciente.ObraSocial),
            PacienteObraSocial = paciente.ObraSocial,
            SinObraSocial = string.IsNullOrEmpty(paciente.ObraSocial)
        };

        await CargarViewBagCreate(paciente);
        return View(vm);
    }

    // POST /AtencionesOdontologia/Create
    [HttpPost, ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrador,Odontólogo")]
    public async Task<IActionResult> Create(AtencionOdontologiaFormViewModel vm)
    {
        if (vm.Prestaciones == null || !vm.Prestaciones.Any())
            ModelState.AddModelError("Prestaciones", "Agregá al menos una prestación");

        if (!ModelState.IsValid)
        {
            var pac0 = await _db.Pacientes.FindAsync(vm.PacienteId);
            await CargarViewBagCreate(pac0);
            return View(vm);
        }

        var paciente = await _db.Pacientes.FindAsync(vm.PacienteId);
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var edad = vm.Fecha.Year - paciente!.FechaNacimiento.Year;
        if (paciente.FechaNacimiento.DayOfYear > vm.Fecha.DayOfYear) edad--;

        var atencion = new AtencionOdontologia
        {
            Fecha = vm.Fecha,
            PacienteId = vm.PacienteId,
            InstitucionId = 1,
            UsuarioId = userId,
            TipoConsulta = vm.TipoConsulta,
            TipoTurno = vm.TipoTurno,
            DiagnosticoId = vm.DiagnosticoId,
            Edad = edad,
            Embarazada = vm.Embarazada,
            SinObraSocial = vm.SinObraSocial,
            Observaciones = string.IsNullOrWhiteSpace(vm.Observaciones) ? null : vm.Observaciones.Trim(),
            Prestaciones = vm.Prestaciones.Select(p => new PrestacionOdontologia
            {
                TipoPrestacionId = p.TipoPrestacionId,
                Cantidad = p.Cantidad
            }).ToList()
        };

        if (!vm.SinObraSocial && !string.IsNullOrWhiteSpace(vm.NuevaObraSocial))
            paciente!.ObraSocial = vm.NuevaObraSocial.Trim();
        else if (!vm.SinObraSocial && string.IsNullOrEmpty(paciente!.ObraSocial))
            atencion.SinObraSocial = true;

        if (vm.Valoracion != null)
        {
            atencion.ValoracionDental = new ValoracionDental
            {
                CariesPerm = vm.Valoracion.CariesPerm,
                PerdidosPerm = vm.Valoracion.PerdidosPerm,
                ObturadosPerm = vm.Valoracion.ObturadosPerm,
                CariesTemp = vm.Valoracion.CariesTemp,
                ExtraccionTemp = vm.Valoracion.ExtraccionTemp,
                ObturadosTemp = vm.Valoracion.ObturadosTemp
            };
        }

        _db.AtencionesOdontologia.Add(atencion);
        await _db.SaveChangesAsync();

        TempData["Exito"] = "Atención registrada correctamente";
        return RedirectToAction(nameof(Index));
    }

    // GET /AtencionesOdontologia/Edit/5
    [Authorize(Roles = "Administrador,Odontólogo")]
    public async Task<IActionResult> Edit(int id)
    {
        var atencion = await _db.AtencionesOdontologia
            .Include(a => a.Prestaciones)
            .Include(a => a.Paciente)
            .Include(a => a.ValoracionDental)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (atencion == null) return NotFound();

        var rol = User.FindFirstValue(ClaimTypes.Role);
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (rol == "Odontólogo" && atencion.UsuarioId != userId)
            return Forbid();

        var vm = new AtencionOdontologiaFormViewModel
        {
            Id = atencion.Id,
            Fecha = atencion.Fecha,
            PacienteId = atencion.PacienteId,
            PacienteNombre = $"{atencion.Paciente.Apellido}, {atencion.Paciente.Nombre}",
            PacienteFechaNacimiento = atencion.Paciente.FechaNacimiento.ToString("yyyy-MM-dd"),
            PacienteSexo = atencion.Paciente.Sexo,
            PacienteTieneObraSocial = !string.IsNullOrEmpty(atencion.Paciente.ObraSocial),
            PacienteObraSocial = atencion.Paciente.ObraSocial,
            TipoConsulta = atencion.TipoConsulta,
            TipoTurno = atencion.TipoTurno,
            DiagnosticoId = atencion.DiagnosticoId,
            Edad = atencion.Edad,
            Embarazada = atencion.Embarazada,
            SinObraSocial = atencion.SinObraSocial,
            Observaciones = atencion.Observaciones,
            Prestaciones = atencion.Prestaciones.Select(p => new PrestacionSeleccionadaVM
            {
                TipoPrestacionId = p.TipoPrestacionId,
                Cantidad = p.Cantidad
            }).ToList(),
            Valoracion = atencion.ValoracionDental == null ? null : new ValoracionDentalVM
            {
                CariesPerm = atencion.ValoracionDental.CariesPerm,
                PerdidosPerm = atencion.ValoracionDental.PerdidosPerm,
                ObturadosPerm = atencion.ValoracionDental.ObturadosPerm,
                CariesTemp = atencion.ValoracionDental.CariesTemp,
                ExtraccionTemp = atencion.ValoracionDental.ExtraccionTemp,
                ObturadosTemp = atencion.ValoracionDental.ObturadosTemp
            }
        };

        await CargarViewBagEdit();
        return View(vm);
    }

    // POST /AtencionesOdontologia/Edit/5
    [HttpPost, ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrador,Odontólogo")]
    public async Task<IActionResult> Edit(int id, AtencionOdontologiaFormViewModel vm)
    {
        if (vm.Prestaciones == null || !vm.Prestaciones.Any())
            ModelState.AddModelError("Prestaciones", "Agregá al menos una prestación");

        if (!ModelState.IsValid)
        {
            await CargarViewBagEdit();
            return View(vm);
        }

        var atencion = await _db.AtencionesOdontologia
            .Include(a => a.Prestaciones)
            .Include(a => a.ValoracionDental)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (atencion == null) return NotFound();

        var rol = User.FindFirstValue(ClaimTypes.Role);
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (rol == "Odontólogo" && atencion.UsuarioId != userId)
            return Forbid();

        var paciente = await _db.Pacientes.FindAsync(atencion.PacienteId);
        var edad = vm.Fecha.Year - paciente!.FechaNacimiento.Year;
        if (paciente.FechaNacimiento.DayOfYear > vm.Fecha.DayOfYear) edad--;

        atencion.Fecha = vm.Fecha;
        atencion.TipoConsulta = vm.TipoConsulta;
        atencion.TipoTurno = vm.TipoTurno;
        atencion.DiagnosticoId = vm.DiagnosticoId;
        atencion.Embarazada = vm.Embarazada;
        atencion.SinObraSocial = vm.SinObraSocial;
        atencion.Observaciones = string.IsNullOrWhiteSpace(vm.Observaciones) ? null : vm.Observaciones.Trim();
        atencion.Edad = edad;

        if (!vm.SinObraSocial && !string.IsNullOrWhiteSpace(vm.NuevaObraSocial))
            paciente!.ObraSocial = vm.NuevaObraSocial.Trim();
        else if (!vm.SinObraSocial && string.IsNullOrEmpty(paciente?.ObraSocial))
            atencion.SinObraSocial = true;

        _db.PrestacionesOdontologia.RemoveRange(atencion.Prestaciones);
        atencion.Prestaciones = vm.Prestaciones.Select(p => new PrestacionOdontologia
        {
            TipoPrestacionId = p.TipoPrestacionId,
            Cantidad = p.Cantidad
        }).ToList();

        if (vm.Valoracion != null)
        {
            if (atencion.ValoracionDental == null)
            {
                atencion.ValoracionDental = new ValoracionDental();
            }
            atencion.ValoracionDental.CariesPerm = vm.Valoracion.CariesPerm;
            atencion.ValoracionDental.PerdidosPerm = vm.Valoracion.PerdidosPerm;
            atencion.ValoracionDental.ObturadosPerm = vm.Valoracion.ObturadosPerm;
            atencion.ValoracionDental.CariesTemp = vm.Valoracion.CariesTemp;
            atencion.ValoracionDental.ExtraccionTemp = vm.Valoracion.ExtraccionTemp;
            atencion.ValoracionDental.ObturadosTemp = vm.Valoracion.ObturadosTemp;
        }
        else if (atencion.ValoracionDental != null)
        {
            atencion.ValoracionDental.IsDeleted = true;
        }

        await _db.SaveChangesAsync();
        TempData["Exito"] = "Atención actualizada correctamente";
        return RedirectToAction(nameof(Index));
    }

    // GET /AtencionesOdontologia/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var atencion = await _db.AtencionesOdontologia
            .Include(a => a.Paciente)
            .Include(a => a.Usuario)
            .Include(a => a.Diagnostico)
            .Include(a => a.Prestaciones).ThenInclude(p => p.TipoPrestacion)
            .Include(a => a.ValoracionDental)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (atencion == null) return NotFound();
        return View(atencion);
    }

    // POST /AtencionesOdontologia/Delete/5
    [HttpPost, ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Delete(int id)
    {
        var atencion = await _db.AtencionesOdontologia.FindAsync(id);
        if (atencion == null) return NotFound();
        atencion.IsDeleted = true;
        await _db.SaveChangesAsync();
        TempData["Exito"] = "Atención eliminada";
        return RedirectToAction(nameof(Index));
    }

    private async Task CargarViewBagCreate(Paciente? paciente)
    {
        ViewBag.Tipos = await _db.TiposPrestacionOdontologia
            .OrderBy(t => t.NombrePrestacion)
            .ToListAsync();
        ViewBag.Diagnosticos = await _db.Diagnosticos
            .OrderBy(d => d.Codigo)
            .ToListAsync();
        if (paciente != null)
            ViewBag.PacienteSubtitulo = $"{paciente.Apellido}, {paciente.Nombre} · DNI {paciente.DNI}";
    }

    private async Task CargarViewBagEdit()
    {
        ViewBag.Tipos = await _db.TiposPrestacionOdontologia
            .OrderBy(t => t.NombrePrestacion)
            .ToListAsync();
        ViewBag.Diagnosticos = await _db.Diagnosticos
            .OrderBy(d => d.Codigo)
            .ToListAsync();
    }
}
