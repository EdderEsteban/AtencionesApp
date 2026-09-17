namespace AtencionesApp.Models.Entities;

  public class AtencionEnfermeria
  {
      public int Id { get; set; }

      // Fecha del acto asistencial. Si la atención llegó de la app móvil es la de
      // captura en el teléfono, que puede ser varios días anterior a esta fila:
      // el enfermero carga en la gira y sincroniza cuando vuelve a tener señal.
      public DateTime Fecha { get; set; }

      // Momento en que el servidor la recibió. Queda en null en las atenciones
      // cargadas por la web, donde registrar y guardar son el mismo instante.
      public DateTime? FechaSincronizacion { get; set; }

      public int Edad { get; set; }
      public int TipoAtencion { get; set; } // 1=Ambulatorio, 2=Internado
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

      public ICollection<PrestacionEnfermeria> Prestaciones { get; set; } = new
  List<PrestacionEnfermeria>();
  }