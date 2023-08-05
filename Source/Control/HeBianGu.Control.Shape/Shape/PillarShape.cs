// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.Shape
{

    /// <summary>
    /// 柱状图
    /// </summary>
    public class PillarShape : ShapeBase
    { 
        public double RadiusY
        {
            get { return (double)GetValue(RadiusYProperty); }
            set { SetValue(RadiusYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusYProperty =
            DependencyProperty.Register("RadiusY", typeof(double), typeof(PillarShape), new FrameworkPropertyMetadata(10.0, (d, e) =>
             {
                 PillarShape control = d as PillarShape;

                 if (control == null) return;

                 if (e.OldValue is double o)
                 {

                 }

                 if (e.NewValue is double n)
                 {

                 }

             }));


        public double RadiusX
        {
            get { return (double)GetValue(RadiusXProperty); }
            set { SetValue(RadiusXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusXProperty =
            DependencyProperty.Register("RadiusX", typeof(double), typeof(PillarShape), new FrameworkPropertyMetadata(50.0, (d, e) =>
             {
                 PillarShape control = d as PillarShape;

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
            return GeometryFactory.CreatePillar(this.Width, this.Height);

        }
    }
}
