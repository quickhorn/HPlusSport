using HPlusSport.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace HPlusSport.API.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ShopContext _context;

        public ProductsService(ShopContext context)
        {
            _context = context;

            _context.Database.EnsureCreated();
        }

        public async Task<Product?> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return null;
            }
            else
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return product;
            }
        }

        public async Task<IEnumerable<Product>?> DeleteProducts(int[] ids)
        {
            var products = await _context.Products.Where(p => ids.Contains(p.Id)).ToArrayAsync();
            if (products == null)
            {
                return null;
            }

            foreach (var id in ids)
            {
                var deletedProduct = await _context.Products.FindAsync(id);
                if (deletedProduct == null)
                {
                    return null;
                }    
            }
            
            _context.Products.RemoveRange(products);
            await _context.SaveChangesAsync();
            return products;
        }
        
        public async Task<IEnumerable<Product>?> GetAllProducts()
        {
            return await _context.Products.ToArrayAsync();
        }

        public async Task<Product?> GetProduct(int id)
        {
            return await (_context.Products.FindAsync(id));
        }

        public async Task<IEnumerable<Product>?> GetProductsInStock()
        {
            return await _context.Products.Where(p => p.IsAvailable).ToArrayAsync();
        }

        public async Task<Product?> PostProduct(Product product)
        {
            if (product != null)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product;
            }
            else
            {
                return null;
            }

        }

        public async Task<Product?> PutProduct(int id, Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return product;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(p => p.Id == id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            
        }
    }
}
