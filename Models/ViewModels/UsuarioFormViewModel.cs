using System.ComponentModel.DataAnnotations;

namespace AtencionesApp.Models.ViewModels;

public class UsuarioFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es requerido")]
    [MaxLength(100)]
    public string Nombre { get; set; } = "";

    [Required(ErrorMessage = "El apellido es requerido")]
    [MaxLength(100)]
    public string Apellido { get; set; } = "";

    [Required(ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    [MaxLength(200)]
    public string Email { get; set; } = "";

    [MaxLength(20)]
    public string? Telefono { get; set; }

    [Required(ErrorMessage = "El rol es requerido")]
    public int RolId { get; set; }

    public List<int> InstitucionIds { get; set; } = new();

    [MinLength(6, ErrorMessage = "Mínimo 6 caracteres")]
    public string? Contrasena { get; set; }

    [Compare("Contrasena", ErrorMessage = "Las contraseñas no coinciden")]
    public string? ConfirmarContrasena { get; set; }

    public bool EsEdicion => Id > 0;
}
