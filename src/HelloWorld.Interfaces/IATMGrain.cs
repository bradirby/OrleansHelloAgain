using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace HelloWorld.Interfaces
{
    public interface IATMGrain : IGrainWithIntegerKey
    {
        //[Transaction(TransactionOption.RequiresNew)]
        Task Transfer(long fromAccount, long toAccount, decimal amountToTransfer);
    }
}
