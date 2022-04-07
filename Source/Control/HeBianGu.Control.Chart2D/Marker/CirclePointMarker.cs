// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows.Media;

namespace HeBianGu.Control.Chart2D
{

    /// <summary>圆环Marker</summary>
    public class CirclePointMarker : ShapePointMarker
    {
        /// <summary> 描绘形状 </summary>
        protected override Geometry DefiningGeometry
        {
            get
            {
                EllipseGeometry e = new EllipseGeometry(ScreenPoint, this.Size / 2, this.Size / 2);
                return e;
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            Pen p = new Pen(this.Fill, 1);

            drawingContext.DrawGeometry(Brushes.White, p, DefiningGeometry);

            //drawingContext.DrawGeometry(this.Fill, this.Pen, DefiningGeometry);
        }
    }
}
