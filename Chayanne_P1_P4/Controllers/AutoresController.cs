using Chayanne_P1_P4.Models;
using Chayanne_P1_P4.Service;
using Microsoft.AspNetCore.Mvc;

namespace Chayanne_P1_P4.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AutoresController(AutoresService autoresService) : ControllerBase
{

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
    public async Task<ActionResult<Autor>> autoresPost([FromBody] Autor autor)
    {
        var creado = await autoresService.CreateAsync(autor);
        return CreatedAtAction(nameof(autoresGetId), new { id = creado.IdAutor }, creado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> autoresPut(int id, [FromBody] Autor autor)
    {
        var actualizado = await autoresService.UpdateAsync(id, autor);
        if (!actualizado) return NotFound(new { message = "No se pudo actualizar" });
        // NoContent es correcto cuando la actualización fue exitosa y no devolvemos cuerpo
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