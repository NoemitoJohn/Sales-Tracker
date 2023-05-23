using CommunityToolkit.Mvvm.DependencyInjection;
using Learn_MVVM_Toolkit.Util;
using Learn_MVVM_Toolkit.ViewModel;
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

namespace Learn_MVVM_Toolkit.Dialog;




public partial class SaleInfoDialog : Window
{
    private SaleInfoDialogViewModel viewModel;


    public SaleInfoDialog()
    {
        InitializeComponent();
        DataContext = viewModel = Ioc.Default.GetService<SaleInfoDialogViewModel>();
        SoldHeader.ItemsSource = viewModel.Sale;
        SizeChanged += (e, s) => { Debug.Log("Width: " + Width); Debug.Log("Height: " + Height); };

//      Width: 675
//      Height: 495

    }


}
