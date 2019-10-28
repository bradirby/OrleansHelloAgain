using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelloWorld.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orleans;

namespace HelloWorldApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        private readonly IClusterClient HelloAppClient;

        public AccountController(ILogger<AccountController> logger, IClusterClient client)
        {
            HelloAppClient = client;
            _logger = logger;
        }

        // GET: api/Account
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Account/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Account
        [HttpPost]
        public async void Post([FromBody] AccountCreateRequestDto reqDto)
        {
            try
            {
                var newAcct = HelloAppClient.GetGrain<IAccountGrain>(Guid.NewGuid());
                var currState = await newAcct.SetAccountName(reqDto.AccountName);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
                throw;
            }
        }

        // PUT: api/Account/5
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
