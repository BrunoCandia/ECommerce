using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data
{
    public static class BrandContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<ProductBrand> brandCollection)
        {
            ////// According to course
            ////var path = Path.Combine("Data", "SeedData", "brands.json");

            ////if (!File.Exists(path))
            ////{
            ////    Debug.WriteLine($"[BrandContextSeed] File does not exist: {path}");
            ////    Console.WriteLine($"[BrandContextSeed] File does not exist: {path}");
            ////    throw new FileNotFoundException($"Seed data file not found at path: {path}");
            ////}

            ////var brandsData = File.ReadAllText(path);

            if (!await brandCollection.Find(b => true).AnyAsync())
            {
                ////var currentDir = Directory.GetCurrentDirectory();
                ////Debug.WriteLine($"[BrandContextSeed] Current directory: {currentDir}");
                ////Console.WriteLine($"[BrandContextSeed] Current directory: {currentDir}");
                ////var path = Path.Combine(currentDir, "Data", "SeedData", "brands.json");

                ////Debug.WriteLine($"[BrandContextSeed] Looking for seed file at: {path}");
                ////Console.WriteLine($"[BrandContextSeed] Looking for seed file at: {path}");

                ////if (!File.Exists(path))
                ////{
                ////    Debug.WriteLine($"[BrandContextSeed] File does not exist: {path}");
                ////    Console.WriteLine($"[BrandContextSeed] File does not exist: {path}");
                ////    throw new FileNotFoundException($"Seed data file not found at path: {path}");
                ////}

                ////var brandsData = File.ReadAllText(path);
                ////var brands = JsonSerializer.Deserialize<IEnumerable<ProductBrand>>(brandsData);

                var brands = new List<ProductBrand>
                {
                    new ProductBrand { Id = "63ca5e40e0aa3968b549af53", Name = "Adidas" },
                    new ProductBrand { Id = "63ca5e4c455900b990b43bc1", Name = "ASICS" },
                    new ProductBrand { Id = "63ca5e59065163c16451bd73", Name = "Victor" },
                    new ProductBrand { Id = "63ca5e655ec1fdc49bd9327d", Name = "Yonex" },
                    new ProductBrand { Id = "63ca5e728c4cff9708ada2a6", Name = "Puma" },
                    new ProductBrand { Id = "63ca5e7ec90ff5c8f44d5ac8", Name = "Nike" },
                    new ProductBrand { Id = "63ca5e8d6110a9c56ee7dc48", Name = "Babolat" }
                };

                if (brands is not null && brands.Any())
                {
                    await brandCollection.InsertManyAsync(brands);
                }
            }
        }
    }
}
