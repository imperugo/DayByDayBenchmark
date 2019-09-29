using FizzWare.NBuilder;

public class OrderItem
{
    public static int Id { get; set; }
    public static Order Order { get; set; }
    public static Product Product { get; set; }
    public static int Quantity { get; set; }

    public int GetList (int elementCount) {

        var result = 0;

        var customers = Builder<OrderItem>.CreateListOfSize (elementCount).Build ();

        result = customers.Count;

        return result;
    }
}