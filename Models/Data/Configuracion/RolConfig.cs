using AtencionesApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtencionesApp.Models.Data.Configuracion;

public class RolConfig : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {
        builder.HasData(
          new Rol { Id = 1, Nombre = "Administrador", PuedeCrear = true, PuedeEditar = true, PuedeLeer = true, PuedeEliminar = true },
          new Rol { Id = 2, Nombre = "Director", PuedeCrear = false, PuedeEditar = false, PuedeLeer = true, PuedeEliminar = false },
          new Rol { Id = 3, Nombre = "Enfermero", PuedeCrear = true, PuedeEditar = true, PuedeLeer = true, PuedeEliminar = false },
          new Rol { Id = 4, Nombre = "Odontólogo", PuedeCrear = true, PuedeEditar = true, PuedeLeer = true, PuedeEliminar = false }

        );
    }
}
