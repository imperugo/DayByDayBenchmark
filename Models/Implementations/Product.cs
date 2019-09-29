using System.Collections.Generic;
using FizzWare.NBuilder;

public class Product {
    public string Description { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }

    public static IList<Product> GetList (int elementCount) {

        return Builder<Product>.CreateListOfSize (elementCount).Build ();

    }
}