using System;
using BenchmarkDotNet.Running;

namespace Benchmark
{
    public class Program
    {
        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
            Console.ReadLine();
        }
    }
}
