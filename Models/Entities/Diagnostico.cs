 namespace AtencionesApp.Models.Entities;

  public class Diagnostico
  {
      public int Id { get; set; }
      public string Codigo { get; set; } = string.Empty;
      public string Descripcion { get; set; } = string.Empty;
      public bool IsDeleted { get; set; }

      public ICollection<AtencionOdontologia> AtencionesOdontologia { get; set; } = new
  List<AtencionOdontologia>();
  }