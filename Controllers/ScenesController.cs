using CineX_API.Data;
using CineX_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace CineX_API.Controllers;

public class ScenesController : ODataController
{
    private readonly CineXDbContext _context;

    public ScenesController(CineXDbContext context)
    {
        _context = context;
    }

    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(_context.Scenes);
    }

    [EnableQuery]
    public IActionResult Get([FromODataUri] int key)
    {
        var scene = _context.Scenes.FirstOrDefault(s => s.Id == key);
        if (scene == null) return NotFound();
        return Ok(scene);
    }

    public async Task<IActionResult> Post([FromBody] Scene scene)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        _context.Scenes.Add(scene);
        await _context.SaveChangesAsync();
        
        return Created(scene);
    }

    public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<Scene> delta)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var scene = await _context.Scenes.FindAsync(key);
        if (scene == null) return NotFound();
        
        delta.Patch(scene);
        await _context.SaveChangesAsync();
        
        return Updated(scene);
    }

    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        var scene = await _context.Scenes.FindAsync(key);
        if (scene == null) return NotFound();
        
        _context.Scenes.Remove(scene);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}
