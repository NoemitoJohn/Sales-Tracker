using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit.ObservableObjects;

public partial class ProductObservable : ObservableObject
{
    [ObservableProperty]
    protected string _name;

    [ObservableProperty]
    protected double _price;

    [ObservableProperty]
    protected int _count;

    public ProductObservable(Product product)
    { 
        
        Name = product.Name;
        Price = product.Price;
        Count = product.Count;
    }
    public ProductObservable(string name,  double price, int count)
    {
        Name = name;
        Price = price;
        Count = count;

    }
}
