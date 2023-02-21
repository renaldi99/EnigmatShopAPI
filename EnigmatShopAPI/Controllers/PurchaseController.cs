using EnigmatShopAPI.Models;
using EnigmatShopAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnigmatShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _service;

        public PurchaseController(IPurchaseService service)
        {
            _service = service;
        }

        [HttpPost("AddPurchaseTransaction")]
        public async Task<ActionResult> CreatePurchase(Purchase payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = 400, msg = "Invalid request" });
            }

            var result = await _service.CreatePurchase(payload);

            if (!result.is_success)
            {
                return BadRequest(new { code = 400, data = result });
            }

                return Ok(new { status = 200, data = result });

        }
    }
}
