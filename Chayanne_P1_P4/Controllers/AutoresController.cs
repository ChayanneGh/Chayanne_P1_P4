using Chayanne_P1_P4.Models;
using Chayanne_P1_P4.Service;
using Microsoft.AspNetCore.Mvc;

namespace Chayanne_P1_P4.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AutoresController(AutoresService autoresService) : ControllerBase
{
    // DTO de entrada para evitar problemas con el ID autoincrementable en POST/PUT
    public record AutorRequest(string Nombres, string Nacionalidad, string FechaNacimiento, double Sueldo);

    [HttpGet]
    public async Task<ActionResult<List<Autor>>> autoresGet()
    {
        var lista = await autoresService.GetAsync();
        return Ok(lista);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Autor>> autoresGetId(int id)
    {
        var autor = await autoresService.GetIdAsync(id);
        if (autor is null) return NotFound();
        return Ok(autor);
    }

    [HttpPost]
    public async Task<ActionResult<Autor>> autoresPost([FromBody] AutorRequest request)
    {
        // Mapeamos el request al modelo original Autor (con IdAutor en 0 para que la BD lo autogenere)
        var autorNuevo = new Autor(0, request.Nombres, request.Nacionalidad, request.FechaNacimiento, request.Sueldo);

        var creado = await autoresService.CreateAsync(autorNuevo);
        return CreatedAtAction(nameof(autoresGetId), new { id = creado.IdAutor }, creado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> autoresPut(int id, [FromBody] AutorRequest request)
    {
        var autorActualizar = new Autor(id, request.Nombres, request.Nacionalidad, request.FechaNacimiento, request.Sueldo);

        var actualizado = await autoresService.UpdateAsync(id, autorActualizar);
        if (!actualizado) return NotFound(new { message = "No se pudo actualizar" });

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> autoresDelete(int id)
    {
        var eliminado = await autoresService.DeleteAsync(id);
        if (!eliminado) return NotFound(new { message = "No se pudo eliminar" });
        return NoContent();
    }
}