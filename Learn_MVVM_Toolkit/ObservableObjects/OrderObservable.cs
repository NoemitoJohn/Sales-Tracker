using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Learn_MVVM_Toolkit.ObservableObjects;

public class OrderObservable : ObservableObject
{


    public string Date { get; }

    public Order.TYPE type { get; }

    public OrderObservable(DateTime date, Order.TYPE type)
    {
        Date = date.ToShortDateString();
        this.type = type;

    }
}
public class DeductionObservable : OrderObservable
{
    public string Description { get; }
    public double Amount { get; }

    public DeductionObservable(DateTime date, Order.TYPE type, string description, double amount) : base(date, type)
    {
        Description = description;
        Amount = amount;
        
    }

}


public class SoldObservable : OrderObservable
{
    public double TotalCosts { get; }
    public double TotalRevenue { get; }
    public double TotalProfit { get; }

    public ObservableCollection<SoldProductObservable> OrderItemList { get; }

    public SoldObservable(Sold sold) : base(sold.Date, sold.Type)
    {
        TotalCosts= sold.TotalCosts;
        TotalRevenue = sold.TotalRevenue;
        TotalProfit = sold.TotalProfit;
        OrderItemList = new ObservableCollection<SoldProductObservable>(sold.Orders);
    }
}