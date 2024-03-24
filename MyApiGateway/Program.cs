using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace MyApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Добавляем Ocelot в сервисы
            builder.Services.AddOcelot();

            var app = builder.Build();

            // Настройка маршрутизации с использованием Ocelot
            app.UseOcelot().Wait();

            app.Run();
        }
    }
}