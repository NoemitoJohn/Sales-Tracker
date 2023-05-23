using Learn_MVVM_Toolkit.ObservableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Learn_MVVM_Toolkit.CustomBehavior;

public class SaleTemplateSelector : DataTemplateSelector
{
    public string SoldTemplateName { get; set; }
    public string DeductionTemplateName { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {

        DataTemplate dataTemplate = new DataTemplate();

        FrameworkElement element = container as FrameworkElement;

        OrderObservable obj = item as OrderObservable;

        if (obj == null) return dataTemplate;

        switch (obj.type)
        {
            case Order.TYPE.CASH:
                dataTemplate = element.FindResource(SoldTemplateName) as DataTemplate;
                break;

            case Order.TYPE.DEDUCTION: 
                dataTemplate = element.FindResource(DeductionTemplateName) as DataTemplate;
                break;
        }

        return dataTemplate;
    }
}
