using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы для поддержки контроллеров
builder.Services.AddControllers();

var app = builder.Build();

// Обработчик для корневого пути "/"
app.MapGet("/", () => "Сервер работает!");
app.MapGet("/api/create-sample/", () => HandleRequest());


// Маппинг контроллеров
app.MapControllers();   

// Запуск приложения на порту 5000
app.Run("http://localhost:5000");

static string HandleRequest()
{
    return "Hello world!";
}

public class ImageJson
{
    public double NameXPos;
    public double NameYPos;
    public double ScaleFactor;
    public int FontSize;

}

