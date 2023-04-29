using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Learn_MVVM_Toolkit.ObservableObjects;
using Learn_MVVM_Toolkit.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Learn_MVVM_Toolkit;

public partial class MainWindowViewModel : ObservableObject
{

    public ObservableCollection<ProductObservable> Products { get; } = new();
    public ObservableCollection<SaleProductObservable> SaleProducts { get; } = new();

    private Order _order;
    private SaleProduct _saleProductSelected;
    private double _total;

    private IDataBaseModel databaseModel;

    public Order Order
    {
        get => _order;
        set => SetProperty(ref _order, value);
    }
    
    public double Total
    {
        get => _total;
        set => SetProperty(ref _total, value);
    }
    public SaleProduct SaleProductSelected
    {
        get => _saleProductSelected;
        set => SetProperty(ref _saleProductSelected, value);
    }

    public MainWindowViewModel(IDataBaseModel DbModel)
    {
        databaseModel = DbModel;
        // TODO: Convert Product Object into ProductObservable

        IList<Product> products = databaseModel.GetAllProductAsList();
        //TODO: Improve this make sure to update the ProductObservable if new Item is added 


        //TODO: Improve this for loop
        for (int i = 0; i < products.Count; i++)
        {
            Products.Add(new ProductObservable(products[i]));
        }
        
        SaleProducts.CollectionChanged += SaleProducts_CollectionChanged;

    }

    private void SaleProducts_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if(e.NewItems != null)
        {
            foreach(SaleProductObservable item in e.NewItems) {     
                Total += item.Total;
            }
        }
    }
    //TODO: Maybe add a method to add an item to Saleproduct?????  
    public void AddToSaleProductObservable(SaleProductObservable sale)
    {
        SaleProducts.Add(sale);
    }

    public void AddToProductObservable(ProductObservable product)
    {
        Products.Add(product);
    }

    public void CreateOrder()
    {
            
    }

}
