using AtencionesApp.Models.Data;
using AtencionesApp.Models.Dtos;
using AtencionesApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtencionesApp.Controllers.Api;

[Route("api/atenciones-enfermeria")]
public class AtencionesEnfermeriaApiController : ApiControllerBase
{
    private readonly AppDbContext _context;
    public AtencionesEnfermeriaApiController(AppDbContext context) { _context = context; }

    // GET /api/atenciones-enfermeria/tipos-prestacion  → para armar el formulario
    [HttpGet("tipos-prestacion")]
    public async Task<IActionResult> TiposPrestacion()
    {
        var tipos = await _context.TiposPrestacionEnfermeria
            .OrderBy(t => t.Grupo).ThenBy(t => t.NombrePrestacion)
            .Select(t => new TipoPrestacionEnfDto
            {
                Id = t.Id,
                Grupo = t.Grupo,
                NombrePrestacion = t.NombrePrestacion
            })
            .ToListAsync();
        return Ok(tipos);
    }

    // POST /api/atenciones-enfermeria  → carga una atención
    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearAtencionEnfermeriaRequest req)
    {
        if (Rol != "Enfermero") return Forbid();

        var sinInst = RequiereInstitucion();
        if (sinInst != null) return sinInst;

        if (req.Prestaciones == null || !req.Prestaciones.Any())
            return BadRequest(new { error = "Agregá al menos una prestación." });

        var paciente = await _context.Pacientes.FindAsync(req.PacienteId);
        if (paciente == null) return BadRequest(new { error = "Paciente no encontrado." });

        var ahora = DateTime.Now;
        var edad = ahora.Year - paciente.FechaNacimiento.Year;
        if (paciente.FechaNacimiento.DayOfYear > ahora.DayOfYear) edad--;

        var atencion = new AtencionEnfermeria
        {
            Fecha = ahora,
            PacienteId = req.PacienteId,
            InstitucionId = InstitucionId!.Value,   // del token
            UsuarioId = UsuarioId,                   // del token
            TipoAtencion = req.TipoAtencion,
            Edad = edad,
            Embarazada = req.Embarazada,
            SinObraSocial = req.SinObraSocial,
            Observaciones = string.IsNullOrWhiteSpace(req.Observaciones) ? null :
req.Observaciones.Trim(),
            Prestaciones = req.Prestaciones.Select(p => new PrestacionEnfermeria
            {
                TipoPrestacionId = p.TipoPrestacionId,
                Cantidad = p.Cantidad
            }).ToList()
        };

        if (!req.SinObraSocial && !string.IsNullOrWhiteSpace(req.NuevaObraSocial))
            paciente.ObraSocial = req.NuevaObraSocial.Trim();
        else if (!req.SinObraSocial && string.IsNullOrEmpty(paciente.ObraSocial))
            atencion.SinObraSocial = true;

        _context.AtencionesEnfermeria.Add(atencion);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Detalle), new { id = atencion.Id }, new { id = atencion.Id });
    }

    // GET /api/atenciones-enfermeria/{id}  → detalle
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Detalle(int id)
    {
        var a = await _context.AtencionesEnfermeria
            .Include(x => x.Paciente)
            .Include(x => x.Usuario)
            .Include(x => x.Institucion)
            .Include(x => x.Prestaciones).ThenInclude(p => p.TipoPrestacion)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (a == null) return NotFound(new { error = "Atención no encontrada." });

        var dto = new AtencionEnfermeriaDetalleDto
        {
            Id = a.Id,
            Fecha = a.Fecha,
            PacienteId = a.PacienteId,
            PacienteNombre = $"{a.Paciente.Apellido}, {a.Paciente.Nombre}",
            PacienteDni = a.Paciente.DNI,
            Edad = a.Edad,
            TipoAtencion = a.TipoAtencion == 1 ? "Ambulatorio" : "Internado",
            Embarazada = a.Embarazada,
            SinObraSocial = a.SinObraSocial,
            Observaciones = a.Observaciones,
            Profesional = $"{a.Usuario.Apellido}, {a.Usuario.Nombre}",
            Institucion = a.Institucion.Nombre,
            Prestaciones = a.Prestaciones.Select(p => new PrestacionDetalleDto
            {
                Nombre = p.TipoPrestacion.NombrePrestacion,
                Cantidad = p.Cantidad
            }).ToList()
        };

        return Ok(dto);
    }
}