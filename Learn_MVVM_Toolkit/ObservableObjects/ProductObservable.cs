using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit.ObservableObjects;

public partial class ProductObservable : ObservableObject
{

    //public string Name
    //{
    //    get => product.Name;
    //    set => SetProperty(product.Name, value, product, (u, n) => u.Name = n);
    //}
    public int ID { get;}

    private string _name;

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }


    //public double Price
    //{
    //    get => product.Price;
    //    set => SetProperty(product.Price, value, product, (u, n) => u.Price = n);
    //}

    private double _price;
    public double Price
    {
        get => _price;
        set => SetProperty(ref _price, value);
    }

    private int _count;

    public int Count
    {
        get => _count;
        set => SetProperty(ref _count, value);
    }

    private readonly Product product;
    
    public ProductObservable(Product p)
    { 
        product = p;
        ID = product.ID;
        Name= product.Name;
        Price= product.Price; 
        Count= product.Count;

    }

    public Product GetProduct() => product;

    public void SubtractToAvailbleCount(int val)
    {
        Count -= val;
        //TODO: Also subract the amount to our database
    }
        
}
