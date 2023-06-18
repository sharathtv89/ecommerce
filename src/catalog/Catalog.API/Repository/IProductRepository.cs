using Catalog.API.Entities;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(string productId);
        Task<IEnumerable<Product>> GetProductByName(string productName);        
        Task<IEnumerable<Product>> GetProductByCategory(string categoryName);

        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string productId);

    }
}