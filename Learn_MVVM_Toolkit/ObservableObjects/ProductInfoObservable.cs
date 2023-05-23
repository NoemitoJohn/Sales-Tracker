using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit.ObservableObjects;

public class ProductInfoObservable : ObservableObject
{

    private int _id;
    private int _sold;
    private string _description;
    private int _productID;
    private Product.Info _info;

    public int ID
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }
    public int ProductID
    {
        get => _productID;
        set => SetProperty(ref _productID, value);
    }
    public int Sold
    {
        get => _sold;
        set => SetProperty(ref _sold, value);
    }
    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public Product.Info Info => _info;

    public ProductInfoObservable(Product.Info info)
    {   
        _info = info;
        ID = info.ID;
        Sold = info.Sold;
        ProductID= info.ProductID;
        Description= info.Description;

    }

}
