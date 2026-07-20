using AtencionesApp.Models.Entities;

namespace AtencionesApp.Services;

/// <summary>
/// Cálculo del índice CPO/ceo a partir de los estados crudos del odontograma.
/// Es la fuente de verdad ÚNICA del servidor: la usan tanto la web (MVC) como la
/// API REST, de modo que el valor persistido nunca dependa de lo que calcule el
/// cliente. Las reglas son idénticas a las de wwwroot/js/odontograma.js (que solo
/// da feedback inmediato en el navegador).
/// </summary>
public static class CalculadoraCpo
{
    // Estados: 0=Sano, 1=Caries, 2=Obturado, 3=Ausente, 4=Extracción indicada, 5=Corona.
    // Se cuenta POR PIEZA (no por superficie). Permanentes: numeración FDI <= 48.
    public static ValoracionDental Calcular(IEnumerable<OdontogramaEstado> estados)
    {
        var v = new ValoracionDental();

        foreach (var g in estados.GroupBy(e => e.NumeroDiente))
        {
            var estrella = g.FirstOrDefault(x => x.Superficie == "*");
            var vals = g.Where(x => x.Estado > 0).Select(x => x.Estado).ToList();
            bool esPermanente = g.Key <= 48;

            if (esPermanente)
            {
                if (estrella?.Estado == 3) { v.PerdidosPerm++; continue; }  // Ausente = Perdido
                if (estrella?.Estado == 4) continue;                         // Extr. indicada no cuenta
                if (vals.Contains(1)) v.CariesPerm++;
                else if (vals.Any(x => x == 2 || x == 5)) v.ObturadosPerm++;
            }
            else // temporario
            {
                if (estrella?.Estado == 3) continue;                         // Ausente no cuenta
                if (estrella?.Estado == 4) { v.ExtraccionTemp++; continue; } // Extr. indicada = "e" de ceo
                if (vals.Contains(1)) v.CariesTemp++;
                else if (vals.Any(x => x == 2 || x == 5)) v.ObturadosTemp++;
            }
        }

        return v;
    }
}
