using System;
using FizzWare.NBuilder;

public class Order
{
    public static Customer Customer { get; set; }
    public static int Id { get; set; }
    public static DateTime OrderDate { get; set; }
    
    public static int GetList (int elementCount) {

        var result = 0;

        var customers = Builder<Order>.CreateListOfSize (elementCount).Build ();

        result = customers.Count;

        return result;
    }
}