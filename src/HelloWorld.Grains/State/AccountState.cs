using System;
using HelloWorld.Interfaces;

namespace HelloWorld.Grains
{
    [Serializable]
    public class AccountState :  IAccountSummaryDto
    {
        public string AccountName { get; set; } = "NewAcct";

        public decimal Balance { get; set; } = 0;

        public DateTime LastModified { get; set; } = DateTime.MinValue;

        public bool IsNew => LastModified == DateTime.MinValue;
    }
}