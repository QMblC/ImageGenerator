﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ImageGenerator.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var connString = "Host=localhost;Port=5432;Database=ImageDB;Username=postgres;Password=1122";

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connString));

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Сервер работает!");
app.UseCors("AllowAll");

app.MapControllers();   

app.Run("http://0.0.0.0:5000");



