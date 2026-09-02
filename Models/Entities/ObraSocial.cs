namespace AtencionesApp.Models.Entities;

public class ObraSocial
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }

    public ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
}
