namespace AtencionesApp.Models.Entities;

public class AtencionOdontologia
{
    public int Id { get; set; }

    // Fecha del acto asistencial. Si la atención llegó de la app móvil es la de
    // captura en el teléfono, que puede ser varios días anterior a esta fila.
    public DateTime Fecha { get; set; }

    // Momento en que el servidor la recibió. Null en las cargadas por la web.
    public DateTime? FechaSincronizacion { get; set; }

    public int Edad { get; set; }
    public int TipoConsulta { get; set; } // 1=PrimeraVez, 2=Ulterior
    public int TipoTurno { get; set; }    // 1=Ventanilla, 2=Profesional, 3=Demanda, 4=Interdisc
    public bool Embarazada { get; set; }
    public bool SinObraSocial { get; set; }
    public string? Observaciones { get; set; }
    public bool IsDeleted { get; set; }

    public int PacienteId { get; set; }
    public Paciente Paciente { get; set; } = null!;

    public int InstitucionId { get; set; }
    public Institucion Institucion { get; set; } = null!;

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public int DiagnosticoId { get; set; }
    public Diagnostico Diagnostico { get; set; } = null!;

    public ValoracionDental? ValoracionDental { get; set; }
    public ICollection<PrestacionOdontologia> Prestaciones { get; set; } = new
    List<PrestacionOdontologia>();
    public ICollection<OdontogramaEstado> OdontogramaEstados { get; set; } = new
    List<OdontogramaEstado>();
}