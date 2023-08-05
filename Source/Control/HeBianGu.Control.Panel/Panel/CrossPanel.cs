// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.General.WpfControlLib;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace HeBianGu.Control.Panel
{
    public class CrossPanel : AnimationPanel
    {
        public CrossPanel()
        {
            CommandBinding binding = new CommandBinding(Commander.Selected);
            this.CommandBindings.Add(binding);
            binding.Executed += (l, k) =>
            {
                if (k.Parameter is UIElement element)
                {
                    int index = this.Children.IndexOf(element);

                    this.StartIndex = index;
                }
            };
            binding.CanExecute += (l, k) =>
            {
                k.CanExecute = true;
            };
        }

        public double SmallSize
        {
            get { return (double)GetValue(SmallSizeProperty); }
            set { SetValue(SmallSizeProperty, value); }
        }

        public static readonly DependencyProperty SmallSizeProperty =
            DependencyProperty.Register("SmallSize", typeof(double), typeof(CrossPanel), new PropertyMetadata(100.0, (d, e) =>
            {
                CrossPanel control = d as CrossPanel;
                if (control == null) 
                    return;
                control.InvalidateArrange();
            }));


        public int? RowCount
        {
            get { return (int?)GetValue(RowCountProperty); }
            set { SetValue(RowCountProperty, value); }
        }

        public static readonly DependencyProperty RowCountProperty =
            DependencyProperty.Register("RowCount", typeof(int?), typeof(CrossPanel), new PropertyMetadata(null, (d, e) =>
            {
                CrossPanel control = d as CrossPanel;
                if (control == null) 
                    return;
                control.InvalidateArrange();
            }));


        public int? ColumnCount
        {
            get { return (int?)GetValue(ColumnCountProperty); }
            set { SetValue(ColumnCountProperty, value); }
        }

        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.Register("ColumnCount", typeof(int?), typeof(CrossPanel), new PropertyMetadata(null, (d, e) =>
            {
                CrossPanel control = d as CrossPanel;
                if (control == null) 
                    return;
                control.InvalidateArrange();

            }));

        /// <summary> 最后一列平铺满 </summary>
        public bool IsFull
        {
            get { return (bool)GetValue(IsFullProperty); }
            set { SetValue(IsFullProperty, value); }
        }

        public static readonly DependencyProperty IsFullProperty =
            DependencyProperty.Register("IsFull", typeof(bool), typeof(CrossPanel), new PropertyMetadata(true, (d, e) =>
            {
                CrossPanel control = d as CrossPanel;
                if (control == null) 
                    return;
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
            FrameworkElement center = children[0] as FrameworkElement;
            double cx = finalSize.Width / 2;
            double cy = finalSize.Height / 2;
            center.Width = finalSize.Width - this.SmallSize * 2;
            center.Height = finalSize.Height - this.SmallSize * 2;
            center.Measure(finalSize);
            Point cp = new Point(cx - center.DesiredSize.Width / 2, cy - center.DesiredSize.Height / 2);
            action(center, cp);
            int count = (children.Count - 1);
            double height = finalSize.Height - this.SmallSize * 2;
            int xcount = this.ColumnCount.HasValue ? this.ColumnCount.Value : (int)(count * (finalSize.Width / (height + finalSize.Width)));
            xcount = xcount + xcount % 2;
            int ycount = this.RowCount.HasValue ? this.RowCount.Value : count - xcount;
            ycount = ycount + ycount % 2;
            Func<int, FrameworkElement, Point> GetPoint = (index, element) =>
            {
                if (index < xcount / 2)
                {
                    double len = finalSize.Width / (xcount / 2);
                    double ix = index * len + len / 2;
                    double iy = this.SmallSize / 2;
                    element.Width = len;
                    element.Height = this.SmallSize;
                    return new Point(ix, iy);
                }
                else if (index < xcount / 2 + ycount / 2)
                {
                    double len = height / (ycount / 2);
                    double ix = finalSize.Width - this.SmallSize / 2;
                    double iy = (index - (xcount / 2)) * len + len / 2 + this.SmallSize;
                    element.Width = this.SmallSize;
                    element.Height = len;
                    return new Point(ix, iy);
                }
                else if (index < xcount + ycount / 2)
                {
                    double len = finalSize.Width / (xcount / 2);
                    double ix = (index - (xcount / 2 + ycount / 2)) * len + len / 2;
                    double iy = finalSize.Height - this.SmallSize / 2;
                    element.Width = len;
                    element.Height = this.SmallSize;
                    return new Point(ix, iy);
                }
                else
                {
                    //  Do ：最后一个单独处理，平铺满
                    int yc = this.RowCount.HasValue ? this.RowCount.Value : count - xcount;
                    double len = this.IsFull && yc % 2 == 1 ? height / (yc - (ycount / 2)) : height / (ycount / 2);
                    double ix = this.SmallSize / 2;
                    double iy = (index - (xcount + ycount / 2)) * len + len / 2 + this.SmallSize;
                    element.Width = this.SmallSize;
                    element.Height = len;
                    return new Point(ix, iy);
                }
            };

            {
                int index = 0;
                foreach (FrameworkElement item in children.Cast<FrameworkElement>())
                {
                    if (item == center) 
                        continue;
                    Point c = GetPoint(index, item);
                    item.Measure(finalSize);
                    Point ip = new Point(c.X - item.DesiredSize.Width / 2, c.Y - item.DesiredSize.Height / 2);
                    action(item, ip);
                    index++;
                }
            }
            return base.ArrangeOverride(finalSize);
        }
    }
}
