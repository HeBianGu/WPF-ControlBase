// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Control.Host
{
    public abstract class ItemsHost : AnimationHost
    {
        public virtual void Location(UIElement element)
        {
            if (element == null) return;
            Rect rect = this.GetElementRect(element);
            this.Container.Visibility = Visibility.Visible;
            this.Container.Width = element.RenderSize.Width;
            this.Container.Height = element.RenderSize.Height;
            this.AnimationLocation(rect.X, rect.Y);
        }
    }
}
