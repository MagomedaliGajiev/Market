    using Autofac;
using Autofac.Extensions.DependencyInjection;
using Example.Abstractions;
using Example.Repository;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
//{
//    containerBuilder.RegisterType<ProductRepository>().As<IProductRepository>();
//});
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddMemoryCache(option =>
{
    option.TrackStatistics = true;
});

builder.Services.AddAutoMapper(typeof(MappingProfile));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


var staticFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");
Directory.CreateDirectory(staticFilesPath);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        staticFilesPath),
    RequestPath = "/static"
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
