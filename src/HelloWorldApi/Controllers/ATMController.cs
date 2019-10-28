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
        public async Task<string> Get(Guid srcAcctId, Guid destAcctId, int amt)
        {
            var fromAcctGrain = HelloAppClient.GetGrain<IAccountGrain>(srcAcctId);
            var toAcctGrain = HelloAppClient.GetGrain<IAccountGrain>(destAcctId);

            var fromAcctSummBefore = await fromAcctGrain.GetAccountSummary();
            if (fromAcctSummBefore.IsNew) throw new ArgumentOutOfRangeException("Account Does Not exist");

            var toAcctSummBefore = await toAcctGrain.GetAccountSummary();
            if (toAcctSummBefore.IsNew) throw new ArgumentOutOfRangeException("Account Does Not exist");

            var beforeStr = $"Summary Before {fromAcctSummBefore.AccountName} ({fromAcctSummBefore.Balance}) to {toAcctSummBefore.AccountName} ({toAcctSummBefore.Balance})";

            var fromAcctSummAfter = await fromAcctGrain.Withdraw(amt);
            var toAcctSummAfter = await toAcctGrain.Deposit(amt);

            await Task.CompletedTask;

            return $"{beforeStr} then Moved {amt} from {fromAcctSummAfter.AccountName} ({fromAcctSummAfter.Balance}) to {toAcctSummAfter.AccountName} ({toAcctSummAfter.Balance})";
        }

        // POST: api/ATM
        public void Post([FromBody] string acctName)
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
