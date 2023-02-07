using EnigmatShopAPI.Models;
using EnigmatShopAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnigmatShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] Product entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = 400, msg = "Invalid request" });
            }

            var res = await _productService.CreateProduct(entity);
            if (res == 0)
            {
                return BadRequest(new { code = 400, msg = "Error request" });
            }
            return Ok(new { code = 200, msg = "Success create product" });
        }
    }
}
