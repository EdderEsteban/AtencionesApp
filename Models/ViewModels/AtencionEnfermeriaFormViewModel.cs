using System.ComponentModel.DataAnnotations;

namespace AtencionesApp.Models.ViewModels;

public class AtencionEnfermeriaFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria")]
    [DataType(DataType.Date)]
    public DateTime Fecha { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Seleccioná un paciente")]
    public int PacienteId { get; set; }
    public string PacienteNombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El tipo de atención es obligatorio")]
    public int TipoAtencion { get; set; }

    public string PacienteFechaNacimiento { get; set; } = string.Empty;

    public bool Embarazada { get; set; }
    public bool SinObraSocial { get; set; }

    public int Edad { get; set; }

    public List<PrestacionSeleccionadaVM> Prestaciones { get; set; } = new();
}

public class PrestacionSeleccionadaVM
{
    public int TipoPrestacionId { get; set; }
    public int Cantidad { get; set; }
}