using AtencionesApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtencionesApp.Models.Data.Configuracion;

public class OdontogramaEstadoConfig : IEntityTypeConfiguration<OdontogramaEstado>
{
    public void Configure(EntityTypeBuilder<OdontogramaEstado> builder)
    {
        builder.Property(o => o.Superficie)
            .HasMaxLength(1)
            .IsRequired();

        builder.HasIndex(o => o.AtencionOdontologiaId);

        builder.HasQueryFilter(o => !o.IsDeleted);
    }
}