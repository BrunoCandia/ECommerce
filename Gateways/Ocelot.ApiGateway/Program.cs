using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("ocelot.Local.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.

// CORS from configuration
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(allowedOrigins ?? Array.Empty<string>())
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = jwtSettings["Key"];
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];

//Add JWT auth to ocelot
////builder.Services.AddAuthentication("Bearer")
////    .AddJwtBearer("Bearer", options =>
////    {
////        options.TokenValidationParameters = new TokenValidationParameters
////        {
////            ValidateIssuer = true,
////            ValidateAudience = true,
////            ValidateLifetime = true,
////            ValidateIssuerSigningKey = true,
////            ValidIssuer = issuer,
////            ValidAudience = audience,
////            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
////        };
////    });

////builder.Services.AddAuthorization();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
////builder.Services.AddOpenApi();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ApiGateway",
        Description = "An ASP.NET Core Web API for managing Ocelot Gateway",
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

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();

    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // Set Swagger UI at the app's root - http://localhost:8010/index.html        
        options.RoutePrefix = string.Empty;

        // Set the Swagger JSON endpoint: http://localhost:8010/swagger/v1/swagger.json
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiGateway v1");

        // Serve Swagger UI at /swagger: http://localhost:8010/swagger/index.html
        ////options.RoutePrefix = "swagger";

        // IMPORTANT
        // If the change is not reflected try hard refreshing the browser or clearing the browser cache. Ctrl + F5 or Ctrl + Shift + R
    });
}

app.UseRouting();

// CORS must come before Ocelot
app.UseCors("AllowFrontend");

////app.UseMiddleware<CorrelationIdMiddleware>();

////app.UseAuthentication();

////app.UseAuthorization();

app.MapControllers();

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Welcome to the Ocelot API Gateway!");
});

////app.UseEndpoints(endpoints =>
////{
////    endpoints.MapGet("/", async context =>
////    {
////        await context.Response.WriteAsync("Welcome to the Ocelot API Gateway!");
////    });
////});

// With the following code to properly await the UseOcelot() call:
await app.UseOcelot();

await app.RunAsync();
