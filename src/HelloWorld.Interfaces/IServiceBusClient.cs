using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace HelloWorld.Interfaces
{
    public interface IServiceBusClient
    {
        Task SendMessageAsync(Message message);
    }
}
