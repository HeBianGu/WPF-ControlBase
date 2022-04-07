// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.Chart2D
{
    public class EllipseMarker : ShapePointMarker
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(EllipseMarker), "S.EllipseMarker.Default");
        public static ComponentResourceKey AnimationKey => new ComponentResourceKey(typeof(EllipseMarker), "S.EllipseMarker.Animation");
        public static ComponentResourceKey Default10Key => new ComponentResourceKey(typeof(EllipseMarker), "S.EllipseMarker.10");
        public static ComponentResourceKey Default5Key => new ComponentResourceKey(typeof(EllipseMarker), "S.EllipseMarker.5");
        public static ComponentResourceKey Default4Key => new ComponentResourceKey(typeof(EllipseMarker), "S.EllipseMarker.4");
        public static ComponentResourceKey Default2Key => new ComponentResourceKey(typeof(EllipseMarker), "S.EllipseMarker.2");
        public static ComponentResourceKey CircleKey => new ComponentResourceKey(typeof(EllipseMarker), "S.EllipseMarker.Circle");


        static EllipseMarker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EllipseMarker), new FrameworkPropertyMetadata(typeof(EllipseMarker)));
        }

        public Rect Rect
        {
            get { return (Rect)GetValue(RectProperty); }
            set { SetValue(RectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RectProperty =
            DependencyProperty.Register("Rect", typeof(Rect), typeof(EllipseMarker), new PropertyMetadata(default(Rect), (d, e) =>
            {
                EllipseMarker control = d as EllipseMarker;

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
