using System.Threading.Tasks;
using Orleans;

namespace HelloWorld.Interfaces
{
    public interface IAccountGrain : IGrainWithIntegerKey
    {
        Task<string> GetAccountName();

        //[Transaction(TransactionOption.Required)]
        Task Withdraw(decimal amount);

        //[Transaction(TransactionOption.Required)]
        Task Deposit(decimal amount);

        //[Transaction(TransactionOption.Required)]
        Task<decimal> GetBalance();
    }
}
