using CommunityToolkit.Mvvm.DependencyInjection;
using Learn_MVVM_Toolkit.Dialog;
using Learn_MVVM_Toolkit.ObservableObjects;
using Learn_MVVM_Toolkit.Util;
using Learn_MVVM_Toolkit.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Learn_MVVM_Toolkit.CustomUserControl;

public partial class ProductUserControl : UserControl, IUserControl
{
    private Window mainWindow;
    private ScrollViewer scrollveiwer;
    public ProductUserControl()
    {
        
        InitializeComponent();     
    }

    // Window Resize Callback
    private void MainWindow_SizeChanged_Handler(object sender, SizeChangedEventArgs e)
    {
        if (scrollveiwer == null)
            scrollveiwer = (ScrollViewer)ItemListing.Template.FindName("ScrollViewPresenter", ItemListing);
        
        var presenter = (ItemsPresenter)scrollveiwer.Content;
        Header.Width = presenter.ActualWidth;
    }

    public void OnDataContextLoaded(Window window, object dataContext)
    {
        mainWindow = window;
        
        mainWindow.SizeChanged += MainWindow_SizeChanged_Handler;

        ComboBoxManager categoryComboBox = new();
        categoryComboBox.Create(CategoryComboBox, dataContext as IComboBoxViewModel);
    }
}
