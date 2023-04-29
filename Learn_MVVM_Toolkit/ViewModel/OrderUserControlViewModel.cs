using CommunityToolkit.Mvvm.ComponentModel;
using Learn_MVVM_Toolkit.ObservableObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit.ViewModel;

public class OrderUserControlViewModel : ObservableObject
{
	MainWindowViewModel mainModel;

    public ObservableCollection<SaleProductObservable> SaleProducts => mainModel.SaleProducts;


    public OrderUserControlViewModel(MainWindowViewModel mainModel)
	{
		this.mainModel = mainModel;
	}
}
