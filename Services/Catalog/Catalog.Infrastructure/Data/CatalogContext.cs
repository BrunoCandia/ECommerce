using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data
{
    public class CatalogContext : ICatalogContext
    {
        private readonly IMongoDatabase _database;

        public IMongoCollection<Product> Products { get; }

        public IMongoCollection<ProductBrand> Brands { get; }

        public IMongoCollection<ProductType> Types { get; }

        public CatalogContext(IMongoClient mongoClient, IConfiguration configuration)
        {
            ////var connectionString = configuration.GetSection("DatabaseSettings:ConnectionString").Value;
            ////var connectionString = configuration.GetConnectionString("ConnectionString");
            ////var client = new MongoClient(connectionString);

            var databaseName = configuration.GetSection("DatabaseSettings:DatabaseName").Value;
            _database = mongoClient.GetDatabase(databaseName);

            Products = _database.GetCollection<Product>("Products");
            Brands = _database.GetCollection<ProductBrand>("Brands");
            Types = _database.GetCollection<ProductType>("Types");
        }

        ////public CatalogContext(IMongoDatabase database)
        ////{
        ////    Products = database.GetCollection<Product>("Products");
        ////    Brands = database.GetCollection<ProductBrand>("Brands");
        ////    Types = database.GetCollection<ProductType>("Types");
        ////}
    }
}
