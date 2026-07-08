using System.ComponentModel.DataAnnotations;

namespace AtencionesApp.Models.ViewModels;

public class MiPerfilViewModel
{
    public string NombreCompleto { get; set; } = "";
    public string Rol { get; set; } = "";

    [Required(ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    [MaxLength(200)]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Ingresá tu contraseña actual para confirmar los cambios")]
    public string ContrasenaActual { get; set; } = "";

    [MinLength(6, ErrorMessage = "Mínimo 6 caracteres")]
    public string? ContrasenaNueva { get; set; }

    [Compare("ContrasenaNueva", ErrorMessage = "Las contraseñas no coinciden")]
    public string? ConfirmarContrasenaNueva { get; set; }
}
