using CommunityToolkit.Mvvm.DependencyInjection;
using Learn_MVVM_Toolkit.CustomUserControl;
using Learn_MVVM_Toolkit.Dialog;
using Learn_MVVM_Toolkit.ViewModel;
using System.Windows;
using System.Windows.Controls;

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
        //OrderContentControl.Content = new OrderUserControl { DataContext = orderViewodel };
        productUserControl = new ProductUserControl { DataContext = productViewModel };

        ProductListingControl.Content = productUserControl;

        SizeChanged += (s, e) => {
            OrderUserControl content = (OrderUserControl)OrderContentControl.Content;

            ItemsPresenter presenter = content.ScrollViewer.Content as ItemsPresenter;
            
            UserControl userControl = (UserControl)FooterUserControl.Content;
            Grid gridFooter =  (Grid) userControl.FindName("GridFooterLayout");
            gridFooter.Width = presenter.ActualWidth;
        };
    }

    private void ProductInfoDialogHandler(object sender, RoutedEventArgs e)
    {

        var prodcutDialogInfo = new ProductInfoDialog();
        
        prodcutDialogInfo.Owner = this;
        prodcutDialogInfo.ShowDialog();
    }

    private void SaleInfoDialogHandler(object sender, RoutedEventArgs e)
    {
        var saleDialogInfo = new SaleInfoDialog();
        saleDialogInfo.Owner = this;
        saleDialogInfo.ShowDialog();
    }
}
