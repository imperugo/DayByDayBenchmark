using System.Collections.Generic;
using FizzWare.NBuilder;

public class Customer
{
    public string EmailAddress { get; set; }
    public string FirstName { get; set; }
    public int Id { get; set; }
    public string LastName { get; set; }
    public string TelephoneNumber { get; set; }

    public static List<Customer> GetList (int elementCount) {

        return (List<Customer>)Builder<Customer>.CreateListOfSize (elementCount).Build ();
    }
}