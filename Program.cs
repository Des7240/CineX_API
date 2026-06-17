using CineX_API.Data;
using CineX_API.Models;
using CineX_API.Services;
using DotNetEnv;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Load biến môi trường từ file .env
Env.Load();

// 2. Cấu hình DbContext (PostgreSQL Neon)
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") ?? builder.Configuration.GetConnectionString("DefaultConnection");
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
// Không cần EntitySet cho bảng trung gian SceneCharacter nếu không truy vấn trực tiếp

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => {
        options.WithTitle("CineX API Documentation");
    });
}

app.UseHttpsRedirection();

// Open API - Tạm thời chưa có Authentication/Authorization theo yêu cầu của user
app.UseAuthorization();

app.MapControllers();

app.Run();
