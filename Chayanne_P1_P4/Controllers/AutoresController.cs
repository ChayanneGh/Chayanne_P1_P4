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
    public async Task<List<Autor>> autoresGet()
    {
        return await autoresService.GetAsync();
    }

    [HttpGet("{id}")]
    public async Task<Autor?> autoresGetId(int id)
    {
        return await autoresService.GetIdAsync(id);
    }

    [HttpPost]
    public async Task<Autor> autoresPost([FromBody] Autor autor)
    {
        await autoresService.CreateAsync(autor);
        return autor;
    }

    [HttpPut("{id}")]
    public async Task<bool> autoresPut(int id, [FromBody] Autor autor)
    {
        return await autoresService.UpdateAsync(id, autor);
    }

    [HttpDelete("{id}")]
    public async Task<bool> autoresDelete(int id)
    {
        return await autoresService.DeletAsync(id);
    }
}
