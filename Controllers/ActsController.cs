using CineX_API.Data;
using CineX_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace CineX_API.Controllers;

public class ActsController : ODataController
{
    private readonly CineXDbContext _context;

    public ActsController(CineXDbContext context)
    {
        _context = context;
    }

    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(_context.Acts);
    }

    [EnableQuery]
    public IActionResult Get([FromODataUri] int key)
    {
        var act = _context.Acts.FirstOrDefault(a => a.Id == key);
        if (act == null) return NotFound();
        return Ok(act);
    }

    public async Task<IActionResult> Post([FromBody] Act act)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        _context.Acts.Add(act);
        await _context.SaveChangesAsync();
        
        return Created(act);
    }

    public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<Act> delta)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var act = await _context.Acts.FindAsync(key);
        if (act == null) return NotFound();
        
        delta.Patch(act);
        await _context.SaveChangesAsync();
        
        return Updated(act);
    }

    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        var act = await _context.Acts.FindAsync(key);
        if (act == null) return NotFound();
        
        _context.Acts.Remove(act);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}
