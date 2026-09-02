using AtencionesApp.Models.Data;
using AtencionesApp.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AtencionesApp.Models.Entities;

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
            .Include(p => p.ObraSocial)
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
            ObraSocialId = p.ObraSocialId,
            ObraSocial = p.ObraSocial != null ? p.ObraSocial.Nombre : null,
            Telefono = p.Telefono
        }).ToList();

        return Ok(resultado);
    }

    // POST /api/pacientes  → alta de paciente (la usa el sync offline del móvil)
    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearPacienteRequest req)
    {
        if (Rol != "Enfermero" && Rol != "Odontólogo") return Forbid();

        if (string.IsNullOrWhiteSpace(req.DNI) || string.IsNullOrWhiteSpace(req.Apellido)
            || string.IsNullOrWhiteSpace(req.Nombre) || string.IsNullOrWhiteSpace(req.Sexo))
            return BadRequest(new { error = "Completá DNI, apellido, nombre y sexo." });

        var dniExiste = await _context.Pacientes.AnyAsync(p => p.DNI == req.DNI.Trim());
        if (dniExiste)
            return BadRequest(new { error = "Ya existe un paciente con ese DNI." });

        var obraSocialId = req.ObraSocialId;
        if (obraSocialId == null && !string.IsNullOrWhiteSpace(req.ObraSocial))
        {
            obraSocialId = await ResolverObraSocialPorNombre(_context, req.ObraSocial);
            if (obraSocialId == null)
                return BadRequest(new { error = $"La obra social '{req.ObraSocial.Trim()}' no figura en el padrón. Actualizá la aplicación para seleccionarla de la lista." });
        }

        if (obraSocialId != null &&
            !await _context.ObrasSociales.AnyAsync(o => o.Id == obraSocialId && !o.IsDeleted))
        {
            return BadRequest(new { error = "La obra social indicada no existe o fue dada de baja." });
        }

        var paciente = new Paciente
        {
            DNI = req.DNI.Trim(),
            Apellido = req.Apellido.Trim(),
            Nombre = req.Nombre.Trim(),
            FechaNacimiento = req.FechaNacimiento,
            Sexo = req.Sexo,
            Domicilio = string.IsNullOrWhiteSpace(req.Domicilio) ? null : req.Domicilio.Trim(),
            Telefono = string.IsNullOrWhiteSpace(req.Telefono) ? null : req.Telefono.Trim(),
            ObraSocialId = obraSocialId
        };

        _context.Pacientes.Add(paciente);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Ficha), new { id = paciente.Id }, new { id = paciente.Id });
    }

    // GET /api/pacientes/{id}  → ficha + historia clínica completa (enf + odo)
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Ficha(int id)
    {
        var p = await _context.Pacientes
            .Include(x => x.ObraSocial)
            .Include(x => x.AtencionesEnfermeria).ThenInclude(a => a.Prestaciones).ThenInclude(pr => pr.TipoPrestacion)
            .Include(x => x.AtencionesOdontologia).ThenInclude(a => a.Prestaciones).ThenInclude(pr => pr.TipoPrestacion)
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
            ObraSocialId = p.ObraSocialId,
            ObraSocial = p.ObraSocial != null ? p.ObraSocial.Nombre : null,
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