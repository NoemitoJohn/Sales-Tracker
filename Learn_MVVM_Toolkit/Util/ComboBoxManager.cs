using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Learn_MVVM_Toolkit.Util;

public interface IComboBoxView
{
    
}

public interface IComboBoxViewModel
{
    public ObservableCollection<string> ComboBoxItems { get; set; }

    void OnComboBoxSelected(string val); 
}

public interface IComboxBoxManagerP
{
    void Create(ComboBox comboBox, IComboBoxViewModel viewModel);
}

public class ComboBoxManager : IComboxBoxManagerP
{
    // ComboBox Object 
    // ViewModel 
    public void Create(ComboBox comboBox, IComboBoxViewModel viewModel)
    {
        if(viewModel.ComboBoxItems == null) throw new NullReferenceException();

        comboBox.ItemsSource = viewModel.ComboBoxItems;
        
        comboBox.SelectedIndex = 0;

        comboBox.SelectionChanged += (sender, e) =>
        {
            string result = e.AddedItems[0].ToString().ToLower();
            viewModel.OnComboBoxSelected(result);
        };
    }

    
}

