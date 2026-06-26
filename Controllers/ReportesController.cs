using System.Globalization;
using System.Security.Claims;
using AtencionesApp.Models.Data;
using AtencionesApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AtencionesApp.Controllers;

[Authorize]
public class ReportesController : Controller
{
    private readonly AppDbContext _context;

    public ReportesController(AppDbContext context)
    {
        _context = context;
    }

    // ── helper: calcula desde/hasta según el período elegido ──
    private (DateTime desde, DateTime hasta, string label) ResolverPeriodo(
        string? periodo, DateTime? desde, DateTime? hasta)
    {
        var hoy = DateTime.Today;
        return periodo switch
        {
            "semana" => (hoy.AddDays(-6), hoy,
                         $"Últimos 7 días ({hoy.AddDays(-6):dd/MM/yyyy} – {hoy:dd/MM/yyyy})"),
            "anio" => (new DateTime(hoy.Year, 1, 1), hoy,
                         $"Año {hoy.Year}"),
            "personalizado" when desde.HasValue && hasta.HasValue
                     => (desde.Value.Date, hasta.Value.Date,
                         $"{desde.Value:dd/MM/yyyy} – {hasta.Value:dd/MM/yyyy}"),
            _ => (new DateTime(hoy.Year, hoy.Month, 1), hoy,
                         $"{hoy.ToString("MMMM yyyy", new CultureInfo("es-AR"))}") // "mes" es el default
        };
    }

    // ── helper: franja etaria ──
    private static int FranjaEdad(int edad) => edad switch
    {
        < 5 => 0,
        < 15 => 1,
        < 30 => 2,
        < 45 => 3,
        < 60 => 4,
        _ => 5
    };

