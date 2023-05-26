using Learn_MVVM_Toolkit.Service;
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

    public partial class SelectedProductDialog : Window, IDialog
    {
        public SelectedProductDialog()
        {
            InitializeComponent();

            Loaded += WindowLoaded;
            
        }

        public void OnDataContextLoaded(object sender, object viewModel)
        {
            if (sender is ProductUserControlViewModel)
            {
                Confirm.Content = "Cart";
            }
            if (sender is OrderUserControlViewModel)
            {
                Confirm.Content = "Save";
            }
            
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            QtyTextBox.CaretIndex = QtyTextBox.Text.Length;
        }
    }
}
