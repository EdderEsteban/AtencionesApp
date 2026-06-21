 namespace AtencionesApp.Models.Entities;

  public class PrestacionEnfermeria
  {
      public int Id { get; set; }
      public int Cantidad { get; set; }
      public bool IsDeleted { get; set; }

      public int AtencionId { get; set; }
      public AtencionEnfermeria Atencion { get; set; } = null!;

      public int TipoPrestacionId { get; set; }
      public TipoPrestacionEnfermeria TipoPrestacion { get; set; } = null!;
  }