﻿using System;
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

namespace Learn_MVVM_Toolkit.CustomUserControl
{
    /// <summary>
    /// Interaction logic for OrderTotal.xaml
    /// </summary>
    public partial class OrderTotal : UserControl
    {
        public Grid FooterLayout => GridFooterLayout;
        public OrderTotal()
        {
            InitializeComponent();
            
        }
    }
}
