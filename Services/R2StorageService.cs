using Amazon.S3;
using Amazon.S3.Transfer;

namespace CineX_API.Services;

public class R2StorageService : IR2StorageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;
    private readonly string _publicUrl;

    public R2StorageService(IConfiguration configuration)
    {
        var accessKey = Environment.GetEnvironmentVariable("R2_ACCESS_KEY") ?? configuration["R2:AccessKey"];
        var secretKey = Environment.GetEnvironmentVariable("R2_SECRET_KEY") ?? configuration["R2:SecretKey"];
        var endpointUrl = Environment.GetEnvironmentVariable("R2_ENDPOINT_URL") ?? configuration["R2:EndpointUrl"];
        _bucketName = Environment.GetEnvironmentVariable("R2_BUCKET_NAME") ?? configuration["R2:BucketName"] ?? "cinex-assets";
        _publicUrl = Environment.GetEnvironmentVariable("R2_PUBLIC_URL") ?? configuration["R2:PublicUrl"] ?? string.Empty;

        var config = new AmazonS3Config
        {
            ServiceURL = endpointUrl,
        };

        _s3Client = new AmazonS3Client(accessKey, secretKey, config);
    }

    public async Task<string> UploadFileAsync(IFormFile file, string prefix)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is empty or null");

        var fileExtension = Path.GetExtension(file.FileName);
        var fileName = $"{prefix}_{Guid.NewGuid()}{fileExtension}";

        using var newMemoryStream = new MemoryStream();
        await file.CopyToAsync(newMemoryStream);

        var uploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = newMemoryStream,
            Key = fileName,
            BucketName = _bucketName,
            ContentType = file.ContentType,
            DisablePayloadSigning = true
        };

        var fileTransferUtility = new TransferUtility(_s3Client);
        await fileTransferUtility.UploadAsync(uploadRequest);

        // Trả về URL nếu có cấu hình R2_PUBLIC_URL (ví dụ Custom Domain cho R2)
        // Nếu không, trả về key để tự build URL ở frontend
        return string.IsNullOrEmpty(_publicUrl) ? fileName : $"{_publicUrl.TrimEnd('/')}/{fileName}";
    }
}
