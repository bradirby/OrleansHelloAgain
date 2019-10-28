using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld.Interfaces
{
    public interface IAccountSummaryDto
    {
        string AccountName { get; }
        decimal Balance { get; }
        bool IsNew { get; }
    }
}
