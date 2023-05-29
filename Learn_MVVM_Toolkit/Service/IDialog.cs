using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Learn_MVVM_Toolkit.Service;

public interface IDialog
{
    object DataContext { get; set; }
    bool? DialogResult { get; set; }
    Window Owner { get; set; }

    void Close();
    bool? ShowDialog();

    void OnDataContextLoaded(object sender, object viewModel);

}
public interface IDialogService
{
    void SetWindowOwner(Window owner);
    void Register<TViewModel, TView>() where TViewModel : IDialogRequestClose
                                       where TView : IDialog;

    bool? ShowDialog<TViewModel>(TViewModel viewModel, object sender) where TViewModel : IDialogRequestClose;
}
public interface IDialogRequestClose
{
    event EventHandler<DialogCloseRequestEventArgs> CloseRequest;
}

public class DialogCloseRequestEventArgs : EventArgs
{
    public bool? DialogResult { get; }
    public DialogCloseRequestEventArgs(bool? result)
    {
        DialogResult = result;
    }
}
public class DialogService : IDialogService
{
    private  Window owner;
    private IDictionary<Type, Type> _mappings;

    public void SetWindowOwner(Window window)
    {
        owner = window;
        _mappings = new Dictionary<Type, Type>();
    }

    
    public void Register<TViewModel, TView>() where TViewModel : IDialogRequestClose where TView : IDialog
    {
        if(owner == null)
        {
            throw new ArgumentNullException(nameof(owner));
        }

        if(_mappings.ContainsKey(typeof(TViewModel)))
        {
            throw new ArgumentException($"Type {typeof(TViewModel)} is already mapped to {typeof(TView)}");
        }
        
        _mappings.Add(typeof(TViewModel), typeof(TView));

    }

    public bool? ShowDialog<TViewModel>(TViewModel viewModel, object sender) where TViewModel : IDialogRequestClose
    {
        Type viewType = _mappings[typeof(TViewModel)];

        IDialog dialog = (IDialog)Activator.CreateInstance(viewType);
            
        EventHandler<DialogCloseRequestEventArgs> handler = null;

        handler = (sender, e) =>
        {
            viewModel.CloseRequest -= handler;

            if (e.DialogResult.HasValue)
            {
                dialog.DialogResult = e.DialogResult;
            }
            else
            {
                dialog.Close();
            }
        };

        viewModel.CloseRequest += handler;

        dialog.DataContext = viewModel;
        dialog.Owner = owner;

        dialog.OnDataContextLoaded(sender, viewModel);


        return dialog.ShowDialog();

    }
    
}


