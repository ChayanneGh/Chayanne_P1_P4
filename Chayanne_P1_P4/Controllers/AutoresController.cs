using Chayanne_P1_P4.Models;
using Chayanne_P1_P4.Service;
using Microsoft.AspNetCore.Mvc;

namespace Chayanne_P1_P4.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AutoresController(AutoresService autoresService) : ControllerBase
{
    [HttpGet]//no funciona
    public async Task<IActionResult> autoresGet()
    {
        try
        {
            var lista = await autoresService.GetAsync();
            return Ok(lista);
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpGet("{IdAutor}")]//no funciona
    public async Task<IActionResult> autoresGetId(long IdAutor)
    {
        try
        {
            var autor = await autoresService.GetIdAsync(IdAutor);
            return Ok(autor);
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpPost]//funciona
    public async Task<IActionResult> autoresPost([FromBody] Autor autorDto)
    {
        try
        {
            Autor autor = await autoresService.CreateAsync(autorDto);
            return Ok(autor);
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpPut("{IdAutor}")]//no funciona
    public async Task<IActionResult> autoresPut(long IdAutor, [FromBody] Autor autorDto)
    {
        try
        {
            if (autorDto.IdAutor != 0 && autorDto.IdAutor != IdAutor)
            {
                return NotFound();
            }
            ;
            await autoresService.UpdateAsync(autorDto);
            return Ok(new { mensaje = "El registro se a Actualizado" });
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpDelete("{IdAutor}")]//funciona
    public async Task<IActionResult> autoresDelete(long IdAutor)
    {
        try
        {
            await autoresService.DeleteAsync(IdAutor);
            return Ok(new { mensaje = "El registro se ha Eliminado" });
        }
        catch
        {
            return NotFound();
        }
    }
}