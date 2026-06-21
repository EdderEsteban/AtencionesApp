namespace AtencionesApp.Models.Entities;

  public class TipoPrestacionEnfermeria
  {
      public int Id { get; set; }
      public string Grupo { get; set; } = string.Empty;
      public string NombrePrestacion { get; set; } = string.Empty;
      public bool IsDeleted { get; set; }

      public ICollection<PrestacionEnfermeria> Prestaciones { get; set; } = new
  List<PrestacionEnfermeria>();
  }