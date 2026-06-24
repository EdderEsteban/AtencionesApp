using AtencionesApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtencionesApp.Models.Data.Configuracion;

public class DiagnosticoConfig : IEntityTypeConfiguration<Diagnostico>
{
    public void Configure(EntityTypeBuilder<Diagnostico> builder)
    {
        builder.HasData(
            // Caries dental
            new Diagnostico { Id = 1,  Codigo = "K02.0", Descripcion = "Caries limitada al esmalte" },
            new Diagnostico { Id = 2,  Codigo = "K02.1", Descripcion = "Caries de la dentina" },
            new Diagnostico { Id = 3,  Codigo = "K02.2", Descripcion = "Caries del cemento" },
            new Diagnostico { Id = 4,  Codigo = "K02.3", Descripcion = "Caries dental detenida" },
            new Diagnostico { Id = 5,  Codigo = "K02.9", Descripcion = "Caries dental, no especificada" },

            // Pulpa y tejidos periapicales
            new Diagnostico { Id = 6,  Codigo = "K04.0", Descripcion = "Pulpitis" },
            new Diagnostico { Id = 7,  Codigo = "K04.1", Descripcion = "Necrosis de la pulpa" },
            new Diagnostico { Id = 8,  Codigo = "K04.4", Descripcion = "Periodontitis apical aguda" },
            new Diagnostico { Id = 9,  Codigo = "K04.5", Descripcion = "Periodontitis apical crónica" },
            new Diagnostico { Id = 10, Codigo = "K04.6", Descripcion = "Absceso periapical con fístula" },
            new Diagnostico { Id = 11, Codigo = "K04.7", Descripcion = "Absceso periapical sin fístula" },

            // Gingivitis y enfermedades periodontales
            new Diagnostico { Id = 12, Codigo = "K05.0", Descripcion = "Gingivitis aguda" },
            new Diagnostico { Id = 13, Codigo = "K05.1", Descripcion = "Gingivitis crónica" },
            new Diagnostico { Id = 14, Codigo = "K05.2", Descripcion = "Periodontitis aguda" },
            new Diagnostico { Id = 15, Codigo = "K05.3", Descripcion = "Periodontitis crónica" },
            new Diagnostico { Id = 16, Codigo = "K05.4", Descripcion = "Periodontosis" },

            // Periodonto y tejidos de sostén
            new Diagnostico { Id = 17, Codigo = "K06.0", Descripcion = "Recesión gingival" },
            new Diagnostico { Id = 18, Codigo = "K06.1", Descripcion = "Agrandamiento gingival" },
            new Diagnostico { Id = 19, Codigo = "K08.1", Descripcion = "Pérdida de dientes por accidente o extracción" },
            new Diagnostico { Id = 20, Codigo = "K08.3", Descripcion = "Raíz dental retenida" },

            // Dientes incluidos
            new Diagnostico { Id = 21, Codigo = "K01.0", Descripcion = "Dientes incluidos" },
            new Diagnostico { Id = 22, Codigo = "K01.1", Descripcion = "Dientes impactados" },

            // Anomalías dentofaciales
            new Diagnostico { Id = 23, Codigo = "K07.0", Descripcion = "Anomalías del tamaño de los maxilares" },
            new Diagnostico { Id = 24, Codigo = "K07.3", Descripcion = "Anomalías de la posición del diente" },

            // Estomatitis y lesiones mucosa
            new Diagnostico { Id = 25, Codigo = "K12.0", Descripcion = "Estomatitis aftosa recurrente" },
            new Diagnostico { Id = 26, Codigo = "K12.1", Descripcion = "Otras formas de estomatitis" },
            new Diagnostico { Id = 27, Codigo = "K13.0", Descripcion = "Enfermedades de los labios" },
            new Diagnostico { Id = 28, Codigo = "K13.7", Descripcion = "Otras lesiones y las no especificadas de la mucosa bucal" },

            // Control / sin patología
            new Diagnostico { Id = 29, Codigo = "Z01.2", Descripcion = "Examen odontológico de rutina" },
            new Diagnostico { Id = 30, Codigo = "Z29.8", Descripcion = "Control preventivo y educación en salud bucal" }
        );
    }
}
