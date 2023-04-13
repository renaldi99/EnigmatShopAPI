using EnigmatShopAPI.Models;
using EnigmatShopAPI.Services;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult> CreatePurchase([FromBody]Purchase payload)
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

            return Ok(new { code = 200, data = result });

        }

        [AllowAnonymous]
        [HttpPost("GetPurchaseTransaction")]
        public async Task<ActionResult> GetPurchaseById([FromQuery] string id)
        {
            var result = await _service.GetPurchaseById(id);
            return Ok(new { code = 200, data = result });
        }

        [AllowAnonymous]
        [HttpPost("DeletePurchaseTransaction")]
        public async Task<ActionResult> DeletePurchaseById([FromQuery] string id)
        {
            var result = await _service.DeletePurchaseById(id);
            return Ok(new { code = 200, message = "Purchase success deleted" });
        }
    }
}
