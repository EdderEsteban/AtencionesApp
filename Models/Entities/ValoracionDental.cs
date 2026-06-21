namespace AtencionesApp.Models.Entities;

  public class ValoracionDental
  {
      public int Id { get; set; }
      public int CariesPerm { get; set; }
      public int PerdidosPerm { get; set; }
      public int ObturadosPerm { get; set; }
      public int CariesTemp { get; set; }
      public int ExtraccionTemp { get; set; }
      public int ObturadosTemp { get; set; }
      public bool IsDeleted { get; set; }

      public int AtencionOdontologiaId { get; set; }
      public AtencionOdontologia AtencionOdontologia { get; set; } = null!;
  }