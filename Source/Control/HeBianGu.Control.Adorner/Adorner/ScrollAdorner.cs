// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Control.Adorner
{
    //public class ScrollAdorner : BorderAdorner
    //{
    //    public ScrollAdorner(UIElement adornedElement) : base(adornedElement)
    //    {

    //    }

    //    public Size CornerSize { get; set; } = new Size(5, 5);

    //    public Brush CornerFill { get; set; } = Brushes.Red;

    //    protected override void OnRender(DrawingContext dc)
    //    {
    //        Rect rect = new Rect(this.AdornedElement.RenderSize);

    //        dc.DrawRectangle(this.Fill, new Pen(this.Stroke, this.StrokeThickness), rect);

    //        //  Do ：四个角
    //        {
    //            dc.DrawRectangle(this.CornerFill, new Pen(this.Stroke, this.StrokeThickness), new Rect(rect.TopLeft - new Vector(CornerSize.Width, CornerSize.Height), CornerSize));
    //            dc.DrawRectangle(this.CornerFill, new Pen(this.Stroke, this.StrokeThickness), new Rect(rect.TopRight - new Vector(0, CornerSize.Height), CornerSize));
    //            dc.DrawRectangle(this.CornerFill, new Pen(this.Stroke, this.StrokeThickness), new Rect(rect.BottomLeft - new Vector(CornerSize.Width, 0), CornerSize));
    //            dc.DrawRectangle(this.CornerFill, new Pen(this.Stroke, this.StrokeThickness), new Rect(rect.BottomRight - new Vector(0, 0), CornerSize));
    //        }

    //        //  Do ：四个边
    //        {
    //            Point center = rect.GetCenter();

    //            dc.DrawRectangle(this.CornerFill, new Pen(this.Stroke, this.StrokeThickness), new Rect(new Point(rect.Left - CornerSize.Width, center.Y - CornerSize.Height / 2.0), CornerSize));
    //            dc.DrawRectangle(this.CornerFill, new Pen(this.Stroke, this.StrokeThickness), new Rect(new Point(rect.Right - 0, center.Y - CornerSize.Height / 2.0), CornerSize));
    //            dc.DrawRectangle(this.CornerFill, new Pen(this.Stroke, this.StrokeThickness), new Rect(new Point(center.X - CornerSize.Width / 2.0, rect.Top - CornerSize.Height), CornerSize));
    //            dc.DrawRectangle(this.CornerFill, new Pen(this.Stroke, this.StrokeThickness), new Rect(new Point(center.X - CornerSize.Width / 2.0, rect.Bottom - 0), CornerSize));
    //        }
    //    }
    //}
}
