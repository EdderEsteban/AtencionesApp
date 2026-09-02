using AtencionesApp.Models.Entities;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  namespace AtencionesApp.Models.Data.Configuracion;

  public class UsuarioAdminConfig : IEntityTypeConfiguration<Usuario>
  {
      public void Configure(EntityTypeBuilder<Usuario> builder)
      {
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
          // El hash se fija como literal a propósito. PasswordHasher usa un salt
          // aleatorio, así que calcularlo acá devolvía un valor distinto en cada
          // construcción del modelo y Entity Framework lo tomaba como dato sembrado
          // modificado: cada migración emitía un UpdateData que le restauraba la
          // contraseña al administrador. Corresponde a la contraseña inicial, que
          // debe cambiarse desde la aplicación en el primer uso.
          admin.PasswordHash = "AQAAAAIAAYagAAAAEIMT/6Yh19Xgm7OpMNQLydTmQw5WfKEx59Lgd2RRgg3i+ot14yAgDjpyD2gib/gkRA==";

          builder.HasData(admin);
      }
  }