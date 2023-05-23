using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Learn_MVVM_Toolkit.ObservableObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit.ViewModel;

public class OrderUserControlViewModel : ObservableObject
{
    private MainWindowViewModel _mainModel;

    private SaleProductObservable _selectedSaleItem;

    public ObservableCollection<SaleProductObservable> SaleProducts => _mainModel.SaleProducts;

    public IRelayCommand RemoveOrderItemCommand { get; }
    public IRelayCommand<SaleProductObservable> SelectedItemCommand { get; }

    public OrderUserControlViewModel(MainWindowViewModel mainModel)
    {
        _mainModel = mainModel;
        RemoveOrderItemCommand = new RelayCommand(RemoveItem);
        SelectedItemCommand = new RelayCommand<SaleProductObservable>(SelectedItem);

    }

    public void RemoveItem()
    {
        SaleProducts.Remove(_selectedSaleItem); 
    }

    public void SelectedItem(SaleProductObservable item)
    {
        _selectedSaleItem = item;

    }


}
