 using System;
using System.Windows; 

namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// An action to close a Window.
    /// </summary>
    public class CloseWindowAction : TriggerAction<FrameworkElement>
    {
        /// <summary>
        /// ClosingCheckFuncProperty
        /// </summary>
        public static readonly DependencyProperty ClosingCheckFuncProperty =
            DependencyProperty.Register("ClosingCheckFunc", typeof(Func<bool>), typeof(CloseWindowAction), new PropertyMetadata(null));

        /// <summary>
        /// Get or set a function that returns a boolean value indicating whether the <see cref="Window"/> can be closed or not
        /// </summary>
        /// <remarks>If the result of function is true, the window can be closed, otherwise, cannot</remarks>
        public Func<bool> ClosingCheckFunc
        {
            get { return (Func<bool>)GetValue(ClosingCheckFuncProperty); }
            set { SetValue(ClosingCheckFuncProperty, value); }
        }

        /// <summary>
        /// Override Invoke method
        /// </summary>
        /// <param name="parameter"></param>
        protected override void Invoke(object parameter)
        {
            if (ClosingCheckFunc != null && !ClosingCheckFunc())
            {
                return;
            }

            var window = AssociatedObject.GetParent<Window>();

            if (window != null)
            {
                window.Close();
            }
        }
    }
}