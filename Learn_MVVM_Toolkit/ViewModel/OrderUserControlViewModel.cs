using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Learn_MVVM_Toolkit.ObservableObjects;
using Learn_MVVM_Toolkit.Service;
using Learn_MVVM_Toolkit.Util;
using System;
using System.Collections.ObjectModel;

namespace Learn_MVVM_Toolkit.ViewModel;

public class OrderUserControlViewModel : ObservableObject, IPopUpViewModel
{
    private MainWindowViewModel _mainModel;
    
    private SaleProductObservable _selectedSaleItem;
    private IDialogService _dialogService;
    
    public event EventHandler<PopUpResultEventArgs> PopUpResult;

    public SaleProductObservable SelectedSaleProduct { get; set; }
    public ObservableCollection<SaleProductObservable> SaleProducts => _mainModel.SaleProducts;

    public IRelayCommand RemoveOrderItemCommand { get; }
    public IRelayCommand EditOrderItemCommand { get; }

    public IRelayCommand<SaleProductObservable> SelectedItemCommand { get; }

    public OrderUserControlViewModel(MainWindowViewModel mainModel, IDialogService dialogService)
    {
        this._mainModel = mainModel;
        this._dialogService = dialogService;
        RemoveOrderItemCommand = new RelayCommand(RemoveItem);
        EditOrderItemCommand = new RelayCommand(EditItem);
        SelectedItemCommand = new RelayCommand<SaleProductObservable>(SelectedItem);
    }

    public void EditItem()
    {
        // PopUpClicked.Invoke();
        PopUpResult?.Invoke(this, new PopUpResultEventArgs(false));

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
        PopUpResult?.Invoke(this, new PopUpResultEventArgs(false));
    }

    public void SelectedItem(SaleProductObservable item)
    {
        _selectedSaleItem = item;
        SelectedSaleProduct = new(item.ProductInfo);
        PopUpResult?.Invoke(this, new PopUpResultEventArgs(true));
    }


}
