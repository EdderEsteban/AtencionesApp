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

        // Solo corresponde resolver o validar la obra social cuando el profesional
        // no marcó "sin obra social": si la marcó, no registró ninguna, así que no
        // hay nada que preservar ni motivo para rechazar la atención.
        var nuevaObraSocialId = req.NuevaObraSocialId;
        if (!req.SinObraSocial)
        {
            if (nuevaObraSocialId == null && !string.IsNullOrWhiteSpace(req.NuevaObraSocial))
            {
                nuevaObraSocialId = await ResolverObraSocialPorNombre(_context, req.NuevaObraSocial);
                if (nuevaObraSocialId == null)
                    return BadRequest(new { error = $"La obra social '{req.NuevaObraSocial.Trim()}' no figura en el padrón. Actualizá la aplicación para seleccionarla de la lista." });
            }

            if (nuevaObraSocialId != null &&
                !await _context.ObrasSociales.AnyAsync(o => o.Id == nuevaObraSocialId && !o.IsDeleted))
            {
                return BadRequest(new { error = "La obra social indicada no existe o fue dada de baja." });
            }
        }

        var ahora = DateTime.Now;

        // La fecha clínica es la de captura en el teléfono (la app puede haber
        // sincronizado días después); la edad se calcula a esa misma fecha, que
        // es la que corresponde al acto asistencial.
        var fechaAtencion = ResolverFechaAtencion(req.FechaRegistroLocal, ahora);

        // Guarda de reenvío (idempotencia).
        //
        // Si el servidor ya grabó esta atención pero la respuesta se perdió en el
        // camino, el teléfono la deja PENDIENTE y la reenvía en el próximo barrido;
        // sin esta guarda el reintento la duplicaría en la historia clínica. La
        // clave es (paciente, profesional, institución, fecha de captura), y solo
        // aplica si la fecha de captura se respetó tal cual: si el reloj del
        // teléfono venía desfasado, ResolverFechaAtencion la reemplaza por la del
        // servidor, que cambia entre reintentos y no sirve como clave.
        //
        // El razonamiento completo —por qué la fecha de captura alcanza como clave,
        // por qué no se usó un UUID del cliente y por qué no hay índice único—
        // está documentado en AtencionesOdontologiaApiController.Crear.
        if (req.FechaRegistroLocal != null && fechaAtencion == req.FechaRegistroLocal.Value)
        {
            var idExistente = await _context.AtencionesEnfermeria
                .Where(a => a.PacienteId == req.PacienteId
                         && a.UsuarioId == UsuarioId
                         && a.InstitucionId == InstitucionId!.Value
                         && a.Fecha == fechaAtencion)
                .Select(a => (int?)a.Id)
                .FirstOrDefaultAsync();

            // 200 y no 201: no se creó nada, se devuelve la que ya estaba.
            if (idExistente != null)
                return Ok(new { id = idExistente.Value });
        }

        var edad = fechaAtencion.Year - paciente.FechaNacimiento.Year;
        if (paciente.FechaNacimiento.DayOfYear > fechaAtencion.DayOfYear) edad--;

        var atencion = new AtencionEnfermeria
        {
            Fecha = fechaAtencion,
            FechaSincronizacion = ahora,
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

        if (!req.SinObraSocial && nuevaObraSocialId != null)
            paciente.ObraSocialId = nuevaObraSocialId;
        else if (!req.SinObraSocial && paciente.ObraSocialId == null)
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