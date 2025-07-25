using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class TypeContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<ProductType> typeCollection)
        {
            if (!await typeCollection.Find(b => true).AnyAsync())
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "types.json");

                if (!File.Exists(path))
                {
                    throw new FileNotFoundException($"Seed data file not found at path: {path}");
                }

                var typesData = File.ReadAllText(path);
                var types = JsonSerializer.Deserialize<IEnumerable<ProductType>>(typesData);

                if (types is not null && types.Any())
                {
                    await typeCollection.InsertManyAsync(types);
                }
            }
        }
    }
}
