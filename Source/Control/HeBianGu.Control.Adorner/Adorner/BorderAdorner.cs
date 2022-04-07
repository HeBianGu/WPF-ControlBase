// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.Adorner
{
    public class BorderAdorner : System.Windows.Documents.Adorner
    {
        public BorderAdorner(UIElement adornedElement) : base(adornedElement)
        {

        }

        public Brush Fill { get; set; } = Brushes.Transparent;

        public Brush Stroke { get; set; } = Brushes.Red;

        public double StrokeThickness { get; set; } = 1;

        protected override void OnRender(DrawingContext dc)
        {
            Rect rect = new Rect(this.AdornedElement.RenderSize);

            dc.DrawRectangle(this.Fill, new Pen(this.Stroke, this.StrokeThickness), rect);
        }
    }
}
