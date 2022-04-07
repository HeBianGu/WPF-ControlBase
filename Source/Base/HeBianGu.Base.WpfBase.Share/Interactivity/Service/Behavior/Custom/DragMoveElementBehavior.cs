// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace HeBianGu.Base.WpfBase
{
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
}