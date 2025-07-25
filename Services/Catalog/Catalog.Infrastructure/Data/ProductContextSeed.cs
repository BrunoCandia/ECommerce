using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class ProductContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<Product> productCollection)
        {
            if (!await productCollection.Find(b => true).AnyAsync())
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "products.json");

                if (!File.Exists(path))
                {
                    throw new FileNotFoundException($"Seed data file not found at path: {path}");
                }

                var productData = File.ReadAllText(path);
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(productData);

                if (products is not null && products.Any())
                {
                    await productCollection.InsertManyAsync(products);
                }
            }
        }
    }
}
