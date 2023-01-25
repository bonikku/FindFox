using API.Data;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt =>
{
  opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(CorsPolicyBuilder => CorsPolicyBuilder.AllowAnyHeader().WithOrigins("https://localhost:4200"));


app.MapControllers();

app.Run();
