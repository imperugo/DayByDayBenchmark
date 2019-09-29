using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

public class BenchmarkLinq {

    [Benchmark (Baseline = true)]
    public int GetLinqResult1000 () {
        return Customer.GetList (1000)
            .Where (x => x.EmailAddress.Contains ('2'))
            .Count ();
    }

    [Benchmark]
    public int GetPLinqResult1000 () {
        return Customer.GetList (1000).AsParallel ()
            .WithExecutionMode (ParallelExecutionMode.ForceParallelism)
            .Where (x => x.EmailAddress.Contains ('2'))
            .Count ();
    }

    [Benchmark]
    public int GetLinqResult100000 () {
        return Customer.GetList (100000)
            .Where (x => x.EmailAddress.Contains ('2'))
            .Count ();
    }

    [Benchmark]
    public int GetPLinqResult100000 () {
        return Customer.GetList (100000).AsParallel ()
            .WithExecutionMode (ParallelExecutionMode.ForceParallelism)
            .Where (x => x.EmailAddress.Contains ('2'))
            .Count ();
    }

    [Benchmark]
    public int GetLinqResult1000000 () {
        return Customer.GetList (1000000)
            .Where (x => x.EmailAddress.Contains ('2'))
            .Count ();
    }

    [Benchmark]
    public int GetPLinqResult1000000 () {
        return Customer.GetList (1000000).AsParallel ()
            .WithExecutionMode (ParallelExecutionMode.ForceParallelism)
            .Where (x => x.EmailAddress.Contains ('2'))
            .Count ();
    }
}