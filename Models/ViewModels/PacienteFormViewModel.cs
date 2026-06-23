 using System.ComponentModel.DataAnnotations;

  namespace AtencionesApp.Models.ViewModels;

  public class PacienteFormViewModel
  {
      public int Id { get; set; }

      [Required(ErrorMessage = "El apellido es obligatorio")]
      [StringLength(100)]
      public string Apellido { get; set; } = string.Empty;

      [Required(ErrorMessage = "El nombre es obligatorio")]
      [StringLength(100)]
      public string Nombre { get; set; } = string.Empty;

      [Required(ErrorMessage = "El DNI es obligatorio")]
      [StringLength(20)]
      public string DNI { get; set; } = string.Empty;

      [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
      [DataType(DataType.Date)]
      public DateTime FechaNacimiento { get; set; }

      [Required(ErrorMessage = "El sexo es obligatorio")]
      public string Sexo { get; set; } = string.Empty;

      [StringLength(200)]
      public string? Domicilio { get; set; }

      [StringLength(50)]
      public string? Telefono { get; set; }

      [StringLength(100)]
      public string? ObraSocial { get; set; }
  }