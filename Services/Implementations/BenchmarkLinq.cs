using System;
using System.Collections.Generic;
using System.Linq;
using Benchmark.Models.Implementations;
using Benchmark.Services.Abstractions;
using BenchmarkDotNet.Attributes;
using Bogus;

namespace Benchmark {

    [MemoryDiagnoser]
    [CsvExporter]
    [RPlotExporter]
    public class BenchmarkLinq : IBenchmarkLinq {
        private readonly List<Customer> customers;

        public BenchmarkLinq () {

            int Ids = 0;
            Faker<Customer> test = new Faker<Customer> ()
                .RuleFor (p => p.Id, f => Ids++)
                .RuleFor (p => p.FirstName, f => f.Random.String2 (9))
                .RuleFor (p => p.LastName, f => f.Random.String2 (9))
                .RuleFor (p => p.EmailAddress, f => f.Random.String2 (9))
                .RuleFor (p => p.TelephoneNumbers, f => f.Make (9, () => f.Phone.PhoneNumber ()));

            customers = test.Generate (500000);

        }

        public void Start () {
            Linq_1000 ();
            PLinq_1000 ();
            Linq_100000 ();
            PLinq_100000 ();
            Linq_500000 ();
            PLinq_500000 ();
        }

        [Benchmark (Baseline = true)]
        public void Linq_1000 () {
            _ = customers
                .Take (1000)
                .All (x => expensiveOperation (x.FirstName + x.LastName + x.EmailAddress + x.Id + x.TelephoneNumbers.FirstOrDefault ()));
        }

        [Benchmark]
        public void PLinq_1000 () {

            _ = customers.AsParallel ()
                .Take (1000)
                .All (x => expensiveOperation (x.FirstName + x.LastName + x.EmailAddress + x.Id + x.TelephoneNumbers.FirstOrDefault ()));
        
        }

        [Benchmark]
        public void Linq_100000 () {

            _ = customers
                .Take (100000)
                .All (x => expensiveOperation (x.FirstName + x.LastName + x.EmailAddress + x.Id + x.TelephoneNumbers.FirstOrDefault ()));

        }

        [Benchmark]
        public void PLinq_100000 () {

            _ = customers.AsParallel ()
                .Take (100000)
                .All (x => expensiveOperation (x.FirstName + x.LastName + x.EmailAddress + x.Id + x.TelephoneNumbers.FirstOrDefault ()));

        }

        [Benchmark]
        public void Linq_500000 () {

            _ = customers.Take (500000)
                .All (x => expensiveOperation (x.FirstName + x.LastName + x.EmailAddress + x.Id + x.TelephoneNumbers.FirstOrDefault ()));

        }

        [Benchmark]
        public void PLinq_500000 () {

            _ = customers.AsParallel ()
                .Take (500000)
                .All (x => expensiveOperation(x.FirstName + x.LastName + x.EmailAddress + x.Id + x.TelephoneNumbers.FirstOrDefault()));

        }

        private bool expensiveOperation (string word) {
            
            var charArray = word.ToCharArray ();
            foreach (var item in charArray) {
                IsPrime (item);
            }

            return true;
        }

        private void IsPrime (int number) {
            if (number <= 1) _ = false;
            if (number == 2) _ = true;
            if (number % 2 == 0) _ = false;

            var boundary = (int) Math.Floor (Math.Sqrt (number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    _ = false;
        }
    }
}