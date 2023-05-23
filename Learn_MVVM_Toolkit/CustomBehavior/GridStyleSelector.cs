using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Learn_MVVM_Toolkit.CustomBehavior;

public class GridStyleSelector : StyleSelector
{
    public int index = 1;
    public override Style SelectStyle(object item, DependencyObject container)
    {
        
        if((index % 2) == 0)
        {
              
            
            Console.WriteLine("Gray Background");
        }
        else
        {
            
            Console.WriteLine("White Background");
        }
        
        index++;

        return base.SelectStyle(item, container);
    }
}
