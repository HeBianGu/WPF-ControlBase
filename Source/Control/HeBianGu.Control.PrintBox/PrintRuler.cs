using HeBianGu.Base.WpfBase;
using System;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.PrintBox
{
    public class PrintRuler : UIElement
    {

        public double PageHeight
        {
            get { return (double)GetValue(PageHeightProperty); }
            set { SetValue(PageHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageHeightProperty =
            DependencyProperty.Register("PageHeight", typeof(double), typeof(PrintRuler), new FrameworkPropertyMetadata(500.0, (d, e) =>
            {
                PrintRuler control = d as PrintRuler;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }
                control.InvalidateVisual();
            }));

        protected override void OnRender(DrawingContext drawingContext)
        {
            double count = this.RenderSize.Height / this.PageHeight;
            int c = (int)Math.Ceiling(count);

            for (int i = 0; i < count; i++)
            {
                if (i == 0)
                    continue;
                Point p1 = new Point(0, i * this.PageHeight);
                Point p2 = new Point(this.RenderSize.Width, i * this.PageHeight);
                drawingContext.DrawLine(new Pen(Brushes.Red, 1) { DashStyle = new DashStyle(new double[2] { 4.0, 4.0 }, 0) }, p1, p2);

                Point p3 = p2 - new Vector(60, 20);
#if NET
                drawingContext.DrawText($"第{i}/{c}页", p3, Brushes.Red, 15);
#else
                var format = DrawingContextExtension.GetFormattedText("第{i}/{c}页", 15, Brushes.Red);
                drawingContext.DrawText(format, p3);
#endif
            }
        }
    }
}
