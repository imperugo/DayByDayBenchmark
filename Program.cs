using System;
using Benchmark.Infrastructure;
using Benchmark.Services.Abstractions;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using HostingEnvironment = Microsoft.AspNetCore.Hosting.Internal.HostingEnvironment;

namespace Benchmark
{
    public class Program
    {
        static void Main(string[] args)
        {
            var provider = Setup();

            var logger = provider.GetRequiredService<ILogger<Program>>();
            var benchmarkLinq = provider.GetRequiredService<IBenchmarkLinq>();

            logger.LogInformation("Benchmark is starting..");

            benchmarkLinq.Start();

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
            Console.ReadLine();
        }

        private static ServiceProvider Setup()
        {
            var services = new ServiceCollection();

            services.ConfigureLogging();
            services.AddSingleton<IHostingEnvironment, HostingEnvironment>();
            services.AddSingleton<IBenchmarkLinq, BenchmarkLinq>();

            return services.BuildServiceProvider();
        }
    }
}