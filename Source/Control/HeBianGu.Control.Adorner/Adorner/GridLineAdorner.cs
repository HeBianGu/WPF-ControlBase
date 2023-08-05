// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Control.Adorner
{
    public class GridLineAdorner : AdornerBase
    {
        public GridLineAdorner(UIElement adornedElement) : base(adornedElement)
        {
            this.Pen = GridLineAdorner.GetPen(adornedElement);
            this.Fill = GridLineAdorner.GetFill(adornedElement);
        }

        public Brush Fill { get; set; }
        public double ScaleLen { get; set; }
        public Pen Pen { get; set; }
        protected override void OnRender(DrawingContext dc)
        {
            Grid grid = this.AdornedElement as Grid;
            if (grid == null)
                return;
            this.Pen = this.Pen ?? new Pen(Brushes.Blue, 1);
            foreach (var item in grid.RowDefinitions)
            {
                dc.DrawLine(this.Pen, new Point(0, item.Offset), new Point(this.ActualWidth, item.Offset));
            }
            dc.DrawLine(this.Pen, new Point(0, grid.ActualHeight), new Point(this.ActualWidth, this.ActualHeight));

            foreach (var item in grid.ColumnDefinitions)
            {
                dc.DrawLine(this.Pen, new Point(item.Offset, 0), new Point(item.Offset, this.ActualHeight));
            }
            dc.DrawLine(this.Pen, new Point(this.ActualWidth, 0), new Point(this.ActualWidth, this.ActualHeight));
        }
    }
}
