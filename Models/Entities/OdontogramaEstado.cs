namespace AtencionesApp.Models.Entities;

public class OdontogramaEstado
{
    public int Id { get; set; }

    public int AtencionOdontologiaId { get; set; }
    public AtencionOdontologia AtencionOdontologia { get; set; } = null!;

    public int NumeroDiente { get; set; }   // FDI: 11-48 permanentes, 51-85 temporarios
    public string Superficie { get; set; } = "";  // V, M, D, L, O, * (todo el diente)
    public int Estado { get; set; } // 1=Caries 2=Obturado 3=Ausente 4=ExtraccionIndicada 5=Corona

    public bool IsDeleted { get; set; }
}