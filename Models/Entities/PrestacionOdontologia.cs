namespace AtencionesApp.Models.Entities;

  public class PrestacionOdontologia
  {
      public int Id { get; set; }
      public int Cantidad { get; set; }
      public bool IsDeleted { get; set; }

      public int AtencionId { get; set; }
      public AtencionOdontologia Atencion { get; set; } = null!;

      public int TipoPrestacionId { get; set; }
      public TipoPrestacionOdontologia TipoPrestacion { get; set; } = null!;
  }