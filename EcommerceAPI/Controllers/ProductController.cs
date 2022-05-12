using EcommerceAPI.Models;
using EcommerceAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
        [HttpGet("{id:int}", Name = "getOneProduct")]
        public IActionResult GetOne(int id)
        {
            var product = productRepository.GetProduct(id);
            return Ok(product);
        }
        [HttpGet("CatID")]
        public IActionResult GetCatProducts([FromQuery]int id)
        {
            var product = productRepository.GetProductsByCatID(id);
            return Ok(product);
        }
        [HttpPatch("{id:int}")]
        
        public IActionResult Edit(int id, Product prod)
        {

            if (ModelState.IsValid)
            {
                try
                {
                   Product p= productRepository.EditProduct(id, prod);

                   return Ok(p);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            /////
            return BadRequest(ModelState);

        }
        [HttpDelete("{id:int}")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Delete(int id)
        {

            if (ModelState.IsValid)
            {
                try
                {
                   Product p= productRepository.Delete(id);

                    return StatusCode(StatusCodes.Status204NoContent, p);// Created(url, dep);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult AddNew(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    productRepository.Add(product);
                    string url = Url.Link("getOneProduct", new { id = product.id });
                    return Created(url, product);

                    
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
    }
}
