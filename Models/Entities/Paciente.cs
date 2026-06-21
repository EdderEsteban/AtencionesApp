namespace AtencionesApp.Models.Entities;

  public class Paciente
  {
      public int Id { get; set; }
      public string Apellido { get; set; } = string.Empty;
      public string Nombre { get; set; } = string.Empty;
      public string DNI { get; set; } = string.Empty;
      public DateTime FechaNacimiento { get; set; }
      public string Sexo { get; set; } = string.Empty;
      public string? Domicilio { get; set; }
      public string? Telefono { get; set; }
      public string? ObraSocial { get; set; }
      public bool IsDeleted { get; set; }

      public ICollection<AtencionEnfermeria> AtencionesEnfermeria { get; set; } = new
  List<AtencionEnfermeria>();
      public ICollection<AtencionOdontologia> AtencionesOdontologia { get; set; } = new
  List<AtencionOdontologia>();
  }