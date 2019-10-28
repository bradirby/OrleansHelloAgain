using System;
using System.Collections.Generic;
using System.Text;
using HelloWorld.Grains;
using HelloWorld.Interfaces;
using Orleans;

namespace HelloWorld.App
{
    public interface IHelloWorldClient
    {

    }


    public class HelloWorldClient
    {
        private readonly IClusterClient ClusterClient;

        public HelloWorldClient(IClusterClient client)
        {
            ClusterClient = client;
        }

        public T GetExistingGrain<T>(Guid id) where T: IHelloWorldGrainWithGuidKey
        {
            var obj = ClusterClient.GetGrain<T>(id);
            return obj;
        }
    }
}
