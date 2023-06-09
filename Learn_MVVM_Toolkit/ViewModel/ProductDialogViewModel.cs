using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Learn_MVVM_Toolkit.Builder;
using Learn_MVVM_Toolkit.Message;
using Learn_MVVM_Toolkit.ObservableObjects;
using Learn_MVVM_Toolkit.Service;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit.ViewModel;

public partial class ProductDialogViewModel : ObservableObject, IDialogRequestClose
{
    private string _name;

    private double _cost;

    private int _count;

    private ObservableCollection<string> _category;

    private double _price;

    private string _description;

    private string _selectedCategory;
    private IDataBaseModel DataBaseService { get; }

    private IMessenger MessengerService { get; }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public double Cost
    {
        get => _cost;
        set
        {
            if (SetProperty(ref _cost, value))
                AddProductItemCommand.NotifyCanExecuteChanged();

        }
    }

    public int Count
    {
        get => _count;
        set
        {
            if (SetProperty(ref _count, value))
                AddProductItemCommand.NotifyCanExecuteChanged();
        }
    }

    public double Price
    {
        get => _price;
        set => SetProperty(ref _price, value);
    }

    public ObservableCollection<string> Category
    {
        get => _category;
        set => SetProperty(ref _category, value);
    }
    public string SelectedCategory
    {
        get => _selectedCategory;
        set => SetProperty(ref _selectedCategory, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }
    public ProductObservable SelectedProductOB { get; }

    public event EventHandler<DialogCloseRequestEventArgs> CloseRequest;

    public IAsyncRelayCommand AddProductItemCommand { get; }
    public IRelayCommand UpdateProductItemCommand { get; }

    public ProductDialogViewModel(ProductObservable productObservable)
    {
        SelectedProductOB = productObservable;

        MessengerService = Ioc.Default.GetService<IMessenger>();

        DataBaseService = Ioc.Default.GetService<IDataBaseModel>();

        AddProductItemCommand = new AsyncRelayCommand(AddProductItem);
        UpdateProductItemCommand = new RelayCommand(UpdateProductItem);

        Category = DataBaseService.GetAllCategory();

        //SetDefault();


        if (productObservable != null)
        {
            Name = productObservable.Name;
            Cost = productObservable.Cost;
            Count = productObservable.Count;
            Price = productObservable.Price;
            SelectedCategory = productObservable.Category;
            Description = productObservable.Info.Description;
        }
    }

    private void UpdateProductItem()
    {
        // udpate item from the database
        // 
        
        
    }
    public void SetDefault()
    {
        Name = "Product Name";
        Cost = 0;
        Price = 0;
        Count = 0;
        Description = "Product Description";
    }


    private async Task AddProductItem()
    {
        ProductBuilder builder = new ProductBuilder();
        builder.SetName(Name).SetCount(Count).SetCost(Cost).SetPrice(Price).SetCategory(SelectedCategory).
            InfoBuilder.SetDescription(Description);

        Product p = builder.Build();

        //Task<Product> insertProductTask = InsertAsync(p);

        var productResult = await InsertAsync(p);

        if (productResult != null)
        {
            //TODO: CLOSE DIALOG
            MessengerService.Send(new AddProductObMessage(productResult));
            
            CloseRequest.Invoke(this, new DialogCloseRequestEventArgs(false)); 

        }
    }

    private async Task<Product> InsertAsync(Product p)
    {
        Product tempProduct = null;

        await Task.Run(() =>
        {
            tempProduct = DataBaseService.InsertProductTransaction(p);
            //Task.Delay(5000).Wait();  
        });

        return tempProduct;
    }

}
