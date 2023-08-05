// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.Adorner
{
    public class SelectedControlTemplateAdorner : ControlTemplateAdorner
    {
        public Brush Fill { get; set; }
        public double ScaleLen { get; set; }
        public Pen Pen { get; set; }

        public SelectedControlTemplateAdorner(UIElement adornedElement) : base(adornedElement)
        {
            this.Pen = new Pen(new SolidColorBrush(Colors.Orange), 2) { DashStyle = new DashStyle(new double[] { 2, 2 }, 0) };
            this.ScaleLen = 0;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            Rect rect = new Rect(this.AdornedElement.RenderSize);
            drawingContext.DrawRectangle(this.Fill, this.Pen, new Rect(rect.Left - ScaleLen, rect.Top - ScaleLen, rect.Width + 2 * ScaleLen, rect.Height + 2 * ScaleLen));
        }
    }
}
