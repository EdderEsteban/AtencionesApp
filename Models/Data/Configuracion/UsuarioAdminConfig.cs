using AtencionesApp.Models.Entities;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  namespace AtencionesApp.Models.Data.Configuracion;

  public class UsuarioAdminConfig : IEntityTypeConfiguration<Usuario>
  {
      public void Configure(EntityTypeBuilder<Usuario> builder)
      {
          var hasher = new PasswordHasher<Usuario>();
          var admin = new Usuario
          {
              Id = 1,
              Nombre = "Admin",
              Apellido = "Sistema",
              Email = "admin@salud.com",
              Telefono = null,
              RolId = 1,
              IsDeleted = false
          };
          admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");

          builder.HasData(admin);
      }
  }