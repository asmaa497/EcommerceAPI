using EcommerceAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = productRepository.GetProducts();
            return Ok(products);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetOne(int id)
        {
            var product = productRepository.GetProduct(id);
            return Ok(product);
        }
    }
}
