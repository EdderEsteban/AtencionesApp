using AtencionesApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtencionesApp.Models.Data.Configuracion;

public class TipoPrestEnfConfig : IEntityTypeConfiguration<TipoPrestacionEnfermeria>
{
    public void Configure(EntityTypeBuilder<TipoPrestacionEnfermeria> builder)
    {
        builder.HasData(

new TipoPrestacionEnfermeria { Id = 1, Grupo = "Parenteral", NombrePrestacion = "Venoclisis", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 2, Grupo = "Parenteral", NombrePrestacion = "V.Endovenosa", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 3, Grupo = "Parenteral", NombrePrestacion = "V.Intramuscular", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 4, Grupo = "Parenteral", NombrePrestacion = "V.Subcutánea", IsDeleted = false },

// Enteral(Id 5 - 6)
new TipoPrestacionEnfermeria { Id = 5, Grupo = "Enteral", NombrePrestacion = "Via Oral", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 6, Grupo = "Enteral", NombrePrestacion = "V.Sublingual", IsDeleted = false },

//Mucosas y Piel(Id 7 - 12)
new TipoPrestacionEnfermeria { Id = 7, Grupo = "Mucosas y Piel", NombrePrestacion = "Adm.O2", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 8, Grupo = "Mucosas y Piel", NombrePrestacion = "Adm.Puff", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 9, Grupo = "Mucosas y Piel", NombrePrestacion = "Nebulizaciones", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 10, Grupo = "Mucosas y Piel", NombrePrestacion = "Adm.Ocular", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 11, Grupo = "Mucosas y Piel", NombrePrestacion = "Adm.Cutanea", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 12, Grupo = "Mucosas y Piel", NombrePrestacion = "Adm.Rectal", IsDeleted = false },

// Curaciones(Id 13 - 17)
new TipoPrestacionEnfermeria { Id = 13, Grupo = "Curaciones", NombrePrestacion = "Curacion Simple", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 14, Grupo = "Curaciones", NombrePrestacion = "Curacion Compleja", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 15, Grupo = "Curaciones", NombrePrestacion = "Curacion de Escara", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 16, Grupo = "Curaciones", NombrePrestacion = "Asistencia a Sutura", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 17, Grupo = "Curaciones", NombrePrestacion = "Extraccion de Puntos", IsDeleted = false }, 

// Vendajes(Id 18 - 20)
new TipoPrestacionEnfermeria { Id = 18, Grupo = "Vendajes", NombrePrestacion = "Vendaje Simple", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 19, Grupo = "Vendajes", NombrePrestacion = "Vendaje Elastico", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 20, Grupo = "Vendajes", NombrePrestacion = "Ferulas", IsDeleted = false },

// Lavajes(Id 21 - 22)
new TipoPrestacionEnfermeria { Id = 21, Grupo = "Lavajes", NombrePrestacion = "Lavado de Oido", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 22, Grupo = "Lavajes", NombrePrestacion = "Lavado de Ocular", IsDeleted = false },

// Sondaje(Id 23 - 24)
new TipoPrestacionEnfermeria { Id = 23, Grupo = "Sondaje", NombrePrestacion = "SNG", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 24, Grupo = "Sondaje", NombrePrestacion = "SV", IsDeleted = false },

// Metodos Fisicos(Id 25)
new TipoPrestacionEnfermeria { Id = 25, Grupo = "Metodos Fisicos", NombrePrestacion = "Metodos Fisicos", IsDeleted = false },

// Rehidratacion oral(Id 26)
new TipoPrestacionEnfermeria { Id = 26, Grupo = "Rehidratacion oral", NombrePrestacion = "Rehidratacion oral", IsDeleted = false },

//Antrop. (Id 27 - 28)
new TipoPrestacionEnfermeria { Id = 27, Grupo = "Antropometria", NombrePrestacion = "Antropometria", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 28, Grupo = "Antropometria", NombrePrestacion = "Percentilos", IsDeleted = false },

// Enema(Id 29)
new TipoPrestacionEnfermeria { Id = 29, Grupo = "Enema", NombrePrestacion = "Enema", IsDeleted = false },

// Controles(Id 30 - 33)
new TipoPrestacionEnfermeria { Id = 30, Grupo = "Controles", NombrePrestacion = "F.Respiratoria", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 31, Grupo = "Controles", NombrePrestacion = "C.Temperatura", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 32, Grupo = "Controles", NombrePrestacion = "C.Glucemias", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 33, Grupo = "Controles", NombrePrestacion = "TA", IsDeleted = false },

// ECG(Id 34)
new TipoPrestacionEnfermeria { Id = 34, Grupo = "ECG", NombrePrestacion = "ECG", IsDeleted = false },


// Internacion(Id 35 - 46)
new TipoPrestacionEnfermeria { Id = 35, Grupo = "Internacion", NombrePrestacion = "TAL", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 36, Grupo = "Internacion", NombrePrestacion = "Traslados y Derivaciones", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 37, Grupo = "Internacion", NombrePrestacion = "Retiro vcl", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 38, Grupo = "Internacion", NombrePrestacion = "Control de goteo", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 39, Grupo = "Internacion", NombrePrestacion = "Cambio de suero", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 40, Grupo = "Internacion", NombrePrestacion = "Control de Permeab.Del cateter", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 41, Grupo = "Internacion", NombrePrestacion = "Higiene y Confort", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 42, Grupo = "Internacion", NombrePrestacion = "Arreglo de la Unidad", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 43, Grupo = "Internacion", NombrePrestacion = "Revision de Ambulancia", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 44, Grupo = "Internacion", NombrePrestacion = "Diag de Enfermeria", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 45, Grupo = "Internacion", NombrePrestacion = "Pase de Guardia", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 46, Grupo = "Internacion", NombrePrestacion = "Confeccion de Report", IsDeleted = false },

// Triagge(Id 47)
new TipoPrestacionEnfermeria { Id = 47, Grupo = "Triagge", NombrePrestacion = "Triagge", IsDeleted = false },

// Otras Actividades(Id 48)
new TipoPrestacionEnfermeria { Id = 48, Grupo = "Otras Actividades", NombrePrestacion = "Otras Actividades", IsDeleted = false },

// Salidas de Ambulancia(Id 49 - 59)
new TipoPrestacionEnfermeria { Id = 49, Grupo = "Salidas de Ambulancia", NombrePrestacion = "Ambulancia Propia", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 50, Grupo = "Salidas de Ambulancia", NombrePrestacion = "Sempro", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 51, Grupo = "Salidas de Ambulancia", NombrePrestacion = "Consultorio", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 52, Grupo = "Salidas de Ambulancia", NombrePrestacion = "Guardia", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 53, Grupo = "Salidas de Ambulancia", NombrePrestacion = "Ramon Carrillo", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 54, Grupo = "Salidas de Ambulancia", NombrePrestacion = "Maternidad", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 55, Grupo = "Salidas de Ambulancia", NombrePrestacion = "Pediatrico", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 56, Grupo = "Salidas de Ambulancia", NombrePrestacion = "Privado", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 57, Grupo = "Salidas de Ambulancia", NombrePrestacion = "Salida auxilio", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 58, Grupo = "Salidas de Ambulancia", NombrePrestacion = "Salud Mental", IsDeleted = false },
new TipoPrestacionEnfermeria { Id = 59, Grupo = "Salidas de Ambulancia", NombrePrestacion = "Salida Programada", IsDeleted = false }
        );
    }
}