// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.General.WpfControlLib;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace HeBianGu.Control.Panel
{
    /// <summary> 停靠控件 </summary>
    public class DockPanel : AnimationPanel
    {
        public DockPanel()
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

                        //if (index >= 0)
                        //{
                        //    if (index > (this.Children.Count - 1) / 2)
                        //    {
                        //        this.StartIndex = (index - (this.Children.Count - 1) / 2);
                        //    }
                        //    else
                        //    {
                        //        this.StartIndex = this.Children.Count - ((this.Children.Count - 1) / 2 - index);
                        //    }
                        //}

                        this.StartIndex = index;
                    }
                };

                binding.CanExecute += (l, k) => { k.CanExecute = true; };

            }
        }


        public DockType Dock
        {
            get { return (DockType)GetValue(DockProperty); }
            set { SetValue(DockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DockProperty =
            DependencyProperty.Register("Dock", typeof(DockType), typeof(DockPanel), new PropertyMetadata(default(DockType), (d, e) =>
            {
                DockPanel control = d as DockPanel;

                if (control == null) return;

                //Dock config = e.NewValue as Dock;
                control.InvalidateArrange();
            }));


        public bool IsFull
        {
            get { return (bool)GetValue(IsFullProperty); }
            set { SetValue(IsFullProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFullProperty =
            DependencyProperty.Register("IsFull", typeof(bool), typeof(DockPanel), new PropertyMetadata(true, (d, e) =>
             {
                 DockPanel control = d as DockPanel;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

                 control.InvalidateArrange();
             }));


        public double SmallSize
        {
            get { return (double)GetValue(SmallSizeProperty); }
            set { SetValue(SmallSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SmallSizeProperty =
            DependencyProperty.Register("SmallSize", typeof(double), typeof(DockPanel), new PropertyMetadata(100.0, (d, e) =>
            {
                DockPanel control = d as DockPanel;

                if (control == null) return;

                //double config = e.NewValue as double;

                control.InvalidateArrange();

            }));



        protected override Size ArrangeOverride(Size finalSize)
        {
            System.Collections.Generic.List<UIElement> children = this.GetChildren();

            Action<UIElement, Point> action = (elment, end) =>
            {
                if (this.IsAnimation)
                {
                    elment.Arrange(new Rect(new Point(0, 0), elment.DesiredSize));

                    if (elment.CheckDefaultTransformGroup())
                    {
                        elment.BeginAnimationXY(end.X, end.Y, this.Duration);
                    }
                }
                else
                {
                    elment.Arrange(new Rect(end, elment.DesiredSize));
                }
            };

            if (this.Dock == DockType.Bottom)
            {
                //  Do ：放置大图
                FrameworkElement center = children[0] as FrameworkElement;

                double cx = finalSize.Width / 2;
                double cy = (finalSize.Height - this.SmallSize) / 2;

                center.Width = finalSize.Width;

                center.Height = finalSize.Height - this.SmallSize;

                center.Measure(finalSize);

                Point cp = new Point(cx - center.DesiredSize.Width / 2, cy - center.DesiredSize.Height / 2);

                action(center, cp);

                //  Do ：放置缩略图

                int count = children.Count - 1;

                double len = finalSize.Width / count;

                int index = 0;

                foreach (FrameworkElement item in children.Cast<FrameworkElement>())
                {
                    if (item == center) continue;

                    double ix = index * len + len / 2;

                    double iy = finalSize.Height - this.SmallSize / 2;

                    item.Width = len;

                    item.Height = this.SmallSize;

                    item.Measure(finalSize);

                    Point ip = new Point(ix - item.DesiredSize.Width / 2, iy - item.DesiredSize.Height / 2);

                    action(item, ip);

                    index++;
                }

            }

            else if (this.Dock == DockType.Top)
            {
                //  Do ：放置大图
                FrameworkElement center = children[0] as FrameworkElement;

                double cx = finalSize.Width / 2;
                double cy = (finalSize.Height - this.SmallSize) / 2 + this.SmallSize;

                center.Width = finalSize.Width;

                center.Height = finalSize.Height - this.SmallSize;

                center.Measure(finalSize);

                Point cp = new Point(cx - center.DesiredSize.Width / 2, cy - center.DesiredSize.Height / 2);

                action(center, cp);

                //  Do ：放置缩略图

                int count = children.Count - 1;

                double len = finalSize.Width / count;

                int index = 0;

                foreach (FrameworkElement item in children.Cast<FrameworkElement>())
                {
                    if (item == center) continue;

                    double ix = index * len + len / 2;

                    double iy = this.SmallSize / 2;

                    item.Width = len;

                    item.Height = this.SmallSize;

                    item.Measure(finalSize);

                    Point ip = new Point(ix - item.DesiredSize.Width / 2, iy - item.DesiredSize.Height / 2);

                    action(item, ip);

                    index++;
                }

            }

            else if (this.Dock == DockType.TopAndBottom)
            {
                //  Do ：放置大图
                FrameworkElement center = children[0] as FrameworkElement;

                double cx = finalSize.Width / 2;

                double cy = finalSize.Height / 2;

                center.Width = finalSize.Width;

                center.Height = finalSize.Height - this.SmallSize * 2;

                center.Measure(finalSize);

                Point cp = new Point(cx - center.DesiredSize.Width / 2, cy - center.DesiredSize.Height / 2);

                action(center, cp);

                //  Do ：放置底部缩略图

                int count = (children.Count - 1) / 2 + (children.Count - 1) % 2;

                Func<int, double> GetLen = index =>
                   {
                       if (IsFull && (children.Count - 1) % 2 == 1 && index >= count)
                       {
                           return finalSize.Width / (count - 1);
                       }
                       else
                       {
                           return finalSize.Width / count;
                       }
                   };

                {
                    int index = 0;

                    foreach (FrameworkElement item in children.Cast<FrameworkElement>())
                    {
                        if (item == center) continue;

                        double len = GetLen(index);

                        double ix = index < count ? index * len + len / 2 : (index - count) * len + len / 2;

                        double iy = index < count ? finalSize.Height - this.SmallSize / 2 : this.SmallSize / 2;

                        item.Width = len;

                        item.Height = this.SmallSize;

                        item.Measure(finalSize);

                        Point ip = new Point(ix - item.DesiredSize.Width / 2, iy - item.DesiredSize.Height / 2);

                        action(item, ip);

                        index++;
                    }
                }
            }

            else if (this.Dock == DockType.Left)
            {
                //  Do ：放置大图
                FrameworkElement center = children[0] as FrameworkElement;

                double cx = (finalSize.Width - this.SmallSize) / 2 + this.SmallSize;

                double cy = finalSize.Height / 2;

                center.Width = finalSize.Width - this.SmallSize;

                center.Height = finalSize.Height;

                center.Measure(finalSize);

                Point cp = new Point(cx - center.DesiredSize.Width / 2, cy - center.DesiredSize.Height / 2);

                action(center, cp);

                //  Do ：放置缩略图

                int count = children.Count - 1;

                double len = finalSize.Height / count;

                int index = 0;

                foreach (FrameworkElement item in children.Cast<FrameworkElement>())
                {
                    if (item == center) continue;

                    double ix = this.SmallSize / 2;

                    double iy = index * len + len / 2;

                    item.Height = len;

                    item.Width = this.SmallSize;

                    item.Measure(finalSize);

                    Point ip = new Point(ix - item.DesiredSize.Width / 2, iy - item.DesiredSize.Height / 2);

                    action(item, ip);

                    index++;
                }

            }

            else if (this.Dock == DockType.Right)
            {
                //  Do ：放置大图
                FrameworkElement center = children[0] as FrameworkElement;

                double cx = (finalSize.Width - this.SmallSize) / 2;

                double cy = finalSize.Height / 2;

                center.Width = finalSize.Width - this.SmallSize;

                center.Height = finalSize.Height;

                center.Measure(finalSize);

                Point cp = new Point(cx - center.DesiredSize.Width / 2, cy - center.DesiredSize.Height / 2);

                action(center, cp);

                //  Do ：放置缩略图

                int count = children.Count - 1;

                double len = finalSize.Height / count;

                int index = 0;

                foreach (FrameworkElement item in children.Cast<FrameworkElement>())
                {
                    if (item == center) continue;

                    double ix = finalSize.Width - this.SmallSize / 2;

                    double iy = index * len + len / 2;

                    item.Height = len;

                    item.Width = this.SmallSize;

                    item.Measure(finalSize);

                    Point ip = new Point(ix - item.DesiredSize.Width / 2, iy - item.DesiredSize.Height / 2);

                    action(item, ip);

                    index++;
                }

            }

            else if (this.Dock == DockType.LeftAndRight)
            {
                //  Do ：放置大图
                FrameworkElement center = children[0] as FrameworkElement;

                double cx = finalSize.Width / 2;

                double cy = finalSize.Height / 2;

                center.Width = finalSize.Width - this.SmallSize * 2;

                center.Height = finalSize.Height;

                center.Measure(finalSize);

                Point cp = new Point(cx - center.DesiredSize.Width / 2, cy - center.DesiredSize.Height / 2);

                action(center, cp);

                //  Do ：放置底部缩略图

                int count = (children.Count - 1) / 2 + (children.Count - 1) % 2;

                Func<int, double> GetLen = index =>
                {
                    if (IsFull && (children.Count - 1) % 2 == 1 && index >= count)
                    {
                        return finalSize.Height / (count - 1);
                    }
                    else
                    {
                        return finalSize.Height / count;
                    }
                };

                {
                    int index = 0;

                    foreach (FrameworkElement item in children.Cast<FrameworkElement>())
                    {
                        if (item == center) continue;

                        double len = GetLen(index);

                        double ix = index < count ? finalSize.Width - this.SmallSize / 2 : this.SmallSize / 2;

                        double iy = index < count ? index * len + len / 2 : (index - count) * len + len / 2;

                        item.Width = this.SmallSize;

                        item.Height = len;

                        item.Measure(finalSize);

                        Point ip = new Point(ix - item.DesiredSize.Width / 2, iy - item.DesiredSize.Height / 2);

                        action(item, ip);

                        index++;
                    }
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

    public enum DockType
    {
        Left, Right, Bottom, Top, LeftAndRight, TopAndBottom
    }


}
