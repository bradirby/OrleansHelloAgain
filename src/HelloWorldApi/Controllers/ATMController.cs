using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloWorld.Grains;
using HelloWorld.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Orleans;

namespace HelloWorldApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ATMController : ControllerBase
    {
        private readonly ILogger<ATMController> _logger;
        private readonly IClusterClient HelloAppClient;

        public ATMController(ILogger<ATMController> logger, IClusterClient client)
        {
            HelloAppClient = client;
            _logger = logger;
        }


        // GET: api/ATM
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            await Task.CompletedTask;
            return new string[] { "value1", "value2" };
        }

        // GET: api/ATM/5
        [HttpGet("{srcAcctId}/{destAcctId}/{amt}")]
        public async Task<string> Get(int srcAcctId, int destAcctId, int amt)
        {

            var fromAcctGrain = HelloAppClient.GetGrain<IAccountGrain>(srcAcctId);
            var toAcctGrain = HelloAppClient.GetGrain<IAccountGrain>(destAcctId);

            var fromAcctName = await fromAcctGrain.GetAccountName();
            var toAcctName = await toAcctGrain.GetAccountName();

            var beforeStr = $"Before {fromAcctName} ({fromAcctGrain.GetBalance().Result}) to {toAcctName} ({toAcctGrain.GetBalance().Result})";

            await fromAcctGrain.Withdraw(amt);
            await toAcctGrain.Deposit(amt);

            Console.WriteLine($"Processed a message from {fromAcctName} to {toAcctName}");
            await Task.CompletedTask;

            return $"{beforeStr} then Moved {amt} from {fromAcctName} ({fromAcctGrain.GetBalance().Result}) to {toAcctName} ({toAcctGrain.GetBalance().Result})";
        }

        // POST: api/ATM
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/ATM/5
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
