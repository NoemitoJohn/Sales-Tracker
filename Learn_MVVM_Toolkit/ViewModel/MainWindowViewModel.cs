using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Learn_MVVM_Toolkit.Dialog;
using Learn_MVVM_Toolkit.ObservableObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Learn_MVVM_Toolkit;

public partial class MainWindowViewModel : ObservableObject
{
    public delegate void Show(MainWindowViewModel model, SaleProduct sale);
    public event Show? ShowAddToOrderDailog;

    public ObservableCollection<Product> Products { get; } = new();

    [ObservableProperty]
    private Order? _order;


    [ObservableProperty]
    private double _total;

    public ObservableCollection<SaleProductObservable> SaleProducts { get; } = new();

    [ObservableProperty]
    private SaleProduct? _saleProductSelected;

    [ObservableProperty]
    private bool _isShowPopup;

    public MainWindowViewModel()
    {
        TestProduct();
        SaleProducts.CollectionChanged += SaleProducts_CollectionChanged;
       //_isShowPopup = true;
    }

    private void SaleProducts_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if(e.NewItems != null)
        {
            foreach(SaleProduct item in e.NewItems) {     
                Total += item.Total;
            }
        }
    }
    //TODO: Maybe add a method to add an item to Saleproduct?????  
    [RelayCommand]
    public void AddProduct(Product product)
    {

        SaleProductSelected = new SaleProduct(product);

        IsShowPopup = true;

    }

    [RelayCommand]
    public void CreateOrder()
    {
     
    }

    void TestProduct()
    {
        Products.Add( new Product("Item 1", 10 , 20));
        Products.Add( new Product("Item 2", 20,  40));
        Products.Add( new Product("Item 1", 30 , 60));
        Products.Add( new Product("Item 1", 40 , 80));
        Products.Add( new Product("Item 1", 50 , 100));
        Products.Add( new Product("Item 1", 60 , 120));
    }

}
