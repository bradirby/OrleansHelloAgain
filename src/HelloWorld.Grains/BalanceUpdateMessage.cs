using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld.Grains
{
    public class BalanceUpdateMessage
    {
        public int AccountNumber { get; set; }

        public decimal Balance { get; set; }
    }
}
