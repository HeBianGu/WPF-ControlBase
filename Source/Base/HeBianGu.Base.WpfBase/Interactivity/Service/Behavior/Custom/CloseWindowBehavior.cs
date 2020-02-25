using System;
using System.Windows;
using System.Windows.Controls.Primitives; 

namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// This behavior allows the designer to close a dialog using a true/false DialogResult through a button.
    /// This is already supported if the IsCancel property is true, but the IsDefault does not auto-dismiss the dialog
    /// without some code behind.  This alleviates that requirement for the very simple dialogs that are completely VM driven.
    /// </summary>
    public class CloseDialogBehavior : Behavior<ButtonBase>
    {
        /// <summary>
        /// DialogResult dependency property
        /// </summary>
        public static readonly DependencyProperty DialogResultProperty = 
            DependencyProperty.Register("DialogResult", typeof(bool), typeof(CloseDialogBehavior), new FrameworkPropertyMetadata(true));

        /// <summary>
        /// Dismiss dialog result value - this is assigned to the window.DialogResult to close the window.
        /// </summary>
        public bool DialogResult
        {
            get { return (bool) base.GetValue(DialogResultProperty); }    
            set { base.SetValue(DialogResultProperty, value);}
        }

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += OnButtonClick;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        /// Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            AssociatedObject.Click -= OnButtonClick;
            base.OnDetaching();
        }

        /// <summary>
        /// This dismisses the associated window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            // Walk up the visual tree and find the window this button is owned by.
            var winParent = AssociatedObject.GetParent<Window>();
            if (winParent != null)
            {
                try
                {
                    winParent.DialogResult = DialogResult;
                }
                catch (InvalidOperationException)
                {
                    // Not a dialog window-- just close it directly.
                    winParent.Close();
                }
            }
        }
    }
}
