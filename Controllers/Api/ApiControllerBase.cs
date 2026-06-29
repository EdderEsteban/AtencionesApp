 using System.Security.Claims;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;

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
  }