// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HeBianGu.Control.Chart2D
{
    public class ShapePointMarker : Shape
    {
        public double Size
        {
            get { return (double)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register(
              "Size",
              typeof(double),
              typeof(ShapePointMarker),
              new FrameworkPropertyMetadata(5.0));


        /// <summary>Pen to outline marker</summary>
        public Pen Pen
        {
            get { return (Pen)GetValue(PenProperty); }
            set { SetValue(PenProperty, value); }
        }

        public static readonly DependencyProperty PenProperty =
            DependencyProperty.Register(
              "Pen",
              typeof(Pen),
              typeof(ShapePointMarker),
              new FrameworkPropertyMetadata(null));

        public Point ScreenPoint
        {
            get { return (Point)GetValue(ScreenPointProperty); }
            set { SetValue(ScreenPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScreenPoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScreenPointProperty =
            DependencyProperty.Register("ScreenPoint", typeof(Point), typeof(ShapePointMarker), new PropertyMetadata(new Point(0, 0)));


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
              "Text",
              typeof(string),
              typeof(ShapePointMarker),
              new FrameworkPropertyMetadata(""));

        //public int FontZize
        //{
        //    get { return (int)GetValue(FontZizeProperty); }
        //    set { SetValue(FontZizeProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for FontZize.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty FontZizeProperty =
        //    DependencyProperty.Register("FontZize", typeof(int), typeof(ShapePointMarker), new PropertyMetadata(10));

        //public Brush FontColor
        //{
        //    get { return (Brush)GetValue(FontColorProperty); }
        //    set { SetValue(FontColorProperty, value); }
        //}

        /// <summary> 描绘形状 </summary>
        protected override Geometry DefiningGeometry
        {
            get
            {
                EllipseGeometry e = new EllipseGeometry(ScreenPoint, this.Size / 2, this.Size / 2);
                return e;
            }
        }

        //// Using a DependencyProperty as the backing store for FontColor.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty FontColorProperty =
        //    DependencyProperty.Register("FontColor", typeof(Brush), typeof(ShapePointMarker), new PropertyMetadata(Brushes.Black));


        /// <summary> 绘制形状 </summary>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            Pen p = new Pen(this.Fill, 1);

            drawingContext.DrawGeometry(Brushes.White, p, DefiningGeometry);
        }
    }
}
