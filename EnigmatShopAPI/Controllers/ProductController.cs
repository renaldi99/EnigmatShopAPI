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
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] Product entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = 400, msg = "Invalid request" });
            }

            var res = await _service.CreateProduct(entity);
            if (res == 0)
            {
                return BadRequest(new { code = 400, msg = "Error request" });
            }
            return Ok(new { code = 200, msg = "Success create product" });
        }

        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = 400, msg = "Invalid request" });
            }

            var res = await _service.UpdateProduct(entity);
            if (res == 0)
            {
                return BadRequest(new { code = 400, msg = "Invalid update request" });

            }

            return Ok(new { code = 200, msg = "Success update product" });
        }
    }
}
