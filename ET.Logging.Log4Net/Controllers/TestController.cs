using ET.Logging.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ET.Logging.Log4Net.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ICustomLogger _logger;

        public TestController(ICustomLogger logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult SayHi()
        {
            _logger.Info("Hello");
            return Ok();
        }
    }
}
