using System.Diagnostics;
using System.Globalization;
using System.Security.Claims;
using AtencionesApp.Models;
using AtencionesApp.Models.Data;
using AtencionesApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtencionesApp.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var rol = User.FindFirstValue(ClaimTypes.Role) ?? "";
        var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        var instId = HttpContext.Session.GetInt32("InstitucionActivaId");
        var instNombre = HttpContext.Session.GetString("InstitucionActivaNombre");

        var hoy = DateTime.Today;
        var hace7 = hoy.AddDays(-6);
        var inicioMes6 = new DateTime(hoy.AddMonths(-5).Year, hoy.AddMonths(-5).Month, 1);
        var cultura = new CultureInfo("es-AR");

        var vm = new DashboardViewModel();

        vm.Labels7Dias = Enumerable.Range(0, 7)
            .Select(i => hoy.AddDays(i - 6).ToString("ddd", cultura).ToUpper())
            .ToList();

        vm.Labels6Meses = Enumerable.Range(0, 6)
            .Select(i => hoy.AddMonths(i - 5).ToString("MMM yyyy", cultura))
            .ToList();

        if (rol == "Enfermero")
        {
            vm.TituloBarras = "Mi actividad — últimos 7 días";
            vm.TituloDonut = "Tipo de atención";
            vm.TituloLinea = "Mi tendencia — últimos 6 meses";
            vm.TituloTop10 = "Mis top 10 prestaciones";

            var rawEnf7 = await _context.AtencionesEnfermeria
                .Where(a => a.UsuarioId == usuarioId && a.Fecha >= hace7)
                .GroupBy(a => a.Fecha.Date)
                .Select(g => new { Fecha = g.Key, Count = g.Count() })
                .ToListAsync();

            vm.SerieEnfermeria = Enumerable.Range(0, 7)
                .Select(i => rawEnf7.FirstOrDefault(x => x.Fecha == hoy.AddDays(i - 6))?.Count ??
0)
                .ToList();

            var totAmb = await _context.AtencionesEnfermeria
                .CountAsync(a => a.UsuarioId == usuarioId && a.TipoAtencion == 1);
            var totInt = await _context.AtencionesEnfermeria
                .CountAsync(a => a.UsuarioId == usuarioId && a.TipoAtencion == 2);
            vm.DonutLabels = new List<string> { "Ambulatorio", "Internado" };
            vm.DonutValores = new List<int> { totAmb, totInt };

            var raw6mEnf = await _context.AtencionesEnfermeria
                .Where(a => a.UsuarioId == usuarioId && a.Fecha >= inicioMes6)
                .GroupBy(a => new { a.Fecha.Year, a.Fecha.Month })
                .Select(g => new { g.Key.Year, g.Key.Month, Count = g.Count() })
                .ToListAsync();

            vm.SerieMensual = Enumerable.Range(0, 6)
                .Select(i =>
                {
                    var mes = hoy.AddMonths(i - 5);
                    return raw6mEnf.FirstOrDefault(x => x.Year == mes.Year && x.Month ==
mes.Month)?.Count ?? 0;
                }).ToList();

            var top10Enf = await _context.PrestacionesEnfermeria
                .Where(p => p.Atencion.UsuarioId == usuarioId)
                .GroupBy(p => p.TipoPrestacion.NombrePrestacion)
                .Select(g => new { Nombre = g.Key, Total = g.Sum(x => x.Cantidad) })
                .OrderByDescending(x => x.Total)
                .Take(10)
                .ToListAsync();

            vm.Top10Nombres = top10Enf.Select(x => x.Nombre).ToList();
            vm.Top10Cantidades = top10Enf.Select(x => x.Total).ToList();
            vm.Top10Tipos = top10Enf.Select(_ => "E").ToList();
        }
        else if (rol == "Odontólogo")
        {
            vm.TituloBarras = "Mi actividad — últimos 7 días";
            vm.TituloDonut = "Tipo de consulta";
            vm.TituloLinea = "Mi tendencia — últimos 6 meses";
            vm.TituloTop10 = "Mis top 10 prestaciones";

            var rawOdo7 = await _context.AtencionesOdontologia
                .Where(a => a.UsuarioId == usuarioId && a.Fecha >= hace7)
                .GroupBy(a => a.Fecha.Date)
                .Select(g => new { Fecha = g.Key, Count = g.Count() })
                .ToListAsync();

            vm.SerieEnfermeria = Enumerable.Range(0, 7)
                .Select(i => rawOdo7.FirstOrDefault(x => x.Fecha == hoy.AddDays(i - 6))?.Count ??
0)
                .ToList();

            var totPrimera = await _context.AtencionesOdontologia
                .CountAsync(a => a.UsuarioId == usuarioId && a.TipoConsulta == 1);
            var totUlterior = await _context.AtencionesOdontologia
                .CountAsync(a => a.UsuarioId == usuarioId && a.TipoConsulta == 2);
            vm.DonutLabels = new List<string> { "Primera vez", "Ulterior" };
            vm.DonutValores = new List<int> { totPrimera, totUlterior };

            var raw6mOdo = await _context.AtencionesOdontologia
                .Where(a => a.UsuarioId == usuarioId && a.Fecha >= inicioMes6)
                .GroupBy(a => new { a.Fecha.Year, a.Fecha.Month })
                .Select(g => new { g.Key.Year, g.Key.Month, Count = g.Count() })
                .ToListAsync();

            vm.SerieMensual = Enumerable.Range(0, 6)
                .Select(i =>
                {
                    var mes = hoy.AddMonths(i - 5);
                    return raw6mOdo.FirstOrDefault(x => x.Year == mes.Year && x.Month ==
mes.Month)?.Count ?? 0;
                }).ToList();

            var top10Odo = await _context.PrestacionesOdontologia
                .Where(p => p.Atencion.UsuarioId == usuarioId)
                .GroupBy(p => p.TipoPrestacion.NombrePrestacion)
                .Select(g => new { Nombre = g.Key, Total = g.Sum(x => x.Cantidad) })
                .OrderByDescending(x => x.Total)
                .Take(10)
                .ToListAsync();

            vm.Top10Nombres = top10Odo.Select(x => x.Nombre).ToList();
            vm.Top10Cantidades = top10Odo.Select(x => x.Total).ToList();
            vm.Top10Tipos = top10Odo.Select(_ => "O").ToList();
        }
        else // Administrador o Director
        {
            var scope = instId.HasValue ? instNombre : "todos los efectores";
            vm.TituloBarras = $"Actividad semanal — Enfermería vs Odontología";
            vm.TituloDonut = "Atenciones por servicio";
            vm.TituloLinea = $"Evolución mensual — {scope}";
            vm.TituloTop10 = $"Top 10 prestaciones — {scope}";

            var rawEnf7 = await _context.AtencionesEnfermeria
                .Where(a => a.Fecha >= hace7 && (!instId.HasValue || a.InstitucionId == instId))
                .GroupBy(a => a.Fecha.Date)
                .Select(g => new { Fecha = g.Key, Count = g.Count() })
                .ToListAsync();

            var rawOdo7 = await _context.AtencionesOdontologia
                .Where(a => a.Fecha >= hace7 && (!instId.HasValue || a.InstitucionId == instId))
                .GroupBy(a => a.Fecha.Date)
                .Select(g => new { Fecha = g.Key, Count = g.Count() })
                .ToListAsync();

            vm.SerieEnfermeria = Enumerable.Range(0, 7)
                .Select(i => rawEnf7.FirstOrDefault(x => x.Fecha == hoy.AddDays(i - 6))?.Count ?? 0)
                .ToList();
            vm.SerieOdontologia = Enumerable.Range(0, 7)
                .Select(i => rawOdo7.FirstOrDefault(x => x.Fecha == hoy.AddDays(i - 6))?.Count ?? 0)
                .ToList();

            var totEnf = await _context.AtencionesEnfermeria
                .CountAsync(a => !instId.HasValue || a.InstitucionId == instId);
            var totOdo = await _context.AtencionesOdontologia
                .CountAsync(a => !instId.HasValue || a.InstitucionId == instId);
            vm.DonutLabels = new List<string> { "Enfermería", "Odontología" };
            vm.DonutValores = new List<int> { totEnf, totOdo };

            var raw6mEnf = await _context.AtencionesEnfermeria
                .Where(a => a.Fecha >= inicioMes6 && (!instId.HasValue || a.InstitucionId == instId))
                .GroupBy(a => new { a.Fecha.Year, a.Fecha.Month })
                .Select(g => new { g.Key.Year, g.Key.Month, Count = g.Count() })
                .ToListAsync();

            var raw6mOdo = await _context.AtencionesOdontologia
                .Where(a => a.Fecha >= inicioMes6 && (!instId.HasValue || a.InstitucionId == instId))
                .GroupBy(a => new { a.Fecha.Year, a.Fecha.Month })
                .Select(g => new { g.Key.Year, g.Key.Month, Count = g.Count() })
                .ToListAsync();

            vm.SerieMensual = Enumerable.Range(0, 6)
                .Select(i =>
                {
                    var mes = hoy.AddMonths(i - 5);
                    var cEnf = raw6mEnf.FirstOrDefault(x => x.Year == mes.Year && x.Month == mes.Month)?.Count ?? 0;
                    var cOdo = raw6mOdo.FirstOrDefault(x => x.Year == mes.Year && x.Month == mes.Month)?.Count ?? 0;
                    return cEnf + cOdo;
                }).ToList();

            var prestEnf = await _context.PrestacionesEnfermeria
                .Where(p => !instId.HasValue || p.Atencion.InstitucionId == instId)
                .GroupBy(p => p.TipoPrestacion.NombrePrestacion)
                .Select(g => new { Nombre = g.Key, Total = g.Sum(x => x.Cantidad) })
                .ToListAsync();

            var prestOdo = await _context.PrestacionesOdontologia
                .Where(p => !instId.HasValue || p.Atencion.InstitucionId == instId)
                .GroupBy(p => p.TipoPrestacion.NombrePrestacion)
                .Select(g => new { Nombre = g.Key, Total = g.Sum(x => x.Cantidad) })
                .ToListAsync();

            var top10 = prestEnf.Select(x => new { x.Nombre, x.Total, Tipo = "E" })
                .Concat(prestOdo.Select(x => new { x.Nombre, x.Total, Tipo = "O" }))
                .GroupBy(x => x.Nombre)
                .Select(g => new { Nombre = g.Key, Total = g.Sum(x => x.Total), Tipo = g.First().Tipo })
                .OrderByDescending(x => x.Total)
                .Take(10)
                .ToList();

            vm.Top10Nombres = top10.Select(x => x.Nombre).ToList();
            vm.Top10Cantidades = top10.Select(x => x.Total).ToList();
            vm.Top10Tipos = top10.Select(x => x.Tipo).ToList();
        }

        return View(vm);
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ??
HttpContext.TraceIdentifier
        });
    }
}