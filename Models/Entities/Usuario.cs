 namespace AtencionesApp.Models.Entities;

  public class Usuario
  {
      public int Id { get; set; }
      public string Nombre { get; set; } = string.Empty;
      public string Apellido { get; set; } = string.Empty;
      public string Email { get; set; } = string.Empty;
      public string? Telefono { get; set; }
      public string PasswordHash { get; set; } = string.Empty;
      public bool IsDeleted { get; set; }

      public int RolId { get; set; }
      public Rol Rol { get; set; } = null!;

      public ICollection<Institucion> Instituciones { get; set; } = new List<Institucion>();
      public ICollection<AtencionEnfermeria> AtencionesEnfermeria { get; set; } = new
  List<AtencionEnfermeria>();
      public ICollection<AtencionOdontologia> AtencionesOdontologia { get; set; } = new
  List<AtencionOdontologia>();
  }