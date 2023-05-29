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


public class SaleInfoDialogViewModel : ObservableObject, IDialogRequestClose, IOrderDatePickerViewModel, IComboBoxViewModel
{
    public event EventHandler<DialogCloseRequestEventArgs> CloseRequest;

    private IList<OrderObservable> orderTemp;

    private DateTime _dateFrom;
    private string selectedCategory;
    private DateTime _dateTo;
    
    public DateTime DateFrom
    {
        get => _dateFrom;
        set => SetProperty(ref _dateFrom, value);
    }
    public DateTime DateTo
    {
        get => _dateTo;
        set => SetProperty(ref _dateTo, value);
    }

    public IRelayCommand SeachOrderDateCommand { get; }

    private ObservableCollection<OrderObservable> _sale;

    public ObservableCollection<OrderObservable> Sale
    {
        get => _sale;
        set => SetProperty(ref _sale, value);
    }

    public ObservableCollection<string> ComboBoxItems { get; set; }


    //TODO: create a method to calculate the total of the list items
    public SaleInfoDialogViewModel(ObservableCollection<OrderObservable> orders)
    {
        DateFrom = DateTime.Now;
        DateTo = DateTime.Now;
        
        Sale = new();
        
        orderTemp = orders.ToList();

        string[] cat = { "CASH", "DEDUCTION" }; //TODO: get data from database

        ComboBoxItems = new ObservableCollection<string>(cat);

        SeachOrderDateCommand = new RelayCommand(SeachOrderDate);
    }

    public void SeachOrderDate()
    {
        Sale = Search(selectedCategory);
    }

    public void OnComboBoxSelected(string val)
    {
        selectedCategory = val;
        
        Sale = Search(val);    
    }

    private ObservableCollection<OrderObservable> Search(string cat)
    {
        ObservableCollection<OrderObservable> temp = new();

        foreach (var item in orderTemp)
        {
            if (item.type.ToString().ToLower() == cat)
            {
                var orderDate = DateTime.Parse(item.Date);

                if (orderDate.Date >= DateFrom.Date && orderDate <= DateTo.Date)
                {
                    temp.Add(item);
                }
            }
        }
        return temp;
    }

}
