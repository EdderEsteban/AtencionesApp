namespace AtencionesApp.Models.Entities;

  public class AtencionEnfermeria
  {
      public int Id { get; set; }
      public DateTime Fecha { get; set; }
      public int Edad { get; set; }
      public int TipoAtencion { get; set; } // 1=Ambulatorio, 2=Internado
      public bool Embarazada { get; set; }
      public bool SinObraSocial { get; set; }
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