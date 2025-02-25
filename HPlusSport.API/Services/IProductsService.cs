using HPlusSport.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HPlusSport.API.Services
{
    public interface IProductsService 
    {
        public Task<IEnumerable<Product>?> GetAllProducts();

        public Task<Product?> GetProduct(int id);

        public Task<IEnumerable<Product>?> GetProductsInStock();



        public Task<Product?> PostProduct(Product product);

        public Task<Product?> PutProduct(int id, Product product);

        public Task<Product?> DeleteProduct(int id);
        public Task<IEnumerable<Product>?> DeleteProducts(int[] ids);
    }
}
