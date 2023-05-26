using CommunityToolkit.Mvvm.DependencyInjection;
using Learn_MVVM_Toolkit.CustomUserControl;
using Learn_MVVM_Toolkit.Util;
using Learn_MVVM_Toolkit.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Learn_MVVM_Toolkit;

public partial class MainWindow : Window
{

    private MainWindowViewModel mainViewModel;

    ProductUserControl productUserControl;

    public MainWindow()
    {

        InitializeComponent();

        DataContext = mainViewModel = Ioc.Default.GetService<MainWindowViewModel>();

        OrderUserControlViewModel orderViewodel = Ioc.Default.GetService<OrderUserControlViewModel>();
        OrderUserControl orderControlView = new();

        UserControlManager orderControl = new(this, orderControlView, orderViewodel);


        OrderContentControl.Content = orderControl.GetUserControl();

        productUserControl = new ProductUserControl();

        ProductListingControl.Content = productUserControl;
        
        SizeChanged += (s, e) => {
            OrderUserControl content = (OrderUserControl)OrderContentControl.Content;

            ItemsPresenter presenter = content.ScrollViewer.Content as ItemsPresenter;
            
            UserControl userControl = (UserControl)FooterUserControl.Content;
            Grid gridFooter =  (Grid) userControl.FindName("GridFooterLayout");
            gridFooter.Width = presenter.ActualWidth;
        };
        
    }

}
