﻿using System;
using System.Net;
using System.Threading.Tasks;
using HelloWorld.Grains;
using HelloWorld.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Runtime;

namespace HelloWorld.App
{
    public static class HelloWorldApp
    {

        const int initializeAttemptsBeforeFailing = 5;
        private static int attempt = 0;

        public static void AddHelloWorldApp(this IServiceCollection services)
        {
            var client = StartClientWithRetries();
            services.AddSingleton<IClusterClient>(client.Result);
        }

        public static async Task<IClusterClient> StartClientWithRetries()
        {
            attempt = 0;
            IClusterClient client;
            client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = CommonSettings.ClusterId;
                    options.ServiceId = CommonSettings.ServiceId;
                })
                .ConfigureLogging(logging => logging.AddConsole())
                .Build();

            await client.Connect(RetryFilter);
            PopulateTestUsers(client);
            Console.WriteLine("Client successfully connect to silo host");
            return client;
        }

        private static void PopulateTestUsers(IClusterClient client)
        {
            var usr1 = client.GetGrain<IAccountGrain>(Guid.NewGuid());
        }

        private static async Task<bool> RetryFilter(Exception exception)
        {
            if (exception.GetType() != typeof(SiloUnavailableException))
            {
                Console.WriteLine($"Cluster client failed to connect to cluster with unexpected error.  Exception: {exception}");
                return false;
            }
            attempt++;
            Console.WriteLine($"Cluster client attempt {attempt} of {initializeAttemptsBeforeFailing} failed to connect to cluster.  Exception: {exception}");
            if (attempt > initializeAttemptsBeforeFailing)
            {
                return false;
            }
            await Task.Delay(TimeSpan.FromSeconds(4));
            return true;
        }

        public static async Task<ISiloHost> StartSilo()
        {
            // define the cluster configuration
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = CommonSettings.ClusterId;
                    options.ServiceId = CommonSettings.ServiceId;
                })
                .AddMemoryGrainStorageAsDefault(options => options.NumStorageGrains = 10)
                .ConfigureServices(context => ConfigureDI(context))
                .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                .UseInMemoryReminderService()
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences())
                .ConfigureLogging(logging => logging.AddConsole())
                .UseDashboard(options => { });

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }

        private static IServiceProvider ConfigureDI(IServiceCollection services)
        {
           // services.AddSingleton<IServiceBusClient>((sp) => new ServiceBusClient(configuration.GetConnectionString("ServiceBusConnectionString")));

            return services.BuildServiceProvider();
        }


    }
}
