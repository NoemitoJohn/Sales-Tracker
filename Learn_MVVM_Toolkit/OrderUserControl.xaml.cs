using CommunityToolkit.Mvvm.DependencyInjection;
using Learn_MVVM_Toolkit.Dialog;
using Learn_MVVM_Toolkit.Util;
using Learn_MVVM_Toolkit.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Learn_MVVM_Toolkit;


public partial class OrderUserControl : UserControl , IPopUpView, IUserControl
{
    private Window mainWindow;
    public FrameworkElement PopUpTarget { get; set; }

    private ScrollViewer scrollViewer;
    public ScrollViewer ScrollViewer  => scrollViewer;
    public OrderUserControl()
    {
        InitializeComponent();
    }
    private void MainWindow_SizeChanged_Handler(object sender, SizeChangedEventArgs e)
    {
        scrollViewer ??= (ScrollViewer)SalesItemControl.Template.FindName("ScrollViewPresenter", SalesItemControl);

        ItemsPresenter presenter = (ItemsPresenter)scrollViewer.Content;
        SaleGridHeader.Width = presenter.ActualWidth;
    }

    private void ItemClickedHandler(object sender, RoutedEventArgs e)
    {
        PopUpTarget = sender as FrameworkElement;
    }

    public void OnDataContextLoaded(Window mainWindow, object dataContext)
    {
        this.mainWindow = mainWindow;
        
        mainWindow.SizeChanged += MainWindow_SizeChanged_Handler;

        PopupManager popUpMangager = new PopupManager(mainWindow);
        
        popUpMangager.CreatePopUp(ItemSelectedPopup, this, dataContext as IPopUpViewModel);
    }

}
