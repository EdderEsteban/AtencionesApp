namespace AtencionesApp.Models.ViewModels;

public class OdontogramaViewModel
{
    public string Modo { get; set; } = "readonly"; // "editable" o "readonly"
    public List<OdontogramaEstadoItemVM> Estados { get; set; } = new();
}

public class OdontogramaEstadoItemVM
{
    public int NumeroDiente { get; set; }
    public string Superficie { get; set; } = ""; // V, M, D, L, O, * (todo el diente)
    public int Estado { get; set; } // 1=Caries 2=Obturado 3=Ausente 4=ExtraccionIndicada 5=Corona
}