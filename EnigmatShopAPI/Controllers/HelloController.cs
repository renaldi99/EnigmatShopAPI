using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnigmatShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        [HttpGet("{name}")]
        public string GetWithParameter(string name)
        {
            return $"Hello {name}";
        }
    }
}
