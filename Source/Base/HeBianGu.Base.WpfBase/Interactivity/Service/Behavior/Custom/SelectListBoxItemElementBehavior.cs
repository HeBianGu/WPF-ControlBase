using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace HeBianGu.Base.WpfBase
{
    /// <summary> 但点击当前控件时ListBoxItem项值也选中 </summary>
    public class SelectListBoxItemElementBehavior : Behavior<FrameworkElement>
    {

        protected override void OnAttached()
        {
            AssociatedObject.AddHandler(UIElement.PreviewMouseLeftButtonDownEvent,
                new MouseButtonEventHandler(OnMouseLeftButtonDown), false);
        }

        protected override void OnDetaching()
        {
            AssociatedObject.RemoveHandler(UIElement.PreviewMouseLeftButtonDownEvent
                , new MouseButtonEventHandler(OnMouseLeftButtonDown));
        }



        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var parent = AssociatedObject.GetParent<ListBoxItem>();

            if(parent!=null)
            {
                parent.IsSelected = true;
            } 
         
        }
    }
}