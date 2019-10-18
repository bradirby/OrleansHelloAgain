using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HelloWorld.Interfaces;
using Microsoft.Azure.ServiceBus;

namespace HelloWorld.Grains
{
    public class ServiceBusClient : IServiceBusClient
    {
        private readonly TopicClient topicClient;

        public ServiceBusClient(string connectionString)
        {
            topicClient = new TopicClient(connectionString, "balanceUpdates");
        }

        public async Task SendMessageAsync(Message message)
        {
            await topicClient.SendAsync(message);
        }
    }
}
