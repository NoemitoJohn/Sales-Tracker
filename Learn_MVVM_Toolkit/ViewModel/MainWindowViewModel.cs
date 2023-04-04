using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Learn_MVVM_Toolkit.Dialog;
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

    public ObservableCollection<SaleProduct> SaleProducts { get; } = new();


    public MainWindowViewModel()
    {
        TestProduct();
        SaleProducts.CollectionChanged += SaleProducts_CollectionChanged;
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

        SaleProduct sale = new SaleProduct(product);
        ShowAddToOrderDailog!.Invoke(this, sale);

    }

    [RelayCommand]
    public void CreateOrder()
    {
        Order order = new Order();
    }

    void TestProduct()
    {
        Products.Add(new Product("Test Item 1", 12, 20));
        Products.Add(new Product("Test Item 2", 45, 50));
        Products.Add(new Product("Test Item 2", 20, 100));
        Products.Add(new Product("Test Item 2", 45, 75));
    }

}
