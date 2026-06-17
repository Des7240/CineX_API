using CineX_API.Data;
using CineX_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace CineX_API.Controllers;

public class ProjectsController : ODataController
{
    private readonly CineXDbContext _context;

    public ProjectsController(CineXDbContext context)
    {
        _context = context;
    }

    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(_context.Projects);
    }

    [EnableQuery]
    public IActionResult Get([FromODataUri] int key)
    {
        var project = _context.Projects.FirstOrDefault(p => p.Id == key);
        if (project == null) return NotFound();
        return Ok(project);
    }

    public async Task<IActionResult> Post([FromBody] Project project)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        
        return Created(project);
    }

    public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<Project> delta)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var project = await _context.Projects.FindAsync(key);
        if (project == null) return NotFound();
        
        delta.Patch(project);
        await _context.SaveChangesAsync();
        
        return Updated(project);
    }

    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        var project = await _context.Projects.FindAsync(key);
        if (project == null) return NotFound();
        
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}
