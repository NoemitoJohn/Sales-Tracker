using Learn_MVVM_Toolkit.Service;
using Learn_MVVM_Toolkit.ViewModel;
using System.Windows;

namespace Learn_MVVM_Toolkit.Dialog;

public partial class SaleInfoDialog : Window, IDialog
{
    //private SaleInfoDialogViewModel viewModel;
    public SaleInfoDialog()
    {
        InitializeComponent();

        // SizeChanged += (e, s) => { Debug.Log("Width: " + Width); Debug.Log("Height: " + Height); };
    }

    public void OnDataContextLoaded(object sender, object viewModel)
    {
        var vModel = (SaleInfoDialogViewModel)viewModel;
        SoldHeader.ItemsSource = vModel.Sale;
    }
}
