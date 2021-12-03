using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


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
}