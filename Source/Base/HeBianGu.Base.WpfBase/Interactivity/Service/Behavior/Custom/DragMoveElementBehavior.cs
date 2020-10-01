using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace HeBianGu.Base.WpfBase
{
    /// <summary> 当点击容器中一项时，将该项目置于容器最顶端 </summary>
    public class DragMoveElementBehavior : Behavior<FrameworkElement>
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