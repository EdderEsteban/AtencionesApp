using Microsoft.AspNetCore.Mvc.Rendering;

namespace AtencionesApp.Models.ViewModels;

public class ReporteEnfermeriaViewModel
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

    // Tipo de atención (donut)
    public int TotalAmbulatorio { get; set; }
    public int TotalInternado { get; set; }

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

    // Por profesional (Director / Admin)
    public List<ResumenProfesionalEnfVM> PorProfesional { get; set; } = new();
}

public class ResumenProfesionalEnfVM
{
    public string Nombre { get; set; } = "";
    public int Atenciones { get; set; }
    public int Ambulatorio { get; set; }
    public int SinOS { get; set; }
    public double PctAmbulatorio =>
        Atenciones > 0 ? Math.Round((double)Ambulatorio / Atenciones * 100, 1) : 0;
    public double PctSinOS =>
        Atenciones > 0 ? Math.Round((double)SinOS / Atenciones * 100, 1) : 0;
}