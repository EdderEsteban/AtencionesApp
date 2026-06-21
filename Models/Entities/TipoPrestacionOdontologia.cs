namespace AtencionesApp.Models.Entities;

public class TipoPrestacionOdontologia
{
    public int Id { get; set; }
    public string NombrePrestacion { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }

    public ICollection<PrestacionOdontologia> Prestaciones { get; set; } = new
List<PrestacionOdontologia>();
}