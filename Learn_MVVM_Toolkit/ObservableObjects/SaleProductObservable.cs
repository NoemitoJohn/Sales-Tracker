using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit.ObservableObjects;

public partial class SaleProductObservable : ObservableValidator
{

    [ObservableProperty]
    private string _name;
    
    [ObservableProperty]
    private double _price;
    
    [ObservableProperty]
    private int _count;
    
    [ObservableProperty]
    private double _total;

    public SaleProductObservable(Product product)
    {
        Name = product.Name;
        Price = product.Price;
        Count = 1;
        Total = Price * Count;
    }



    partial void OnPriceChanged(double value)
    {
        // TODO: Prevent user entering a number of 0;
        Total = Price * Count;
    }
    partial void OnCountChanged(int value)
    {
        Total = Price * Count;
    }
}
