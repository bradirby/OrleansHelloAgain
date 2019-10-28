using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace HelloWorld.Interfaces
{
    public interface IATMGrain : IGrainWithGuidKey
    {
        //[Transaction(TransactionOption.RequiresNew)]
        Task Transfer(Guid fromAccount, Guid toAccount, decimal amountToTransfer);
    }
}
