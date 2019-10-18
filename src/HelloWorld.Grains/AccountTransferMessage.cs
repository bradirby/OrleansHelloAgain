using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld.Grains
{
    public class AccountTransferMessage
    {
        public int From { get; set; }

        public int To { get; set; }

        public decimal Amount { get; set; }
    }
}
