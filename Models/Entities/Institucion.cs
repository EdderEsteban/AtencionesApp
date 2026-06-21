namespace AtencionesApp.Models.Entities;

public class Institucion
{
    public int Id { get; set; }
    public string CodigoSISA { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Dependencia { get; set; } = string.Empty;
    public string Tipologia { get; set; } = string.Empty;
    public string? SitioWeb { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    public ICollection<AtencionEnfermeria> AtencionesEnfermeria { get; set; } = new
List<AtencionEnfermeria>();
    public ICollection<AtencionOdontologia> AtencionesOdontologia { get; set; } = new
List<AtencionOdontologia>();
}