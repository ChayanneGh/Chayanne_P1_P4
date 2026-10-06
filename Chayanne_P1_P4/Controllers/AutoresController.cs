using Chayanne_P1_P4.Service;
using Chayanne_P1_P4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chayanne_P1_P4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AutoresController(AutoresService autoresService) : ControllerBase
{
    private readonly AutoresService _autoresService = autoresService;

    [HttpPost]
    public async Task<string> autoresPost(AutorSet autor)
    {
        if (!await _autoresService.CreateAsync(autor))
        {
            throw new Exception("No se pudo crear el autor");
        }
        return "Autor creado exitosamente";
    }

    [HttpPut("{id}")]
    public async Task<string> autoresPut(int id, AutorSet autor)
    {
        if (!await _autoresService.UpdateAsync(id, autor))
        {
            throw new Exception("No se pudo actualizar el autor");
        }
        return "Autor actualizado exitosamente";
    }

    [HttpDelete("{id}")]
    public async Task<string> autoresDelete(int id)
    {
        if (!await _autoresService.DeletAsync(id))
        {
            throw new Exception("No se pudo eliminar el autor");
        }
        return "Autor eliminado exitosamente";
    }

    [HttpGet]
    public async Task<EnumerableQuery<AutorGet>> autoresGet()
    {
        var tabla = await _autoresService.GetAsync();
        if (tabla == null)
        {
            throw new Exception("No se encontraron autores");
        }
        return tabla;
    }
    
    [HttpGet("{id}")]
    public async Task<AutorGet> autoresGetId(int id)
    {
        var autor = await _autoresService.GetIdAsync(id);
        if (autor == null)
        {
            throw new Exception($"No se encontró el autor con Id {id}");
        }
        return autor;
    }
}
