using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input; 
using System.Text.RegularExpressions;

namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// WPF attached behavior which attaches a regular expression to a TextBox.
    /// </summary>
    public class MaskedTextBoxBehavior : Behavior<TextBox>
    {
        #region MaskProperty
        /// <summary>
        /// This property serves a dual purpose.  First, it allows the behavior to be used as a traditional
        /// attached property behavior - without use of the Blend tool.  Second, it acts as a local property
        /// to store the expression.
        /// </summary>
        public static DependencyProperty MaskProperty =
            DependencyProperty.RegisterAttached("Mask", typeof(string), typeof(MaskedTextBoxBehavior),
                new FrameworkPropertyMetadata(null, OnMaskChanged));

        /// <summary>
        /// Returns whether MaskedTextBoxBehavior is enabled via attached property
        /// </summary>
        /// <param name="textBox"></param>
        /// <returns>Regular Expression Mask</returns>
        public static string GetMask(TextBox textBox)
        {
            return (string) textBox.GetValue(MaskProperty);
        }

        /// <summary>
        /// Adds MaskedTextBoxBehavior to TextBox
        /// </summary>
        /// <param name="textBox">TextBox to apply</param>
        /// <param name="value">True/False</param>
        public static void SetMask(TextBox textBox, string value)
        {
            textBox.SetValue(MaskProperty, value);
        }

        /// <summary>
        /// Mask to use for the regular expression.
        /// </summary>
        public string Mask
        {
            get { return (string) GetValue(MaskProperty); }
            set { SetValue(MaskProperty, value); }
        }

        /// <summary>
        /// This associates a new MaskedTextBoxBehavior onto a TextBox using the applied mask.
        /// </summary>
        /// <param name="dpo"></param>
        /// <param name="e"></param>
        private static void OnMaskChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
        {
            // If this is being applied to a TextBox then add it to the behaviors collection.
            TextBox tb = dpo as TextBox;
            if (tb != null)
            {
                var behColl = Interaction.GetBehaviors(tb);
                var existingBehavior = behColl.FirstOrDefault(b => b.GetType() == typeof(MaskedTextBoxBehavior)) as MaskedTextBoxBehavior;

                string mask = (e.NewValue != null) ? e.NewValue.ToString() : null;
                if (string.IsNullOrEmpty(mask) && existingBehavior != null)
                {
                    behColl.Remove(existingBehavior);
                }
                else if (!string.IsNullOrEmpty(mask) && existingBehavior == null)
                {
                    behColl.Add(new MaskedTextBoxBehavior { Mask = mask });
                }
            }
        }
        #endregion

        /// <summary>
        /// This attaches the property handlers - PreviewKeyDown and Clipboard support.
        /// </summary>
        protected override void OnAttached()
        {
            AssociatedObject.PreviewKeyDown += OnKeyDown;
            AssociatedObject.PreviewTextInput += OnTextInput;
            DataObject.AddPastingHandler(AssociatedObject, OnClipboardPaste);
        }

        /// <summary>
        /// This removes all our handlers.
        /// </summary>
        protected override void OnDetaching()
        {
            AssociatedObject.PreviewKeyDown -= OnKeyDown;
            AssociatedObject.PreviewTextInput -= OnTextInput;
            DataObject.RemovePastingHandler(AssociatedObject, OnClipboardPaste);
        }

        /// <summary>
        /// This is called to process text (character) input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsMatch(GetTextFuture(e.Text));
        }

        /// <summary>
        /// This method handles paste and drag/drop events onto the TextBox.  It restricts the character
        /// set to numerics and ensures we have consistent behavior.
        /// </summary>
        /// <param name="sender">TextBox sender</param>
        /// <param name="e">EventArgs</param>
        private void OnClipboardPaste(object sender, DataObjectPastingEventArgs e)
        {
            string text = e.SourceDataObject.GetData(e.FormatToApply) as string;
            if (!string.IsNullOrEmpty(text))
            {
                if (IsMatch(GetTextFuture(text)))
                    return;
            }
            e.CancelCommand();
        }

        /// <summary>
        /// This checks the PreviewKeyDown on the TextBox to check for the SPACE
        /// character which is not considered a TextInput element.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = !IsMatch(GetTextFuture(" "));
        }

        /// <summary>
        /// This returns the next text value if the pending event is processed.
        /// </summary>
        /// <param name="textToAdd">Text to add to the TextBox</param>
        /// <returns>String</returns>
        private string GetTextFuture(string textToAdd)
        {
            string currentText = AssociatedObject.Text;
            if (AssociatedObject.SelectionStart >= 0)
                currentText = currentText.Remove(AssociatedObject.SelectionStart, AssociatedObject.SelectionLength);
            currentText = currentText.Insert(AssociatedObject.CaretIndex, textToAdd);
            return currentText;
        }

        /// <summary>
        /// This method is used to test the string input.
        /// </summary>
        /// <param name="textToCheck"></param>
        /// <returns></returns>
        private bool IsMatch(string textToCheck)
        {
            return string.IsNullOrEmpty(Mask) 
                || Regex.IsMatch(textToCheck, Mask, RegexOptions.IgnorePatternWhitespace);
        }
    }
}