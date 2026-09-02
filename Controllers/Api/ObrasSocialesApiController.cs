using AtencionesApp.Models.Data;
using AtencionesApp.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtencionesApp.Controllers.Api;

[Route("api/obras-sociales")]
public class ObrasSocialesApiController : ApiControllerBase
{
    private readonly AppDbContext _context;

    public ObrasSocialesApiController(AppDbContext context)
    {
        _context = context;
    }

    // GET /api/obras-sociales
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var obras = await _context.ObrasSociales
            .Where(o => !o.IsDeleted)
            .OrderBy(o => o.Nombre)
            .Select(o => new ObraSocialDto { Id = o.Id, Nombre = o.Nombre })
            .ToListAsync();
        return Ok(obras);
    }
}
