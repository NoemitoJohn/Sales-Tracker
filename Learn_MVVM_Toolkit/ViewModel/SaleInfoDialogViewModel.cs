using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Learn_MVVM_Toolkit.ObservableObjects;
using Learn_MVVM_Toolkit.Service;
using Learn_MVVM_Toolkit.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;

namespace Learn_MVVM_Toolkit.ViewModel;


public class SaleInfoDialogViewModel : ObservableObject, IDialogRequestClose
{
    public event EventHandler<DialogCloseRequestEventArgs> CloseRequest;


    private MainWindowViewModel mainViewModel;

    private IList<OrderObservable> orderTemp;

    private DateTime dateFrom;
    private DateTime dateTo;

    private ObservableCollection<OrderObservable> _sale;

    private string _selectedCategory;

    public string SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            if (SetProperty(ref _selectedCategory, value))
            {
                OnComboBoxSelected(value);
            }
        }
    }


    private double _orderTotalProfit;
    private double _ordersTotalCost;
    private double _orderTotalRevenue;
    private double _orderTotalDeduction;

    public DateTime DateFrom
    {
        get => dateFrom;
        set => SetProperty(ref dateFrom, value);
    }
    public DateTime DateTo
    {
        get => dateTo;
        set => SetProperty(ref dateTo, value);
    }

    public double OrderTotalCost
    {
        get => _ordersTotalCost;
        set => SetProperty(ref _ordersTotalCost, value);
    }

    public double OrderTotalRevenue
    {
        get => _orderTotalRevenue;
        set => SetProperty(ref _orderTotalRevenue, value);
    }

    public double OrderTotalProfit
    {
        get => _orderTotalProfit;
        set => SetProperty(ref _orderTotalProfit, value);
    }

    public double OrderTotalDeduction
    {
        get => _orderTotalDeduction;
        set => SetProperty(ref _orderTotalDeduction, value);
    }

    public ObservableCollection<OrderObservable> Sale
    {
        get => _sale;
        set
        {
            if (SetProperty(ref _sale, value))
            {

            }

        }
    }

    public ObservableCollection<string> ComboBoxItems { get; set; }

    public IRelayCommand SeachOrderDateCommand { get; }

    public IRelayCommand CreateDeductionCommand { get; }
    public Dictionary<string, ObservableCollection<object>> ComboBoxItemsSource { get; set; }

    //TODO: create a method to calculate the total of the list items
    public SaleInfoDialogViewModel(ObservableCollection<OrderObservable> orders, MainWindowViewModel mainViewModel)
    {
        this.mainViewModel = mainViewModel;

        DateFrom = DateTime.Now;
        DateTo = DateTime.Now;

        orderTemp = orders.ToList();

        string[] cat = { "CASH", "DEDUCTION" }; //TODO: get data from database
        SelectedCategory = cat[0];

        ComboBoxItems = new ObservableCollection<string>(cat);

        SeachOrderDateCommand = new RelayCommand(SeachOrderDate);
        CreateDeductionCommand = new RelayCommand(CreateDeduction);
    }

    void CreateDeduction()
    {
        DeductionObservable deduction = new(DateTime.Now, Order.TYPE.DEDUCTION, "Electricity", 500);

        orderTemp.Insert(0, deduction);

        var orderByDate = GetOrdersByDate().ToList();

        CalCulateAllTotals(orderByDate);

        Sale = GetOrdersBySearch(SelectedCategory, orderByDate);

        mainViewModel.Sale.Insert(0, deduction);
    }

    public void SeachOrderDate()
    {
        var ordersByDate = GetOrdersByDate().ToList();

        Sale = GetOrdersBySearch(SelectedCategory, ordersByDate);

        CalCulateAllTotals(ordersByDate);

    }

    public void OnComboBoxSelected(string val)
    {
        //selectedCategory = val;

        var ordersByDate = GetOrdersByDate().ToList();

        Sale = GetOrdersBySearch(val, ordersByDate);

        CalCulateAllTotals(ordersByDate);
    }


    private void CalCulateAllTotals(List<OrderObservable> orders)
    {

        double totalCost = 0;
        double totalRevenue = 0;
        double totalProfit = 0;
        double totalDeduction = 0;

        foreach (var item in orders)
        {
            if (item is DeductionObservable)
            {
                DeductionObservable d = (DeductionObservable)item;
                totalDeduction += d.Amount;
            }

            if (item is SoldObservable)
            {
                SoldObservable s = (SoldObservable)item;
                totalCost += s.TotalCosts;
                totalRevenue += s.TotalRevenue;
                totalProfit += s.TotalProfit;
            }
        }

        OrderTotalCost = totalCost;
        OrderTotalRevenue = totalRevenue;
        OrderTotalProfit = totalProfit;
        OrderTotalDeduction = totalDeduction;
    }

    private ObservableCollection<OrderObservable> GetOrdersBySearch(string searchVal, List<OrderObservable> list)
    {
        ObservableCollection<OrderObservable> tempOrder = new();

        foreach (var item in list)
        {
            if (item.type.ToString().ToLower() == searchVal.ToLower())
            {
                tempOrder.Add(item);
            }
        }

        return tempOrder;
    }

    private ObservableCollection<OrderObservable> GetOrdersByDate()
    {
        ObservableCollection<OrderObservable> temp = new();

        foreach (var item in orderTemp)
        {
            var orderDate = DateTime.Parse(item.Date);

            if (orderDate.Date >= DateFrom.Date && orderDate <= DateTo.Date)
            {
                temp.Add(item);
            }
        }
        return temp;
    }
}
