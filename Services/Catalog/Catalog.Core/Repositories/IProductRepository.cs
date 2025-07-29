using Catalog.Core.Entities;
using Catalog.Core.Helper;
using Catalog.Core.Specifications;

namespace Catalog.Core.Repositories
{
    public interface IProductRepository
    {
        Task<Pagination<Product>> GetProductsAsync(CatalogSpecParams catalogSpecParams);

        Task<IEnumerable<Product>> GetProductsAsync();

        Task<Product> GetProductAsync(string id);

        Task<IEnumerable<Product>> GetProductsByNameAsync(string name);

        Task<IEnumerable<Product>> GetProductsByBrandAsync(string brand);

        Task<Product> CreateProductAsync(Product product);

        Task<bool> UpdateProductAsync(Product product);

        Task<bool> DeleteProductAsync(string id);
    }
}
