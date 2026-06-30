namespace AtencionesApp.Models.Dtos;

public class PacienteListItemDto
{
    public int Id { get; set; }
    public string Apellido { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string DNI { get; set; } = "";
    public int Edad { get; set; }
    public string Sexo { get; set; } = "";
    public string? ObraSocial { get; set; }
    public string? Telefono { get; set; }
}

public class PacienteDetalleDto
{
    public int Id { get; set; }
    public string Apellido { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string DNI { get; set; } = "";
    public DateTime FechaNacimiento { get; set; }
    public int Edad { get; set; }
    public string Sexo { get; set; } = "";
    public string? Domicilio { get; set; }
    public string? Telefono { get; set; }
    public string? ObraSocial { get; set; }
    public List<AtencionTimelineDto> Atenciones { get; set; } = new();
}

public class AtencionTimelineDto
{
    public int Id { get; set; }
    public string Tipo { get; set; } = ""; // "E" enfermería · "O" odontología
    public DateTime Fecha { get; set; }
    public string Resumen { get; set; } = ""; // ej "Ambulatorio" / "1ª vez · Ventanilla"
    public string? Diagnostico { get; set; } // solo odot
    public List<string> Prestaciones { get; set; } = new();
    public string? Observaciones { get; set; }
}

