using ELibrary.Consumers.ElasticService.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELibrary.Consumers.ElasticService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IBookClient _bookClient;
        private readonly ILogger<TestController> _logger;

        public TestController(IBookClient bookClient, ILogger<TestController> logger)
        {
            _bookClient = bookClient;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> TestBookConnectionAsync()
        {
            try
            {
                var book = await _bookClient.GetBookAsync(1);

                return Ok(book);
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to get Book with Id: 1");
            }

            return BadRequest("Failed");
        }
    }
}
