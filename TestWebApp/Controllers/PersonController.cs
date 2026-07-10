using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestWebApp.Controllers
{
    [Route("api/people")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        [HttpGet("health1")]
        public IActionResult Test()
        {
            return Ok("Healthy");
        }

        [HttpGet("health2")]
        public IActionResult Test2()
        {
            return Ok("Healthy 2");
        }
    }
}
