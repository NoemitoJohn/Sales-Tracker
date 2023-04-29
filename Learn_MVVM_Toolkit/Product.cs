using Learn_MVVM_Toolkit.ObservableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit;

public class Product
{
    /// <summary>
    /// Create a product object and insert to the database
    /// </summary>
    /// <param name="name">Name of product</param>
    /// <param name="count">Count of the Product</param>
    /// <param name="price">Price of the Product</param>
    public Product(string name, int count, double price)
    {
        
        Name = name;
        Price = price;
        Count = count;
    }

    /// <summary>
    /// Get the product object to the database and assign with an ID 
    /// </summary>
    /// <param name="Id">ID of the Product</param>
    /// <param name="name">Name of the Product</param>
    /// <param name="count">Count of the Product</param>
    /// <param name="price">Price of the Product</param>
    public Product(int Id, string name, int count, double price)
    {
        ID = Id;
        Name = name;
        Count = count;
        Price = price;
    }


    public ProductObservable ToObservableObject()
    {
        return new ProductObservable(this);
    }


    public int ID { get; set; } 
    public string Name { get;  set; }
    public int Count { get; set; }
    public double Price { get; set; }
}
