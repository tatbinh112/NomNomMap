using DotNetEnv;
using NomNomMap.Api.Data;
using Microsoft.EntityFrameworkCore;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var conn = Environment.GetEnvironmentVariable("ConnectionStrings__Default")
          ?? builder.Configuration.GetConnectionString("Default")
          ?? "Host=localhost;Port=5432;Database=foodmap;Username=postgres;Password=postgres";

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(conn, o => o.UseNetTopologySuite()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("dev", p => p
        .WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("dev");
app.MapControllers();

app.Run();
