using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit.ObservableObjects;

public class SaleProductObservable : ObservableObject
{

    private string _name;
    private double _total;
    private double _price;
    private int _count;
    private readonly ProductObservable productObservable;

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
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
    public int Count
    {
        get => _count;
        set => SetProperty(ref _count, value);
    }
    public double Total
    {
        get
        {
            _total = Price * Count;
            return _total;
        }
        set => SetProperty(ref _total, value);
    }
    public int Available => productObservable.Count;

    public ProductObservable ProductInfo => productObservable;

    public SaleProductObservable()
    {

    }

    public SaleProductObservable(ProductObservable ObservableP)
    {
        productObservable = ObservableP;
        Name = productObservable.Name; 
        Price = productObservable.Price;
        Count = 1;
        Total = Price * Count;
    }

    public double GetTotalCost() => productObservable.Cost * Count;
    public double GetTotalPrice() => Total;
    public double GetTotalProfit () => GetTotalPrice() - GetTotalCost();

}
