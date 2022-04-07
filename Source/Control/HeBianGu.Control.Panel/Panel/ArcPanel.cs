// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.Panel
{
    /// <summary> 环状布局容器 </summary>
    public class ArcPanel : AnimationPanel
    {
        public double Len
        {
            get { return (double)GetValue(LenProperty); }
            set { SetValue(LenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LenProperty =
            DependencyProperty.Register("Len", typeof(double), typeof(ArcPanel), new PropertyMetadata(100.0, (d, e) =>
            {
                ArcPanel control = d as ArcPanel;

                if (control == null) return;

                //double config = e.NewValue as double;

                control.InvalidateArrange();

            }));


        public bool AngleToCenter
        {
            get { return (bool)GetValue(AngleToCenterProperty); }
            set { SetValue(AngleToCenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AngleToCenterProperty =
            DependencyProperty.Register("AngleToCenter", typeof(bool), typeof(ArcPanel), new PropertyMetadata(default(bool), (d, e) =>
            {
                ArcPanel control = d as ArcPanel;

                if (control == null) return;

                //bool config = e.NewValue as bool;

                control.InvalidateArrange();

            }));

        public double StartAngle
        {
            get { return (double)GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register("StartAngle", typeof(double), typeof(ArcPanel), new PropertyMetadata(0.0, (d, e) =>
             {
                 ArcPanel control = d as ArcPanel;

                 if (control == null) return;

                 //double config = e.NewValue as double;

                 control.InvalidateArrange();

             }));


        public double EndAngle
        {
            get { return (double)GetValue(EndAngleProperty); }
            set { SetValue(EndAngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndAngleProperty =
            DependencyProperty.Register("EndAngle", typeof(double), typeof(CirclePanel), new PropertyMetadata(360.0, (d, e) =>
             {
                 CirclePanel control = d as CirclePanel;

                 if (control == null) return;

                 //double config = e.NewValue as double;

                 control.InvalidateArrange();

             }));




        protected override Size ArrangeOverride(Size finalSize)
        {
            System.Collections.Generic.List<UIElement> children = this.GetChildren();

            //  Do ：中心点
            Point center = new Point(finalSize.Width / 2, finalSize.Height / 2);

            Point start = new Point(finalSize.Width / 2 + Len, finalSize.Height / 2);

            double totalAngle = this.EndAngle - this.StartAngle;

            for (int i = 0; i < children.Count; i++)
            {
                UIElement elment = children[i];

                double angle = -this.StartAngle - (totalAngle / (children.Count - 1)) * i;

                Matrix matrix = new Matrix();

                matrix.RotateAt(angle, center.X, center.Y);

                Point end = matrix.Transform(start);

                elment.Measure(finalSize);

                elment.RenderTransformOrigin = new Point(0.5, 0.5);

                end = new Point(end.X - elment.DesiredSize.Width / 2, end.Y - elment.DesiredSize.Height / 2);

                if (this.IsAnimation)
                {
                    elment.Arrange(new Rect(new Point(0, 0), elment.DesiredSize));

                    if (elment.CheckDefaultTransformGroup())
                    {
                        elment.BeginAnimationXY(end.X, end.Y, this.Duration);

                        if (this.AngleToCenter)
                        {
                            elment.BeginAnimationAngle(angle, this.Duration);
                        }
                    }
                }
                else
                {
                    elment.Arrange(new Rect(end, elment.DesiredSize));
                }


            }

            return base.ArrangeOverride(finalSize);
        }

        //  Do ：用于设置整个容器的大小
        protected override Size MeasureOverride(Size availableSize)
        {
            Size value = base.MeasureOverride(availableSize);

            bool isAll = false;

            foreach (UIElement item in this.Children)
            {
                if (item.Visibility == Visibility.Visible)
                {
                    isAll = true; break;
                }
            }

            //  Do ：都是隐藏的
            if (!isAll) return value;

            return new Size(this.Len * 2, this.Len * 2);
        }
    }
}
