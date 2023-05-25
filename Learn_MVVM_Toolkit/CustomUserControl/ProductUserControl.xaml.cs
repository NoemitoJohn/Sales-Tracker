using CommunityToolkit.Mvvm.DependencyInjection;
using Learn_MVVM_Toolkit.Dialog;
using Learn_MVVM_Toolkit.ObservableObjects;
using Learn_MVVM_Toolkit.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Learn_MVVM_Toolkit.CustomUserControl;

/// <summary>
/// Interaction logic for ProductUserControl.xaml
/// </summary>
public partial class ProductUserControl : UserControl
{
    private Window mainWindow;
    private ScrollViewer scrollveiwer;
    private ProductUserControlViewModel viewModel;
    private SelectedProductDialog itemInfoDialog;
    public ProductUserControl()
    {
        // get the content of the content control
        InitializeComponent();
        mainWindow = App.Current.MainWindow;
        DataContext = viewModel = Ioc.Default.GetService<ProductUserControlViewModel>();
        // get the pop up control
        
        mainWindow.SizeChanged += MainWindow_SizeChanged_Handler;
        PreviewKeyDown += PreviewKeyDown_Handler;
        mainWindow.StateChanged += MainWindow_StateChanged_Handler;
        

    }

    // MainWindow Check State if minimize
    private void MainWindow_StateChanged_Handler(object sender, EventArgs e)
    {
        if (mainWindow == null) return;
        if (mainWindow.WindowState == WindowState.Minimized && OrderPopup.IsOpen == true)
            OrderPopup.IsOpen = false;

    }

    // Window Resize Callback
    private void MainWindow_SizeChanged_Handler(object sender, SizeChangedEventArgs e)
    {
        if (scrollveiwer == null)
            scrollveiwer = (ScrollViewer)ItemListing.Template.FindName("ScrollViewPresenter", ItemListing);
        
        var presenter = (ItemsPresenter)scrollveiwer.Content;
        Header.Width = presenter.ActualWidth;
    }

    private void PreviewKeyDown_Handler(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            if (OrderPopup.IsOpen == true)
                OrderPopup.IsOpen = false;
        }
        
    }

    private void AddCartButton(object sender, RoutedEventArgs e)
    {
        ProductUserControlViewModel context = (ProductUserControlViewModel)DataContext;
        SaleProductObservable selected = context.SelectedSaleProduct;

        if(selected.Count > 0 && selected.Count <= selected.Available) OrderPopup.IsOpen = false;
        
    }



}
