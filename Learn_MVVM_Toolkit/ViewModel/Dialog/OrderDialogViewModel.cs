using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit.ViewModel.Dialog;

public partial class OrderDialogViewModel : ObservableObject
{
    [ObservableProperty]
    string _name;

    [ObservableProperty]
    double _price;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(calTotal))]
    int _count;

    [ObservableProperty]
    double _total;

    public double calTotal => Price * Count;

    MainWindowViewModel _mainViewModel;
    public OrderDialogViewModel(MainWindowViewModel mainViewModel, SaleProduct sale)
    {

        Name = sale.Name;
        Price = sale.Price;
        Count = 1;
        Total = calTotal;
        _mainViewModel = mainViewModel;
    }

    [RelayCommand]
    public void AddToCart()
    {
        SaleProduct sale = new SaleProduct(Name, Price, Count, Total);
        _mainViewModel.SaleProducts.Add(sale);

    }
    partial void OnCountChanged(int value)
    {
        OnPropertyChanged(nameof(calTotal));
        Total = calTotal;
    }
    partial void OnPriceChanged(double value)
    {
        OnPropertyChanged(nameof(calTotal));
        Total = calTotal;
    }
}
