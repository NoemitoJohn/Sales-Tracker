using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Learn_MVVM_Toolkit.Util;

public interface IComboBoxView
{
    
}

public interface IComboBoxViewModel
{
    public ObservableCollection<string> ComboBoxItems { get; set; }

    void OnComboBoxSelected(string val); 
}

public interface IComboxBoxManager
{
    void Create(ComboBox comboBox, IComboBoxViewModel viewModel);
}

public class ComboBoxManager : IComboxBoxManager
{
    // ComboBox Object 
    // ViewModel 
    public void Create(ComboBox comboBox, IComboBoxViewModel viewModel)
    {
        if(viewModel.ComboBoxItems == null) throw new NullReferenceException();

        comboBox.ItemsSource = viewModel.ComboBoxItems;
        
        comboBox.SelectedIndex = 1;

        viewModel.OnComboBoxSelected(comboBox.SelectedItem.ToString().ToLower());

        comboBox.SelectionChanged += (sender, e) =>
        {
            string result = e.AddedItems[0].ToString().ToLower();
            viewModel.OnComboBoxSelected(result);
        };
    }

    
}

