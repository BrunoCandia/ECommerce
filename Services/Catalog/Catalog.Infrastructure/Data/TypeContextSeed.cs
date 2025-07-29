using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data
{
    public static class TypeContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<ProductType> typeCollection)
        {
            if (!await typeCollection.Find(b => true).AnyAsync())
            {
                ////var currentDir = Directory.GetCurrentDirectory();
                ////Debug.WriteLine($"[TypeContextSeed] Current directory: {currentDir}");
                ////Console.WriteLine($"[TypeContextSeed] Current directory: {currentDir}");
                ////var path = Path.Combine(currentDir, "Data", "SeedData", "types.json");

                ////Debug.WriteLine($"[TypeContextSeed] Looking for seed file at: {path}");
                ////Console.WriteLine($"[TypeContextSeed] Looking for seed file at: {path}");

                ////if (!File.Exists(path))
                ////{
                ////    Debug.WriteLine($"[TypeContextSeed] File does not exist: {path}");
                ////    Console.WriteLine($"[TypeContextSeed] File does not exist: {path}");
                ////    throw new FileNotFoundException($"Seed data file not found at path: {path}");
                ////}

                ////var typesData = File.ReadAllText(path);
                ////var types = JsonSerializer.Deserialize<IEnumerable<ProductType>>(typesData);

                var types = new List<ProductType>
                {
                    new ProductType { Id = "63ca5d4bc3a8a58f47299f97", Name = "Shoes" },
                    new ProductType { Id = "63ca5d6d958e43ee1cd375fe", Name = "Rackets" },
                    new ProductType { Id = "63ca5d7d380402dce7f06ebc", Name = "Football" },
                    new ProductType { Id = "63ca5d8849bc19321b8be5f1", Name = "Kit Bags" }
                };

                if (types is not null && types.Any())
                {
                    await typeCollection.InsertManyAsync(types);
                }
            }
        }
    }
}
