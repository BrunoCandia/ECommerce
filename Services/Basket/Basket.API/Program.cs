using Asp.Versioning;
using Basket.Application;
using Basket.Application.GrpcServices;
using Basket.Application.Messages;
using Basket.Core.Repositories;
using Basket.Infrastructure.Messages;
using Basket.Infrastructure.Repositories;
using Discount.Grpc.Protos;
using MassTransit;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register layer services
builder.Services.AddApplicationServices();

builder.Services.AddScoped<IBasketEventBusPublisher, BasketEventBusPublisher>();

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
        Title = "Basket.Api",
        Description = "An ASP.NET Core Web API for managing Basket items",
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

// Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "BasketInstance";
});

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddScoped<DiscountGrpcService>();

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(configureClient =>
{
    var discountUrl = builder.Configuration["GrpcSettings:DiscountUrl"];

    if (string.IsNullOrWhiteSpace(discountUrl))
    {
        throw new InvalidOperationException("GrpcSettings:DiscountUrl configuration value is missing or empty.");
    }

    configureClient.Address = new Uri(discountUrl);
});

// Register MassTransit and RabbitMQ
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ct, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    });
});

////builder.Services.AddMassTransitHostedService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    ////app.MapOpenApi();

    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // Set Swagger UI at the app's root - http://localhost:8001/index.html        
        options.RoutePrefix = string.Empty;

        // Set the Swagger JSON endpoint: http://localhost:8001/swagger/v1/swagger.json
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.Api v1");

        // Serve Swagger UI at /swagger: http://localhost:8001/swagger/index.html
        ////options.RoutePrefix = "swagger";

        // IMPORTANT
        // If the change is not reflected try hard refreshing the browser or clearing the browser cache. Ctrl + F5 or Ctrl + Shift + R
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
