﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit;

public class Product
{
    public Product(string name, double price, int count)
    {
        Name = name;
        Price = price;
        Count = count;
    }
    // Emprove this
    public string Name { get;  set; }
    public double Price { get; set; }
    public int Count { get; set; }
}
