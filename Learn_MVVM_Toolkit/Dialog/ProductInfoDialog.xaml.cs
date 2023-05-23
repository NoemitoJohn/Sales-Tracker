using CommunityToolkit.Mvvm.DependencyInjection;
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

/// <summary>
/// Interaction logic for ProductInfoDialog.xaml
/// </summary>
public partial class ProductInfoDialog : Window
{
    ScrollViewer scrollViewer;


    public ProductInfoDialog()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetService<ProductInfoDialogViewModel>();
        SizeChanged += ProductInfoDialog_SizeChanged_Handler;

    }

    private void ProductInfoDialog_SizeChanged_Handler(object sender, SizeChangedEventArgs e)
    {
        if (scrollViewer == null)
            scrollViewer = (ScrollViewer)ItemControlListing.Template.FindName("ScrollViewPresenter", ItemControlListing);

        var presenter = scrollViewer.Content as ItemsPresenter;
        GridHeader.Width = presenter.ActualWidth;

    }
}
