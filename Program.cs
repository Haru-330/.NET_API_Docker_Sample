using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration; // Use the Configuration property of the builder object

builder.Services.AddDbContext<BookContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
// CORSエラーの回避のために追加
// 5173はViteのデフォルトポートなので、他のポートを使用している場合は適宜変更してください。
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                    builder => builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins("http://localhost:5173")
                );
});
var app = builder.Build();

// Apply pending migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<BookContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting(); // 追加

app.MapControllers(); //追加

app.UseCors(); // CORSを有効にするために追加
app.Run();