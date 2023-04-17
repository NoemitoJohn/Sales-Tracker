using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Learn_MVVM_Toolkit.CustomUserControl;

/// <summary>
/// Interaction logic for ProductUserControl.xaml
/// </summary>
public partial class ProductUserControl : UserControl
{
    public ProductUserControl()
    {
        InitializeComponent();
        QtyTextBox.SelectionLength= 5;
    }



    private void B_Click(object sender, RoutedEventArgs e)
    {
        OrderPopup.IsOpen = false;
        OrderPopup.PlacementTarget = ((Button)sender);
        OrderPopup.Placement = PlacementMode.Bottom;
        OrderPopup.HorizontalOffset = -(OrderPopup.Width - ((Button)sender).ActualWidth);



    }

    private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }
}
