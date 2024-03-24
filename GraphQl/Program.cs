using Autofac;
using Autofac.Extensions.DependencyInjection;
using GraphQl.Abstractions;
using GraphQl.Mapper;
using GraphQl.Query;
using GraphQl.Sevices;
using Microsoft.EntityFrameworkCore;

namespace GraphQl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMemoryCache();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            //builder.Services.AddPooledDbContextFactory<AppDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("db")));


            builder.Services.AddSingleton<IProductService, ProductService>();
            builder.Services.AddSingleton<ICategoryService, CategoryService>();
            builder.Services.AddSingleton<IStorageService, StorageService>();

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
            {
                cb.Register(c => new AppDbContext(builder.Configuration.GetConnectionString("db"))).InstancePerDependency();
            });

            builder.Services
                .AddGraphQLServer()
                .AddQueryType<MySimpleQuery>();

            builder.Services.AddSingleton<AppDbContext>();

            var app = builder.Build();

            app.MapGraphQL();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            app.Run();
        }
    }
}
