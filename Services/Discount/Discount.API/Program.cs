using Discount.API.Services;
using Discount.Application;
using Discount.Core.Repositories;
using Discount.Infrastruture.Extensions;
using Discount.Infrastruture.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register layer services
builder.Services.AddApplicationServices();

builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

builder.Services.AddGrpc();

var app = builder.Build();

// Migrate Database
app.MigrateDatabase<Program>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

////app.UseAuthorization();

// Register gRPC service and root endpoint using top-level route registrations
app.MapGrpcService<DiscountService>();
app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Communication with grpc endpoints must be made through a grpc client");
});

////app.UseEndpoints(endpoints =>
////{
////    endpoints.MapGrpcService<DiscountService>();
////    endpoints.MapGet("/", async context =>
////    {
////        await context.Response.WriteAsync("Communication with grpc endpoints must be made through a grpc client");
////    });
////});

app.Run();
