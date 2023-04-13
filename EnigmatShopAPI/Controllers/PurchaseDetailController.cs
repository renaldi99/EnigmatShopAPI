using EnigmatShopAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnigmatShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseDetailController : ControllerBase
    {
        private readonly IPurchaseDetailService _service;

        public PurchaseDetailController(IPurchaseDetailService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost("DeletePurchaseDetailById")]
        public async Task<ActionResult> DeletePurchaseDetailById([FromQuery] string id)
        {
            var result = await _service.DeletePurchaseDetailById(id);
            return Ok(new { code = 200, message = "PurchaseDetail success deleted" });
        }
    }
}
