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
using System.Windows.Shapes;
using Learn_MVVM_Toolkit.ViewModel.Dialog;

namespace Learn_MVVM_Toolkit.Dialog;

/// <summary>
/// Interaction logic for OrderDialog.xaml
/// </summary>
public partial class OrderDialog : Window
{
    public OrderDialog(MainWindowViewModel mainViewModel, SaleProduct sale)
    {
        DataContext = new OrderDialogViewModel(mainViewModel, sale);

        InitializeComponent();

        testCheckBox.Click += TestCheckBox_Click;
    }

    private void TestCheckBox_Click(object sender, RoutedEventArgs e)
    {

    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }
}
