using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;

namespace HelloWorld.Interfaces
{
    public interface IUserGrain : IHelloWorldGrainWithStringKey
    {
        Task<IAccountSummaryDto> SetUserPersonalInfo(UserPersonalInfoDto info);

        Task<IEnumerable<IAccountGrain>> GetUserAccounts();

        Task<IEnumerable<IAccountGrain>> CreateAccounts();

        Task<IEnumerable<IAccountGrain>> DeleteAccounts(IAccountGrain acct);
    }
}






