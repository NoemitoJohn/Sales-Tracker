using CommunityToolkit.Mvvm.ComponentModel;

namespace Learn_MVVM_Toolkit.ObservableObjects;

public class SoldProductObservable : ObservableObject
{
    public string Name { get; }
    public int Count { get;}
    public double TotalCost { get; }
    public double TotalPrice { get; }
    public double TotalProfit { get; }

    public SoldProductObservable(string name, int count, double totalCost, double totalPrice, double totalProfit)
    {
        Name = name;
        Count = count;
        TotalCost = totalCost;
        TotalPrice = totalPrice;
        TotalProfit = totalProfit;
    }
}

