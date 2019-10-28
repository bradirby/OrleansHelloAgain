using System.Threading.Tasks;
using Orleans;

namespace HelloWorld.Interfaces
{
    public interface IAccountGrain : IHelloWorldGrainWithGuidKey
    {
        Task<IAccountSummaryDto> SetAccountName(string acctName);

        Task<IAccountSummaryDto> GetAccountSummary();

        Task<IAccountSummaryDto> Withdraw(decimal amount);

        Task<IAccountSummaryDto> Deposit(decimal amount);

    }
}
