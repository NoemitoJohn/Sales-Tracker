using System.Windows;

namespace Learn_MVVM_Toolkit.Util;

public class UserControlManager : IUserControlManager
{
    Window mainWindow;
    
    IUserControl userControl;
    
    public UserControlManager(Window mainWindow, IUserControl userControl, object viewModel)
    {
        this.userControl = userControl;
        this.mainWindow = mainWindow;
        userControl.DataContext = viewModel;

        this.userControl.OnDataContextLoaded(this.mainWindow, viewModel);    

    }

    public IUserControl GetUserControl() => userControl;
}
public interface IUserControl
{
    public object DataContext { get; set; }
    void OnDataContextLoaded(Window window, object dataContext);
    
}

public interface IUserControlManager
{
    IUserControl GetUserControl();
}
