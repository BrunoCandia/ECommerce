using Catalog.Core.Entities;
using Catalog.Core.Repositories;
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
