using EcommerceAPI.Models;
using EcommerceAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            
            this.categoryRepository = categoryRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = this.categoryRepository.GetAll();
            return Ok(categories);
        }
    }
}
