using CommunityToolkit.Mvvm.ComponentModel;
using Learn_MVVM_Toolkit.ObservableObjects;
using System.Collections.ObjectModel;

namespace Learn_MVVM_Toolkit.ViewModel;

public class SaleInfoDialogViewModel : ObservableObject
{
	
	private MainWindowViewModel _mainWindowViewModel;
	public ObservableCollection<OrderObservable> Sale => _mainWindowViewModel.Sale;

	//TODO: create a method to calculate the total of the list items

    public SaleInfoDialogViewModel(MainWindowViewModel mainViewModel)
	{
		_mainWindowViewModel = mainViewModel;


	}
}
