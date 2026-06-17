using CineX_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CineX_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileUploadController : ControllerBase
{
    private readonly IR2StorageService _r2StorageService;

    public FileUploadController(IR2StorageService r2StorageService)
    {
        _r2StorageService = r2StorageService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file, [FromForm] string prefix = "cinex")
    {
        try
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty or not provided.");

            var url = await _r2StorageService.UploadFileAsync(file, prefix);
            return Ok(new { Url = url });
        }
        catch (Exception ex)
        {
            // Trong thực tế nên log error lại
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
