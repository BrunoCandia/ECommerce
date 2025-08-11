using Asp.Versioning;
using EventBus.Messages.Common;
using MassTransit;
using Microsoft.OpenApi.Models;
using Order.API.EventBusConsumer;
using Order.Application;
using Order.Infrastructure;
using Order.Infrastructure.Data;
using Order.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register layer services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// Consumer
builder.Services.AddScoped<BasketOrderConsumer>();

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
        Title = "Order.Api",
        Description = "An ASP.NET Core Web API for managing Order items",
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

// Register MassTransit and RabbitMQ
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<BasketOrderConsumer>();
    config.UsingRabbitMq((ct, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, e =>
        {
            e.ConfigureConsumer<BasketOrderConsumer>(ct);
        });
    });
});

////builder.Services.AddMassTransitHostedService();

var app = builder.Build();

//Apply db migration
app.MigrateDatabase<OrderContext>((context, services) =>
{
    var logger = services.GetRequiredService<ILogger<OrderContextSeed>>();

    OrderContextSeed.SeedAsync(context, logger).Wait();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    ////app.MapOpenApi();

    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = string.Empty;

        // Set the Swagger JSON endpoint: http://localhost:8000/swagger/v1/swagger.json
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Order.Api v1");

        // Serve Swagger UI at /swagger: http://localhost:8000/swagger/index.html
        ////options.RoutePrefix = "swagger";

        // IMPORTANT
        // If the change is not reflected try hard refreshing the browser or clearing the browser cache. Ctrl + F5 or Ctrl + Shift + R
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
