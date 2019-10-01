using System.Collections.Generic;

namespace Benchmark.Models.Implementations
{
    public class Customer
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public int Id { get; set; }
        public string LastName { get; set; }
        public List<string> TelephoneNumbers { get; set; }
    }
}