// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace HeBianGu.Base.WpfBase
{
    /// <summary> 单点击当前控件时ListBoxItem项值也选中 </summary>
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
            ListBoxItem parent = AssociatedObject.GetParent<ListBoxItem>();

            if (parent != null)
            {
                parent.IsSelected = true;
            }
        }
    }

}