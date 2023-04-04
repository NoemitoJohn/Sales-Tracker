using Learn_MVVM_Toolkit.Dialog;
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

namespace Learn_MVVM_Toolkit;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        MainWindowViewModel model = (MainWindowViewModel)DataContext;
        // UserControl for Orders
        OrderControl.Content = new OrderUserControl { DataContext = this.DataContext };

        model.ShowAddToOrderDailog += Model_ShowAddToOrderDailog;

    }

    private void Model_ShowAddToOrderDailog(MainWindowViewModel mainViewModel, SaleProduct sale)
    {
        var dialog = new OrderDialog(mainViewModel, sale);
        dialog.Owner = this;
        
        dialog.ShowDialog();

        
    }
}
