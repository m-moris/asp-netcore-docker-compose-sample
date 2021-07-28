using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    [ApiController]
    [Route("")]
    public class HelloController : ControllerBase
    {
        private readonly IHttpClientFactory _factory;
        private readonly ILogger<HelloController> _logger;

        public HelloController(IHttpClientFactory factory, ILogger<HelloController> logger)
        {
            _factory = factory;
            _logger = logger;
        }

        [HttpGet("hello")]
        public IActionResult Hello()
        {
            return new JsonResult(new HelloResponse {Message = "hello world"});
        }

        [HttpGet("hello2")]
        public async Task<IActionResult> Hello2()
        {
            var client = _factory.CreateClient();
            var res = await client.GetAsync(new Uri("http://backend/w"));
            res.EnsureSuccessStatusCode();
            return new JsonResult(await res.Content.ReadAsStringAsync());
        }
    }

    public class HelloResponse
    {
        public String Message { get; set; }
    }
}