using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ImageGenerator.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var connString = "Host=localhost;Port=5432;Database=ImageDB;Username=postgres;Password=1122";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connString));

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Сервер работает!");
app.MapGet("/api/create-sample/", () => HandleRequest());

app.MapControllers();   

app.Run("http://localhost:5000");

static string HandleRequest()
{
    return "Hello world!";
}



