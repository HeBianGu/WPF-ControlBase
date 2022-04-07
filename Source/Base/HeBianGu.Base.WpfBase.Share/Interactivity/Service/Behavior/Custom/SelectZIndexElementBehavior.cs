// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace HeBianGu.Base.WpfBase
{
    /// <summary> 当点击容器中一项时，将该项目置于容器最顶端 </summary>
    public class SelectZIndexElementBehavior : Behavior<FrameworkElement>
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
            int maxIndex = 0;

            if (AssociatedObject.Parent is Panel panel)
            {
                foreach (UIElement item in panel.Children)
                {
                    if (maxIndex < Panel.GetZIndex(item))
                        maxIndex = Panel.GetZIndex(item);
                }
            }


            Panel.SetZIndex(AssociatedObject, maxIndex + 1);
        }
    }
}