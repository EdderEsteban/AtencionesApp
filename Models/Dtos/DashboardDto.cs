namespace AtencionesApp.Models.Dtos;

  public class DashboardDto
  {
      public string TituloBarras { get; set; } = "";
      public string TituloDonut { get; set; } = "";
      public string TituloLinea { get; set; } = "";
      public string TituloTop10 { get; set; } = "";

      // Gráfica 1 — barras actividad últimos 7 días
      public List<string> Labels7Dias { get; set; } = new();
      public List<int> Serie7Dias { get; set; } = new();

      // Gráfica 2 — donut
      public List<string> DonutLabels { get; set; } = new();
      public List<int> DonutValores { get; set; } = new();

      // Gráfica 3 — línea tendencia 6 meses
      public List<string> Labels6Meses { get; set; } = new();
      public List<int> Serie6Meses { get; set; } = new();

      // Gráfica 4 — top 10 prestaciones
      public List<string> Top10Nombres { get; set; } = new();
      public List<int> Top10Cantidades { get; set; } = new();
  }