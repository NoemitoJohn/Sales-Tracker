using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Learn_MVVM_Toolkit.Dialog;
using Learn_MVVM_Toolkit.ObservableObjects;
using Learn_MVVM_Toolkit.Service;
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

    public event Notify PopUpClicked;

    public event EventHandler ShowDailogItemInfo;
    //

    public SaleProductObservable SelectedSaleProduct { get; set; }
    public ObservableCollection<SaleProductObservable> SaleProducts => _mainModel.SaleProducts;

    public IRelayCommand RemoveOrderItemCommand { get; }
    public IRelayCommand EditOrderItemCommand { get; }

    public IRelayCommand<SaleProductObservable> SaveEditCommand { get; }
    public IRelayCommand<SaleProductObservable> SelectedItemCommand { get; }

    IDialogService _dialogService;

    public OrderUserControlViewModel(MainWindowViewModel mainModel, IDialogService dialogService)
    {
        this._mainModel = mainModel;
        this._dialogService = dialogService;
        RemoveOrderItemCommand = new RelayCommand(RemoveItem);
        EditOrderItemCommand = new RelayCommand(EditItem);
        SelectedItemCommand = new RelayCommand<SaleProductObservable>(SelectedItem);
        SaveEditCommand = new RelayCommand<SaleProductObservable>(SaveEdit);

    }

    public void SaveEdit(SaleProductObservable saleProduct)
    {
        if (saleProduct.Count > 0 && saleProduct.Count <= saleProduct.Available)
        {
            int index = SaleProducts.IndexOf(_selectedSaleItem);
            SaleProducts[index] = saleProduct;
            //TODO: create an event and notify the window dialog that its time to close :) 
        }
    }

    public void EditItem()
    {
        PopUpClicked.Invoke();
        
        var viewModel = new SelectedProductDialogViewModel(SelectedSaleProduct);
        
        bool? result = _dialogService.ShowDialog(viewModel, this);

        if (result.HasValue)
        {
            if(result.Value) 
            {
                var newItem = viewModel.SelectedItem;
                
                if (!SaleProducts.Contains(_selectedSaleItem)) return;

                int itemIndex = SaleProducts.IndexOf(_selectedSaleItem);
                SaleProducts[itemIndex] = newItem;
            }
        }
    }


    public void RemoveItem()
    {
        SaleProducts.Remove(_selectedSaleItem);
        PopUpClicked.Invoke();
    }

    public void SelectedItem(SaleProductObservable item)
    {
        _selectedSaleItem = item;
        SelectedSaleProduct = new(item.ProductInfo);
    }


}
