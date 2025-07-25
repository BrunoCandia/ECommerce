using Catalog.Application;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register layer services
builder.Services.AddApplicationServices();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Catalog.Api",
        Description = "An ASP.NET Core Web API for managing Catalog items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});

builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));

builder.Services.AddScoped<ICatalogContext, CatalogContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBrandRepository, ProductRepository>();
builder.Services.AddScoped<ITypeRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // Set Swagger UI at the app's root
        // Sample:
        // If "launchBrowser": false in launchSettings.json then manually enter http://localhost:5073 to see the Swagger UI
        // If "launchBrowser": true in launchSettings.json then it will automatically open the browser to the URL http://localhost:5073/index.html
        options.RoutePrefix = string.Empty;
    });
}

app.UseAuthorization();

app.MapControllers();

try
{
    using (var scope = app.Services.CreateScope())
    {
        var catalogContext = scope.ServiceProvider.GetRequiredService<CatalogContext>();

        await BrandContextSeed.SeedDataAsync(catalogContext.Brands);
        await TypeContextSeed.SeedDataAsync(catalogContext.Types);
        await ProductContextSeed.SeedDataAsync(catalogContext.Products);
    }
}
catch (Exception ex)
{
    Console.WriteLine("An error occurred while seeding the database: {message}", ex.Message);
    throw;
}

app.Run();
