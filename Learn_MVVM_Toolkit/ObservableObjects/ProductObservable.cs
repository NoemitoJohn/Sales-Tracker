using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit.ObservableObjects;

public partial class ProductObservable : ObservableObject
{

    #region Fields
    private int _index;
    private string _name;
    private int _count;
    private double _price;
    private readonly Product product;
    private string _category;
    private double _retailPrice;
    private ProductInfoObservable _info;
    #endregion

    #region Properties
    
    public int ID { get; }
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
    public int Count
    {
        get => _count;
        set => SetProperty(ref _count, value);
    }
    public double Cost
    {
        get => _price;
        set => SetProperty(ref _price, value);
    }
    public double Price
    {
        get => _retailPrice;
        set => SetProperty(ref _retailPrice, value);
    }
    public string Category
    {
        get => _category;
        set => SetProperty(ref _category, value);
    }
    public ProductInfoObservable Info
    {
        get => _info;
        set => SetProperty(ref _info, value);
    }
    #endregion
    public Product ProductBase => product;


    public ProductObservable(int index, Product p) :this(p)
    {
        _index = index; 
    }

    public ProductObservable(Product p)
    {
        
        product = p;
        ID = product.ID;
        Name = product.Name;
        Count = product.Count;
        Cost = product.Cost;
        Price = product.Price;
        Category = product.Category;
        Info = new(p.Information);
        
    }   
    public int GetIndex () => _index;
    
    public void SubtractToAvailbleCount(int val)
    {
        Count -= val;
        //TODO: Also subract the amount to our database
    }

}
