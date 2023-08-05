// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

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
            get { return (bool)base.GetValue(DialogResultProperty); }
            set { base.SetValue(DialogResultProperty, value); }
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
            Window winParent = AssociatedObject.GetParent<Window>();
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


    public class DoubleClickWindowStateBehavior : Behavior<Control>
    {
        protected override void OnAttached()
        {
            AssociatedObject.MouseDoubleClick += AssociatedObject_MouseDoubleClick;
        }

        private void AssociatedObject_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Application.Current.MainWindow == null) return;

                WindowState state = Application.Current.MainWindow.WindowState;

                Application.Current.MainWindow.WindowState = state == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
            }
            catch
            {

            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseDoubleClick -= AssociatedObject_MouseDoubleClick;
        }
    }


    /// <summary> 设置鼠标放下时 窗口拖动</summary>
    public class DragMoveWindowBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            AssociatedObject.AddHandler(UIElement.MouseLeftButtonDownEvent,
                new MouseButtonEventHandler(OnMouseLeftButtonDown), false);
        }

        protected override void OnDetaching()
        {
            AssociatedObject.RemoveHandler(UIElement.PreviewMouseLeftButtonDownEvent
                , new MouseButtonEventHandler(OnMouseLeftButtonDown));
        }



        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Application.Current.MainWindow?.DragMove();
            }
            catch
            {

            }
        }
    }
}
