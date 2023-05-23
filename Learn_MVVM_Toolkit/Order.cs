using Learn_MVVM_Toolkit.ObservableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit;

public class Order
{
    
    public enum TYPE
    {
        CASH,
        CREDIT,
        DEDUCTION
    }

    public DateTime Date { get; }
    public TYPE Type { get; }

    public Order(DateTime date, TYPE type)
    {
        Date = date;
        Type = type;
    }

    public static TYPE GetType(string type)
    {
        TYPE t = TYPE.CASH;
        

        switch (type)
        {
            case "CASH":
                t = TYPE.CASH;
                break;
            case "CREDIT":
                t = TYPE.CREDIT;
                break;
            case "DEDUCTION":
                t = TYPE.DEDUCTION;
                break;
        }

        return t;

    }


}

public class Sold : Order
{
    public double TotalCosts { get; }
    public double TotalRevenue { get; }
    public double TotalProfit { get; }
    public List<SoldProductObservable> Orders { get; }

    public Sold(DateTime date, TYPE type, double totalCosts, double totalRevenue, double totalProfit, List<SoldProductObservable> orders) : base(date, type)
    {
        TotalCosts = totalCosts;
        TotalRevenue = totalRevenue;
        TotalProfit = totalProfit;
        Orders = orders;
    }


}


