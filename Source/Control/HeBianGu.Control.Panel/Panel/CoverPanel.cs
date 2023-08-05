// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.General.WpfControlLib;
using System;
using System.Windows;
using System.Windows.Input;

namespace HeBianGu.Control.Panel
{
    /// <summary> 环状布局容器 </summary>
    public class CoverPanel : AnimationPanel
    {
        public CoverPanel()
        {
            //  Do ：定位到中心位置
            {
                CommandBinding binding = new CommandBinding(Commander.Selected);

                this.CommandBindings.Add(binding);

                binding.Executed += (l, k) =>
                {
                    if (k.Parameter is UIElement element)
                    {
                        int index = this.Children.IndexOf(element);

                        if (index >= 0)
                        {
                            if (index > (this.Children.Count - 1) / 2)
                            {
                                this.StartIndex = (index - (this.Children.Count - 1) / 2);
                            }
                            else
                            {
                                this.StartIndex = this.Children.Count - ((this.Children.Count - 1) / 2 - index);
                            }
                        }
                    }
                };
            }
        }

        public double ScaleMin
        {
            get { return (double)GetValue(ScaleMinProperty); }
            set { SetValue(ScaleMinProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleMinProperty =
            DependencyProperty.Register("ScaleMin", typeof(double), typeof(CoverPanel), new PropertyMetadata(0.3, (d, e) =>
             {
                 CoverPanel control = d as CoverPanel;

                 if (control == null) return;

                 //double config = e.NewValue as double;

                 control.InvalidateArrange();

             }));


        public double PressValue
        {
            get { return (double)GetValue(PressValueProperty); }
            set { SetValue(PressValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressValueProperty =
            DependencyProperty.Register("PressValue", typeof(double), typeof(CoverPanel), new PropertyMetadata(0.6, (d, e) =>
             {
                 CoverPanel control = d as CoverPanel;

                 if (control == null) return;

                 //double config = e.NewValue as double;

                 control.InvalidateArrange();

             }));


        public VerticalAlignment VerticalContentAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalContentAlignmentProperty); }
            set { SetValue(VerticalContentAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalContentAlignmentProperty =
            DependencyProperty.Register("VerticalContentAlignment", typeof(VerticalAlignment), typeof(CoverPanel), new PropertyMetadata(VerticalAlignment.Center, (d, e) =>
             {
                 CoverPanel control = d as CoverPanel;

                 if (control == null) return;

                 //VerticalAlignment config = e.NewValue as VerticalAlignment;

                 control.InvalidateArrange();

             }));
        protected override Size ArrangeOverride(Size finalSize)
        {
            System.Collections.Generic.List<UIElement> children = this.GetChildren();

            //  Do ：中心点
            Point center = new Point(finalSize.Width / 2, finalSize.Height / 2);

            double len = finalSize.Width / (children.Count - 1);

            double v = (children.Count - 1) / 2.0;


            for (int i = 0; i < children.Count; i++)
            {
                UIElement elment = children[i];

                //  Do ：距离中心的位置，越靠近中心越小
                double centerAccent = v - i;

                double centerValue = Math.Abs(centerAccent);

                double scalePercent = (1 - this.ScaleMin) * (v - centerValue) / v + this.ScaleMin;

                elment.RenderTransformOrigin = new Point(0.5, 0.5);

                if (this.VerticalContentAlignment == VerticalAlignment.Bottom)
                {
                    elment.RenderTransformOrigin = new Point(0.5, 1.0);

                }
                else if (this.VerticalContentAlignment == VerticalAlignment.Top)
                {
                    elment.RenderTransformOrigin = new Point(0.5, 0.0);
                }
                else
                {
                    elment.RenderTransformOrigin = new Point(0.5, 0.5);
                }

                System.Windows.Controls.Panel.SetZIndex(elment, children.Count - (int)centerValue);

                Point end = new Point(center.X - centerAccent * len * this.PressValue, finalSize.Height / 2);

                elment.Measure(finalSize);

                end = new Point(end.X - elment.DesiredSize.Width / 2, end.Y - elment.DesiredSize.Height / 2);

                if (this.IsAnimation)
                {
                    elment.Arrange(new Rect(new Point(0, 0), elment.DesiredSize));

                    if (elment.CheckDefaultTransformGroup())
                    {
                        elment.BeginAnimationXY(end.X, end.Y, this.Duration);
                        elment.BeginAnimationScale(scalePercent, scalePercent, this.Duration);
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
            return base.MeasureOverride(availableSize);
        }
    }


}
