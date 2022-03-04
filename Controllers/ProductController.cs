using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RateLimit_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly Product _product;
        public ProductController(Product product)
        {
            _product = product;
        }
        // GET: api/<ProductController>
        [HttpGet("GetAllProducts")]
        public IActionResult Get()
        {
            try
            {
                List<Product> products = _product.GetProducts();
                if (products != null && products.Count != 0)
                    return StatusCode(StatusCodes.Status200OK, products);
                else
                    return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET api/<ProductController>/5
        [HttpGet("GetProduct/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Product product = _product.GetProductbyId(id);
                if (product == null)
                    return StatusCode(StatusCodes.Status404NotFound);
                else
                    return StatusCode(StatusCodes.Status200OK, product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] Product productModel)
        {
            try
            {
                int productId = _product.AddProduct(productModel);
                if (productId > 0)
                    return StatusCode(StatusCodes.Status201Created, productModel);
                else
                    return StatusCode(StatusCodes.Status400BadRequest); //we can return some other status code may be have to see
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product productModel)
        {
            try
            {
                int productId = _product.UpdateProduct(productModel, id);
                if (productId > 0)
                    return StatusCode(StatusCodes.Status204NoContent, "product updated successfully");
                else
                    return StatusCode(StatusCodes.Status404NotFound); //we can return some other status code may be have to see
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool IsDeleted = _product.DeleteProduct(id);
                if (IsDeleted)
                    return StatusCode(StatusCodes.Status204NoContent, "product deleted successfully");
                else
                    return StatusCode(StatusCodes.Status400BadRequest); //we can return some other status code may be have to see
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
