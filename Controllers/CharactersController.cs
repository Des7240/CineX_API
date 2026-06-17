using CineX_API.Data;
using CineX_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace CineX_API.Controllers;

public class CharactersController : ODataController
{
    private readonly CineXDbContext _context;

    public CharactersController(CineXDbContext context)
    {
        _context = context;
    }

    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(_context.Characters);
    }

    [EnableQuery]
    public IActionResult Get([FromODataUri] int key)
    {
        var character = _context.Characters.FirstOrDefault(c => c.Id == key);
        if (character == null) return NotFound();
        return Ok(character);
    }

    public async Task<IActionResult> Post([FromBody] Character character)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        _context.Characters.Add(character);
        await _context.SaveChangesAsync();
        
        return Created(character);
    }

    public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<Character> delta)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var character = await _context.Characters.FindAsync(key);
        if (character == null) return NotFound();
        
        delta.Patch(character);
        await _context.SaveChangesAsync();
        
        return Updated(character);
    }

    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        var character = await _context.Characters.FindAsync(key);
        if (character == null) return NotFound();
        
        _context.Characters.Remove(character);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}
