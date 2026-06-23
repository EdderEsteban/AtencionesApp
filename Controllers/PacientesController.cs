using System.Security.Claims;
  using AtencionesApp.Models.Data;
  using AtencionesApp.Models.Entities;
  using AtencionesApp.Models.ViewModels;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.EntityFrameworkCore;

  namespace AtencionesApp.Controllers;

  [Authorize]
  public class PacientesController : Controller
  {
      private readonly AppDbContext _context;

      public PacientesController(AppDbContext context)
      {
          _context = context;
      }

      // GET: /Pacientes?q=...
      public async Task<IActionResult> Index(string? q)
      {
          ViewBag.Busqueda = q;

          var query = _context.Pacientes.AsQueryable();

          if (!string.IsNullOrWhiteSpace(q))
          {
              query = query.Where(p =>
                  p.DNI.Contains(q) ||
                  p.Nombre.Contains(q) ||
                  p.Apellido.Contains(q));
          }
          else
          {
              query = query.Where(p => false);
          }

          var pacientes = await query
              .OrderBy(p => p.Apellido)
              .ThenBy(p => p.Nombre)
              .Take(50)
              .ToListAsync();

          return View(pacientes);
      }

      // GET: /Pacientes/Create
      [Authorize(Roles = "Administrador,Enfermero,Odontólogo")]
      public IActionResult Create()
      {
          return View(new PacienteFormViewModel());
      }

      // POST: /Pacientes/Create
      [HttpPost]
      [ValidateAntiForgeryToken]
      [Authorize(Roles = "Administrador,Enfermero,Odontólogo")]
      public async Task<IActionResult> Create(PacienteFormViewModel vm)
      {
          if (!ModelState.IsValid)
              return View(vm);

          var dniExiste = await _context.Pacientes.AnyAsync(p => p.DNI == vm.DNI);
          if (dniExiste)
          {
              ModelState.AddModelError("DNI", "Ya existe un paciente con ese DNI");
              return View(vm);
          }

          var paciente = new Paciente
          {
              Apellido = vm.Apellido.Trim(),
              Nombre = vm.Nombre.Trim(),
              DNI = vm.DNI.Trim(),
              FechaNacimiento = vm.FechaNacimiento,
              Sexo = vm.Sexo,
              Domicilio = vm.Domicilio?.Trim(),
              Telefono = vm.Telefono?.Trim(),
              ObraSocial = vm.ObraSocial?.Trim()
          };

          _context.Pacientes.Add(paciente);
          await _context.SaveChangesAsync();

          TempData["Exito"] = $"Paciente {paciente.Apellido}, {paciente.Nombre} creado correctamente.";
          return RedirectToAction(nameof(Index));
      }

      // GET: /Pacientes/Edit/5
      [Authorize(Roles = "Administrador,Enfermero,Odontólogo")]
      public async Task<IActionResult> Edit(int id)
      {
          var paciente = await _context.Pacientes.FindAsync(id);
          if (paciente == null)
              return NotFound();

          var vm = new PacienteFormViewModel
          {
              Id = paciente.Id,
              Apellido = paciente.Apellido,
              Nombre = paciente.Nombre,
              DNI = paciente.DNI,
              FechaNacimiento = paciente.FechaNacimiento,
              Sexo = paciente.Sexo,
              Domicilio = paciente.Domicilio,
              Telefono = paciente.Telefono,
              ObraSocial = paciente.ObraSocial
          };

          return View(vm);
      }

      // POST: /Pacientes/Edit/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      [Authorize(Roles = "Administrador,Enfermero,Odontólogo")]
      public async Task<IActionResult> Edit(int id, PacienteFormViewModel vm)
      {
          if (!ModelState.IsValid)
              return View(vm);

          var dniExiste = await _context.Pacientes
              .AnyAsync(p => p.DNI == vm.DNI && p.Id != id);
          if (dniExiste)
          {
              ModelState.AddModelError("DNI", "Ya existe otro paciente con ese DNI");
              return View(vm);
          }

          var paciente = await _context.Pacientes.FindAsync(id);
          if (paciente == null)
              return NotFound();

          paciente.Apellido = vm.Apellido.Trim();
          paciente.Nombre = vm.Nombre.Trim();
          paciente.DNI = vm.DNI.Trim();
          paciente.FechaNacimiento = vm.FechaNacimiento;
          paciente.Sexo = vm.Sexo;
          paciente.Domicilio = vm.Domicilio?.Trim();
          paciente.Telefono = vm.Telefono?.Trim();
          paciente.ObraSocial = vm.ObraSocial?.Trim();

          await _context.SaveChangesAsync();

          TempData["Exito"] = $"Paciente {paciente.Apellido}, {paciente.Nombre} actualizado.";
          return RedirectToAction(nameof(Index));
      }

      // GET: /Pacientes/Details/5
      public async Task<IActionResult> Details(int id)
      {
          var paciente = await _context.Pacientes
              .Include(p => p.AtencionesEnfermeria)
                  .ThenInclude(a => a.Prestaciones)
                      .ThenInclude(pr => pr.TipoPrestacion)
              .Include(p => p.AtencionesOdontologia)
                  .ThenInclude(a => a.Prestaciones)
                      .ThenInclude(pr => pr.TipoPrestacion)
              .Include(p => p.AtencionesOdontologia)
                  .ThenInclude(a => a.ValoracionDental)
              .FirstOrDefaultAsync(p => p.Id == id);

          if (paciente == null)
              return NotFound();

          return View(paciente);
      }

      // POST: /Pacientes/Delete/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      [Authorize(Roles = "Administrador")]
      public async Task<IActionResult> Delete(int id)
      {
          var paciente = await _context.Pacientes.FindAsync(id);
          if (paciente == null)
              return NotFound();

          paciente.IsDeleted = true;
          await _context.SaveChangesAsync();

          TempData["Exito"] = "Paciente eliminado.";
          return RedirectToAction(nameof(Index));
      }
  }