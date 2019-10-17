using System;
using HelloWorld.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Extensions.Logging;
using Orleans.TestingHost;

namespace HelloWorld.Grains.Tests
{
    [TestClass]
    public class HelloGrainTest
    {
        private TestCluster cluster;

        [ClassInitialize]
        public void TestFixtureSetup(TestContext context)
        {
            cluster = new TestClusterBuilder(1)
                .Build();

            //.Configure<ClusterOptions>(options =>
            //{
            //    //ClusterId is the name for the Orleans cluster must be the same for silo and client so they can talk to each other
            //    options.ClusterId = CommonSettings.ClusterId;

            //    //ServiceId is the ID used for the application and it must not change across deployments
            //    options.ServiceId = CommonSettings.ServiceId;
            //})

            ////This tells the silo where to listen. For this example, we are using a loopback.
            //.Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)

            ////Adds the grain class and interface assembly as application parts to your orleans application.
            //.ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences())

            //.ConfigureLogging(logging => logging.AddConsole());

        }

        [TestMethod]
        public void CtorAssignsLogger()
        {
            var mockLogger = new Mock<ILogger<HelloGrain>>();
            var sut = new HelloGrain(mockLogger.Object);
            Assert.AreEqual(mockLogger.Object, sut.logger);
        }

        [TestMethod]
        public void SayHelloReturnsProperString()
        {
            //var hello = cluster.GrainFactory.GetGrain<IHello>();
            //var greeting = await hello.SayHello();

            //cluster.StopAllSilos();

            //Assert.Equal("Hello, World", greeting);
        }
    }
}
