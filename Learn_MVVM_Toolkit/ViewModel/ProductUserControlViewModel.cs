using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
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
using System.Windows.Input;

namespace Learn_MVVM_Toolkit.ViewModel;

public delegate void Notify();

public partial class ProductUserControlViewModel : ObservableObject
{

    private MainWindowViewModel _mainViewModel;
    private IDialogService dialogService { get; }
    public ObservableCollection<ProductObservable> Products { get; }
    private SaleProductObservable _selectedSaleProduct;
    public SaleProductObservable SelectedSaleProduct
    {
        get => _selectedSaleProduct;
        set => SetProperty(ref _selectedSaleProduct, value);
    }

    public IRelayCommand AddProductCommand { get; }
    public IRelayCommand AddCartCommand { get; }

    public MainWindowViewModel MainViewModel => _mainViewModel;

    public ProductUserControlViewModel(MainWindowViewModel mainViewModel, IDialogService dialogService)
    {
        _mainViewModel = mainViewModel;
        Products = _mainViewModel.Products;
        AddProductCommand = new RelayCommand<ProductObservable>(AddProduct);
        AddCartCommand = new RelayCommand<SaleProductObservable>(AddCart);

        this.dialogService = dialogService;
    }

    //Convert the selected ProductObservable into SaleProductObservable
    private void AddProduct(ProductObservable product)
    {
        SelectedSaleProduct = new SaleProductObservable(product);

        SelectedProductDialogViewModel viewModel = new(SelectedSaleProduct);

        bool? result = dialogService.ShowDialog(viewModel, this);
        // Show dialog
        if (result.HasValue)
        {
            if(result.Value)
            {
                AddCart(viewModel.SelectedItem);
            }
        }
    }
    public void AddCart(SaleProductObservable sale)
    {
        /** TODO: Get the OrderUserControlViewModel object and add to selected item
        //if (sale.Count <= 0) return;
        //// TODO: Improve!!
        //if (sale.Count <= sale.Available)
        //{
        //    //IsShowPopup = false;
        //    _mainViewModel.AddToSaleProductObservable(sale);
        //    //sale.ProductInfo.SubtractToAvailbleCount(sale.Count);
        //    SelectedSaleProduct = null;
        //    SelectedItemCarted.Invoke();
        //}
        **/
        _mainViewModel.AddToSaleProductObservable(sale);
        SelectedSaleProduct = null;
    }
}
