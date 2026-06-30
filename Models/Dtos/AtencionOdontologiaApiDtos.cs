namespace AtencionesApp.Models.Dtos;

public class OdontogramaEstadoInput
{
    public int NumeroDiente { get; set; }
    public string Superficie { get; set; } = "";   // V/D/L/M/O o *
    public int Estado { get; set; }                 // 1 Caries,2 Obturado,3 Ausente,4 ExtrIndicada,5 Corona
}

public class CrearAtencionOdontologiaRequest
{
    public int PacienteId { get; set; }
    public int TipoConsulta { get; set; }   // 1 PrimeraVez, 2 Ulterior
    public int TipoTurno { get; set; }       // 1 Ventanilla,2 Profesional,3 Demanda,4 Interdisc
    public int DiagnosticoId { get; set; }
    public bool Embarazada { get; set; }
    public bool SinObraSocial { get; set; }
    public string? NuevaObraSocial { get; set; }
    public string? Observaciones { get; set; }
    public List<PrestacionInputDto> Prestaciones { get; set; } = new();
    public List<OdontogramaEstadoInput> Odontograma { get; set; } = new();
}

public class ValoracionDentalDto
{
    public int CariesPerm { get; set; }
    public int PerdidosPerm { get; set; }
    public int ObturadosPerm { get; set; }
    public int CariesTemp { get; set; }
    public int ExtraccionTemp { get; set; }
    public int ObturadosTemp { get; set; }
}

public class TipoPrestacionOdoDto
{
    public int Id { get; set; }
    public string NombrePrestacion { get; set; } = "";
}

public class DiagnosticoDto
{
    public int Id { get; set; }
    public string Codigo { get; set; } = "";
    public string Descripcion { get; set; } = "";
}

public class AtencionOdontologiaDetalleDto
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public int PacienteId { get; set; }
    public string PacienteNombre { get; set; } = "";
    public string PacienteDni { get; set; } = "";
    public int Edad { get; set; }
    public string TipoConsulta { get; set; } = "";
    public string TipoTurno { get; set; } = "";
    public string? Diagnostico { get; set; }
    public bool Embarazada { get; set; }
    public bool SinObraSocial { get; set; }
    public string? Observaciones { get; set; }
    public string Profesional { get; set; } = "";
    public string Institucion { get; set; } = "";
    public List<PrestacionDetalleDto> Prestaciones { get; set; } = new();
    public ValoracionDentalDto Valoracion { get; set; } = new();
    public List<OdontogramaEstadoInput> Odontograma { get; set; } = new();
}
