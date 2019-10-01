using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace Benchmark.Infrastructure
{
    internal class HostingEnvironment : IHostingEnvironment
    {
        public HostingEnvironment()
        {
            ApplicationName = "DayByDayBenchmark";
            EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (string.IsNullOrEmpty(EnvironmentName))
            {
                Console.WriteLine("Warning: ASPNETCORE_ENVIRONMENT not set, will be used Development");
                EnvironmentName = "Development";
            }

            ContentRootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }

        public string EnvironmentName { get; set; }

        public string ApplicationName { get; set; }
        public string WebRootPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IFileProvider WebRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ContentRootPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IFileProvider ContentRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
