using AtencionesApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtencionesApp.Models.Data.Configuracion;

public class TipoPrestOdoConfig : IEntityTypeConfiguration<TipoPrestacionOdontologia>
{
    public void Configure(EntityTypeBuilder<TipoPrestacionOdontologia> builder)
    {
        builder.HasData(

new TipoPrestacionOdontologia { Id = 1, NombrePrestacion = "Motivacion Orientación dx y terap.", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 2, NombrePrestacion = "Tto Farmacológico", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 3, NombrePrestacion = "Enseñanza THO-Revelado de placa", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 4, NombrePrestacion = "Topicación c/flúor", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 5, NombrePrestacion = "Sellado fosas y fisuras", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 6, NombrePrestacion = "Remineralización", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 7, NombrePrestacion = "Inactivación de caries", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 8, NombrePrestacion = "Tartrectomía", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 9, NombrePrestacion = "Obturación provisoria", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 10, NombrePrestacion = "Obturación definitiva", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 11, NombrePrestacion = "Apertura/instrumentación de conducto", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 12, NombrePrestacion = "Obturación unirradicular", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 13, NombrePrestacion = "Obturación multirradicular", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 14, NombrePrestacion = "Tto pulpar temporario", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 15, NombrePrestacion = "Exodoncia temporario", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 16, NombrePrestacion = "Exodoncia simple", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 17, NombrePrestacion = "Exodoncia compleja", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 18, NombrePrestacion = "Trat complicación post quirúrgica", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 19, NombrePrestacion = "Rx. Intraoral", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 20, NombrePrestacion = "Desgaste tejido dentario", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 21, NombrePrestacion = "Paso intermedio prótesis", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 22, NombrePrestacion = "Instalación de prótesis", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 23, NombrePrestacion = "Inst placa de relaja/mucop", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 24, NombrePrestacion = "Compostura / Agregado", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 25, NombrePrestacion = "Alivio de prótesis", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 26, NombrePrestacion = "Sol estudio complementario", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 27, NombrePrestacion = "Derivación/Interconsulta", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 28, NombrePrestacion = "Alta Básica", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 29, NombrePrestacion = "Consultorio Estomatología", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 30, NombrePrestacion = "Biopsia", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 31, NombrePrestacion = "Consultorio Red Cirugía", IsDeleted = false },
new TipoPrestacionOdontologia { Id = 32, NombrePrestacion = "Otro", IsDeleted = false }
        );
    }
}