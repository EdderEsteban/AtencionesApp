using System.ComponentModel.DataAnnotations;

namespace AtencionesApp.Models.ViewModels
{
    public class AtencionOdontologiaFormViewModel
    {
        public int Id { get; set; }

        public int PacienteId { get; set; }
        public string PacienteNombre { get; set; } = "";
        public string PacienteFechaNacimiento { get; set; } = "";
        public string PacienteSexo { get; set; } = "";
        public bool PacienteTieneObraSocial { get; set; }
        public string? PacienteObraSocial { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Seleccioná el tipo de consulta")]
        public int TipoConsulta { get; set; }

        [Required(ErrorMessage = "Seleccioná el tipo de turno")]
        public int TipoTurno { get; set; }

        [Required(ErrorMessage = "Seleccioná un diagnóstico")]
        public int DiagnosticoId { get; set; }

        public int Edad { get; set; }
        public bool Embarazada { get; set; }
        public bool SinObraSocial { get; set; }
        public string? NuevaObraSocial { get; set; }
        public string? Observaciones { get; set; }
        public List<OdontogramaEstadoItemVM> OdontogramaEstados { get; set; } = new();
        public List<PrestacionSeleccionadaVM> Prestaciones { get; set; } = new();
        public ValoracionDentalVM? Valoracion { get; set; }
    }

    public class ValoracionDentalVM
    {
        public int CariesPerm { get; set; }
        public int PerdidosPerm { get; set; }
        public int ObturadosPerm { get; set; }
        public int CariesTemp { get; set; }
        public int ExtraccionTemp { get; set; }
        public int ObturadosTemp { get; set; }
    }
}
