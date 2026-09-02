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