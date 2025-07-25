using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class BrandContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<ProductBrand> brandCollection)
        {
            if (!await brandCollection.Find(b => true).AnyAsync())
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "brands.json");

                if (!File.Exists(path))
                {
                    throw new FileNotFoundException($"Seed data file not found at path: {path}");
                }

                var brandsData = File.ReadAllText(path);
                var brands = JsonSerializer.Deserialize<IEnumerable<ProductBrand>>(brandsData);

                if (brands is not null && brands.Any())
                {
                    await brandCollection.InsertManyAsync(brands);
                }
            }
        }
    }
}
