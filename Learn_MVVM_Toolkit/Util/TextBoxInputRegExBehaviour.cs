
using Microsoft.Xaml.Behaviors;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Learn_MVVM_Toolkit.Util;

public class TextBoxInputRegExBehaviour : Behavior<TextBox>
{
    public static readonly DependencyProperty RegularExpressionProperty =
         DependencyProperty.Register(nameof(RegularExpression), 
             typeof(string),
             typeof(TextBoxInputRegExBehaviour), 
             new FrameworkPropertyMetadata(".*"));

    public string RegularExpression
    {
        get { return (string)GetValue(RegularExpressionProperty); }
        set { SetValue(RegularExpressionProperty, value); }
    }
    
    public static readonly DependencyProperty MaxLengthProperty =
        DependencyProperty.Register(nameof(MaxLength), 
            typeof(int), 
            typeof(TextBoxInputRegExBehaviour),
            new FrameworkPropertyMetadata(int.MinValue));

    public int MaxLength
    {
        get { return (int)GetValue(MaxLengthProperty); }
        set { SetValue(MaxLengthProperty, value); }
    }

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

    protected override void OnAttached()
    {
        base.OnAttached();

        AssociatedObject.PreviewTextInput += PreviewTextInputHandler;
        AssociatedObject.PreviewKeyDown += PreviewKeyDownHandler;
        DataObject.AddPastingHandler(AssociatedObject, PastingHandler);
    }

    void PreviewTextInputHandler(object sender, TextCompositionEventArgs e)
    {
        string text;
        if (this.AssociatedObject.Text.Length < this.AssociatedObject.CaretIndex)
            text = this.AssociatedObject.Text;
        else
        {
            //  Remaining text after removing selected text.
            string remainingTextAfterRemoveSelection;

            if(TreatSelectedText(out remainingTextAfterRemoveSelection))
            {
                text = remainingTextAfterRemoveSelection.Insert(AssociatedObject.SelectionStart, e.Text);
            }
            else
            {
                text = AssociatedObject.Text.Insert(this.AssociatedObject.CaretIndex, e.Text);
            }

            //text = TreatSelectedText(out remainingTextAfterRemoveSelection)
            //    ? remainingTextAfterRemoveSelection.Insert(AssociatedObject.SelectionStart, e.Text)
            //    : AssociatedObject.Text.Insert(this.AssociatedObject.CaretIndex, e.Text);
        }

        e.Handled = !ValidateText(text);
    }

    private void PastingHandler(object sender, DataObjectPastingEventArgs e)
    {
        if (e.DataObject.GetDataPresent(DataFormats.Text))
        {
            string? text= Convert.ToString(e.DataObject.GetData(DataFormats.Text));

            if (!ValidateText(text!))
                e.CancelCommand();
        }
        else
            e.CancelCommand();
    }

     void PreviewKeyDownHandler(object sender, KeyEventArgs e)
    {
        if (string.IsNullOrEmpty(this.EmptyValue))
            return;

        string text = null;

        // Handle the Backspace key
        if (e.Key == Key.Back)
        {
            if (!this.TreatSelectedText(out text))
            {
                if (AssociatedObject.SelectionStart > 0)
                    text = this.AssociatedObject.Text.Remove(AssociatedObject.SelectionStart - 1, 1);
            }
        }
        // Handle the Delete key
        else if (e.Key == Key.Delete)
        {
            // If text was selected, delete it
            if (!this.TreatSelectedText(out text) && this.AssociatedObject.Text.Length > AssociatedObject.SelectionStart)
            {
                // Otherwise delete next symbol
                text = this.AssociatedObject.Text.Remove(AssociatedObject.SelectionStart, 1);
            }
        }

        if (text == string.Empty)
        {
            this.AssociatedObject.Text = this.EmptyValue;
            if (e.Key == Key.Back)
                AssociatedObject.SelectionStart++;
            e.Handled = true;
        }
    }

    private bool ValidateText(string text)
    {
        return (new Regex(this.RegularExpression, RegexOptions.IgnoreCase)).IsMatch(text) && (MaxLength == int.MinValue || text.Length <= MaxLength);
    }


    private bool TreatSelectedText(out string text)
    {
        text = null;
        if (AssociatedObject.SelectionLength <= 0)
            return false;

        var length = this.AssociatedObject.Text.Length;
        if (AssociatedObject.SelectionStart >= length)
            return true;

        if (AssociatedObject.SelectionStart + AssociatedObject.SelectionLength >= length)
            AssociatedObject.SelectionLength = length - AssociatedObject.SelectionStart;

        text = this.AssociatedObject.Text.Remove(AssociatedObject.SelectionStart, AssociatedObject.SelectionLength);
        return true;
    }
}
