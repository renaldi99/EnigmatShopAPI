using EnigmatShopAPI.Exceptions;
using EnigmatShopAPI.Models;
using EnigmatShopAPI.Services;
using Microsoft.AspNetCore.Authorization;
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
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var result = await _service.CreateCustomer(entity);
            if (result == 0)
            {
                return BadRequest(new { code = 400, msg = "Error from request" });
            }

            return Ok(new { code = 200, msg = "Success add customer" });
        }
            

        [HttpGet("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById([FromQuery] string id)
        {
            
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            var result = await _service.GetCustomerById(id);
            if (result is null) return NotFound(new { code = 404, msg = "Not found"});
            return Ok(new {code = 200, msg = "Data found", data = result});
           
        }

        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer entity)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var result = await _service.UpdateCustomer(entity);
            if (result == 0)
            {
                return BadRequest(new { code = 400, msg = "Invalid request client" });
            }
            return Ok(new { code = 200, msg = "Success update customer" });
        }

        [HttpDelete("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomerById([FromQuery] string customerId)
        {
            if (customerId.Equals(String.Empty))
            {
                return BadRequest(new { code = 400, msg = "Invalid request client" });
            }

            var result = await _service.DeleteCustomerById(customerId);
            if (result == 0)
            {
                return BadRequest(new { code = 404, msg = "Not found" });
            }
            return Ok(new { code = 200, msg = "Success delete customer" });
        }

        [HttpGet("SearchCustomerByName/{customerName}")]
        public async Task<IActionResult> SearchCustomerByName(string customerName)
        {
            if (customerName.Equals(""))
            {
               return BadRequest(new { code = 400, msg = "Invalid request client" });

            }

            var result = await _service.GetCustomerByName(customerName);
            return Ok(new { code = 200, msg = "Data found", data = result });
        }
    }
}
