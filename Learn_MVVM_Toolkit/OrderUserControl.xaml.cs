using CommunityToolkit.Mvvm.DependencyInjection;
using Learn_MVVM_Toolkit.Dialog;
using Learn_MVVM_Toolkit.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Learn_MVVM_Toolkit;

/// <summary>
/// Interaction logic for OrderUserControl.xaml
/// </summary>
public partial class OrderUserControl : UserControl
{
    private Window mainWindow;
    private OrderUserControlViewModel viewModel;

    private ScrollViewer scrollViewer;
    public ScrollViewer ScrollViewer  => scrollViewer;
    public OrderUserControl()
    {

        InitializeComponent();
        Init();
    
    }


    private void Init()
    {
        DataContext = viewModel = Ioc.Default.GetService<OrderUserControlViewModel>();

        mainWindow = Application.Current.MainWindow;

        mainWindow.SizeChanged += MainWindow_SizeChanged_Handler;
        viewModel.PopUpClicked += ViewModel_PopUpClicked;
        viewModel.ShowDailogItemInfo += ViewModel_ShowDailogItemInfo;
    }

    private void ViewModel_ShowDailogItemInfo(object sender, System.EventArgs e)
    {
        //var dialog = new SelectedProductDialog(sender);
        //dialog.Owner = App.Current.MainWindow;
        //dialog.ShowDialog();
    }

    private void ViewModel_PopUpClicked()
    {
        if (ItemSelectedPopup.IsOpen == true) ItemSelectedPopup.IsOpen = false;
    }

    private void MainWindow_SizeChanged_Handler(object sender, SizeChangedEventArgs e)
    {
        if (scrollViewer == null)
            scrollViewer = (ScrollViewer)SalesItemControl.Template.FindName("ScrollViewPresenter", SalesItemControl);

        ItemsPresenter presenter = (ItemsPresenter)scrollViewer.Content;
        SaleGridHeader.Width = presenter.ActualWidth;
    }

    private void ItemClickedHandler(object sender, RoutedEventArgs e)
    {
        ItemSelectedPopup.IsOpen = false;

        Button itemButton = (Button)sender;
        double width = itemButton.ActualWidth;

        Border popupContent = (Border)ItemSelectedPopup.Child; // get PopUp Content 
        //PopUp placement
        ItemSelectedPopup.PlacementTarget = itemButton;
        ItemSelectedPopup.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
        ItemSelectedPopup.IsOpen = true;
        ItemSelectedPopup.HorizontalOffset = (width - popupContent.ActualWidth) - 2;
        
    }
}
