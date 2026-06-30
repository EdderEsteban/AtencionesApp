using System.Globalization;
using AtencionesApp.Models.Data;
using AtencionesApp.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtencionesApp.Controllers.Api;

[Route("api/dashboard")]
public class DashboardApiController : ApiControllerBase
{
    private readonly AppDbContext _context;
    public DashboardApiController(AppDbContext context) { _context = context; }

    // GET /api/dashboard  → 4 gráficas del profesional, en su institución activa
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        if (Rol != "Enfermero" && Rol != "Odontólogo")
            return Forbid(); // el dashboard de la app es para profesionales

        var sinInst = RequiereInstitucion();
        if (sinInst != null) return sinInst;

        var usuarioId = UsuarioId;
        var instId = InstitucionId!.Value;

        var hoy = DateTime.Today;
        var hace7 = hoy.AddDays(-6);
        var inicioMes6 = new DateTime(hoy.AddMonths(-5).Year, hoy.AddMonths(-5).Month, 1);
        var cultura = new CultureInfo("es-AR");

        var vm = new DashboardDto
        {
            TituloBarras = "Mi actividad — últimos 7 días",
            TituloLinea = "Mi tendencia — últimos 6 meses",
            TituloTop10 = "Mis top 10 prestaciones",
            Labels7Dias = Enumerable.Range(0, 7)
                .Select(i => hoy.AddDays(i - 6).ToString("ddd", cultura).ToUpper()).ToList(),
            Labels6Meses = Enumerable.Range(0, 6)
                .Select(i => hoy.AddMonths(i - 5).ToString("MMM yyyy", cultura)).ToList()
        };

        if (Rol == "Enfermero")
        {
            vm.TituloDonut = "Tipo de atención";

            var raw7 = await _context.AtencionesEnfermeria
                .Where(a => a.UsuarioId == usuarioId && a.InstitucionId == instId && a.Fecha >=
hace7)
                .GroupBy(a => a.Fecha.Date)
                .Select(g => new { Fecha = g.Key, Count = g.Count() })
                .ToListAsync();
            vm.Serie7Dias = Enumerable.Range(0, 7)
                .Select(i => raw7.FirstOrDefault(x => x.Fecha == hoy.AddDays(i - 6))?.Count ??
0).ToList();

            var amb = await _context.AtencionesEnfermeria
                .CountAsync(a => a.UsuarioId == usuarioId && a.InstitucionId == instId &&
a.TipoAtencion == 1);
            var inte = await _context.AtencionesEnfermeria
                .CountAsync(a => a.UsuarioId == usuarioId && a.InstitucionId == instId &&
a.TipoAtencion == 2);
            vm.DonutLabels = new() { "Ambulatorio", "Internado" };
            vm.DonutValores = new() { amb, inte };

            var raw6 = await _context.AtencionesEnfermeria
                .Where(a => a.UsuarioId == usuarioId && a.InstitucionId == instId && a.Fecha >=
inicioMes6)
                .GroupBy(a => new { a.Fecha.Year, a.Fecha.Month })
                .Select(g => new { g.Key.Year, g.Key.Month, Count = g.Count() })
                .ToListAsync();
            vm.Serie6Meses = Enumerable.Range(0, 6).Select(i =>
            {
                var mes = hoy.AddMonths(i - 5);
                return raw6.FirstOrDefault(x => x.Year == mes.Year && x.Month == mes.Month)?.Count ??
0;
            }).ToList();

            var top10 = await _context.PrestacionesEnfermeria
                .Where(p => p.Atencion.UsuarioId == usuarioId && p.Atencion.InstitucionId == instId)
                .GroupBy(p => p.TipoPrestacion.NombrePrestacion)
                .Select(g => new { Nombre = g.Key, Total = g.Sum(x => x.Cantidad) })
                .OrderByDescending(x => x.Total).Take(10).ToListAsync();
            vm.Top10Nombres = top10.Select(x => x.Nombre).ToList();
            vm.Top10Cantidades = top10.Select(x => x.Total).ToList();
        }
        else // Odontólogo
        {
            vm.TituloDonut = "Tipo de consulta";

            var raw7 = await _context.AtencionesOdontologia
                .Where(a => a.UsuarioId == usuarioId && a.InstitucionId == instId && a.Fecha >=
hace7)
                .GroupBy(a => a.Fecha.Date)
                .Select(g => new { Fecha = g.Key, Count = g.Count() })
                .ToListAsync();
            vm.Serie7Dias = Enumerable.Range(0, 7)
                .Select(i => raw7.FirstOrDefault(x => x.Fecha == hoy.AddDays(i - 6))?.Count ??
0).ToList();

            var pv = await _context.AtencionesOdontologia
                .CountAsync(a => a.UsuarioId == usuarioId && a.InstitucionId == instId &&
a.TipoConsulta == 1);
            var ult = await _context.AtencionesOdontologia
                .CountAsync(a => a.UsuarioId == usuarioId && a.InstitucionId == instId &&
a.TipoConsulta == 2);
            vm.DonutLabels = new() { "Primera vez", "Ulterior" };
            vm.DonutValores = new() { pv, ult };

            var raw6 = await _context.AtencionesOdontologia
                .Where(a => a.UsuarioId == usuarioId && a.InstitucionId == instId && a.Fecha >=
inicioMes6)
                .GroupBy(a => new { a.Fecha.Year, a.Fecha.Month })
                .Select(g => new { g.Key.Year, g.Key.Month, Count = g.Count() })
                .ToListAsync();
            vm.Serie6Meses = Enumerable.Range(0, 6).Select(i =>
            {
                var mes = hoy.AddMonths(i - 5);
                return raw6.FirstOrDefault(x => x.Year == mes.Year && x.Month == mes.Month)?.Count ??
0;
            }).ToList();

            var top10 = await _context.PrestacionesOdontologia
                .Where(p => p.Atencion.UsuarioId == usuarioId && p.Atencion.InstitucionId == instId)
                .GroupBy(p => p.TipoPrestacion.NombrePrestacion)
                .Select(g => new { Nombre = g.Key, Total = g.Sum(x => x.Cantidad) })
                .OrderByDescending(x => x.Total).Take(10).ToListAsync();
            vm.Top10Nombres = top10.Select(x => x.Nombre).ToList();
            vm.Top10Cantidades = top10.Select(x => x.Total).ToList();
        }

        return Ok(vm);
    }
}