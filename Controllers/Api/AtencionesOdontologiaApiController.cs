using AtencionesApp.Models.Data;
using AtencionesApp.Models.Dtos;
using AtencionesApp.Models.Entities;
using AtencionesApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtencionesApp.Controllers.Api;

[Route("api/atenciones-odontologia")]
public class AtencionesOdontologiaApiController : ApiControllerBase
{
    private readonly AppDbContext _context;
    public AtencionesOdontologiaApiController(AppDbContext context) { _context = context; }

    // GET .../tipos-prestacion
    [HttpGet("tipos-prestacion")]
    public async Task<IActionResult> TiposPrestacion()
    {
        var tipos = await _context.TiposPrestacionOdontologia
            .OrderBy(t => t.NombrePrestacion)
            .Select(t => new TipoPrestacionOdoDto { Id = t.Id, NombrePrestacion = t.NombrePrestacion })
            .ToListAsync();
        return Ok(tipos);
    }

    // GET .../diagnosticos
    [HttpGet("diagnosticos")]
    public async Task<IActionResult> Diagnosticos()
    {
        var diags = await _context.Diagnosticos
            .OrderBy(d => d.Codigo)
            .Select(d => new DiagnosticoDto { Id = d.Id, Codigo = d.Codigo, Descripcion = d.Descripcion })
            .ToListAsync();
        return Ok(diags);
    }

