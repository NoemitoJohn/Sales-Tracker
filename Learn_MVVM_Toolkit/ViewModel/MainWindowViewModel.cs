using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Learn_MVVM_Toolkit.ObservableObjects;
using Learn_MVVM_Toolkit.Service;
using Learn_MVVM_Toolkit.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Learn_MVVM_Toolkit;

public partial class MainWindowViewModel : ObservableObject
{
    public ObservableCollection<ProductObservable> Products { get; } = new();
    public ObservableCollection<SaleProductObservable> SaleProducts { get; } = new();
    public ObservableCollection<OrderObservable> Sale { get; } = new(); // Improve naming!!!!!

    private Order _order;

    private double _total;

    private IDataBaseModel databaseModel;
    private IDialogService dialogService;

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

    public IRelayCommand CheckOutCommand { get; }
    public IRelayCommand OpenSaleInfoCommand { get; }
    public IRelayCommand OpenProductInfoCommand { get; }

    public MainWindowViewModel(IDataBaseModel DbModel, IDialogService dialogService)
    {
        databaseModel = DbModel;
        this.dialogService = dialogService;
        // get all sold orders from database
        List<Sold> solds = databaseModel.GetAllSoldOrders();
        // get all of the product in the database 
        IList<Product> products = databaseModel.GetAllProductAsList();

         
        CheckOutCommand = new RelayCommand(CheckOut);
        OpenSaleInfoCommand = new RelayCommand(OpenSaleInfo);
        OpenProductInfoCommand = new RelayCommand(OpenProductInfo);
        //TODO: Improve this make sure to update the ProductObservable if new Item is added 

        DeductionObservable deductionTest = new(DateTime.Now, Order.TYPE.DEDUCTION, "Bayad sa turko", 100);
        DeductionObservable deductionTest1 = new(new DateTime(2023,05,28), Order.TYPE.DEDUCTION, "Bayad sa turko", 100);
        DeductionObservable deductionTest2 = new(new DateTime(2023, 05, 26), Order.TYPE.DEDUCTION, "Bayad sa turko", 100);
        DeductionObservable deductionTest3 = new(new DateTime(2023, 05, 24), Order.TYPE.DEDUCTION, "Bayad sa turko", 100);
       

        


        //TODO: Improve this for loop move to DataBase class 
        // 
        for (int i = 0; i < products.Count; i++)
        {
            Products.Add(new ProductObservable(i,products[i])); //Add
        }

        for (int i = 0; i < solds.Count; i++)
        {
            Sale.Insert(0, new SoldObservable(solds[i])); //Insert 
        }

        Sale.Add(deductionTest);
        Sale.Add(deductionTest1);
        Sale.Add(deductionTest2);
        Sale.Add(deductionTest3);


        SaleProducts.CollectionChanged += SaleProducts_CollectionChanged_Handler;
         
    }

    // if item add or remove to the observable sales object update total
    private void SaleProducts_CollectionChanged_Handler(object sender, NotifyCollectionChangedEventArgs e) { 
    
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                foreach (SaleProductObservable item in e.NewItems)
                    Total += item.Total;
                break;

            case NotifyCollectionChangedAction.Remove:
                foreach (SaleProductObservable item in e.OldItems)
                {
                    int itemIndex = item.ProductInfo.GetIndex();
                    Total -= item.Total;
                }
                break;
            case NotifyCollectionChangedAction.Replace:
                var oldItem = e.OldItems[0] as SaleProductObservable;
                var newItem = e.NewItems[0] as SaleProductObservable;
                Total -= oldItem.Total;
                Total += newItem.Total;
                break;
        }
    }
    //TODO: Maybe add a method to add an item to Saleproduct?????  
    public void AddToSaleProductObservable(SaleProductObservable sale) =>
        SaleProducts.Add(sale);
    

    public void AddToProductObservable(ProductObservable product) =>    
        Products.Add(product);
    

    public void CheckOut()
    {
        //
        if (SaleProducts.Count <= 0) return;
        List<SoldProductObservable> orders = new();

        double totalCost = 0;
        double totalPrice = 0;
        double totalProfit = 0;

        DateTime d = DateTime.Now; //TODO: Test date ONLY refer to create order method

        foreach (SaleProductObservable item in SaleProducts)
        {
            SoldProductObservable sold =
                new(item.Name, item.Count, item.GetTotalCost(),
                item.GetTotalPrice(), item.GetTotalProfit());

            totalCost += sold.TotalCost;
            totalPrice += sold.TotalPrice;
            totalProfit += sold.TotalProfit;

            int newCount = Products[item.ProductInfo.GetIndex()].Count -= item.Count;
            // update dataBase 
            orders.Add(sold);
        }

        Sold soldOrder = new(d, Order.TYPE.CASH, totalCost, totalPrice, totalProfit, orders);

        databaseModel.InsertSold(soldOrder);

        Sale.Insert(0, new SoldObservable(soldOrder));

        ResetOrder();

    }

    private void ResetOrder()
    {
        SaleProducts.Clear();
        Total = 0;
    }
    // Command 
    protected void OpenSaleInfo()
    {
        var viewModel = new SaleInfoDialogViewModel(Sale);
        bool? result = dialogService.ShowDialog(viewModel, this);

        if (result.HasValue)
        {
            if (result.Value)
            {

            }
        }

    }
    public bool SaleExist(int index)
    {
        bool result = false;

        foreach (var item in SaleProducts) 
        {
            if(item.ProductInfo.GetIndex() == index)
            {
                result = true;
            }
        
        }
        return result;

    }

    protected void OpenProductInfo()
    {
        var viewModel = new ProductInfoDialogViewModel(Products);
        bool? resut = dialogService.ShowDialog(viewModel, this);
        
        if (resut.HasValue)
        {
            if (resut.Value)
            {

            }
        }
    }

    public void CreateOrder()
    {

    }

}
