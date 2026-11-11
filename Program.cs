using Microsoft.EntityFrameworkCore;
using QuanLyThietBiMang.Data;
using QuanLyThietBiMang.Services;

var builder = WebApplication.CreateBuilder(args);

// Tránh logger Event Viewer trên Windows làm ứng dụng dừng khi ghi lỗi.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Đăng ký DbContext với SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddScoped<DeviceQueryService>();
builder.Services.AddScoped<DeviceValidator>();
builder.Services.AddScoped<DeviceCommandService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed dữ liệu mẫu tự động
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Đã xảy ra lỗi khi khởi tạo cơ sở dữ liệu.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
