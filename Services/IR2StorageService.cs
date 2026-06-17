namespace CineX_API.Services;

public interface IR2StorageService
{
    /// <summary>
    /// Upload file lên Cloudflare R2 và trả về URL public
    /// </summary>
    Task<string> UploadFileAsync(IFormFile file, string prefix);
}
