﻿using Learn_MVVM_Toolkit.Dialog;
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
using Learn_MVVM_Toolkit.CustomUserControl;


namespace Learn_MVVM_Toolkit;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    private MainWindowViewModel mainViewModel;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = mainViewModel = new MainWindowViewModel();
        
        OrderControl.Content = new OrderUserControl { DataContext = this.DataContext };

        ProductListingControl.Content = new ProductUserControl { DataContext = this.DataContext };

        mainViewModel.ShowAddToOrderDailog += Model_ShowAddToOrderDailog;

    }

    private void Model_ShowAddToOrderDailog(MainWindowViewModel mainViewModel, SaleProduct sale)
    {
        var dialog = new OrderDialog(mainViewModel, sale);
        dialog.Owner = this;
        
        dialog.ShowDialog();

        
    }
}
