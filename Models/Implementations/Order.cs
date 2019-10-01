using System;

namespace Benchmark.Models.Implementations
{
    public class Order
    {
        public static Customer Customer { get; set; }
        public static int Id { get; set; }
        public static DateTime OrderDate { get; set; }
    }
}