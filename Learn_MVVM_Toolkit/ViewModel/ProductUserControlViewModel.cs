using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using Learn_MVVM_Toolkit.Dialog;
using Learn_MVVM_Toolkit.ObservableObjects;
using Learn_MVVM_Toolkit.Service;
using Learn_MVVM_Toolkit.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Learn_MVVM_Toolkit.ViewModel;

public delegate void Notify();

public partial class ProductUserControlViewModel : ObservableObject, IComboBoxViewModel
{

    private string selectedCategory;

    private MainWindowViewModel _mainViewModel;
    private IDialogService dialogService { get; }
    public ObservableCollection<ProductObservable> Products1 { get; set; }

    private ObservableCollection<string> _comboBoxItems;

    private ObservableCollection<ProductObservable> _products;

    public ObservableCollection<ProductObservable> Products
    {
        get => _products;
        set => SetProperty(ref _products, value);
    }

    private SaleProductObservable _selectedSaleProduct;

    public SaleProductObservable SelectedSaleProduct
    {
        get => _selectedSaleProduct;
        set => SetProperty(ref _selectedSaleProduct, value);
    }

    public IRelayCommand AddProductCommand { get; }
    public IRelayCommand AddCartCommand { get; }


    private string _nameSearch;

    public string NameSearch
    {
        get => _nameSearch;
        set
        {

            if (SetProperty(ref _nameSearch, value))
            {
                if (SearchCommand.CanExecute(value))
                    SearchCommand.Execute(value);
            }
        }
    }

    private string[] _category;

    public string[] Category
    {
        get => _category;
        set => SetProperty(ref _category, value);
    }

    public MainWindowViewModel MainViewModel => _mainViewModel;

    public IList<ProductObservable> productTemp;
    private IRelayCommand<string> SearchCommand { get; }

    public ObservableCollection<string> ComboBoxItems
    {
        get => _comboBoxItems;
        set => SetProperty(ref _comboBoxItems, value);
    }

    public ProductUserControlViewModel(MainWindowViewModel mainViewModel, IDialogService dialogService)
    {
        _mainViewModel = mainViewModel;
        Category = new string[6];

        Category[0] = "NONE";
        Category[1] = "Category 1";
        Category[2] = "Category 2";
        Category[3] = "Category 3";
        Category[4] = "Category 4";
        Category[5] = "Category 5";

        selectedCategory = "none";

        ComboBoxItems = new ObservableCollection<string>(Category);

        productTemp = new List<ProductObservable>(_mainViewModel.Products.ToList());

        Products = new ObservableCollection<ProductObservable>(productTemp);

        SearchCommand = new RelayCommand<string>(Search, (string value) => value.Length >= 3 || value.Length == 0);

        AddProductCommand = new RelayCommand<ProductObservable>(AddProduct);

        AddCartCommand = new RelayCommand<SaleProductObservable>(AddCart);

        this.dialogService = dialogService;


    }

    private void Search(string value)
    {

        if (selectedCategory == "none")
        {
            var searchAll = productTemp.Where(p => p.Name.ToLower().Contains(value.ToLower()));

            if (searchAll.Any())
            {
                Products = new ObservableCollection<ProductObservable>(searchAll);
            }
        }
        else
        {
            var searchAllCategory = productTemp.Where(p => p.Name.ToLower().Contains(value.ToLower()) && p.Category.ToLower() == selectedCategory);

            if (searchAllCategory.Any())
            {
                Products = new ObservableCollection<ProductObservable>(searchAllCategory);
            }
        }


    }

    //Convert the selected ProductObservable into SaleProductObservable
    private void AddProduct(ProductObservable product)
    {
        if (_mainViewModel.SaleExist(product.GetIndex()) == true) return;

        SelectedSaleProduct = new SaleProductObservable(product);
        // if product is already exist in sales list disable add button????

        SelectedProductDialogViewModel viewModel = new(SelectedSaleProduct);

        bool? result = dialogService.ShowDialog(viewModel, this);
        // Show dialog
        if (result.HasValue)
        {
            if (result.Value)
            {
                AddCart(viewModel.SelectedItem);
            }
        }
    }

    public void AddCart(SaleProductObservable sale)
    {
        _mainViewModel.AddToSaleProductObservable(sale);
        SelectedSaleProduct = null;
    }


    public void OnComboBoxSelected(string val)
    {
        selectedCategory = val;

        if (val == "none")
        {
            Products = new ObservableCollection<ProductObservable>(productTemp);
            return;
        }

        var cat = productTemp.Where(p => p.Category.ToLower().Contains(val.ToLower()));

        if (cat.Any())
        {
            Products = new ObservableCollection<ProductObservable>(cat);
        }
    }
}
