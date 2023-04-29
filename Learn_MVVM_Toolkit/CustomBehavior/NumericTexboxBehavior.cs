using Learn_MVVM_Toolkit.Util;
using Microsoft.Xaml.Behaviors;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Converters;
using System.Security.Policy;
using static System.Net.Mime.MediaTypeNames;
using System.Printing.IndexedProperties;

namespace Learn_MVVM_Toolkit.CustomBehavior;

public class NumericTexboxBehavior : Behavior<TextBox>
{
    public static readonly DependencyProperty EmptyValueProperty =
    DependencyProperty.Register(nameof(EmptyValue),
        typeof(string),
        typeof(TextBoxInputRegExBehaviour),
        null);

    public string EmptyValue
    {
        get { return (string)GetValue(EmptyValueProperty); }
        set { SetValue(EmptyValueProperty, value); }
    }


    private string Text;
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.PreviewTextInput += PreviewTextHandler;
        AssociatedObject.PreviewKeyDown += PreviewKeyDownHandler;

        Text = EmptyValue;

    }

    private void PreviewKeyDownHandler(object sender, KeyEventArgs e)
    {

        if (e.Key == Key.Back)
        {
            if (AssociatedObject.SelectionStart > 0)
            {
                Text = AssociatedObject.Text.Remove(AssociatedObject.SelectionStart - 1, 1);
            }

        }

        if (e.Key == Key.Up)
        {
            int count = int.Parse(AssociatedObject.Text);
            count++;
            AssociatedObject.Text = count.ToString();
            AssociatedObject.SelectionStart = AssociatedObject.Text.Length;
        }

        if (e.Key == Key.Down)
        {
            int count = int.Parse(AssociatedObject.Text);

            if (count <= int.Parse(EmptyValue))
                return;

            count--;
            AssociatedObject.Text = count.ToString();
            AssociatedObject.SelectionStart = AssociatedObject.Text.Length;

        }


        if (Text == string.Empty)
        {
            this.AssociatedObject.Text = Text = this.EmptyValue;
            if (e.Key == Key.Back)
            {
                AssociatedObject.SelectionStart++;

            }
            e.Handled = true;
        }
    }


    private void PreviewTextHandler(object sender, TextCompositionEventArgs e)
    {
        e.Handled = Validate(e.Text);

    }

    private bool Validate(string text)
    {
        Regex regex = new Regex("[^0-9]+");
        bool T = regex.IsMatch(text);
        return T;
    }
}
