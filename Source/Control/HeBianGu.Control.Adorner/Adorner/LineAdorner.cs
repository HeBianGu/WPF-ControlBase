// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Control.Adorner
{
    public class LineAdorner : BorderAdorner
    {
        public LineAdorner(UIElement adornedElement) : base(adornedElement)
        {
            this.Pen = new Pen(Brushes.DeepSkyBlue, 4);
            this.Dock = Dock.Top;
            this.ScaleLen = 0;
            this.Fill = new SolidColorBrush(Colors.LightSkyBlue) { Opacity = 0.2 };
        }

        public Dock Dock { get; set; }
        protected override void OnRender(DrawingContext dc)
        {
            Rect rect = new Rect(this.AdornedElement.RenderSize);
            rect = new Rect(rect.Left - ScaleLen, rect.Top - ScaleLen, rect.Width + 2 * ScaleLen, rect.Height + 2 * ScaleLen);
            dc.DrawRectangle(this.Fill, null, rect);

            if (Dock == Dock.Left)
            {
                dc.DrawLine(this.Pen, rect.TopLeft, rect.BottomLeft);
            }
            if (Dock == Dock.Top)
            {
                dc.DrawLine(this.Pen, rect.TopLeft, rect.TopRight);
            }
            if (Dock == Dock.Right)
            {
                dc.DrawLine(this.Pen, rect.BottomRight, rect.TopRight);
            }
            if (Dock == Dock.Bottom)
            {
                dc.DrawLine(this.Pen, rect.BottomLeft, rect.BottomRight);
            }
        }
    }
}
