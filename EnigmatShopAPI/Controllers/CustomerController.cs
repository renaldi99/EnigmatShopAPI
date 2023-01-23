using EnigmatShopAPI.Models;
using EnigmatShopAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnigmatShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> CreateNewCustomer([FromBody] Customer entity)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return ValidationProblem(ModelState);
                }

                var result = await _service.CreateCustomer(entity);
                if (result == 0)
                {
                    return BadRequest(new { code = 400, msg = "Error from request" });
                }

                return Ok(new { code = 200, msg = "Success Add Customer" });
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById([FromQuery] string id)
        {
            try
            {
                var result = await _service.GetCustomerById(id);
                if (result is null) return NotFound(new { code = 404, msg = "Not Found"});
                return Ok(new {code = 200, msg = "Data Found", data = result});
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
