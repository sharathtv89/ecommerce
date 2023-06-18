using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;
        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException("catalogContext");
        }
        public async Task<Product> CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
            return await GetProductById(product.Id);
        }

        public async Task<bool> DeleteProduct(string productId)
        {
            var result = await _context.Products.DeleteOneAsync(filter => filter.Id == productId);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            return await _context.Products.Find(p => p.Category == categoryName).ToListAsync();
        }       

        public async Task<Product> GetProductById(string productId)
        {
            return await _context.Products.Find(p => p.Id == productId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string productName)
        {
            return await _context.Products.Find(p => p.Name == productName).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var result = await _context.Products.ReplaceOneAsync(filter => filter.Id == product.Id, product);
            if(result.IsAcknowledged && result.ModifiedCount > 0)
                return await _context.Products.Find(filter => filter.Id == product.Id).FirstOrDefaultAsync();
            
            return null;
        }
    }
}