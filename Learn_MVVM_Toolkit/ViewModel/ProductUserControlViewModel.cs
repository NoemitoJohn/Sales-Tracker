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

public partial class ProductUserControlViewModel : ObservableObject
{
    MainWindowViewModel mainViewModel;

    public ObservableCollection<Product> Products => mainViewModel.Products;

    [ObservableProperty]
    private bool _isShowPopup;

    [ObservableProperty]
    private SaleProductObservable _selectedProduct;


    public ProductUserControlViewModel(MainWindowViewModel mainViewModel)
    {
        this.mainViewModel = mainViewModel;
        IsShowPopup = false; 
    }


    [RelayCommand]
    public void AddProduct(Product product)
    {

        SelectedProduct = new SaleProductObservable(product);
        IsShowPopup = true;
    }

    [RelayCommand]
    public void AddCart()
    {
        IsShowPopup = false;

    }
}
