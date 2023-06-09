using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Learn_MVVM_Toolkit.Message;
using Learn_MVVM_Toolkit.ObservableObjects;
using Learn_MVVM_Toolkit.Service;
using Learn_MVVM_Toolkit.Util;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;

namespace Learn_MVVM_Toolkit.ViewModel;

public delegate void Notify();

public partial class ProductUserControlViewModel : ObservableObject, IComboBoxViewModel, IRecipient<AddProductObMessage>, IRecipient<ProductObNewCountMessage>
{

    private string selectedCategory;

    private long delay = 1000;

    private SaleProductObservable _selectedSaleProduct;
    private IMessenger MessengerService { get; }

    Timer timer;
    private IDialogService DialogService { get; }
    public ObservableCollection<ProductObservable> Products1 { get; set; }

    private ObservableCollection<string> _comboBoxItems;

    private ObservableCollection<ProductObservable> _products;

    public ObservableCollection<ProductObservable> Products
    {
        get => _products;
        set => SetProperty(ref _products, value);
    }
    public SaleProductObservable SelectedSaleProduct
    {
        get => _selectedSaleProduct;
        set => SetProperty(ref _selectedSaleProduct, value);
    }
    public IRelayCommand AddProductCommand { get; }
    public IRelayCommand AddCartCommand { get; }

    private string _nameSearch;

    public string NameSearchText
    {
        get => _nameSearch;
        set
        {

            if (SetProperty(ref _nameSearch, value))
            {
                TimeToExecuteSearch(value);

                
            }
        }
    }

    private string[] _category;

    public string[] Category
    {
        get => _category;
        set => SetProperty(ref _category, value);
    }

    private MainWindowViewModel MainViewModel { get; }

    public IList<ProductObservable> productTemp;
    private IRelayCommand<string> SearchCommand { get; }

    public ObservableCollection<string> ComboBoxItems
    {
        get => _comboBoxItems;
        set => SetProperty(ref _comboBoxItems, value);
    }
   
    public ProductUserControlViewModel(MainWindowViewModel mainViewModel, IDialogService dialogService, IMessenger messengerService)
    {
        MessengerService = messengerService;
        MainViewModel = mainViewModel;
        DialogService = dialogService;
        
        Category = new string[6];

        Category[0] = "NONE";
        Category[1] = "Category 1";
        Category[2] = "Category 2";
        Category[3] = "Category 3";
        Category[4] = "Category 4";
        Category[5] = "Category 5"; 
        selectedCategory = "none";

        timer = new(delay);

        ComboBoxItems = new ObservableCollection<string>(Category);

        productTemp = new List<ProductObservable>(MainViewModel.Products.ToList());

        Products = new ObservableCollection<ProductObservable>(productTemp);

        SearchCommand = new RelayCommand<string>(Search, (string value) => value.Length >= 1 || value.Length == 0);

        AddProductCommand = new RelayCommand<ProductObservable>(AddProduct);

        AddCartCommand = new RelayCommand<SaleProductObservable>(AddCart);

        MessengerService.Register<AddProductObMessage>(this);
        MessengerService.Register<ProductObNewCountMessage>(this);

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
        if (MainViewModel.SaleExist(product.GetIndex()) == true) return;

        SelectedSaleProduct = new SaleProductObservable(product);               // obeject to send across
        // if product is already exist in sales list disable add button????

        SelectedProductDialogViewModel viewModel = new(SelectedSaleProduct);

        bool? result = DialogService.ShowDialog(viewModel, this);
        // Show dialog
        if (result.HasValue)
        {
            if (result.Value)
            {
                AddCart(viewModel.SelectedItem); // sender
            }
        }
    }

    public void AddCart(SaleProductObservable sale)
    {
        MessengerService.Send(new AddSaleProductObMessage(sale));
        SelectedSaleProduct = null;
    }
    
    void TimeToExecuteSearch(object value)
    {

        timer.Stop();
        
        timer.Start();
        
        ElapsedEventHandler handler = null;
        
        handler = (sender, s) =>
        {
            timer.Stop();
            timer.Elapsed -= handler;
            
            if (SearchCommand.CanExecute(value))
                SearchCommand.Execute(value);
            
        };

        timer.Elapsed += handler;


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

    public void Receive(AddProductObMessage message)
    {
        ProductObservable p = new(Products.Count, message.Value);
        productTemp.Add(p);
        Products = new ObservableCollection<ProductObservable>(productTemp);
    }

    public void Receive(ProductObNewCountMessage message)
    {
        Products[message.Value.ProductId].Count = message.Value.Count;
    }
}
