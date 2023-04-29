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

namespace Learn_MVVM_Toolkit.Dialog
{
    /// <summary>
    /// Interaction logic for AddProductDialog.xaml
    /// </summary>
    public partial class AddProductDialog : Window
    {
        private readonly ProductDialogViewModel viewModel;

        public AddProductDialog()
        { 
            InitializeComponent();
            DataContext = viewModel = Ioc.Default.GetService<ProductDialogViewModel>();
            
            viewModel.Name = "Item 1";
            viewModel.Price = 1;
            viewModel.Count= 1;
        }


    }

}