    public async Task<IActionResult> Enfermeria(
    string? periodo, DateTime? desde, DateTime? hasta, int? profesionalId)
    {
        var rol = User.FindFirstValue(ClaimTypes.Role) ?? "";
        var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        var instId = HttpContext.Session.GetInt32("InstitucionActivaId");
        var instNombre = HttpContext.Session.GetString("InstitucionActivaNombre");

        if (rol == "Odontólogo") return Forbid();

        var (fechaDesde, fechaHasta, label) = ResolverPeriodo(periodo, desde, hasta);

        var vm = new ReporteEnfermeriaViewModel
        {
            Desde = fechaDesde,
            Hasta = fechaHasta,
            PeriodoLabel = label,
            ProfesionalId = profesionalId,
            InstitucionNombre = instNombre
        };

        // ── query base ──
        var baseQuery = _context.AtencionesEnfermeria
            .Where(a => a.Fecha.Date >= fechaDesde && a.Fecha.Date <= fechaHasta);

        if (rol == "Enfermero")
        {
            baseQuery = baseQuery.Where(a => a.UsuarioId == usuarioId);
        }
        else
        {
            if (instId.HasValue)
                baseQuery = baseQuery.Where(a => a.InstitucionId == instId);

            var enfsQuery = _context.Usuarios
                .Where(u => u.Rol.Nombre == "Enfermero" && !u.IsDeleted);
            if (instId.HasValue)
                enfsQuery = enfsQuery.Where(u => u.Instituciones.Any(i => i.Id == instId));

            vm.Profesionales = await enfsQuery
                .OrderBy(u => u.Apellido)
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.Apellido}, {u.Nombre}"
                })
                .ToListAsync();

            if (profesionalId.HasValue)
            {
                baseQuery = baseQuery.Where(a => a.UsuarioId == profesionalId);
                var prof = await _context.Usuarios.FindAsync(profesionalId);
                vm.ProfesionalNombre = prof != null ? $"{prof.Apellido}, {prof.Nombre}" : null;
            }
        }

        // ── cargar en memoria ──
        var atenciones = await baseQuery
            .Include(a => a.Paciente)
            .Include(a => a.Usuario)
            .ToListAsync();

        var atencionIds = atenciones.Select(a => a.Id).ToList();

        // ── resumen ──
        vm.TotalAtenciones = atenciones.Count;
        vm.PacientesUnicos = atenciones.Select(a => a.PacienteId).Distinct().Count();
        vm.TotalSinOS = atenciones.Count(a => a.SinObraSocial);
        vm.TotalConOS = vm.TotalAtenciones - vm.TotalSinOS;
        vm.TotalEmbarazadas = atenciones.Count(a => a.Embarazada);
        vm.PorcentajeSinOS = vm.TotalAtenciones > 0
            ? Math.Round((double)vm.TotalSinOS / vm.TotalAtenciones * 100, 1) : 0;
        vm.PorcentajeEmbarazadas = vm.TotalAtenciones > 0
            ? Math.Round((double)vm.TotalEmbarazadas / vm.TotalAtenciones * 100, 1) : 0;
        vm.TotalPrestaciones = await _context.PrestacionesEnfermeria
            .Where(p => atencionIds.Contains(p.AtencionId))
            .SumAsync(p => (int?)p.Cantidad) ?? 0;

        // ── tipo atención ──
        vm.TotalAmbulatorio = atenciones.Count(a => a.TipoAtencion == 1);
        vm.TotalInternado = atenciones.Count(a => a.TipoAtencion == 2);

        // ── cobertura OS ──  (ya calculado arriba)

        // ── sexo ──
        vm.SexoM = atenciones.Count(a => a.Paciente.Sexo == "M");
        vm.SexoF = atenciones.Count(a => a.Paciente.Sexo == "F");
        vm.SexoNoEsp = atenciones.Count(a => a.Paciente.Sexo == "NoEspecificado");

        // ── franjas etarias ──
        vm.SerieEdad = Enumerable.Range(0, 6)
            .Select(i => atenciones.Count(a => FranjaEdad(a.Edad) == i))
            .ToList();

        // ── actividad en el tiempo ──
        var dias = (fechaHasta - fechaDesde).Days + 1;
        if (dias <= 31)
        {
            vm.LabelsActividad = Enumerable.Range(0, dias)
                .Select(i => fechaDesde.AddDays(i).ToString("dd/MM"))
                .ToList();
            vm.SerieActividad = Enumerable.Range(0, dias)
                .Select(i => atenciones.Count(a => a.Fecha.Date == fechaDesde.AddDays(i)))
                .ToList();
        }
        else
        {
            var semanas = (int)Math.Ceiling(dias / 7.0);
            vm.LabelsActividad = Enumerable.Range(0, semanas)
                .Select(i => $"Sem {fechaDesde.AddDays(i * 7):dd/MM}")
                .ToList();
            vm.SerieActividad = Enumerable.Range(0, semanas)
                .Select(i =>
                {
                    var ini = fechaDesde.AddDays(i * 7);
                    var fin = ini.AddDays(6);
                    return atenciones.Count(a => a.Fecha.Date >= ini && a.Fecha.Date <= fin);
                })
                .ToList();
        }

        // ── top 10 prestaciones ──
        var top10 = await _context.PrestacionesEnfermeria
            .Where(p => atencionIds.Contains(p.AtencionId))
            .GroupBy(p => p.TipoPrestacion.NombrePrestacion)
            .Select(g => new { Nombre = g.Key, Total = g.Sum(x => x.Cantidad) })
            .OrderByDescending(x => x.Total)
            .Take(10)
            .ToListAsync();

        vm.Top10Nombres = top10.Select(x => x.Nombre).ToList();
        vm.Top10Cantidades = top10.Select(x => x.Total).ToList();

        // ── por profesional (Director / Admin) ──
        if (rol != "Enfermero")
        {
            vm.PorProfesional = atenciones
                .GroupBy(a => new
                {
                    a.UsuarioId,
                    Nombre = $"{a.Usuario.Apellido}, {a.Usuario.Nombre}"
                })
                .Select(g => new ResumenProfesionalEnfVM
                {
                    Nombre = g.Key.Nombre,
                    Atenciones = g.Count(),
                    Ambulatorio = g.Count(a => a.TipoAtencion == 1),
                    SinOS = g.Count(a => a.SinObraSocial)
                })
                .OrderByDescending(x => x.Atenciones)
                .ToList();
        }

        return View(vm);
    }

    public async Task<IActionResult> Odontologia(
      string? periodo, DateTime? desde, DateTime? hasta, int? profesionalId)
    {
        var rol = User.FindFirstValue(ClaimTypes.Role) ?? "";
        var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        var instId = HttpContext.Session.GetInt32("InstitucionActivaId");
        var instNombre = HttpContext.Session.GetString("InstitucionActivaNombre");

        if (rol == "Enfermero") return Forbid();

        var (fechaDesde, fechaHasta, label) = ResolverPeriodo(periodo, desde, hasta);

        var vm = new ReporteOdontologiaViewModel
        {
            Desde = fechaDesde,
            Hasta = fechaHasta,
            PeriodoLabel = label,
            ProfesionalId = profesionalId,
            InstitucionNombre = instNombre
        };

        // ── query base ──
        var baseQuery = _context.AtencionesOdontologia
            .Where(a => a.Fecha.Date >= fechaDesde && a.Fecha.Date <= fechaHasta);

        if (rol == "Odontólogo")
        {
            baseQuery = baseQuery.Where(a => a.UsuarioId == usuarioId);
        }
        else
        {
            if (instId.HasValue)
                baseQuery = baseQuery.Where(a => a.InstitucionId == instId);

            var odosQuery = _context.Usuarios
                .Where(u => u.Rol.Nombre == "Odontólogo" && !u.IsDeleted);
            if (instId.HasValue)
                odosQuery = odosQuery.Where(u => u.Instituciones.Any(i => i.Id == instId));

            vm.Profesionales = await odosQuery
                .OrderBy(u => u.Apellido)
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.Apellido}, {u.Nombre}"
                })
                .ToListAsync();

            if (profesionalId.HasValue)
            {
                baseQuery = baseQuery.Where(a => a.UsuarioId == profesionalId);
                var prof = await _context.Usuarios.FindAsync(profesionalId);
                vm.ProfesionalNombre = prof != null ? $"{prof.Apellido}, {prof.Nombre}" : null;
            }
        }

        // ── cargar en memoria ──
        var atenciones = await baseQuery
            .Include(a => a.Paciente)
            .Include(a => a.Usuario)
            .Include(a => a.Diagnostico)
            .ToListAsync();

        var atencionIds = atenciones.Select(a => a.Id).ToList();

        // ── resumen ──
        vm.TotalAtenciones = atenciones.Count;
        vm.PacientesUnicos = atenciones.Select(a => a.PacienteId).Distinct().Count();
        vm.TotalSinOS = atenciones.Count(a => a.SinObraSocial);
        vm.TotalConOS = vm.TotalAtenciones - vm.TotalSinOS;
        vm.TotalEmbarazadas = atenciones.Count(a => a.Embarazada);
        vm.PorcentajeSinOS = vm.TotalAtenciones > 0
            ? Math.Round((double)vm.TotalSinOS / vm.TotalAtenciones * 100, 1) : 0;
        vm.PorcentajeEmbarazadas = vm.TotalAtenciones > 0
            ? Math.Round((double)vm.TotalEmbarazadas / vm.TotalAtenciones * 100, 1) : 0;
        vm.TotalPrestaciones = await _context.PrestacionesOdontologia
            .Where(p => atencionIds.Contains(p.AtencionId))
            .SumAsync(p => (int?)p.Cantidad) ?? 0;

        // ── tipo consulta ──
        vm.TotalPrimeraVez = atenciones.Count(a => a.TipoConsulta == 1);
        vm.TotalUlterior = atenciones.Count(a => a.TipoConsulta == 2);

        // ── tipo turno ──
        vm.TotalVentanilla = atenciones.Count(a => a.TipoTurno == 1);
        vm.TotalPorProfesional = atenciones.Count(a => a.TipoTurno == 2);
        vm.TotalDemanda = atenciones.Count(a => a.TipoTurno == 3);
        vm.TotalInterdisc = atenciones.Count(a => a.TipoTurno == 4);

        // ── sexo ──
        vm.SexoM = atenciones.Count(a => a.Paciente.Sexo == "M");
        vm.SexoF = atenciones.Count(a => a.Paciente.Sexo == "F");
        vm.SexoNoEsp = atenciones.Count(a => a.Paciente.Sexo == "NoEspecificado");

        // ── franjas etarias ──
        vm.SerieEdad = Enumerable.Range(0, 6)
            .Select(i => atenciones.Count(a => FranjaEdad(a.Edad) == i))
            .ToList();

        // ── actividad en el tiempo ──
        var dias = (fechaHasta - fechaDesde).Days + 1;
        if (dias <= 31)
        {
            vm.LabelsActividad = Enumerable.Range(0, dias)
                .Select(i => fechaDesde.AddDays(i).ToString("dd/MM"))
                .ToList();
            vm.SerieActividad = Enumerable.Range(0, dias)
                .Select(i => atenciones.Count(a => a.Fecha.Date == fechaDesde.AddDays(i)))
                .ToList();
        }
        else
        {
            var semanas = (int)Math.Ceiling(dias / 7.0);
            vm.LabelsActividad = Enumerable.Range(0, semanas)
                .Select(i => $"Sem {fechaDesde.AddDays(i * 7):dd/MM}")
                .ToList();
            vm.SerieActividad = Enumerable.Range(0, semanas)
                .Select(i =>
                {
                    var ini = fechaDesde.AddDays(i * 7);
                    var fin = ini.AddDays(6);
                    return atenciones.Count(a => a.Fecha.Date >= ini && a.Fecha.Date <= fin);
                })
                .ToList();
        }

        // ── top 10 prestaciones ──
        var top10Prest = await _context.PrestacionesOdontologia
            .Where(p => atencionIds.Contains(p.AtencionId))
            .GroupBy(p => p.TipoPrestacion.NombrePrestacion)
            .Select(g => new { Nombre = g.Key, Total = g.Sum(x => x.Cantidad) })
            .OrderByDescending(x => x.Total)
            .Take(10)
            .ToListAsync();

        vm.Top10Nombres = top10Prest.Select(x => x.Nombre).ToList();
        vm.Top10Cantidades = top10Prest.Select(x => x.Total).ToList();

        // ── top 10 diagnósticos ──
        var top10Diag = atenciones
            .GroupBy(a => $"{a.Diagnostico.Codigo} — {a.Diagnostico.Descripcion}")
            .Select(g => new { Nombre = g.Key, Total = g.Count() })
            .OrderByDescending(x => x.Total)
            .Take(10)
            .ToList();

        vm.Top10DiagNombres = top10Diag.Select(x => x.Nombre).ToList();
        vm.Top10DiagCantidades = top10Diag.Select(x => x.Total).ToList();

        // ── CPO promedio ──
        var valoraciones = await _context.ValoracionesDentales
            .Where(v => atencionIds.Contains(v.AtencionOdontologiaId))
            .ToListAsync();

        vm.HayValoraciones = valoraciones.Count > 0;
        if (vm.HayValoraciones)
        {
            vm.PromCariesPerm = Math.Round(valoraciones.Average(v => (double)v.CariesPerm), 1);
            vm.PromPerdidosPerm = Math.Round(valoraciones.Average(v => (double)v.PerdidosPerm), 1);
            vm.PromObturadosPerm = Math.Round(valoraciones.Average(v => (double)v.ObturadosPerm), 1);
            vm.PromCariesTemp = Math.Round(valoraciones.Average(v => (double)v.CariesTemp), 1);
            vm.PromExtraccionTemp = Math.Round(valoraciones.Average(v => (double)v.ExtraccionTemp), 1);
            vm.PromObturadosTemp = Math.Round(valoraciones.Average(v => (double)v.ObturadosTemp), 1);
        }

        // ── por profesional (Director / Admin) ──
        if (rol != "Odontólogo")
        {
            vm.PorProfesional = atenciones
                .GroupBy(a => new
                {
                    a.UsuarioId,
                    Nombre = $"{a.Usuario.Apellido}, {a.Usuario.Nombre}"
                })
                .Select(g => new ResumenProfesionalOdoVM
                {
                    Nombre = g.Key.Nombre,
                    Atenciones = g.Count(),
                    PrimeraVez = g.Count(a => a.TipoConsulta == 1),
                    SinOS = g.Count(a => a.SinObraSocial)
                })
                .OrderByDescending(x => x.Atenciones)
                .ToList();
        }

        return View(vm);
    }
}