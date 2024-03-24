namespace GraphQl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddGraphQLServer()
                .AddQueryType();

            var app = builder.Build();


            app.Run();
        }
    }
}
