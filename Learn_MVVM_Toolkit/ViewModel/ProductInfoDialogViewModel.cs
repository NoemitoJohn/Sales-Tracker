using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Learn_MVVM_Toolkit.Message;
using Learn_MVVM_Toolkit.ObservableObjects;
using Learn_MVVM_Toolkit.Service;
using Learn_MVVM_Toolkit.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace Learn_MVVM_Toolkit.ViewModel;

public class ProductInfoDialogViewModel : ObservableObject, IDialogRequestClose, IRecipient<AddProductObMessage>
{
    private string _selectedOrderBy;
    private string _selectedCategory;
    private List<ProductObservable> productTemp;

    private ObservableCollection<ProductObservable> _products;
    private IDialogService dialogService;
    private IMessenger MessengerService { get; }
    public ObservableCollection<ProductObservable> Products
    {
        get => _products;
        set => SetProperty(ref _products, value);
    }

    public ObservableCollection<string> ComboBoxItems { get; set; }
    public ObservableCollection<string> CategoryItemsSource { get; set; } = new();

    public string SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            if(SetProperty(ref _selectedCategory, value))
            {
                GetProductCategory(value);
            }
        }
    }

    public string SelectedOrderBy
    {
        get => _selectedOrderBy;
        set => SetProperty(ref _selectedOrderBy, value);
    }


    private string _searchText;

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public IRelayCommand AddProductCommand { get; set; }

    public IRelayCommand SearchProductCommand { get; set; }

    public IRelayCommand<ProductObservable> UpdateProductCommand { get; set; }

    public ObservableCollection<string> OrderByItemsSource { get; set; } = new();

    public event EventHandler<DialogCloseRequestEventArgs> CloseRequest;

    public ProductInfoDialogViewModel(ObservableCollection<ProductObservable> products)
    {

        SearchProductCommand = new RelayCommand(SearchProduct);
        AddProductCommand = new RelayCommand(AddProduct);
        UpdateProductCommand = new RelayCommand<ProductObservable>(UpdateProduct);
        
        dialogService = Ioc.Default.GetService<IDialogService>();
        MessengerService = Ioc.Default.GetService<IMessenger>();

        MessengerService.Register<AddProductObMessage>(this);

        
        productTemp = products.ToList();

        CategoryItemsSource.Add("ALL");
        CategoryItemsSource.Add("Category 2");
        CategoryItemsSource.Add("Category 3");

        OrderByItemsSource.Add("NONE");
        OrderByItemsSource.Add("Count");
        OrderByItemsSource.Add("Cost");
        OrderByItemsSource.Add("Price");

        SelectedCategory = CategoryItemsSource[0];
        SelectedOrderBy = OrderByItemsSource[0];

        Products = new(productTemp);
    }

    private void AddProduct()
    {
        
        var vModel = new ProductDialogViewModel(null);

        bool? dialogResult = dialogService.ShowDialog(vModel, this);

    }


    private void UpdateProduct(ProductObservable p)
    {
        
        var vModel = new ProductDialogViewModel(p);
    
        bool? dialogResult = dialogService.ShowDialog(vModel, this);
    
    }


    private void GetProductCategory(string value)
    {
        List<ProductObservable> catProductSorted = CategorySort(value);
        
        if (!string.IsNullOrEmpty(SearchText))
        {
            Products = new ObservableCollection<ProductObservable>(SearchSort(catProductSorted));
            return;
        }

        
        Products = new ObservableCollection<ProductObservable>(catProductSorted);
    }

    private List<ProductObservable> CategorySort(string category)
    {
        List<ProductObservable> categories = new();


        if (category.ToLower() == CategoryItemsSource[0].ToLower())
        {
            categories = productTemp;
            
        }

        foreach (var item in productTemp)
        {
            if (item.Category.ToLower() == category.ToLower())
            {
                categories.Add(item);
            }
        }
         
        return categories;
    }



    private void SearchProduct()
    {
        var products = CategorySort(SelectedCategory);

        if (string.IsNullOrEmpty(SearchText))
        {

            Products = new(products);
            return;
        }

        Products = new ObservableCollection<ProductObservable>(SearchSort(products));
    }

    private List<ProductObservable> SearchSort(List<ProductObservable> products)
    {
        List<ProductObservable> result = new();

        foreach (var item in products)
        {
            if (item.Name.ToLower().Contains(SearchText.ToLower()))
            {
                result.Add(item);
            }
        }

        return result;
    }


    public void Receive(AddProductObMessage message)
    {
        ProductObservable p = new(message.Value);
        productTemp.Add(p);
        Products = new(productTemp);
    }
}
