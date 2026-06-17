using CineX_API.Data;
using CineX_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace CineX_API.Controllers;

public class LocationsController : ODataController
{
    private readonly CineXDbContext _context;

    public LocationsController(CineXDbContext context)
    {
        _context = context;
    }

    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(_context.Locations);
    }

    [EnableQuery]
    public IActionResult Get([FromODataUri] int key)
    {
        var location = _context.Locations.FirstOrDefault(l => l.Id == key);
        if (location == null) return NotFound();
        return Ok(location);
    }

    public async Task<IActionResult> Post([FromBody] Location location)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        _context.Locations.Add(location);
        await _context.SaveChangesAsync();
        
        return Created(location);
    }

    public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<Location> delta)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var location = await _context.Locations.FindAsync(key);
        if (location == null) return NotFound();
        
        delta.Patch(location);
        await _context.SaveChangesAsync();
        
        return Updated(location);
    }

    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        var location = await _context.Locations.FindAsync(key);
        if (location == null) return NotFound();
        
        _context.Locations.Remove(location);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}
