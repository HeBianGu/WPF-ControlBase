using System;
using System.Windows; 

namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// A behavior for <see cref="Window"/> that can handle the closing event.
    /// </summary>    
    [TypeConstraint(typeof(Window))]
    public class ClosingWindowBehavior : Behavior<Window>
    {
        /// <summary>
        /// ClosingCheckFunc dp
        /// </summary>
        public static readonly DependencyProperty ClosingCheckFuncProperty =
            DependencyProperty.Register("ClosingCheckFunc", typeof(Func<bool>), typeof(ClosingWindowBehavior), new PropertyMetadata(null));

        /// <summary>
        /// Get or set a function that returns a boolean value indicating whether the <see cref="Window"/> can be closed or not.
        /// </summary>
        /// <remarks>If the result of function is true, the window can be closed, otherwise, cannot.</remarks>
        public Func<bool> ClosingCheckFunc
        {
            get { return (Func<bool>)GetValue(ClosingCheckFuncProperty); }
            set { SetValue(ClosingCheckFuncProperty, value); }
        }

        /// <summary>
        /// When attached
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject != null)
            {
                AssociatedObject.Closing -= AssociatedObject_Closing;
            }

            AssociatedObject.Closing += AssociatedObject_Closing;
        }

        /// <summary>
        /// When detaching
        /// </summary>
        protected override void OnDetaching()
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.Closing -= AssociatedObject_Closing;
            }
            base.OnDetaching();
        }

        private void AssociatedObject_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ClosingCheckFunc != null)
            {
                e.Cancel = !ClosingCheckFunc();
            }
        }
    }
}