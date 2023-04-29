using Learn_MVVM_Toolkit.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Learn_MVVM_Toolkit.ViewModel;

using Learn_MVVM_Toolkit.CustomUserControl;
using Learn_MVVM_Toolkit.Dialog;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace Learn_MVVM_Toolkit;

public partial class MainWindow : Window
{

    private MainWindowViewModel mainViewModel;

    ProductUserControl productUserControl;
    public MainWindow()
    {

        InitializeComponent();

        DataContext = Ioc.Default.GetService<MainWindowViewModel>();

        ProductUserControlViewModel productViewModel = Ioc.Default.GetService<ProductUserControlViewModel>();
        OrderUserControlViewModel orderViewodel = Ioc.Default.GetService<OrderUserControlViewModel>();

        //TODO: Edit this for later
        OrderControl.Content = new OrderUserControl { DataContext = orderViewodel };
        productUserControl = new ProductUserControl { DataContext = productViewModel };

        ProductListingControl.Content = productUserControl;

    }


    private void AddProductDialogHandler(object sender, RoutedEventArgs e)
    {
        AddProductDialog dialog = new AddProductDialog
        {
            //DataContext = Ioc.Default.GetService<ProductDialogViewModel>()
        };
        
        dialog.Owner = this;
        dialog.ShowDialog();
    }
}
