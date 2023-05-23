using CommunityToolkit.Mvvm.DependencyInjection;
using Learn_MVVM_Toolkit.Service;
using Learn_MVVM_Toolkit.Util;
using Learn_MVVM_Toolkit.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Learn_MVVM_Toolkit;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    IDataBaseModel dataBaseModel; 

    public App()
    {
        Ioc.Default.ConfigureServices(CofigureServices());

        dataBaseModel = Ioc.Default.GetService<IDataBaseModel>();

        dataBaseModel.CreateDatabase();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        List<Product> p = ProductGenerator.CreateFakeProduct(1000);

        //for (int i = 0; i < p.Count; i++)
        //{
        //    dataBaseModel.InsertProductTransaction(p[i]);
        //}
        
    }

    

    public IServiceProvider CofigureServices()
    {
        var serviceCollection = new ServiceCollection();
        
        serviceCollection.AddSingleton<IDataBaseModel, DataBaseModel>();

        serviceCollection.AddScoped<MainWindowViewModel>();
        serviceCollection.AddTransient<ProductUserControlViewModel>();
        serviceCollection.AddTransient<OrderUserControlViewModel>();
        serviceCollection.AddTransient<ProductDialogViewModel>();
        serviceCollection.AddTransient<ProductInfoDialogViewModel>();
        serviceCollection.AddTransient<SaleInfoDialogViewModel>();


        return serviceCollection.BuildServiceProvider();
    }

}
