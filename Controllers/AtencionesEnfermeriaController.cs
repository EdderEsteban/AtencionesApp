using AtencionesApp.Models.Data;
  using AtencionesApp.Models.Entities;
  using AtencionesApp.Models.ViewModels;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.EntityFrameworkCore;
  using System.Security.Claims;

  namespace AtencionesApp.Controllers;

  [Authorize]
  public class AtencionesEnfermeriaController : Controller
  {
      private readonly AppDbContext _db;

      public AtencionesEnfermeriaController(AppDbContext db)
      {
          _db = db;
      }

      // GET /AtencionesEnfermeria
      public async Task<IActionResult> Index(string? q, DateTime? fecha)
      {
          var query = _db.AtencionesEnfermeria
              .Include(a => a.Paciente)
              .Include(a => a.Usuario)
              .AsQueryable();

          var rol = User.FindFirstValue(ClaimTypes.Role);
          var instId = HttpContext.Session.GetInt32("InstitucionActivaId");
          if (rol == "Enfermero")
          {
              var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
              query = query.Where(a => a.UsuarioId == userId);
              if (instId.HasValue)
                  query = query.Where(a => a.InstitucionId == instId);
          }
          else if (rol == "Administrador" || rol == "Director")
          {
              if (instId.HasValue)
                  query = query.Where(a => a.InstitucionId == instId);
          }
          else
          {
              return Forbid();
          }

          if (!string.IsNullOrWhiteSpace(q))
              query = query.Where(a =>
                  a.Paciente.Apellido.Contains(q) ||
                  a.Paciente.Nombre.Contains(q) ||
                  a.Paciente.DNI.Contains(q));

          if (fecha.HasValue)
              query = query.Where(a => a.Fecha.Date == fecha.Value.Date);

          var atenciones = await query
              .OrderByDescending(a => a.Fecha)
              .Take(100)
              .ToListAsync();

          ViewBag.Q = q;
          ViewBag.Fecha = fecha?.ToString("yyyy-MM-dd");
          return View(atenciones);
      }

      // GET /AtencionesEnfermeria/Create?pacienteId=X
      [Authorize(Roles = "Administrador,Enfermero")]
      public async Task<IActionResult> Create(int? pacienteId)
      {
          if (!pacienteId.HasValue)
              return RedirectToAction("Index", "Pacientes");

          var paciente = await _db.Pacientes.FindAsync(pacienteId.Value);
          if (paciente == null) return NotFound();

          var vm = new AtencionEnfermeriaFormViewModel
          {
              Fecha = DateTime.Today,
              PacienteId = paciente.Id,
              PacienteNombre = $"{paciente.Apellido}, {paciente.Nombre}",
              PacienteFechaNacimiento = paciente.FechaNacimiento.ToString("yyyy-MM-dd"),
              PacienteSexo = paciente.Sexo,
              PacienteTieneObraSocial = !string.IsNullOrEmpty(paciente.ObraSocial),
              PacienteObraSocial = paciente.ObraSocial,
              SinObraSocial = string.IsNullOrEmpty(paciente.ObraSocial)
          };

          ViewBag.Tipos = await _db.TiposPrestacionEnfermeria
              .OrderBy(t => t.Grupo).ThenBy(t => t.NombrePrestacion)
              .ToListAsync();
          ViewBag.PacienteSubtitulo = $"{paciente.Apellido}, {paciente.Nombre} · DNI {paciente.DNI}";
          return View(vm);
      }

      // POST /AtencionesEnfermeria/Create
      [HttpPost, ValidateAntiForgeryToken]
      [Authorize(Roles = "Administrador,Enfermero")]
      public async Task<IActionResult> Create(AtencionEnfermeriaFormViewModel vm)
      {
          if (vm.Prestaciones == null || !vm.Prestaciones.Any())
              ModelState.AddModelError("Prestaciones", "Agregá al menos una prestación");

          if (!ModelState.IsValid)
          {
              ViewBag.Tipos = await _db.TiposPrestacionEnfermeria
                  .OrderBy(t => t.Grupo).ThenBy(t => t.NombrePrestacion)
                  .ToListAsync();
              return View(vm);
          }

          var institucionId = HttpContext.Session.GetInt32("InstitucionActivaId");
          if (institucionId == null)
              return RedirectToAction("SeleccionarInstitucion", "Account");

          var paciente = await _db.Pacientes.FindAsync(vm.PacienteId);
          var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

          var ahora = DateTime.Now;
          var edad = ahora.Year - paciente!.FechaNacimiento.Year;
          if (paciente.FechaNacimiento.DayOfYear > ahora.DayOfYear) edad--;

          var atencion = new AtencionEnfermeria
          {
              Fecha = ahora,
              PacienteId = vm.PacienteId,
              InstitucionId = institucionId.Value,
              UsuarioId = userId,
              TipoAtencion = vm.TipoAtencion,
              Edad = edad,
              Embarazada = vm.Embarazada,
              SinObraSocial = vm.SinObraSocial,
              Observaciones = string.IsNullOrWhiteSpace(vm.Observaciones) ? null : vm.Observaciones.Trim(),
              Prestaciones = vm.Prestaciones.Select(p => new PrestacionEnfermeria
              {
                  TipoPrestacionId = p.TipoPrestacionId,
                  Cantidad = p.Cantidad
              }).ToList()
          };

          if (!vm.SinObraSocial && !string.IsNullOrWhiteSpace(vm.NuevaObraSocial))
              paciente!.ObraSocial = vm.NuevaObraSocial.Trim();
          else if (!vm.SinObraSocial && string.IsNullOrEmpty(paciente!.ObraSocial))
              atencion.SinObraSocial = true;

          _db.AtencionesEnfermeria.Add(atencion);
          await _db.SaveChangesAsync();

          TempData["Exito"] = "Atención registrada correctamente";
          return RedirectToAction(nameof(Index));
      }

      // GET /AtencionesEnfermeria/Edit/5
      [Authorize(Roles = "Administrador,Enfermero")]
      public async Task<IActionResult> Edit(int id)
      {
          var atencion = await _db.AtencionesEnfermeria
              .Include(a => a.Prestaciones)
              .Include(a => a.Paciente)
              .FirstOrDefaultAsync(a => a.Id == id);

          if (atencion == null) return NotFound();

          var rol = User.FindFirstValue(ClaimTypes.Role);
          var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
          if (rol == "Enfermero" && atencion.UsuarioId != userId)
              return Forbid();

          if (rol != "Administrador" && atencion.Fecha.Date != DateTime.Today)
          {
              TempData["Error"] = "Solo se pueden editar atenciones del día. Para modificar cargas de fechas anteriores, contactá a un administrador.";
              return RedirectToAction(nameof(Details), new { id });
          }

          var vm = new AtencionEnfermeriaFormViewModel
          {
              Id = atencion.Id,
              Fecha = atencion.Fecha,
              PacienteId = atencion.PacienteId,
              PacienteNombre = $"{atencion.Paciente.Apellido}, {atencion.Paciente.Nombre}",
              TipoAtencion = atencion.TipoAtencion,
              Embarazada = atencion.Embarazada,
              SinObraSocial = atencion.SinObraSocial,
              Observaciones = atencion.Observaciones,
              PacienteSexo = atencion.Paciente.Sexo,
              PacienteTieneObraSocial = !string.IsNullOrEmpty(atencion.Paciente.ObraSocial),
              PacienteObraSocial = atencion.Paciente.ObraSocial,
              Edad = atencion.Edad,
              PacienteFechaNacimiento = atencion.Paciente.FechaNacimiento.ToString("yyyy-MM-dd"),
              Prestaciones = atencion.Prestaciones.Select(p => new PrestacionSeleccionadaVM
              {
                  TipoPrestacionId = p.TipoPrestacionId,
                  Cantidad = p.Cantidad
              }).ToList()
          };

          ViewBag.Tipos = await _db.TiposPrestacionEnfermeria
              .OrderBy(t => t.Grupo).ThenBy(t => t.NombrePrestacion)
              .ToListAsync();
          return View(vm);
      }

      // POST /AtencionesEnfermeria/Edit/5
      [HttpPost, ValidateAntiForgeryToken]
      [Authorize(Roles = "Administrador,Enfermero")]
      public async Task<IActionResult> Edit(int id, AtencionEnfermeriaFormViewModel vm)
      {
          if (vm.Prestaciones == null || !vm.Prestaciones.Any())
              ModelState.AddModelError("Prestaciones", "Agregá al menos una prestación");

          if (!ModelState.IsValid)
          {
              ViewBag.Tipos = await _db.TiposPrestacionEnfermeria
                  .OrderBy(t => t.Grupo).ThenBy(t => t.NombrePrestacion)
                  .ToListAsync();
              return View(vm);
          }

          var atencion = await _db.AtencionesEnfermeria
              .Include(a => a.Prestaciones)
              .FirstOrDefaultAsync(a => a.Id == id);

          if (atencion == null) return NotFound();

          var rol = User.FindFirstValue(ClaimTypes.Role);
          var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
          if (rol == "Enfermero" && atencion.UsuarioId != userId)
              return Forbid();

          if (rol != "Administrador" && atencion.Fecha.Date != DateTime.Today)
          {
              TempData["Error"] = "Solo se pueden editar atenciones del día. Para modificar cargas de fechas anteriores, contactá a un administrador.";
              return RedirectToAction(nameof(Details), new { id });
          }

          var paciente = await _db.Pacientes.FindAsync(atencion.PacienteId);

          atencion.TipoAtencion = vm.TipoAtencion;
          atencion.Embarazada = vm.Embarazada;
          atencion.SinObraSocial = vm.SinObraSocial;
          atencion.Observaciones = string.IsNullOrWhiteSpace(vm.Observaciones) ? null : vm.Observaciones.Trim();

          if (!vm.SinObraSocial && !string.IsNullOrWhiteSpace(vm.NuevaObraSocial))
              paciente!.ObraSocial = vm.NuevaObraSocial.Trim();
          else if (!vm.SinObraSocial && string.IsNullOrEmpty(paciente?.ObraSocial))
              atencion.SinObraSocial = true;

          _db.PrestacionesEnfermeria.RemoveRange(atencion.Prestaciones);
          atencion.Prestaciones = vm.Prestaciones.Select(p => new PrestacionEnfermeria
          {
              TipoPrestacionId = p.TipoPrestacionId,
              Cantidad = p.Cantidad
          }).ToList();

          await _db.SaveChangesAsync();
          TempData["Exito"] = "Atención actualizada correctamente";
          return RedirectToAction(nameof(Index));
      }

      // GET /AtencionesEnfermeria/Details/5
      public async Task<IActionResult> Details(int id)
      {
          var atencion = await _db.AtencionesEnfermeria
              .Include(a => a.Paciente)
              .Include(a => a.Usuario)
              .Include(a => a.Prestaciones).ThenInclude(p => p.TipoPrestacion)
              .FirstOrDefaultAsync(a => a.Id == id);

          if (atencion == null) return NotFound();
          return View(atencion);
      }

      // POST /AtencionesEnfermeria/Delete/5
      [HttpPost, ValidateAntiForgeryToken]
      [Authorize(Roles = "Administrador")]
      public async Task<IActionResult> Delete(int id)
      {
          var atencion = await _db.AtencionesEnfermeria.FindAsync(id);
          if (atencion == null) return NotFound();
          atencion.IsDeleted = true;
          await _db.SaveChangesAsync();
          TempData["Exito"] = "Atención eliminada";
          return RedirectToAction(nameof(Index));
      }
  }