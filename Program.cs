using Microsoft.EntityFrameworkCore;
using Week1.Rbac.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Default");

    // Nếu dùng gói Pomelo.EntityFrameworkCore.MySql:
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

    // Nếu dùng gói MySQL.EntityFrameworkCore chính chủ của Oracle, hãy mở comment dòng dưới và comment dòng trên:
    // options.UseMySQL(connectionString!);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        "/swagger/v1/swagger.json",
        "Week 1 RBAC API v1");
});

app.MapControllers();

app.Run();