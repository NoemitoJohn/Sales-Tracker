using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit.ObservableObjects;

public partial class SaleProductObservable : ObservableObject
{

    private string _name;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    private double _price;
    public double Price
    {
        get => _price;
        set
        {
            if (SetProperty(ref _price, value))
            {
                OnPropertyChanged(nameof(Total));
            }
        }
    }

    private int _count;
    public int Count
    {
        get => _count;
        set
        {
            if (SetProperty(ref _count, value))
            {
                OnPropertyChanged(nameof(Total));
            }
        }
    }

    private double _total;
    public double Total
    {
        get
        {
            _total = Price * Count;
            return _total;
        }
        set => SetProperty(ref _total, value);
    }

    private readonly Product product;

    private readonly ProductObservable productObservable;
    public SaleProductObservable(ProductObservable ObservableP)
    {
        productObservable = ObservableP;
        product = productObservable.GetProduct();
        Name = productObservable.Name; 
        Price = productObservable.Price;
        Count = 1;
        Total = Price * Count;
    }

    public int GetProductAvailableCount()
    {
        return productObservable.Count;
    }

    public ProductObservable GetProduct()
    {
        return productObservable;
    }
}
