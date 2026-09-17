 using System.Security.Claims;
  using AtencionesApp.Models.Data;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.EntityFrameworkCore;

  namespace AtencionesApp.Controllers.Api;

  [ApiController]
  [Authorize(AuthenticationSchemes = "Bearer")]
  public abstract class ApiControllerBase : ControllerBase
  {
      protected int UsuarioId =>
          int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

      protected string Rol =>
          User.FindFirstValue(ClaimTypes.Role) ?? "";

      // institucionId del token; null si el usuario aún no eligió institución
      protected int? InstitucionId
      {
          get
          {
              var raw = User.FindFirst("institucionId")?.Value;
              return int.TryParse(raw, out var id) ? id : null;
          }
      }

      // Helper: corta con 409 si todavía no hay institución elegida
      protected IActionResult? RequiereInstitucion()
      {
          if (InstitucionId == null)
              return Conflict(new { error = "Seleccioná una institución antes de continuar." });
          return null;
      }

      // Fecha del acto asistencial para una atención que llega de la app móvil.
      // La app la captura en el teléfono y puede sincronizarla días después, así
      // que la fecha clínica es la de captura y no la de llegada al servidor: la
      // historia clínica tiene que decir cuándo se atendió al paciente, no cuándo
      // hubo internet.
      //
      // Se cae a `ahora` en tres casos: si no llegó (versiones de la app
      // anteriores a este campo), si viene del futuro —reloj del teléfono
      // adelantado— o si es tan vieja que no puede ser real. Ante un dato dudoso
      // conviene una fecha conservadora antes que una imposible.
      protected static DateTime ResolverFechaAtencion(DateTime? fechaCaptura, DateTime ahora)
      {
          if (fechaCaptura == null) return ahora;

          // Tolerancia de 5 minutos: un reloj levemente adelantado no invalida la carga.
          if (fechaCaptura.Value > ahora.AddMinutes(5)) return ahora;
          if (fechaCaptura.Value < ahora.AddYears(-1)) return ahora;

          return fechaCaptura.Value;
      }

      // Resuelve el nombre de una obra social contra el padrón, para las versiones
      // de la app móvil que todavía envían texto en lugar del identificador.
      // Devuelve null si no hay ninguna coincidencia.
      protected static async Task<int?> ResolverObraSocialPorNombre(
          AppDbContext db, string? nombre)
      {
          if (string.IsNullOrWhiteSpace(nombre)) return null;
          var buscado = nombre.Trim();

          // Coincidencia por nombre completo. La collation de la columna es
          // case-insensitive, así que la comparación no distingue mayúsculas.
          var porNombre = await db.ObrasSociales
              .Where(o => !o.IsDeleted && o.Nombre == buscado)
              .Select(o => (int?)o.Id)
              .FirstOrDefaultAsync();
          if (porNombre != null) return porNombre;

          // Coincidencia por sigla: la app suele mandar "PAMI" y el padrón lo tiene
          // como "(PAMI) INSTITUTO NACIONAL DE...".
          var conParentesis = "(" + buscado + ")";
          return await db.ObrasSociales
              .Where(o => !o.IsDeleted && o.Nombre.StartsWith(conParentesis))
              .Select(o => (int?)o.Id)
              .FirstOrDefaultAsync();
      }
  }