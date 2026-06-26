using System.ComponentModel.DataAnnotations;

namespace AtencionesApp.Models.ViewModels;

public class PrestacionEnfermeriaFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El grupo es requerido")]
    [MaxLength(100)]
    public string Grupo { get; set; } = "";

    [Required(ErrorMessage = "El nombre es requerido")]
    [MaxLength(200)]
    public string NombrePrestacion { get; set; } = "";
}
