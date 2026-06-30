namespace AtencionesApp.Models.Dtos;

public class PrestacionInputDto
{
    public int TipoPrestacionId { get; set; }
    public int Cantidad { get; set; }
}

public class CrearAtencionEnfermeriaRequest
{
    public int PacienteId { get; set; }
    public int TipoAtencion { get; set; } // 1 Ambulatorio, 2 Internado
    public bool Embarazada { get; set; }
    public bool SinObraSocial { get; set; }
    public string? NuevaObraSocial { get; set; }
    public string? Observaciones { get; set; }
    public List<PrestacionInputDto> Prestaciones { get; set; } = new();
}

public class TipoPrestacionEnfDto
{
    public int Id { get; set; }
    public string Grupo { get; set; } = "";
    public string NombrePrestacion { get; set; } = "";
}

public class PrestacionDetalleDto
{
    public string Nombre { get; set; } = "";
    public int Cantidad { get; set; }
}

public class AtencionEnfermeriaDetalleDto
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public int PacienteId { get; set; }
    public string PacienteNombre { get; set; } = "";
    public string PacienteDni { get; set; } = "";
    public int Edad { get; set; }
    public string TipoAtencion { get; set; } = "";
    public bool Embarazada { get; set; }
    public bool SinObraSocial { get; set; }
    public string? Observaciones { get; set; }
    public string Profesional { get; set; } = "";
    public string Institucion { get; set; } = "";
    public List<PrestacionDetalleDto> Prestaciones { get; set; } = new();
}