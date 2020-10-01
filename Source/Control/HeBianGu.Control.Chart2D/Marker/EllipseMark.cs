using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.Chart2D
{
    public class EllipseMark: ShapePointMarker
    {

        public Rect Rect
        {
            get { return (Rect)GetValue(RectProperty); }
            set { SetValue(RectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RectProperty =
            DependencyProperty.Register("Rect", typeof(Rect), typeof(EllipseMark), new PropertyMetadata(default(Rect), (d, e) =>
             {
                 EllipseMark control = d as EllipseMark;

                 if (control == null) return;

                

             }));



        /// <summary> 描绘形状 </summary>
        protected override Geometry DefiningGeometry
        {
            get
            {
                EllipseGeometry e = new EllipseGeometry(ScreenPoint, this.Rect.Width / 2, this.Rect.Height / 2);
                return e;
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            Pen p = new Pen(this.Stroke, this.StrokeThickness);

            drawingContext.DrawGeometry(this.Fill, p, DefiningGeometry);

            //drawingContext.DrawGeometry(this.Fill, this.Pen, DefiningGeometry);

        }
    }
}
