using CommunityToolkit.Mvvm.ComponentModel;
using Learn_MVVM_Toolkit.ObservableObjects;
using Learn_MVVM_Toolkit.Service;
using System;
using System.Collections.ObjectModel;

namespace Learn_MVVM_Toolkit.ViewModel;

public class SaleInfoDialogViewModel : ObservableObject , IDialogRequestClose
{
    public event EventHandler<DialogCloseRequestEventArgs> CloseRequest;

    public ObservableCollection<OrderObservable> Sale { get; }

	//TODO: create a method to calculate the total of the list items
    public SaleInfoDialogViewModel(ObservableCollection<OrderObservable> orders)
	{
		Sale = orders;
	}
}
