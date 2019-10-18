using System;

namespace HelloWorld.Grains
{
    [Serializable]
    public class AccountGrainState
    {
        public string AccountName { get; set; } = "Acct" + Guid.NewGuid().ToString();
        public decimal Value { get; set; } = 1000;
    }
}