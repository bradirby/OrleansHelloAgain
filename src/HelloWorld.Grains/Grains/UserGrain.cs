using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HelloWorld.Interfaces;
using Orleans;

namespace HelloWorld.Grains.Grains
{
    public class UserGrain : Grain<UserState>, IUserGrain
    {
        async Task<IAccountSummaryDto> IUserGrain.SetUserPersonalInfo(UserPersonalInfoDto info)
        {
            throw new NotImplementedException();
        }

        async Task<IEnumerable<IAccountGrain>> IUserGrain.GetUserAccounts()
        {
            throw new NotImplementedException();
        }

        async Task<IEnumerable<IAccountGrain>> IUserGrain.CreateAccounts()
        {
            throw new NotImplementedException();
        }

        async Task<IEnumerable<IAccountGrain>> IUserGrain.DeleteAccounts(IAccountGrain acct)
        {
            throw new NotImplementedException();
        }

    }
}
