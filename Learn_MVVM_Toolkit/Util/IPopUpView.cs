using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace Learn_MVVM_Toolkit.Util;

public interface IPopUpView
{
    public FrameworkElement PopUpTarget { get; set; }
    
}
public interface IPopUpMangager
{

    public void CreatePopUp<TPopUpView, TViewModel>(Popup popUp, TPopUpView view, TViewModel viewModel) where TPopUpView : IPopUpView where TViewModel : IPopUpViewModel;

}

public interface IPopUpViewModel
{
    event EventHandler<PopUpResultEventArgs> PopUpResult;
}

public class PopUpResultEventArgs : EventArgs
{
    public bool Result { get; set; }
    public PopUpResultEventArgs(bool result)
    {
        Result = result;
    }
}

public class PopupManager : IPopUpMangager
{
    EventHandler<PopUpResultEventArgs> PopupHandler;
    Window mainWindow;

    public PopupManager(Window main)
    {
        mainWindow = main;

    }

    public void CreatePopUp<TPopUpView, TViewModel>(Popup popUp, TPopUpView view, TViewModel viewModel) where TPopUpView : IPopUpView where TViewModel : IPopUpViewModel
    {
        if (mainWindow == null) throw new NullReferenceException();
        if (popUp == null) throw new ArgumentNullException();

        mainWindow.KeyDown += (sender, e) =>
        {
            if(e.Key == System.Windows.Input.Key.Escape)
                popUp.IsOpen = false;
        };

        PopupHandler = (sender, e) =>
        {
            if (e.Result == false)
            {
                popUp.IsOpen = e.Result;
                return;
            }

            if (view.PopUpTarget == null) throw new NullReferenceException();

            popUp.IsOpen = false;
            double width = view.PopUpTarget.ActualWidth;
            popUp.PlacementTarget = view.PopUpTarget;
            popUp.Placement = PlacementMode.Bottom;
            popUp.IsOpen = e.Result;
            FrameworkElement popUpContent = popUp.Child as FrameworkElement;

            popUp.HorizontalOffset = width - popUpContent.ActualWidth;

        };

        viewModel.PopUpResult += PopupHandler;
    }



}





