using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using Learn_MVVM_Toolkit.ObservableObjects;
using Learn_MVVM_Toolkit.Service;
using Learn_MVVM_Toolkit.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Learn_MVVM_Toolkit.Service.DataBaseModel;

namespace Learn_MVVM_Toolkit.ViewModel;

public partial class ProductDialogViewModel : ObservableObject
{
    private string _name;

    private double _price;

    private int _count;


    private IDataBaseModel dataBaseModel;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }


    public double Price
    {
        get => _price;
        set
        {
            if (SetProperty(ref _price, value))
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


    private MainWindowViewModel _mainViewModel;

    public IRelayCommand AddProductItemCommand { get; }

    public ProductDialogViewModel(MainWindowViewModel mainViewModel)
    {
        this._mainViewModel = mainViewModel;
        dataBaseModel = Ioc.Default.GetService<IDataBaseModel>();
        AddProductItemCommand = new RelayCommand(AddProductItem, CanAddItem);

    }
    private void AddProductItem()
    {
        Product productData = new Product(Name, Count, Price);

        Result r = dataBaseModel.InsertProduct(productData);

        // the problem is the new inserted product dont have an ID 
        if (r.IsSuccess)
        {   
            productData.ID = r.ID;

            ProductObservable productObservable = new ProductObservable(productData);
            _mainViewModel.AddToProductObservable(productObservable);
        }


    }

    private bool CanAddItem()
    {
        return !string.IsNullOrEmpty(Name) && Price > 0 && Count > 0;
    }

}
