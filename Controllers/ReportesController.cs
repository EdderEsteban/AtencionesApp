using System.Globalization;
using System.Security.Claims;
using AtencionesApp.Models.Data;
using AtencionesApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.RateLimiting;
using ClosedXML.Excel;

namespace AtencionesApp.Controllers;

[Authorize]
[EnableRateLimiting("reportes")]
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
            if (instId.HasValue)
                baseQuery = baseQuery.Where(a => a.InstitucionId == instId);
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
            if (instId.HasValue)
                baseQuery = baseQuery.Where(a => a.InstitucionId == instId);
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
    // ════════════════════════════════════════════════════════════
    //  EXPORTAR ENFERMERÍA A EXCEL
    // ════════════════════════════════════════════════════════════
    public async Task<IActionResult> ExportarEnfermeriaExcel(
        string? periodo, DateTime? desde, DateTime? hasta, int? profesionalId)
    {
        var rol = User.FindFirstValue(ClaimTypes.Role) ?? "";
        var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        var instId = HttpContext.Session.GetInt32("InstitucionActivaId");
        var instNombre = HttpContext.Session.GetString("InstitucionActivaNombre") ?? "Todos los efectores";

        if (rol == "Odontólogo") return Forbid();

        var (fechaDesde, fechaHasta, label) = ResolverPeriodo(periodo, desde, hasta);

        var baseQuery = _context.AtencionesEnfermeria
            .Where(a => a.Fecha.Date >= fechaDesde && a.Fecha.Date <= fechaHasta);

        if (instId.HasValue)
            baseQuery = baseQuery.Where(a => a.InstitucionId == instId);

        if (rol == "Enfermero")
            baseQuery = baseQuery.Where(a => a.UsuarioId == usuarioId);
        else if (profesionalId.HasValue)
            baseQuery = baseQuery.Where(a => a.UsuarioId == profesionalId);

        var atenciones = await baseQuery
            .Include(a => a.Paciente)
            .Include(a => a.Usuario)
            .Include(a => a.Prestaciones).ThenInclude(p => p.TipoPrestacion)
            .OrderBy(a => a.Fecha)
            .ToListAsync();

        string profNombre = "Todos";
        if (rol == "Enfermero") profNombre = User.Identity?.Name ?? "—";
        else if (profesionalId.HasValue)
        {
            var prof = await _context.Usuarios.FindAsync(profesionalId);
            if (prof != null) profNombre = $"{prof.Apellido}, {prof.Nombre}";
        }

        var total = atenciones.Count;
        var sinOS = atenciones.Count(a => a.SinObraSocial);
        var embarazadas = atenciones.Count(a => a.Embarazada);

        using var wb = new XLWorkbook();

        // ── Hoja Resumen ──
        var wsR = wb.Worksheets.Add("Resumen");
        wsR.Cell(1, 1).Value = "Reporte de Enfermería";
        wsR.Cell(1, 1).Style.Font.Bold = true;
        wsR.Cell(1, 1).Style.Font.FontSize = 14;
        var resumen = new (string, string)[]
        {
          ("Período", label),
          ("Institución", instNombre),
          ("Profesional", profNombre),
          ("", ""),
          ("Atenciones", total.ToString()),
          ("Pacientes únicos", atenciones.Select(a => a.PacienteId).Distinct().Count().ToString()),
          ("Prestaciones", atenciones.Sum(a => a.Prestaciones.Sum(p => p.Cantidad)).ToString()),
          ("Sin Obra Social", $"{sinOS} ({(total > 0 ? Math.Round(100.0 * sinOS / total, 1) : 0)}%)"),
          ("Con Obra Social", (total - sinOS).ToString()),
          ("Embarazadas", $"{embarazadas} ({(total > 0 ? Math.Round(100.0 * embarazadas / total, 1) :
  0)}%)"),
          ("Ambulatorio", atenciones.Count(a => a.TipoAtencion == 1).ToString()),
          ("Internado", atenciones.Count(a => a.TipoAtencion == 2).ToString()),
        };
        int r = 3;
        foreach (var (k, v) in resumen)
        {
            wsR.Cell(r, 1).Value = k;
            wsR.Cell(r, 1).Style.Font.Bold = true;
            wsR.Cell(r, 2).Value = v;
            r++;
        }
        wsR.Columns().AdjustToContents();

        // ── Hoja Atenciones ──
        var ws = wb.Worksheets.Add("Atenciones");
        var heads = new[] { "Fecha", "Paciente", "DNI", "Edad", "Sexo", "Tipo", "Obra Social",
  "Embarazada", "Profesional", "Prestaciones" };
        for (int i = 0; i < heads.Length; i++) ws.Cell(1, i + 1).Value = heads[i];
        ws.Row(1).Style.Font.Bold = true;

        int row = 2;
        foreach (var a in atenciones)
        {
            ws.Cell(row, 1).Value = a.Fecha.ToString("dd/MM/yyyy HH:mm");
            ws.Cell(row, 2).Value = $"{a.Paciente.Apellido}, {a.Paciente.Nombre}";
            ws.Cell(row, 3).Value = a.Paciente.DNI;
            ws.Cell(row, 4).Value = a.Edad;
            ws.Cell(row, 5).Value = a.Paciente.Sexo;
            ws.Cell(row, 6).Value = a.TipoAtencion == 1 ? "Ambulatorio" : "Internado";
            ws.Cell(row, 7).Value = a.SinObraSocial ? "Sin OS" : "Con OS";
            ws.Cell(row, 8).Value = a.Embarazada ? "Sí" : "No";
            ws.Cell(row, 9).Value = $"{a.Usuario.Apellido}, {a.Usuario.Nombre}";
            ws.Cell(row, 10).Value = string.Join(" · ", a.Prestaciones.Select(p =>
    $"{p.TipoPrestacion.NombrePrestacion} (x{p.Cantidad})"));
            row++;
        }
        ws.Columns().AdjustToContents();

        // ── Hoja Prestaciones ──
        var wsP = wb.Worksheets.Add("Prestaciones");
        wsP.Cell(1, 1).Value = "Prestación";
        wsP.Cell(1, 2).Value = "Total";
        wsP.Row(1).Style.Font.Bold = true;
        var prest = atenciones.SelectMany(a => a.Prestaciones)
            .GroupBy(p => p.TipoPrestacion.NombrePrestacion)
            .Select(g => new { Nombre = g.Key, Total = g.Sum(x => x.Cantidad) })
            .OrderByDescending(x => x.Total).ToList();
        int rp = 2;
        foreach (var p in prest)
        {
            wsP.Cell(rp, 1).Value = p.Nombre; wsP.Cell(rp, 2).Value = p.Total;
            rp++;
        }
        wsP.Columns().AdjustToContents();

        // ── Hoja Por profesional (Director / Admin) ──
        if (rol != "Enfermero")
        {
            var wsProf = wb.Worksheets.Add("Por profesional");
            var hp = new[] { "Profesional", "Atenciones", "% Ambulatorio", "% Sin OS" };
            for (int i = 0; i < hp.Length; i++) wsProf.Cell(1, i + 1).Value = hp[i];
            wsProf.Row(1).Style.Font.Bold = true;
            var porProf = atenciones
                .GroupBy(a => $"{a.Usuario.Apellido}, {a.Usuario.Nombre}")
                .Select(g => new
                {
                    Nombre = g.Key,
                    Atenciones = g.Count(),
                    PctAmb = g.Count() > 0 ? Math.Round(100.0 * g.Count(a => a.TipoAtencion == 1) /
    g.Count(), 1) : 0,
                    PctSinOS = g.Count() > 0 ? Math.Round(100.0 * g.Count(a => a.SinObraSocial) /
    g.Count(), 1) : 0
                })
                .OrderByDescending(x => x.Atenciones).ToList();
            int rpr = 2;
            foreach (var p in porProf)
            {
                wsProf.Cell(rpr, 1).Value = p.Nombre;
                wsProf.Cell(rpr, 2).Value = p.Atenciones;
                wsProf.Cell(rpr, 3).Value = p.PctAmb;
                wsProf.Cell(rpr, 4).Value = p.PctSinOS;
                rpr++;
            }
            wsProf.Columns().AdjustToContents();
        }

        using var ms = new MemoryStream();
        wb.SaveAs(ms);
        var fileName = $"Reporte_Enfermeria_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";
        return File(ms.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }

    // ════════════════════════════════════════════════════════════
    //  EXPORTAR ODONTOLOGÍA A EXCEL
    // ════════════════════════════════════════════════════════════
    public async Task<IActionResult> ExportarOdontologiaExcel(
        string? periodo, DateTime? desde, DateTime? hasta, int? profesionalId)
    {
        var rol = User.FindFirstValue(ClaimTypes.Role) ?? "";
        var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        var instId = HttpContext.Session.GetInt32("InstitucionActivaId");
        var instNombre = HttpContext.Session.GetString("InstitucionActivaNombre") ?? "Todos los efectores";

        if (rol == "Enfermero") return Forbid();

        var (fechaDesde, fechaHasta, label) = ResolverPeriodo(periodo, desde, hasta);

        var baseQuery = _context.AtencionesOdontologia
            .Where(a => a.Fecha.Date >= fechaDesde && a.Fecha.Date <= fechaHasta);

        if (instId.HasValue)
            baseQuery = baseQuery.Where(a => a.InstitucionId == instId);

        if (rol == "Odontólogo")
            baseQuery = baseQuery.Where(a => a.UsuarioId == usuarioId);
        else if (profesionalId.HasValue)
            baseQuery = baseQuery.Where(a => a.UsuarioId == profesionalId);

        var atenciones = await baseQuery
            .Include(a => a.Paciente)
            .Include(a => a.Usuario)
            .Include(a => a.Diagnostico)
            .Include(a => a.ValoracionDental)
            .Include(a => a.Prestaciones).ThenInclude(p => p.TipoPrestacion)
            .OrderBy(a => a.Fecha)
            .ToListAsync();

        string profNombre = "Todos";
        if (rol == "Odontólogo") profNombre = User.Identity?.Name ?? "—";
        else if (profesionalId.HasValue)
        {
            var prof = await _context.Usuarios.FindAsync(profesionalId);
            if (prof != null) profNombre = $"{prof.Apellido}, {prof.Nombre}";
        }

        string TurnoTexto(int t) => t switch
        { 1 => "Ventanilla", 2 => "Profesional", 3 => "Demanda", _ => "Interdisc." };

        var total = atenciones.Count;
        var sinOS = atenciones.Count(a => a.SinObraSocial);
        var embarazadas = atenciones.Count(a => a.Embarazada);
        var valoraciones = atenciones.Where(a => a.ValoracionDental != null)
                                     .Select(a => a.ValoracionDental!).ToList();

        using var wb = new XLWorkbook();

        // ── Hoja Resumen ──
        var wsR = wb.Worksheets.Add("Resumen");
        wsR.Cell(1, 1).Value = "Reporte de Odontología";
        wsR.Cell(1, 1).Style.Font.Bold = true;
        wsR.Cell(1, 1).Style.Font.FontSize = 14;
        var resumen = new (string, string)[]
        {
          ("Período", label),
          ("Institución", instNombre),
          ("Profesional", profNombre),
          ("", ""),
          ("Atenciones", total.ToString()),
          ("Pacientes únicos", atenciones.Select(a => a.PacienteId).Distinct().Count().ToString()),
          ("Prestaciones", atenciones.Sum(a => a.Prestaciones.Sum(p => p.Cantidad)).ToString()),
          ("Sin Obra Social", $"{sinOS} ({(total > 0 ? Math.Round(100.0 * sinOS / total, 1) : 0)}%)"),
          ("Con Obra Social", (total - sinOS).ToString()),
          ("Embarazadas", $"{embarazadas} ({(total > 0 ? Math.Round(100.0 * embarazadas / total, 1) :
  0)}%)"),
          ("Primera vez", atenciones.Count(a => a.TipoConsulta == 1).ToString()),
          ("Ulterior", atenciones.Count(a => a.TipoConsulta == 2).ToString()),
          ("", ""),
          ("CPO/ceo promedio del período", valoraciones.Any() ? "" : "Sin valoraciones"),
          ("  C perm.", valoraciones.Any() ? Math.Round(valoraciones.Average(v =>
  (double)v.CariesPerm), 1).ToString() : "—"),
          ("  P perm.", valoraciones.Any() ? Math.Round(valoraciones.Average(v =>
  (double)v.PerdidosPerm), 1).ToString() : "—"),
          ("  O perm.", valoraciones.Any() ? Math.Round(valoraciones.Average(v =>
  (double)v.ObturadosPerm), 1).ToString() : "—"),
          ("  c temp.", valoraciones.Any() ? Math.Round(valoraciones.Average(v =>
  (double)v.CariesTemp), 1).ToString() : "—"),
          ("  e temp.", valoraciones.Any() ? Math.Round(valoraciones.Average(v =>
  (double)v.ExtraccionTemp), 1).ToString() : "—"),
          ("  o temp.", valoraciones.Any() ? Math.Round(valoraciones.Average(v =>
  (double)v.ObturadosTemp), 1).ToString() : "—"),
        };
        int r = 3;
        foreach (var (k, v) in resumen)
        {
            wsR.Cell(r, 1).Value = k;
            wsR.Cell(r, 1).Style.Font.Bold = true;
            wsR.Cell(r, 2).Value = v;
            r++;
        }
        wsR.Columns().AdjustToContents();

        // ── Hoja Atenciones ──
        var ws = wb.Worksheets.Add("Atenciones");
        var heads = new[] { "Fecha", "Paciente", "DNI", "Edad", "Sexo", "Consulta", "Turno", "Obra Social", "Embarazada", "Diagnóstico", "Profesional", "Prestaciones" };
        for (int i = 0; i < heads.Length; i++) ws.Cell(1, i + 1).Value = heads[i];
        ws.Row(1).Style.Font.Bold = true;

        int row = 2;
        foreach (var a in atenciones)
        {
            ws.Cell(row, 1).Value = a.Fecha.ToString("dd/MM/yyyy HH:mm");
            ws.Cell(row, 2).Value = $"{a.Paciente.Apellido}, {a.Paciente.Nombre}";
            ws.Cell(row, 3).Value = a.Paciente.DNI;
            ws.Cell(row, 4).Value = a.Edad;
            ws.Cell(row, 5).Value = a.Paciente.Sexo;
            ws.Cell(row, 6).Value = a.TipoConsulta == 1 ? "1ª vez" : "Ulterior";
            ws.Cell(row, 7).Value = TurnoTexto(a.TipoTurno);
            ws.Cell(row, 8).Value = a.SinObraSocial ? "Sin OS" : "Con OS";
            ws.Cell(row, 9).Value = a.Embarazada ? "Sí" : "No";
            ws.Cell(row, 10).Value = a.Diagnostico != null ? $"{a.Diagnostico.Codigo} — {a.Diagnostico.Descripcion}" : "—";
            ws.Cell(row, 11).Value = $"{a.Usuario.Apellido}, {a.Usuario.Nombre}";
            ws.Cell(row, 12).Value = string.Join(" · ", a.Prestaciones.Select(p =>
    $"{p.TipoPrestacion.NombrePrestacion} (x{p.Cantidad})"));
            row++;
        }
        ws.Columns().AdjustToContents();

        // ── Hoja Prestaciones ──
        var wsP = wb.Worksheets.Add("Prestaciones");
        wsP.Cell(1, 1).Value = "Prestación";
        wsP.Cell(1, 2).Value = "Total";
        wsP.Row(1).Style.Font.Bold = true;
        var prest = atenciones.SelectMany(a => a.Prestaciones)
            .GroupBy(p => p.TipoPrestacion.NombrePrestacion)
            .Select(g => new { Nombre = g.Key, Total = g.Sum(x => x.Cantidad) })
            .OrderByDescending(x => x.Total).ToList();
        int rp = 2;
        foreach (var p in prest)
        {
            wsP.Cell(rp, 1).Value = p.Nombre; wsP.Cell(rp, 2).Value = p.Total;
            rp++;
        }
        wsP.Columns().AdjustToContents();

        // ── Hoja Diagnósticos ──
        var wsD = wb.Worksheets.Add("Diagnósticos");
        wsD.Cell(1, 1).Value = "Diagnóstico";
        wsD.Cell(1, 2).Value = "Cantidad";
        wsD.Row(1).Style.Font.Bold = true;
        var diags = atenciones.Where(a => a.Diagnostico != null)
            .GroupBy(a => $"{a.Diagnostico!.Codigo} — {a.Diagnostico.Descripcion}")
            .Select(g => new { Nombre = g.Key, Total = g.Count() })
            .OrderByDescending(x => x.Total).ToList();
        int rd = 2;
        foreach (var d in diags)
        {
            wsD.Cell(rd, 1).Value = d.Nombre; wsD.Cell(rd, 2).Value = d.Total;
            rd++;
        }
        wsD.Columns().AdjustToContents();

        // ── Hoja Por profesional (Director / Admin) ──
        if (rol != "Odontólogo")
        {
            var wsProf = wb.Worksheets.Add("Por profesional");
            var hp = new[] { "Profesional", "Atenciones", "% 1ª vez", "% Sin OS" };
            for (int i = 0; i < hp.Length; i++) wsProf.Cell(1, i + 1).Value = hp[i];
            wsProf.Row(1).Style.Font.Bold = true;
            var porProf = atenciones
                .GroupBy(a => $"{a.Usuario.Apellido}, {a.Usuario.Nombre}")
                .Select(g => new
                {
                    Nombre = g.Key,
                    Atenciones = g.Count(),
                    PctPV = g.Count() > 0 ? Math.Round(100.0 * g.Count(a => a.TipoConsulta == 1) /
    g.Count(), 1) : 0,
                    PctSinOS = g.Count() > 0 ? Math.Round(100.0 * g.Count(a => a.SinObraSocial) /
    g.Count(), 1) : 0
                })
                .OrderByDescending(x => x.Atenciones).ToList();
            int rpr = 2;
            foreach (var p in porProf)
            {
                wsProf.Cell(rpr, 1).Value = p.Nombre;
                wsProf.Cell(rpr, 2).Value = p.Atenciones;
                wsProf.Cell(rpr, 3).Value = p.PctPV;
                wsProf.Cell(rpr, 4).Value = p.PctSinOS;
                rpr++;
            }
            wsProf.Columns().AdjustToContents();
        }

        using var ms = new MemoryStream();
        wb.SaveAs(ms);
        var fileName = $"Reporte_Odontologia_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";
        return File(ms.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }
}