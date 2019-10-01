using System;
using System.Collections.Generic;
using System.Linq;
using Benchmark.Models.Implementations;
using Benchmark.Services.Abstractions;
using BenchmarkDotNet.Attributes;
using Bogus;
using Microsoft.Extensions.Logging;

namespace Benchmark
{
    [MemoryDiagnoser]
    [CsvExporter]
    [RPlotExporter]
    public class BenchmarkLinq : IBenchmarkLinq
    {
        private readonly List<Customer> customers;
        private readonly ILogger<BenchmarkLinq> logger;

        public BenchmarkLinq(ILogger<BenchmarkLinq> logger)
        {
            this.logger = logger;

            logger.LogInformation("Start of the chain process..");

            int Ids = 0;
            Faker<Customer> test = new Faker<Customer>()
                                         .RuleFor(p => p.Id, f => Ids++)
                                         .RuleFor(p => p.FirstName, f => f.Random.String2(9))
                                         .RuleFor(p => p.LastName, f => f.Random.String2(9))
                                         .RuleFor(p => p.EmailAddress, f => f.Random.String2(9))
                                         .RuleFor(p => p.TelephoneNumbers, f => f.Make(9, () => f.Phone.PhoneNumber()));

            List<Customer> tempList = test.Generate(500000);

            customers = tempList;
        }

        public void Start()
        {
            Linq_1000();

            PLinq_1000();

            Linq_100000();

            PLinq_100000();

            Linq_500000();

            PLinq_500000();
        }

        [Benchmark(Baseline = true)]
        private void Linq_1000()
        {
            logger.LogInformation($"Starting first call: {nameof(Linq_1000)}");

            var i = from x in customers.Take(1000)
                    where (CharsCount(x.FirstName + x.LastName + x.EmailAddress + x.Id + x.TelephoneNumbers.FirstOrDefault()) != false)
                    select x.FirstName.Length + x.LastName.Length + x.EmailAddress.Length + x.Id + x.TelephoneNumbers.FirstOrDefault().Length;

            logger.LogInformation($"First call finished: {nameof(Linq_1000)}");
        }

        [Benchmark]
        private void PLinq_1000()
        {
            logger.LogInformation($"Starting second call: {nameof(PLinq_1000)}");

            var i = from x in customers.AsParallel().Take(1000)
                    where (CharsCount(x.FirstName + x.LastName + x.EmailAddress + x.Id + x.TelephoneNumbers.FirstOrDefault()) != false)
                    select x.FirstName.Length + x.LastName.Length + x.EmailAddress.Length + x.Id + x.TelephoneNumbers.FirstOrDefault().Length;

            logger.LogInformation($"Second call finished: {nameof(Linq_1000)}");
        }

        [Benchmark]
        private void Linq_100000()
        {
            logger.LogInformation($"Starting third call: {nameof(Linq_100000)}");

            var i = from x in customers.Take(100000)
                    where (CharsCount(x.FirstName + x.LastName + x.EmailAddress + x.Id + x.TelephoneNumbers.FirstOrDefault()) != false)
                    select x.FirstName.Length + x.LastName.Length + x.EmailAddress.Length + x.Id + x.TelephoneNumbers.FirstOrDefault().Length;

            logger.LogInformation($"Third call finished: {nameof(Linq_100000)}");
        }

        [Benchmark]
        private void PLinq_100000()
        {
            logger.LogInformation($"Starting fourth call: {nameof(PLinq_100000)}");

            var i = from x in customers.AsParallel().Take(100000)
                    where (CharsCount(x.FirstName + x.LastName + x.EmailAddress + x.Id + x.TelephoneNumbers.FirstOrDefault()) != false)
                    select x.FirstName.Length + x.LastName.Length + x.EmailAddress.Length + x.Id + x.TelephoneNumbers.FirstOrDefault().Length;

            logger.LogInformation($"Fourth call finished: {nameof(PLinq_100000)}");
        }

        [Benchmark]
        private void Linq_500000()
        {
            logger.LogInformation($"Starting fifth call: {nameof(Linq_500000)}");

            var i = from x in customers
                    where (CharsCount(x.FirstName + x.LastName + x.EmailAddress + x.Id + x.TelephoneNumbers.FirstOrDefault()) != false)
                    select x.FirstName.Length + x.LastName.Length + x.EmailAddress.Length + x.Id + x.TelephoneNumbers.FirstOrDefault().Length;

            logger.LogInformation($"Fifth call finished: {nameof(Linq_500000)}");
        }

        [Benchmark]
        private void PLinq_500000()
        {
            logger.LogInformation($"Starting sixth call: {nameof(Linq_500000)}");

            var i = from x in customers.AsParallel()
                    where (CharsCount(x.FirstName + x.LastName + x.EmailAddress + x.Id + x.TelephoneNumbers.FirstOrDefault()) != false)
                    select x.FirstName.Length + x.LastName.Length + x.EmailAddress.Length + x.Id + x.TelephoneNumbers.FirstOrDefault().Length;

            logger.LogInformation($"Sixth call finished: {nameof(Linq_500000)}");
        }

        private bool CharsCount(string word)
        {
            var charArray = word.ToCharArray();
            int count = 0;
            foreach (var item in charArray)
            {
                count += item;
            }

            return IsPrime(count);
        }

        private bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }
    }
}