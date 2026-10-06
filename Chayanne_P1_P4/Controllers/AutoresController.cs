using Chayanne_P1_P4.Models;
using Chayanne_P1_P4.Service;
using Microsoft.AspNetCore.Mvc;

namespace Chayanne_P1_P4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AutoresController(AutoresService autoresService) : ControllerBase
{
    private readonly AutoresService _autoresService = autoresService;

    [HttpGet]
    public async Task<List<AutorGet>> autoresGet()
    {
        // Retorna la lista directamente; .NET la convierte a JSON automáticamente
        return await _autoresService.GetAsync();
    }

    [HttpGet("{id}")]
    public async Task<AutorGet?> autoresGetId(int id)
    {
        return await _autoresService.GetIdAsync(id);
    }

    [HttpPost]
    public async Task<bool> autoresPost(AutorSet autor)
    {
        return await _autoresService.CreateAsync(autor);
    }

    [HttpPut("{id}")]
    public async Task<bool> autoresPut(int id, AutorSet autor)
    {
        return await _autoresService.UpdateAsync(id, autor);
    }

    [HttpDelete("{id}")]
    public async Task<bool> autoresDelete(int id)
    {
        return await _autoresService.DeletAsync(id);
    }
}