    // POST /api/atenciones-odontologia
    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearAtencionOdontologiaRequest req)
    {
        if (Rol != "Odontólogo") return Forbid();

        var sinInst = RequiereInstitucion();
        if (sinInst != null) return sinInst;

        if (req.Prestaciones == null || !req.Prestaciones.Any())
            return BadRequest(new { error = "Agregá al menos una prestación." });

        var paciente = await _context.Pacientes.FindAsync(req.PacienteId);
        if (paciente == null) return BadRequest(new { error = "Paciente no encontrado." });

        if (!await _context.Diagnosticos.AnyAsync(d => d.Id == req.DiagnosticoId))
            return BadRequest(new { error = "Diagnóstico no válido." });

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
        // camino (corte de red, timeout, la app cerrada), el teléfono la deja
        // PENDIENTE y la vuelve a enviar en el próximo barrido. Sin esta guarda el
        // reintento crearía una segunda fila: la misma atención duplicada en la
        // historia clínica, que es un error clínico, no cosmético.
        //
        // La clave es (paciente, profesional, institución, fecha de captura). No
        // hace falta una clave generada por el cliente porque la app ya manda
        // `fechaRegistroLocal` y en un reenvío manda exactamente la misma: el
        // instante de captura identifica el acto asistencial. Dos atenciones
        // distintas del mismo paciente con el mismo profesional no pueden haberse
        // registrado en el mismo instante.
        //
        // Solo aplica si la fecha de captura se respetó tal cual. Si
        // ResolverFechaAtencion la descartó (no vino, o el reloj del teléfono
        // estaba desfasado) la fecha pasa a ser la del servidor, que cambia entre
        // reintentos y no sirve como clave: en ese caso se sigue de largo y la
        // atención se crea, porque es preferible un duplicado detectable a
        // descartar una atención real por una coincidencia de otra cosa.
        //
        // La consulta incluye a propósito las atenciones con IsDeleted: si un
        // administrador la dio de baja, el reintento no debe resucitarla — basta
        // con informarle al teléfono que ya llegó.
        if (req.FechaRegistroLocal != null && fechaAtencion == req.FechaRegistroLocal.Value)
        {
            var idExistente = await _context.AtencionesOdontologia
                .Where(a => a.PacienteId == req.PacienteId
                         && a.UsuarioId == UsuarioId
                         && a.InstitucionId == InstitucionId!.Value
                         && a.Fecha == fechaAtencion)
                .Select(a => (int?)a.Id)
                .FirstOrDefaultAsync();

            // 200 y no 201: no se creó nada, se devuelve la que ya estaba. La app
            // lo trata igual (mira 2xx + el id) y la marca SINCRONIZADO.
            if (idExistente != null)
                return Ok(new { id = idExistente.Value });
        }

        var edad = fechaAtencion.Year - paciente.FechaNacimiento.Year;
        if (paciente.FechaNacimiento.DayOfYear > fechaAtencion.DayOfYear) edad--;

        var odo = req.Odontograma ?? new();

        // Estados crudos del odontograma (solo lo distinto de "Sano").
        var estados = odo.Where(e => e.Estado > 0).Select(e => new OdontogramaEstado
        {
            NumeroDiente = e.NumeroDiente,
            Superficie = e.Superficie,
            Estado = e.Estado
        }).ToList();

        var atencion = new AtencionOdontologia
        {
            Fecha = fechaAtencion,
            FechaSincronizacion = ahora,
            PacienteId = req.PacienteId,
            InstitucionId = InstitucionId!.Value,   // del token
            UsuarioId = UsuarioId,                   // del token
            TipoConsulta = req.TipoConsulta,
            TipoTurno = req.TipoTurno,
            DiagnosticoId = req.DiagnosticoId,
            Edad = edad,
            Embarazada = req.Embarazada,
            SinObraSocial = req.SinObraSocial,
            Observaciones = string.IsNullOrWhiteSpace(req.Observaciones) ? null : req.Observaciones.Trim(),
            Prestaciones = req.Prestaciones.Select(p => new PrestacionOdontologia
            {
                TipoPrestacionId = p.TipoPrestacionId,
                Cantidad = p.Cantidad
            }).ToList(),
            ValoracionDental = CalculadoraCpo.Calcular(estados),  // ← CPO recalculado en el servidor
            OdontogramaEstados = estados
        };

        if (!req.SinObraSocial && nuevaObraSocialId != null)
            paciente.ObraSocialId = nuevaObraSocialId;
        else if (!req.SinObraSocial && paciente.ObraSocialId == null)
            atencion.SinObraSocial = true;

        _context.AtencionesOdontologia.Add(atencion);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Detalle), new { id = atencion.Id }, new { id = atencion.Id });
    }

    // GET /api/atenciones-odontologia/{id}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Detalle(int id)
    {
        var a = await _context.AtencionesOdontologia
            .Include(x => x.Paciente)
            .Include(x => x.Usuario)
            .Include(x => x.Institucion)
            .Include(x => x.Diagnostico)
            .Include(x => x.Prestaciones).ThenInclude(p => p.TipoPrestacion)
            .Include(x => x.ValoracionDental)
            .Include(x => x.OdontogramaEstados)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (a == null) return NotFound(new { error = "Atención no encontrada." });

        var v = a.ValoracionDental;
        var dto = new AtencionOdontologiaDetalleDto
        {
            Id = a.Id,
            Fecha = a.Fecha,
            PacienteId = a.PacienteId,
            PacienteNombre = $"{a.Paciente.Apellido}, {a.Paciente.Nombre}",
            PacienteDni = a.Paciente.DNI,
            Edad = a.Edad,
            TipoConsulta = a.TipoConsulta == 1 ? "1ª vez" : "Ulterior",
            TipoTurno = TurnoTexto(a.TipoTurno),
            Diagnostico = a.Diagnostico != null ? $"{a.Diagnostico.Codigo} — {a.Diagnostico.Descripcion}" : null,
            Embarazada = a.Embarazada,
            SinObraSocial = a.SinObraSocial,
            Observaciones = a.Observaciones,
            Profesional = $"{a.Usuario.Apellido}, {a.Usuario.Nombre}",
            Institucion = a.Institucion.Nombre,
            Prestaciones = a.Prestaciones.Select(p => new PrestacionDetalleDto
            {
                Nombre = p.TipoPrestacion.NombrePrestacion,
                Cantidad = p.Cantidad
            }).ToList(),
            Valoracion = v == null ? new ValoracionDentalDto() : new ValoracionDentalDto
            {
                CariesPerm = v.CariesPerm,
                PerdidosPerm = v.PerdidosPerm,
                ObturadosPerm = v.ObturadosPerm,
                CariesTemp = v.CariesTemp,
                ExtraccionTemp = v.ExtraccionTemp,
                ObturadosTemp = v.ObturadosTemp
            },
            Odontograma = a.OdontogramaEstados.Select(e => new OdontogramaEstadoInput
            {
                NumeroDiente = e.NumeroDiente,
                Superficie = e.Superficie,
                Estado = e.Estado
            }).ToList()
        };

        return Ok(dto);
    }

    private static string TurnoTexto(int t) => t switch
    {
        1 => "Ventanilla",
        2 => "Profesional",
        3 => "Demanda",
        _ => "Interdisc."
    };
}
