using Microsoft.AspNetCore.Mvc.Rendering;

  namespace AtencionesApp.Models.ViewModels;

  public class ReporteOdontologiaViewModel
  {
      // Filtros
      public DateTime Desde { get; set; }
      public DateTime Hasta { get; set; }
      public string PeriodoLabel { get; set; } = "";
      public int? ProfesionalId { get; set; }
      public string? ProfesionalNombre { get; set; }
      public List<SelectListItem> Profesionales { get; set; } = new();
      public string? InstitucionNombre { get; set; }

      // Resumen
      public int TotalAtenciones { get; set; }
      public int PacientesUnicos { get; set; }
      public int TotalPrestaciones { get; set; }
      public double PorcentajeSinOS { get; set; }
      public double PorcentajeEmbarazadas { get; set; }

      // Actividad en el tiempo
      public List<string> LabelsActividad { get; set; } = new();
      public List<int> SerieActividad { get; set; } = new();

      // Tipo de consulta (donut)
      public int TotalPrimeraVez { get; set; }
      public int TotalUlterior { get; set; }

      // Tipo de turno (barras)
      public int TotalVentanilla { get; set; }
      public int TotalPorProfesional { get; set; }
      public int TotalDemanda { get; set; }
      public int TotalInterdisc { get; set; }

      // Cobertura OS (donut)
      public int TotalConOS { get; set; }
      public int TotalSinOS { get; set; }

      // Perfil pacientes
      public int SexoM { get; set; }
      public int SexoF { get; set; }
      public int SexoNoEsp { get; set; }
      public int TotalEmbarazadas { get; set; }
      public List<int> SerieEdad { get; set; } = new();
      public List<string> LabelsEdad { get; set; } =
          new() { "< 5", "5-14", "15-29", "30-44", "45-59", "60+" };

      // Top 10 prestaciones
      public List<string> Top10Nombres { get; set; } = new();
      public List<int> Top10Cantidades { get; set; } = new();

      // Top 10 diagnósticos
      public List<string> Top10DiagNombres { get; set; } = new();
      public List<int> Top10DiagCantidades { get; set; } = new();

      // CPO promedio del período
      public bool HayValoraciones { get; set; }
      public double PromCariesPerm { get; set; }
      public double PromPerdidosPerm { get; set; }
      public double PromObturadosPerm { get; set; }
      public double PromCariesTemp { get; set; }
      public double PromExtraccionTemp { get; set; }
      public double PromObturadosTemp { get; set; }

      // Por profesional (Director / Admin)
      public List<ResumenProfesionalOdoVM> PorProfesional { get; set; } = new();
  }

  public class ResumenProfesionalOdoVM
  {
      public string Nombre { get; set; } = "";
      public int Atenciones { get; set; }
      public int PrimeraVez { get; set; }
      public int SinOS { get; set; }
      public double PctPrimeraVez =>
          Atenciones > 0 ? Math.Round((double)PrimeraVez / Atenciones * 100, 1) : 0;
      public double PctSinOS =>
          Atenciones > 0 ? Math.Round((double)SinOS / Atenciones * 100, 1) : 0;
  }