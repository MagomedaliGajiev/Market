using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace MyApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ��������� Ocelot � �������
            builder.Services.AddOcelot();

            var app = builder.Build();

            // ��������� ������������� � �������������� Ocelot
            app.UseOcelot().Wait();

            app.Run();
        }
    }
}