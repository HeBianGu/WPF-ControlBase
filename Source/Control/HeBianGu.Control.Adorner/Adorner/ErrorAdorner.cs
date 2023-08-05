// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.Adorner
{
    public class ErrorAdorner : BorderAdorner
    {
        public ErrorAdorner(UIElement adornedElement) : base(adornedElement)
        {
            this.Pen = new Pen(Brushes.Red, 1);
            this.ScaleLen = 3;
            this.Fill = new SolidColorBrush(Colors.Red) { Opacity = 0.5 };
        }
    }
}
