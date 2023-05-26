using CommunityToolkit.Mvvm.ComponentModel;
using Learn_MVVM_Toolkit.ObservableObjects;
using Learn_MVVM_Toolkit.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_MVVM_Toolkit.ViewModel;

public class ProductInfoDialogViewModel : ObservableObject, IDialogRequestClose
{
    public ObservableCollection<ProductObservable> Products { get; }
   
    public event EventHandler<DialogCloseRequestEventArgs> CloseRequest;

    public ProductInfoDialogViewModel(ObservableCollection<ProductObservable> products)
    {
        Products = products;
    }

    
}
