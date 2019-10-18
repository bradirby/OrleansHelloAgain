using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorld.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orleans;

namespace HelloWorldApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        private readonly ILogger<HelloWorldController> _logger;
        private readonly IClusterClient HelloAppClient;

        public HelloWorldController(ILogger<HelloWorldController> logger, IClusterClient client)
        {
            HelloAppClient = client;
            _logger = logger;
        }

        // GET: api/HelloWorld
        [HttpGet]
        public IEnumerable<string> Get()
        {
            // example of calling grains from the initialized client
            var friend = HelloAppClient.GetGrain<IHello>(0);
            var response = friend.SayHello("Good morning from API!");

            return new string[] { response.Result };
        }

        // GET: api/HelloWorld/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {

            return "value";
        }

        // POST: api/HelloWorld
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/HelloWorld/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
