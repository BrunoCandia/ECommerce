using Asp.Versioning;
using Catalog.Application;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositories;
using Common.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Serilog configuration
builder.Host.UseSerilog(Logging.ConfigureLogger);

// Register layer services
builder.Services.AddApplicationServices();

builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
    // Specify the default API version
    options.DefaultApiVersion = new ApiVersion(1, 0);

    // Advertise the API versions supported by this API
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
////builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

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

app.UseCors(x => x.AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials()
                  .WithOrigins("http://localhost:4200"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    ////app.MapOpenApi();

    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // Set Swagger UI at the app's root - http://localhost:8000/index.html
        // Sample:
        // If "launchBrowser": false in launchSettings.json then manually enter http://localhost:5073 to see the Swagger UI
        // If "launchBrowser": true in launchSettings.json then it will automatically open the browser to the URL http://localhost:5073/index.html
        options.RoutePrefix = string.Empty;

        // Set the Swagger JSON endpoint: http://localhost:8000/swagger/v1/swagger.json
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.Api v1");

        // Serve Swagger UI at /swagger: http://localhost:8000/swagger/index.html
        ////options.RoutePrefix = "swagger";

        // IMPORTANT
        // If the change is not reflected try hard refreshing the browser or clearing the browser cache. Ctrl + F5 or Ctrl + Shift + R
    });
}

app.UseAuthorization();

app.MapControllers();

try
{
    using (var scope = app.Services.CreateScope())
    {
        var catalogContext = scope.ServiceProvider.GetRequiredService<ICatalogContext>();

        await BrandContextSeed.SeedDataAsync(catalogContext.Brands);
        await TypeContextSeed.SeedDataAsync(catalogContext.Types);
        await ProductContextSeed.SeedDataAsync(catalogContext.Products);
    }
}
catch (Exception ex)
{
    ////Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
    throw;
}

app.Run();
