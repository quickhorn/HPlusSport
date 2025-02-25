using HPlusSport.API.Models;
using HPlusSport.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPlusSport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(ShopContext context, IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _productsService.GetAllProducts();
            return Ok(products);
        }

        //[HttpGet, Route("/Products/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            var product = await _productsService.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("InStock")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsInStock()
        {
            return Ok(await _productsService.GetProductsInStock());
        }

        [HttpPost]
        public async Task<ActionResult> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (product != null)
            {
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, await _productsService.PostProduct(product));
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            var updatedProduct = await _productsService.PutProduct(id, product);
            if (updatedProduct == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct([FromRoute]int id)
        {
            var product = await _productsService.DeleteProduct(id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(product);
            }
        }

        [HttpDelete("batch")]
        public async Task<ActionResult> DeleteProducts([FromBody] int[] ids)
        {
            //var products = await _context.Products.Where(p => ids.Contains(p.Id)).ToArrayAsync();
            var products = await _productsService.DeleteProducts(ids);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

    }
}
