using AtencionesApp.Models.Data;
using AtencionesApp.Models.Dtos;
using AtencionesApp.Models.Entities;
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

        var ahora = DateTime.Now;
        var edad = ahora.Year - paciente.FechaNacimiento.Year;
        if (paciente.FechaNacimiento.DayOfYear > ahora.DayOfYear) edad--;

        var odo = req.Odontograma ?? new();

        var atencion = new AtencionOdontologia
        {
            Fecha = ahora,
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
            ValoracionDental = CalcularCPO(odo),     // ← CPO calculado en el servidor
            OdontogramaEstados = odo.Where(e => e.Estado > 0).Select(e => new OdontogramaEstado
            {
                NumeroDiente = e.NumeroDiente,
                Superficie = e.Superficie,
                Estado = e.Estado
            }).ToList()
        };

        if (!req.SinObraSocial && !string.IsNullOrWhiteSpace(req.NuevaObraSocial))
            paciente.ObraSocial = req.NuevaObraSocial.Trim();
        else if (!req.SinObraSocial && string.IsNullOrEmpty(paciente.ObraSocial))
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

    // ── CPO/ceo calculado en el servidor (porteado de odontograma.js) ──
    private static ValoracionDental CalcularCPO(List<OdontogramaEstadoInput> estados)
    {
        var v = new ValoracionDental();
        foreach (var g in estados.GroupBy(e => e.NumeroDiente))
        {
            var num = g.Key;
            var estrella = g.FirstOrDefault(x => x.Superficie == "*");
            var vals = g.Where(x => x.Estado > 0).Select(x => x.Estado).ToList();
            bool esPermanente = num <= 48;

            if (esPermanente)
            {
                if (estrella?.Estado == 3) { v.PerdidosPerm++; continue; }  // Ausente
                if (estrella?.Estado == 4) continue;                         // Extr. indicada no cuenta
                if (vals.Contains(1)) v.CariesPerm++;
                else if (vals.Any(x => x == 2 || x == 5)) v.ObturadosPerm++;
            }
            else // temporario
            {
                if (estrella?.Estado == 3) continue;                         // Ausente no cuenta
                if (estrella?.Estado == 4) { v.ExtraccionTemp++; continue; } // Extr. indicada
                if (vals.Contains(1)) v.CariesTemp++;
                else if (vals.Any(x => x == 2 || x == 5)) v.ObturadosTemp++;
            }
        }
        return v;
    }

    private static string TurnoTexto(int t) => t switch
    {
        1 => "Ventanilla",
        2 => "Profesional",
        3 => "Demanda",
        _ => "Interdisc."
    };
}
