using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Benchmark
{
    [RankColumn]
    [MemoryDiagnoser]
    public class BenchmarkLinq
    {

        [Benchmark(Baseline = true)]
        public void GetLinqResult1000()
        {
            var list = Enumerable.Range(1, 1000).ToList();
            list.ForEach(x => IsPrime(x));
        }

        [Benchmark]
        public void GetPLinqResult1000()
        {
            var list = Enumerable.Range(1, 1000).ToList();
            list.AsParallel().ForAll(x => IsPrime(x));
        }

        [Benchmark]
        public void GetLinqResult100000()
        {
            var list = Enumerable.Range(1, 100000).ToList();
            list.ForEach(x => IsPrime(x));
        }

        [Benchmark]
        public void GetPLinqResult100000()
        {
            var list = Enumerable.Range(1, 100000).ToList();
            list.AsParallel().ForAll(x => IsPrime(x));
        }

        [Benchmark]
        public void GetLinqResult1000000()
        {
            var list = Enumerable.Range(1, 1000000).ToList();
            list.ForEach(x => IsPrime(x));
        }

        [Benchmark]
        public void GetPLinqResult1000000()
        {
            var list = Enumerable.Range(1, 1000000).ToList();
            list.AsParallel().ForAll(x => IsPrime(x));
        }

        [Benchmark]
        public void GetPLinqResult1000000Forced()
        {
            var list = Enumerable.Range(1, 1000000).ToList();
            list.AsParallel().WithExecutionMode(ParallelExecutionMode.ForceParallelism).WithDegreeOfParallelism(Environment.ProcessorCount).ForAll(x => IsPrime(x));
        }

        //[Benchmark]
        //public void GetLinqResult100000000()
        //{
        //    var list = Enumerable.Range(1, 100000000).ToList();
        //    list.ForEach(x => IsPrime(x));
        //}

        //[Benchmark]
        //public void GetPLinqResult100000000()
        //{
        //    var list = Enumerable.Range(1, 10000000).ToList();
        //    list.AsParallel().ForAll(x => IsPrime(x));
        //}

        //[Benchmark]
        //public void GetPLinqResult100000000Forced()
        //{
        //    var list = Enumerable.Range(1, 10000000).ToList();
        //    list.AsParallel().WithExecutionMode(ParallelExecutionMode.ForceParallelism).WithDegreeOfParallelism(Environment.ProcessorCount).ForAll(x => IsPrime(x));
        //}

        private static bool IsPrime(int number)
        {
            number *= 9;
            bool res = false;

            if (number < 2)
                return false;

            int limit = (int)Math.Sqrt(number);

            for (int i = 2; i <= limit; i++)
            {
                if (number % i == 0)
                    res = false;
            }

            return res;
        }
    }
}