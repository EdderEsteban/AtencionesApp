using System.ComponentModel.DataAnnotations;

namespace AtencionesApp.Models.ViewModels;

public class PrestacionOdontologiaFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es requerido")]
    [MaxLength(200)]
    public string NombrePrestacion { get; set; } = "";
}
