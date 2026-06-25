 namespace AtencionesApp.Models.ViewModels;

  public class DashboardViewModel
  {
      // Gráfica 1 — Barras semanales (últimos 7 días)
      public List<string> Labels7Dias { get; set; } = new();
      public List<int> SerieEnfermeria { get; set; } = new();
      public List<int> SerieOdontologia { get; set; } = new();

      // Gráfica 2 — Donut
      public List<string> DonutLabels { get; set; } = new();
      public List<int> DonutValores { get; set; } = new();

      // Gráfica 3 — Línea tendencia (últimos 6 meses)
      public List<string> Labels6Meses { get; set; } = new();
      public List<int> SerieMensual { get; set; } = new();

      // Gráfica 4 — Top 10 prestaciones
      public List<string> Top10Nombres { get; set; } = new();
      public List<int> Top10Cantidades { get; set; } = new();
      public List<string> Top10Tipos { get; set; } = new(); // "E" = Enfermería, "O" = Odontología

      // Títulos dinámicos según rol
      public string TituloBarras { get; set; } = string.Empty;
      public string TituloDonut { get; set; } = string.Empty;
      public string TituloLinea { get; set; } = string.Empty;
      public string TituloTop10 { get; set; } = string.Empty;
  }