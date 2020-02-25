using System.Windows;
using System.Windows.Controls; 

namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// This behavior selects all text in a TextBox when it gets focus
    /// </summary>
    public class SelectTextOnFocusBehavior : Behavior<TextBox>
    {
        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.GotKeyboardFocus += SelectAllText;
            AssociatedObject.GotMouseCapture += SelectAllText;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        /// Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.GotKeyboardFocus -= SelectAllText;
            AssociatedObject.GotMouseCapture -= SelectAllText;
        }

        /// <summary>
        /// This selects the text in the TextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAllText(object sender, RoutedEventArgs e)
        {
            AssociatedObject.SelectAll();
        } 
    }
}
