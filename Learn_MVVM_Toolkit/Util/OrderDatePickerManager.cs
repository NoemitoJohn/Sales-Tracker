using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Learn_MVVM_Toolkit.Util;

public interface IOrderDatePickerViewModel
{
	public DateTime DateFrom { get; set; }
	public DateTime DateTo { get; set; }


}
public interface IOrderDatePickerView
{

}

public class OrderDatePickerManager
{
	EventHandler<SelectionChangedEventArgs> selectionChangeDateFrom;
	EventHandler<SelectionChangedEventArgs> selectionChangeDateTo;
	private DatePicker datePickerFrom;
	private DatePicker datePickerTo;


    public OrderDatePickerManager(IOrderDatePickerViewModel viewModel ,DatePicker datePickerFrom, DatePicker datePickerTo)
	{
		this.datePickerFrom = datePickerFrom;
		this.datePickerTo = datePickerTo;
		
		selectionChangeDateFrom = (sender, e) =>
		{
			viewModel.DateFrom = (DateTime)e.AddedItems[0];

		};
		selectionChangeDateTo = (sender, e) =>
		{
            viewModel.DateTo = (DateTime)e.AddedItems[0];
        };

		datePickerFrom.SelectedDateChanged += selectionChangeDateFrom;
		
		datePickerTo.SelectedDateChanged += selectionChangeDateTo;
	}
	public void Disposed()
	{
		this.datePickerFrom.SelectedDateChanged -= selectionChangeDateFrom;
		this.datePickerTo.SelectedDateChanged -= selectionChangeDateTo;
	}
}
