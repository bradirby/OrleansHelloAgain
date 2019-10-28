using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HelloWorld.Interfaces;
using Orleans;
using Orleans.Concurrency;

namespace HelloWorld.Grains
{
    [StatelessWorker]
    public class ATMGrain : Grain, IATMGrain
    {
        Task IATMGrain.Transfer(Guid fromAccount, Guid toAccount, decimal amountToTransfer)
        {
            return Task.WhenAll(
                this.GrainFactory.GetGrain<IAccountGrain>(fromAccount).Withdraw(amountToTransfer),
                this.GrainFactory.GetGrain<IAccountGrain>(toAccount).Deposit(amountToTransfer));
        }
    }
}
