using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HelloWorld.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Orleans;

namespace HelloWorld.Grains
{
    public class AccountGrain : Grain<AccountState>, IAccountGrain
    {
        //private readonly IServiceBusClient serviceBusClient;
        private readonly ILogger Logger;

        public AccountGrain(/*IServiceBusClient serviceBusClient*/ILogger<AccountGrain> logger)
        {
          //  this.serviceBusClient = serviceBusClient;
          Logger = logger;
        }

        async Task<IAccountSummaryDto> IAccountGrain.Deposit(decimal amount)
        {
            try
            {
                State.Balance += amount;
                await WriteStateAsync();
                await NotifyBalanceUpdate();
                return GetAccountSummaryInternal();
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e.Message);
                throw;
            }
        }

        protected override Task WriteStateAsync()
        {
            State.LastModified = DateTime.Now;
            return base.WriteStateAsync();
        }


        async Task<IAccountSummaryDto> IAccountGrain.Withdraw(decimal amount)
        {
            try
            {
                State.Balance -= amount;
                await WriteStateAsync();
                await NotifyBalanceUpdate();
                return GetAccountSummaryInternal();
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e.Message);
                throw;
            }
        }

        IAccountSummaryDto GetAccountSummaryInternal()
        {
            return State;
        }


        Task<IAccountSummaryDto> IAccountGrain.GetAccountSummary()
        {
            return Task.FromResult(GetAccountSummaryInternal());
        }

        async Task<IAccountSummaryDto> IAccountGrain.SetAccountName(string acctName)
        {
            try
            {
                State.AccountName = acctName;
                await WriteStateAsync();
                return GetAccountSummaryInternal();
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e.Message);
                throw;
            }
        }



        private async Task NotifyBalanceUpdate()
        {
            try
            {
                var balanceUpdate = new BalanceUpdateMessage
                {
                    AccountNumber = (int)this.GetPrimaryKeyLong(),
                    Balance = this.State.Balance
                };

                var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(balanceUpdate)));
                //await serviceBusClient.SendMessageAsync(message);
                await Task.CompletedTask;
            }
            catch (Exception e)
            {
                Logger.Log(LogLevel.Error, e.Message);
                throw;
            }
        }


    }
}
