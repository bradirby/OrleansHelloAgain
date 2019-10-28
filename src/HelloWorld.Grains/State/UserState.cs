using System;
using System.Collections.Generic;
using System.Text;
using HelloWorld.Interfaces;

namespace HelloWorld.Grains
{
    public class UserState
    {
        public string Username { get; set; }
        public List<IAccountGrain> Accounts { get; set; }
    }
}
