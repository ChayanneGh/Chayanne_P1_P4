using Chayanne_P1_P4.Service;
using Chayanne_P1_P4.Models;
using Chayanne_P1_P4.Service;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;

namespace Chayanne_P1_P4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AutoresController(AutoresService autoresService) : ControllerBase
{
    private readonly AutoresService _autoresService = autoresService;

    [HttpPost]
    public async Task<string> autores(AutorSet autor)
    {
        if (!await _autoresService.CreateAsync(autor))
        {
            throw new Exception("No se pudo crear el autor");
        }
        return "Autor creado exitosamente";
    }

    [HttpPut]
    public async Task<string> autores(int id, AutorSet autor)
    {
        if (!await _autoresService.UpdateAsync(id, autor))
        {
            throw new Exception("No se pudo actualizar el autor");
        }
        return "Autor actualizado exitosamente";
    }

    [HttpDelete]

    [HttpGet]
    public async Task<EnumerableQuery<AutorGet>> autores()
    {
        var tabla = await _autoresService.GetAsync();
        if (tabla == null)
        {
            throw new Exception("No se encontraron autores");
        }
        return tabla;
    }
    
    [HttpGet("{id}")]
    public async Task<AutorGet> autores(int id)
    {
        var autor = await _autoresService.GetIdAsync(id);
        if (autor == null)
        {
            throw new Exception($"No se encontró el autor con Id {id}");
        }
        return autor;
    }
}
