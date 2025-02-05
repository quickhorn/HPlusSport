using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HPlusSport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        [Route("/Products")]
        public void GetProducts()
        {
            
        }

        [HttpGet]
        [Route("/Products/{id?}")]
        public void GetProduct(int? id)
        {
            if (id == null)
            {
                return;
            }
        }
    }
}
