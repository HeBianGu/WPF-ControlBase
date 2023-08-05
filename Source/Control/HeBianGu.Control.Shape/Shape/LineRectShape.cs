// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.Shape
{
    public class LineRectShape : ShapeBase
    {

        public double LineMargin
        {
            get { return (double)GetValue(LineMarginProperty); }
            set { SetValue(LineMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineMarginProperty =
            DependencyProperty.Register("LineMargin", typeof(double), typeof(LineRectShape), new FrameworkPropertyMetadata(10.0, (d, e) =>
             {
                 LineRectShape control = d as LineRectShape;

                 if (control == null) return;

                 if (e.OldValue is double o)
                 {

                 }

                 if (e.NewValue is double n)
                 {

                 }

             }));

        protected override Geometry CreateGeometry()
        {
            return GeometryFactory.CreateLineRect(this.Width, this.Height,this.LineMargin);
        }
    }
}
