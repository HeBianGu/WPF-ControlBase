// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.Shape
{
    public class CornerRadiusShape : ShapeBase
    { 
        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(double), typeof(CornerRadiusShape), new FrameworkPropertyMetadata(5.0, (d, e) =>
             {
                 CornerRadiusShape control = d as CornerRadiusShape;

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
            return GeometryFactory.CreateCornerRadius(this.Width,this.Height,this.CornerRadius);
        }
    }
}
