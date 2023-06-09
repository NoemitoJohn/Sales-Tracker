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
    /// Create a product object
    /// </summary>
    /// <param name="iD">Name of the product</param>
    /// <param name="name">Name of the product</param>
    /// <param name="count">Count of the product</param>
    /// <param name="price">Price of the product</param>
    /// <param name="retailPrice">RetailPrice of the product</param>
    /// <param name="category">Category of the product</param>
    /// <param name="information">Info of the product</param>
    public Product() { }
    public Product(int iD, string name, int count, double price, double retailPrice, string category, Info information)
    {
        ID = iD;
        Name = name;
        Count = count;
        Cost = price;
        Price = retailPrice;
        Category = category;
        Information = information;
    }
    /// <summary>
    /// Create a product object from the database data 
    /// </summary>
    /// <param name="id">ID of the Product from the database</param>
    /// <param name="name">Name of the Product from the database</param>
    /// <param name="count">Count of the Product from the database</param>
    /// <param name="price">Price of the Product from the database</param>
    /// <param name="retailPrice">RetailPrice of the Product from the database</param>
    /// <param name="category">Category of the Product from the database</param>
    /// <param name="information">Info data from the Info Table of the Product from the database</param>

    public int ID { get; }
    public string Name { get; }
    public int Count { get; }
    public double Cost { get; }
    public double Price { get; }
    public string Category { get; }
    public Info Information { get; }

    public class Info
    {
        public int ID { get; }
        public int ProductID { get; }
        public int Sold { get; set; }
        public string Description { get; }

        public Info()
        {
            ID = 0;
            ProductID = 0;
            Sold = 0;
            Description = string.Empty;

        }

        public Info(int id, int productID, int sold, string description)
        {
            ID = id;
            ProductID = productID;
            Sold = sold;
            Description = description;
        }

        
    }

}
