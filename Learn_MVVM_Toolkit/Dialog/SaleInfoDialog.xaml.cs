using Learn_MVVM_Toolkit.Service;
using Learn_MVVM_Toolkit.Util;
using Learn_MVVM_Toolkit.ViewModel;
using System;
using System.Windows;

namespace Learn_MVVM_Toolkit.Dialog;

public partial class SaleInfoDialog : Window, IDialog
{
    private SaleInfoDialogViewModel vModel;
    

    public SaleInfoDialog()
    {
        InitializeComponent();  
    }

    public void OnDataContextLoaded(object sender, object viewModel)
    {
        vModel = (SaleInfoDialogViewModel)viewModel;
        
    }
}
