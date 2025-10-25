using Catalog.Core.Entities;
using Catalog.Core.Helper;
using Catalog.Core.Repositories;
using Catalog.Core.Specifications;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, IBrandRepository, ITypeRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);

            return product;
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var deleteResult = await _catalogContext.Products.DeleteOneAsync(p => p.Id == id);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Product> GetProductAsync(string id)
        {
            return await _catalogContext.Products
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByBrandAsync(string brand)
        {
            return await _catalogContext.Products
                .Find(p => p.Brands.Name == brand.ToLower())
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            return await _catalogContext.Products
                .Find(p => p.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task<Pagination<Product>> GetProductsAsync(CatalogSpecParams catalogSpecParams)
        {
            var filter = Builders<Product>.Filter.Empty;

            if (!string.IsNullOrWhiteSpace(catalogSpecParams.Search))
            {
                filter = filter & Builders<Product>.Filter.Where(p => p.Name.ToLower().Contains(catalogSpecParams.Search.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(catalogSpecParams.BrandId))
            {
                filter = filter & Builders<Product>.Filter.Where(p => p.Brands != null && p.Brands.Id == catalogSpecParams.BrandId);
            }

            if (!string.IsNullOrWhiteSpace(catalogSpecParams.TypeId))
            {
                filter = filter & Builders<Product>.Filter.Where(p => p.Types != null && p.Types.Id == catalogSpecParams.TypeId);
            }

            if (catalogSpecParams.Brands is { Count: > 0 })
            {
                filter = filter & Builders<Product>.Filter.Where(p => p.Brands != null && catalogSpecParams.Brands.Contains(p.Brands.Id));
            }

            if (catalogSpecParams.Types is { Count: > 0 })
            {
                filter = filter & Builders<Product>.Filter.Where(p => p.Types != null && catalogSpecParams.Types.Contains(p.Types.Id));
            }

            var totalCount = (int)await _catalogContext.Products.CountDocumentsAsync(filter);

            var sortDefinition = Builders<Product>.Sort.Ascending(p => p.Name);

            if (!string.IsNullOrWhiteSpace(catalogSpecParams.Sort))
            {
                sortDefinition = catalogSpecParams.Sort switch
                {
                    "priceAsc" => Builders<Product>.Sort.Ascending(p => p.Price),
                    "priceDesc" => Builders<Product>.Sort.Descending(p => p.Price),
                    _ => Builders<Product>.Sort.Ascending(p => p.Name)
                };
            }

            var products = await _catalogContext.Products
                .Find(filter)
                .Sort(sortDefinition)
                .Skip((catalogSpecParams.PageIndex - 1) * catalogSpecParams.PageSize)
                .Limit(catalogSpecParams.PageSize)
                .ToListAsync();

            return new Pagination<Product>(catalogSpecParams.PageIndex, catalogSpecParams.PageSize, totalCount, products);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _catalogContext.Products
                .Find(p => true)
                .ToListAsync();
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var replaceOneResult = await _catalogContext.Products.ReplaceOneAsync(
                filter: p => p.Id == product.Id,
                replacement: product);

            return replaceOneResult.IsAcknowledged && replaceOneResult.ModifiedCount > 0;
        }

        public async Task<IEnumerable<ProductBrand>> GetBrandsAsync()
        {
            return await _catalogContext.Brands
                .Find(p => true)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductType>> GetTypesAsync()
        {
            return await _catalogContext.Types
                .Find(p => true)
                .ToListAsync();
        }
    }
}
