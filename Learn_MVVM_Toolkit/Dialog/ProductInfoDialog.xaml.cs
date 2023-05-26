using Learn_MVVM_Toolkit.Service;
using System.Windows;
using System.Windows.Controls;

namespace Learn_MVVM_Toolkit.Dialog;

public partial class ProductInfoDialog : Window, IDialog
{
    ScrollViewer scrollViewer;


    public ProductInfoDialog()
    {
        InitializeComponent();
        SizeChanged += SizeChanged_Handler;

    }

    public void OnDataContextLoaded(object sender, object viewModel)
    {
        
    }

    private void SizeChanged_Handler(object sender, SizeChangedEventArgs e)
    {
        if (scrollViewer == null)
            scrollViewer = (ScrollViewer)ItemControlListing.Template.FindName("ScrollViewPresenter", ItemControlListing);

        var presenter = scrollViewer.Content as ItemsPresenter;
        GridHeader.Width = presenter.ActualWidth;
    }
}
