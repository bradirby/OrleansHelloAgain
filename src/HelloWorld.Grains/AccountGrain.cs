using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HelloWorld.Interfaces;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using Orleans;

namespace HelloWorld.Grains
{
    public class AccountGrain : Grain<AccountGrainState>, IAccountGrain
    {
        //private readonly IServiceBusClient serviceBusClient;

        public AccountGrain(/*IServiceBusClient serviceBusClient*/)
        {
          //  this.serviceBusClient = serviceBusClient;
        }

        async Task IAccountGrain.Deposit(decimal amount)
        {
            try
            {
                this.State.Value += amount;
                await this.WriteStateAsync();

                await NotifyBalanceUpdate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        async Task IAccountGrain.Withdraw(decimal amount)
        {
            this.State.Value -= amount;
            await this.WriteStateAsync();

            await NotifyBalanceUpdate();
        }

        Task<decimal> IAccountGrain.GetBalance()
        {
            return Task.FromResult(this.State.Value);
        }

        public Task<string> GetAccountName()
        {
            return Task.FromResult(State.AccountName);
        }

        public string AcctName => State.AccountName;

        private async Task NotifyBalanceUpdate()
        {
            var balanceUpdate = new BalanceUpdateMessage
            {
                AccountNumber = (int)this.GetPrimaryKeyLong(),
                Balance = this.State.Value
            };

            var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(balanceUpdate)));
            //await serviceBusClient.SendMessageAsync(message);
            await Task.CompletedTask;
        }
    }
}
