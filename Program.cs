using CineX_API.Data;
using CineX_API.Models;
using CineX_API.Services;
using DotNetEnv;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Scalar.AspNetCore;

// Kích hoạt tính năng hỗ trợ DateTime (Local/Unspecified) trên PostgreSQL
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// 1. Load biến môi trường từ file .env
Env.Load();

// 2. Cấu hình DbContext (PostgreSQL Neon)
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") ?? builder.Configuration.GetConnectionString("DefaultConnection");

// Xử lý chuỗi kết nối dạng URI (postgres://...) để tương thích hoàn hảo với EF Core / Npgsql
if (!string.IsNullOrEmpty(connectionString) && connectionString.StartsWith("postgres://"))
{
    var uri = new Uri(connectionString);
    var userInfo = uri.UserInfo.Split(':');
    connectionString = $"Host={uri.Host};Port={(uri.Port > 0 ? uri.Port : 5432)};Database={uri.LocalPath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SslMode=Require;Trust Server Certificate=true;";
}

builder.Services.AddDbContext<CineXDbContext>(options =>
    options.UseNpgsql(connectionString));

// 3. Đăng ký Services
builder.Services.AddScoped<IR2StorageService, R2StorageService>();

// 4. Cấu hình OData
var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<Project>("Projects");
modelBuilder.EntitySet<Act>("Acts");
modelBuilder.EntitySet<Location>("Locations");
modelBuilder.EntitySet<Character>("Characters");
modelBuilder.EntitySet<Scene>("Scenes");
var sceneCharacterBuilder = modelBuilder.EntitySet<SceneCharacter>("SceneCharacters");
sceneCharacterBuilder.EntityType.HasKey(sc => sc.SceneId);
sceneCharacterBuilder.EntityType.HasKey(sc => sc.CharacterId);
builder.Services.AddControllers()
    .AddOData(options => options
        .Select()
        .Filter()
        .OrderBy()
        .Expand()
        .Count()
        .SetMaxTop(100)
        .AddRouteComponents("odata", modelBuilder.GetEdmModel()));

// Cấu hình OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// Thêm CORS để cho phép client gọi API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        corsBuilder =>
        {
            corsBuilder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
        });
});

var app = builder.Build();

// --- TỰ ĐỘNG APPLY MIGRATIONS KHI KHỞI ĐỘNG (Quan trọng khi deploy Render) ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<CineXDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Migration Error: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
// Đã tắt if (app.Environment.IsDevelopment()) để cho phép xem tài liệu API trên Render (Production)
app.MapOpenApi();
app.MapScalarApiReference(options => {
    options.WithTitle("CineX API Documentation");
});

app.UseHttpsRedirection();

// Kích hoạt CORS
app.UseCors("AllowAll");

// Open API - Tạm thời chưa có Authentication/Authorization theo yêu cầu của user
app.UseAuthorization();

app.MapControllers();

app.Run();
