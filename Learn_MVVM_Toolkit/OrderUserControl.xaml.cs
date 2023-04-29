﻿using CommunityToolkit.Mvvm.DependencyInjection;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Learn_MVVM_Toolkit;

/// <summary>
/// Interaction logic for OrderUserControl.xaml
/// </summary>
public partial class OrderUserControl : UserControl
{
    public OrderUserControl()
    {

        DataContext = Ioc.Default.GetService<OrderUserControlViewModel>();
        InitializeComponent();
    }
}
