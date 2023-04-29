using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Learn_MVVM_Toolkit.ObservableObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Learn_MVVM_Toolkit.ViewModel;

public partial class ProductUserControlViewModel : ObservableObject
{
    private MainWindowViewModel mainViewModel;
    public ObservableCollection<ProductObservable> Products => mainViewModel.Products;

    private bool _isShowPopup;

    //TODO: Improve this make sure that there is no UI ralated Properties HEREEEEEEE!!!!
    public bool IsShowPopup 
    {
        get => _isShowPopup;
        set => SetProperty(ref _isShowPopup, value);
    }

    private SaleProductObservable _selectedSaleProduct;
    public SaleProductObservable SelectedSaleProduct 
    {
        get => _selectedSaleProduct;
        set => SetProperty(ref _selectedSaleProduct, value);
    }

    public IRelayCommand AddProductCommand { get; }
    public IRelayCommand AddCartCommand { get; }
        
    public ProductUserControlViewModel(MainWindowViewModel mainViewModel)
    {
        this.mainViewModel = mainViewModel;
        
        IsShowPopup = false;

        AddProductCommand = new RelayCommand<ProductObservable>(AddProduct);
        AddCartCommand = new RelayCommand<SaleProductObservable>(AddCart);
    }

    private void AddProduct(ProductObservable product)
    {

        SelectedSaleProduct = new SaleProductObservable(product);

        IsShowPopup = true;
    }
    public void AddCart(SaleProductObservable sale)
    {
        // TODO: Get the OrderUserControlViewModel object and add to selected item
 
        if (sale.Count <= sale.GetProductAvailableCount())
        {
            IsShowPopup = false;
            mainViewModel.AddToSaleProductObservable(sale);
            sale.GetProduct().SubtractToAvailbleCount(sale.Count);
        }
    }
}
