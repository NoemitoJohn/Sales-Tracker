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
    private Popup popup;

    public ProductUserControl()
    {
        
        // get the content of the content control
        InitializeComponent();
        mainWindow = App.Current.MainWindow;

        ProductInfoPopUp controlPopup = new ProductInfoPopUp(DataContext);
        ContentControlPopUp.Content = controlPopup;
        popup = (Popup)controlPopup.FindName("PopUp");

        // get the pop up control

        mainWindow.SizeChanged += MainWindow_SizeChanged_Handler;
        PreviewKeyDown += PreviewKeyDown_Handler;
        mainWindow.StateChanged += MainWindow_StateChanged_Handler;
    }


    // MainWindow Check State if minimize
    private void MainWindow_StateChanged_Handler(object sender, EventArgs e)
    {
        if (mainWindow == null) return;
        if (mainWindow.WindowState == WindowState.Minimized && popup.IsOpen == true)
            popup.IsOpen = false;

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
            if (popup.IsOpen == true)
                popup.IsOpen = false;
        }
    }

    private void ShowPopupWindowHandler(object sender, RoutedEventArgs e)
    {

        popup.DataContext = DataContext;
        popup.PlacementTarget = ((Button)sender);
        popup.Placement = PlacementMode.Bottom;
        popup.HorizontalOffset = -(popup.Width - ((Button)sender).ActualWidth);
        popup.IsOpen = true;

    }

    //TODO: Make the floating window close if the user click out side the floating window (Main Window)


}
