using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Learn_MVVM_Toolkit.ObservableObjects;
using Learn_MVVM_Toolkit.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit.ViewModel;

public class SelectedProductDialogViewModel : ObservableObject, IDialogRequestClose
{
    public IRelayCommand OkCommand { get; }
    public IRelayCommand CancelCommand { get; }

    public event EventHandler<DialogCloseRequestEventArgs> CloseRequest;
    public SaleProductObservable SelectedItem { get; }

    public SelectedProductDialogViewModel(SaleProductObservable selectedItem)
    {
        OkCommand = new RelayCommand(() =>
        {
            if (SelectedItem.Count > 0 && SelectedItem.Count <= SelectedItem.Available)
                CloseRequest?.Invoke(this, new DialogCloseRequestEventArgs(true));

        });

        CancelCommand = new RelayCommand(() => { CloseRequest?.Invoke(this, new DialogCloseRequestEventArgs(false)); });
        SelectedItem = selectedItem;
    }






}
