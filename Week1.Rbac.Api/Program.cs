using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using Week1.Rbac.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Controller
builder.Services.AddControllers();

// Kết nối MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString =
        builder.Configuration.GetConnectionString("Default");

    options.UseMySQL(connectionString!);
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Bật Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Map Controller
app.MapControllers();

app.Run();