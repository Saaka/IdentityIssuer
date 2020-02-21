using Microsoft.AspNetCore.Mvc;

namespace IdentityIssuer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
