using AtencionesApp.Models.Data;
using AtencionesApp.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtencionesApp.Controllers.Api;

[Route("api/pacientes")]
public class PacientesApiController : ApiControllerBase
{
    private readonly AppDbContext _context;
    public PacientesApiController(AppDbContext context) { _context = context; }

    // GET /api/pacientes?q=texto  → búsqueda global por DNI / apellido / nombre
    [HttpGet]
    public async Task<IActionResult> Buscar([FromQuery] string? q)
    {
        if (string.IsNullOrWhiteSpace(q) || q.Trim().Length < 2)
            return BadRequest(new { error = "Ingresá al menos 2 caracteres para buscar." });

        q = q.Trim();
        var pacientes = await _context.Pacientes
            .Where(p => p.DNI.Contains(q) || p.Apellido.Contains(q) || p.Nombre.Contains(q))
            .OrderBy(p => p.Apellido).ThenBy(p => p.Nombre)
            .Take(50)
            .ToListAsync();

        var hoy = DateTime.Today;
        var resultado = pacientes.Select(p => new PacienteListItemDto
        {
            Id = p.Id,
            Apellido = p.Apellido,
            Nombre = p.Nombre,
            DNI = p.DNI,
            Edad = CalcularEdad(p.FechaNacimiento, hoy),
            Sexo = p.Sexo,
            ObraSocial = p.ObraSocial,
            Telefono = p.Telefono
        }).ToList();

        return Ok(resultado);
    }

    // GET /api/pacientes/{id}  → ficha + historia clínica completa (enf + odo)
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Ficha(int id)
    {
        var p = await _context.Pacientes
            .Include(x => x.AtencionesEnfermeria).ThenInclude(a => a.Prestaciones).ThenInclude(pr =>
pr.TipoPrestacion)
            .Include(x => x.AtencionesOdontologia).ThenInclude(a => a.Prestaciones).ThenInclude(pr =>
pr.TipoPrestacion)
            .Include(x => x.AtencionesOdontologia).ThenInclude(a => a.Diagnostico)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (p == null) return NotFound(new { error = "Paciente no encontrado." });

        var timeline = new List<AtencionTimelineDto>();

        timeline.AddRange(p.AtencionesEnfermeria.Select(a => new AtencionTimelineDto
        {
            Id = a.Id,
            Tipo = "E",
            Fecha = a.Fecha,
            Resumen = a.TipoAtencion == 1 ? "Ambulatorio" : "Internado",
            Prestaciones = a.Prestaciones
                .Select(pr => $"{pr.TipoPrestacion.NombrePrestacion} (x{pr.Cantidad})").ToList(),
            Observaciones = a.Observaciones
        }));

        timeline.AddRange(p.AtencionesOdontologia.Select(a => new AtencionTimelineDto
        {
            Id = a.Id,
            Tipo = "O",
            Fecha = a.Fecha,
            Resumen = (a.TipoConsulta == 1 ? "1ª vez" : "Ulterior") + " · " + TurnoTexto(a.TipoTurno),
            Diagnostico = a.Diagnostico != null ? $"{a.Diagnostico.Codigo} — {a.Diagnostico.Descripcion}" : null,
            Prestaciones = a.Prestaciones
                .Select(pr => $"{pr.TipoPrestacion.NombrePrestacion} (x{pr.Cantidad})").ToList(),
            Observaciones = a.Observaciones
        }));

        var dto = new PacienteDetalleDto
        {
            Id = p.Id,
            Apellido = p.Apellido,
            Nombre = p.Nombre,
            DNI = p.DNI,
            FechaNacimiento = p.FechaNacimiento,
            Edad = CalcularEdad(p.FechaNacimiento, DateTime.Today),
            Sexo = p.Sexo,
            Domicilio = p.Domicilio,
            Telefono = p.Telefono,
            ObraSocial = p.ObraSocial,
            Atenciones = timeline
                .OrderByDescending(t => t.Fecha).ThenByDescending(t => t.Id).ToList()
        };

        return Ok(dto);
    }

    private static int CalcularEdad(DateTime nacimiento, DateTime hoy)
    {
        var edad = hoy.Year - nacimiento.Year;
        if (nacimiento.DayOfYear > hoy.DayOfYear) edad--;
        return edad;
    }

    private static string TurnoTexto(int t) => t switch
    {
        1 => "Ventanilla",
        2 => "Profesional",
        3 => "Demanda",
        _ => "Interdisc."
    };
}