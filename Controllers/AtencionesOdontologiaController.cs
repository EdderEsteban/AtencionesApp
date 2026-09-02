using AtencionesApp.Models.Data;
using AtencionesApp.Models.Entities;
using AtencionesApp.Models.ViewModels;
using AtencionesApp.Services;
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
        var instId = HttpContext.Session.GetInt32("InstitucionActivaId");
        if (rol == "Odontólogo")
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            query = query.Where(a => a.UsuarioId == userId);
            if (instId.HasValue)
                query = query.Where(a => a.InstitucionId == instId);
        }
        else if (rol == "Administrador" || rol == "Director")
        {
            if (instId.HasValue)
                query = query.Where(a => a.InstitucionId == instId);
        }
        else
        {
            return Forbid();
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

        var paciente = await _db.Pacientes
            .Include(p => p.ObraSocial)
            .FirstOrDefaultAsync(p => p.Id == pacienteId.Value);
        if (paciente == null) return NotFound();

        var vm = new AtencionOdontologiaFormViewModel
        {
            Fecha = DateTime.Today,
            PacienteId = paciente.Id,
            PacienteNombre = $"{paciente.Apellido}, {paciente.Nombre}",
            PacienteFechaNacimiento = paciente.FechaNacimiento.ToString("yyyy-MM-dd"),
            PacienteSexo = paciente.Sexo,
            PacienteTieneObraSocial = paciente.ObraSocialId != null,
            PacienteObraSocial = paciente.ObraSocial?.Nombre,
            SinObraSocial = paciente.ObraSocialId == null
        };

        // Auto-cargar estados del último odontograma del paciente
        var ultimaAtencion = await _db.AtencionesOdontologia
            .Where(a => a.PacienteId == pacienteId.Value)
            .Include(a => a.OdontogramaEstados)
            .OrderByDescending(a => a.Fecha)
            .FirstOrDefaultAsync();

        if (ultimaAtencion != null)
        {
            vm.OdontogramaEstados = ultimaAtencion.OdontogramaEstados
                .Select(e => new OdontogramaEstadoItemVM
                {
                    NumeroDiente = e.NumeroDiente,
                    Superficie = e.Superficie,
                    Estado = e.Estado
                }).ToList();
        }

        await CargarViewBagCreate(paciente);
        return View(vm);
    }

    // POST /AtencionesOdontologia/Create
    [HttpPost, ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrador,Odontólogo")]
    public async Task<IActionResult> Create(AtencionOdontologiaFormViewModel vm, string? odontogramaJson)
    {
        if (vm.Prestaciones == null || !vm.Prestaciones.Any())
            ModelState.AddModelError("Prestaciones", "Agregá al menos una prestación");

        if (!ModelState.IsValid)
        {
            var pac0 = await _db.Pacientes.FindAsync(vm.PacienteId);
            await CargarViewBagCreate(pac0);
            return View(vm);
        }

        var institucionId = HttpContext.Session.GetInt32("InstitucionActivaId");
        if (institucionId == null)
            return RedirectToAction("SeleccionarInstitucion", "Account");

        var paciente = await _db.Pacientes.FindAsync(vm.PacienteId);
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var ahora = DateTime.Now;
        var edad = ahora.Year - paciente!.FechaNacimiento.Year;
        if (paciente.FechaNacimiento.DayOfYear > ahora.DayOfYear) edad--;

        var atencion = new AtencionOdontologia
        {
            Fecha = ahora,
            PacienteId = vm.PacienteId,
            InstitucionId = institucionId.Value,
            UsuarioId = userId,
            TipoConsulta = vm.TipoConsulta,
            TipoTurno = vm.TipoTurno,
            DiagnosticoId = vm.DiagnosticoId,
            Edad = edad,
            Embarazada = vm.Embarazada,
            SinObraSocial = vm.SinObraSocial,
            Observaciones = string.IsNullOrWhiteSpace(vm.Observaciones) ? null : vm.Observaciones.Trim(),
            Prestaciones = (vm.Prestaciones ?? new()).Select(p => new PrestacionOdontologia
            {
                TipoPrestacionId = p.TipoPrestacionId,
                Cantidad = p.Cantidad
            }).ToList()
        };

        if (!vm.SinObraSocial && vm.NuevaObraSocialId != null)
            paciente!.ObraSocialId = vm.NuevaObraSocialId;
        else if (!vm.SinObraSocial && paciente!.ObraSocialId == null)
            atencion.SinObraSocial = true;

        // Odontograma (solo estados distintos de "Sano") → estados crudos
        var estados = new List<OdontogramaEstado>();
        if (!string.IsNullOrWhiteSpace(odontogramaJson) && odontogramaJson != "[]")
        {
            var opts = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var items = System.Text.Json.JsonSerializer.Deserialize<List<OdontogramaEstadoItemVM>>(odontogramaJson, opts) ?? new();
            estados = items.Where(e => e.Estado > 0).Select(e => new OdontogramaEstado
            {
                NumeroDiente = e.NumeroDiente,
                Superficie = e.Superficie,
                Estado = e.Estado
            }).ToList();
        }
        atencion.OdontogramaEstados = estados;

        // CPO/ceo recalculado en el servidor a partir de los estados crudos.
        // No se confía en vm.Valoracion (el navegador lo calcula solo para mostrar en vivo).
        atencion.ValoracionDental = CalculadoraCpo.Calcular(estados);

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
            .Include(a => a.Paciente).ThenInclude(p => p.ObraSocial)
            .Include(a => a.ValoracionDental)
            .Include(a => a.OdontogramaEstados)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (atencion == null) return NotFound();

        var rol = User.FindFirstValue(ClaimTypes.Role);
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (rol == "Odontólogo" && atencion.UsuarioId != userId)
            return Forbid();

        if (rol != "Administrador" && atencion.Fecha.Date != DateTime.Today)
        {
            TempData["Error"] = "Solo se pueden editar atenciones del día. Para modificar cargas de fechas anteriores, contactá a un administrador.";
            return RedirectToAction(nameof(Details), new { id });
        }

        var vm = new AtencionOdontologiaFormViewModel
        {
            Id = atencion.Id,
            Fecha = atencion.Fecha,
            PacienteId = atencion.PacienteId,
            PacienteNombre = $"{atencion.Paciente.Apellido}, {atencion.Paciente.Nombre}",
            PacienteFechaNacimiento = atencion.Paciente.FechaNacimiento.ToString("yyyy-MM-dd"),
            PacienteSexo = atencion.Paciente.Sexo,
            PacienteTieneObraSocial = atencion.Paciente.ObraSocialId != null,
            PacienteObraSocial = atencion.Paciente.ObraSocial?.Nombre,
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
            },
            OdontogramaEstados = atencion.OdontogramaEstados.Select(e => new OdontogramaEstadoItemVM
            {
                NumeroDiente = e.NumeroDiente,
                Superficie = e.Superficie,
                Estado = e.Estado
            }).ToList()
        };

        await CargarViewBagEdit();
        return View(vm);
    }

    // POST /AtencionesOdontologia/Edit/5
    [HttpPost, ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrador,Odontólogo")]
    public async Task<IActionResult> Edit(int id, AtencionOdontologiaFormViewModel vm, string? odontogramaJson)
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
            .Include(a => a.OdontogramaEstados)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (atencion == null) return NotFound();

        var rol = User.FindFirstValue(ClaimTypes.Role);
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (rol == "Odontólogo" && atencion.UsuarioId != userId)
            return Forbid();

        if (rol != "Administrador" && atencion.Fecha.Date != DateTime.Today)
        {
            TempData["Error"] = "Solo se pueden editar atenciones del día. Para modificar cargas de fechas anteriores, contactá a un administrador.";
            return RedirectToAction(nameof(Details), new { id });
        }

        var paciente = await _db.Pacientes.FindAsync(atencion.PacienteId);

        atencion.TipoConsulta = vm.TipoConsulta;
        atencion.TipoTurno = vm.TipoTurno;
        atencion.DiagnosticoId = vm.DiagnosticoId;
        atencion.Embarazada = vm.Embarazada;
        atencion.SinObraSocial = vm.SinObraSocial;
        atencion.Observaciones = string.IsNullOrWhiteSpace(vm.Observaciones) ? null : vm.Observaciones.Trim();

        if (!vm.SinObraSocial && vm.NuevaObraSocialId != null)
            paciente!.ObraSocialId = vm.NuevaObraSocialId;
        else if (!vm.SinObraSocial && paciente?.ObraSocialId == null)
            atencion.SinObraSocial = true;

        _db.PrestacionesOdontologia.RemoveRange(atencion.Prestaciones);
        atencion.Prestaciones = (vm.Prestaciones ?? new()).Select(p => new PrestacionOdontologia
        {
            TipoPrestacionId = p.TipoPrestacionId,
            Cantidad = p.Cantidad
        }).ToList();

        // Odontograma: soft-delete de los estados anteriores y alta de los nuevos
        foreach (var e in atencion.OdontogramaEstados)
            e.IsDeleted = true;

        var nuevosEstados = new List<OdontogramaEstado>();
        if (!string.IsNullOrWhiteSpace(odontogramaJson) && odontogramaJson != "[]")
        {
            var opts = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var items = System.Text.Json.JsonSerializer.Deserialize<List<OdontogramaEstadoItemVM>>(odontogramaJson, opts) ?? new();
            nuevosEstados = items.Where(e => e.Estado > 0).Select(e => new OdontogramaEstado
            {
                AtencionOdontologiaId = id,
                NumeroDiente = e.NumeroDiente,
                Superficie = e.Superficie,
                Estado = e.Estado
            }).ToList();
            foreach (var e in nuevosEstados)
                _db.OdontogramaEstados.Add(e);
        }

        // CPO/ceo recalculado en el servidor a partir de los estados nuevos (no se confía en vm.Valoracion).
        var cpo = CalculadoraCpo.Calcular(nuevosEstados);
        if (atencion.ValoracionDental == null)
            atencion.ValoracionDental = new ValoracionDental();
        atencion.ValoracionDental.IsDeleted = false;
        atencion.ValoracionDental.CariesPerm = cpo.CariesPerm;
        atencion.ValoracionDental.PerdidosPerm = cpo.PerdidosPerm;
        atencion.ValoracionDental.ObturadosPerm = cpo.ObturadosPerm;
        atencion.ValoracionDental.CariesTemp = cpo.CariesTemp;
        atencion.ValoracionDental.ExtraccionTemp = cpo.ExtraccionTemp;
        atencion.ValoracionDental.ObturadosTemp = cpo.ObturadosTemp;

        await _db.SaveChangesAsync();
        TempData["Exito"] = "Atención actualizada correctamente";
        return RedirectToAction(nameof(Index));
    }

    // GET /AtencionesOdontologia/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var atencion = await _db.AtencionesOdontologia
            .Include(a => a.Paciente).ThenInclude(p => p.ObraSocial)
            .Include(a => a.Usuario)
            .Include(a => a.Diagnostico)
            .Include(a => a.Prestaciones).ThenInclude(p => p.TipoPrestacion)
            .Include(a => a.ValoracionDental)
            .Include(a => a.OdontogramaEstados)
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
        ViewBag.ObrasSociales = await _db.ObrasSociales
            .Where(o => !o.IsDeleted)
            .OrderBy(o => o.Nombre)
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
        ViewBag.ObrasSociales = await _db.ObrasSociales
            .Where(o => !o.IsDeleted)
            .OrderBy(o => o.Nombre)
            .ToListAsync();
    }
}
