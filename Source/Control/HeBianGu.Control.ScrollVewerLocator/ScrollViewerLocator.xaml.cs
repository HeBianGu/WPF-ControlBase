// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HeBianGu.Control.ScrollVewerLocator
{
    /// <summary> 绑定 ScrollViewer 的鸟瞰图</summary>
    public partial class ScrollViewerLocator : ScrollLocatorBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ScrollViewerLocator), "S.ScrollViewerLocator.Default");

        static ScrollViewerLocator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollViewerLocator), new FrameworkPropertyMetadata(typeof(ScrollViewerLocator)));
        }

        public ScrollViewerLocator()
        {
            {
                CommandBinding binding = new CommandBinding(ScrollViewerLocatorCommands.FullScreen);
                binding.Executed += (l, k) =>
                {
                    if (this.ScrollViewer is ScrollViewerTransfor scroll)
                    {
                        scroll.IsFullParent = true;
                    }
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(ScrollViewerLocatorCommands.TrimSize);
                binding.Executed += (l, k) =>
                {
                    if (this.ScrollViewer is ScrollViewerTransfor scroll)
                    {
                        scroll.IsFullParent = false;
                    }
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(ScrollViewerLocatorCommands.Plus);
                binding.Executed += (l, k) =>
                {
                    if (this.ScrollViewer is ScrollViewerTransfor scroll)
                    {
                        scroll.SetScaleToViewCenter(100);
                    }
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(ScrollViewerLocatorCommands.Minus);
                binding.Executed += (l, k) =>
                {
                    if (this.ScrollViewer is ScrollViewerTransfor scroll)
                    { 
                        scroll.SetScaleToViewCenter(-100); 
                    }
                };
                this.CommandBindings.Add(binding);
            }


            {
                CommandBinding binding = new CommandBinding(ScrollViewerLocatorCommands.LocatorCenter);
                binding.Executed += (l, k) =>
                {
                    if (this.ScrollViewer is ScrollViewerTransfor scroll)
                    {
                        scroll.LocatorCenter();
                    }
                };
                this.CommandBindings.Add(binding);
            }
        }

        public ScrollViewer ScrollViewer
        {
            get { return (ScrollViewer)GetValue(ScrollViewerProperty); }
            set { SetValue(ScrollViewerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScrollViewerProperty =
            DependencyProperty.Register("ScrollViewer", typeof(ScrollViewer), typeof(ScrollViewerLocator), new PropertyMetadata(default(ScrollViewer), (d, e) =>
             {
                 ScrollViewerLocator control = d as ScrollViewerLocator;

                 if (control == null) return;

                 ScrollViewer config = e.NewValue as ScrollViewer;

                 if (e.NewValue is ScrollViewer n)
                 {
                     control.OnAttached(n);
                 }

                 if (e.OldValue is ScrollViewer o)
                 {
                     control.OnDetaching(o);
                 }

             }));

        protected override void LocationPoint(Point p)
        {
            double h = this.GetScrollHorizontalOffset(p.X);
            double v = this.GetScrollVerticalOffset(p.Y);

            this.ScrollViewer.ScrollToHorizontalOffset(h);
            this.ScrollViewer.ScrollToVerticalOffset(v);
        }

        private void OnAttached(ScrollViewer viewer)
        {
            viewer.ScrollChanged += ScrollViewer_ScrollChanged;
        }

        protected override void RefreshLocation()
        {
            if (this.Canvas == null || this.Mask == null) return;

            if (this.ScrollViewer == null)
                return;

            this.Canvas.Width = this.GetVisualWidth();
            this.Canvas.Height = this.GetVisualHeight();

            if (this.ScrollViewer.ExtentWidth == 0 || this.ScrollViewer.ExtentHeight == 0) return;

            this.Mask.Width = this.GetRelativeWidth(this.ScrollViewer.ActualWidth);
            this.Mask.Height = this.GetRelativeHeight(this.ScrollViewer.ActualHeight);

            if (this.ScrollViewer is ScrollViewerTransfor transfor)
            {
                this.Visual = transfor.GetVisual();
            }
            else
            {
                this.Visual = this.ScrollViewer.Content as Visual;
            }
        }

        private void OnDetaching(ScrollViewer viewer)
        {
            viewer.ScrollChanged -= ScrollViewer_ScrollChanged;
        }


        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            double h = this.GetRelativeWidth(this.ScrollViewer.HorizontalOffset);
            double v = this.GetRelativeHeight(this.ScrollViewer.VerticalOffset);

            Canvas.SetLeft(this.Mask, h);
            Canvas.SetTop(this.Mask, v);
            this.RefreshLocation();

            if (this.Mask.Background is VisualBrush visualBrush)
            {
                double x = this.ScrollViewer.HorizontalOffset / this.ScrollViewer.ExtentWidth;
                double y = this.ScrollViewer.VerticalOffset / this.ScrollViewer.ExtentHeight;
                double width = this.ScrollViewer.ViewportWidth / this.ScrollViewer.ExtentWidth;
                double hight = this.ScrollViewer.ViewportHeight / this.ScrollViewer.ExtentHeight;
                visualBrush.Viewbox = new Rect(x, y, width, hight);
            }

        }

        private double GetScrollHorizontalOffset(double h)
        {
            return (h / this.GetVisualWidth()) * this.ScrollViewer.ExtentWidth;
        }

        private double GetScrollVerticalOffset(double h)
        {
            return (h / this.GetVisualHeight()) * this.ScrollViewer.ExtentHeight;
        }

        private double GetRelativeWidth(double value)
        {
            double v = this.GetVisualWidth();

            return v * (value / this.ScrollViewer.ExtentWidth);
        }

        private double GetRelativeHeight(double value)
        {
            double v = this.GetVisualHeight();

            return v * (value / this.ScrollViewer.ExtentHeight);
        }

        /// <summary> 计算缩略图的宽度 </summary>
        private double GetVisualWidth()
        {
            if (this.ScrollViewer == null)
                return this.ActualWidth;

            double x1 = this.ActualWidth / this.ActualHeight;

            double x2 = this.ScrollViewer.ExtentWidth / this.ScrollViewer.ExtentHeight;

            if (x1 > x2)
            {
                return x2 * (this.ActualWidth / x1);
            }

            return this.ActualWidth;
        }

        /// <summary> 计算缩略图的高度 </summary> 
        private double GetVisualHeight()
        {
            if (this.ScrollViewer == null)
                return this.ActualHeight;

            double x1 = this.ActualHeight / this.ActualWidth;

            double x2 = this.ScrollViewer.ExtentHeight / this.ScrollViewer.ExtentWidth;

            if (x1 > x2)
            {
                return x2 * (this.ActualHeight / x1);
            }

            return this.ActualHeight;
        }



        protected override bool Checked()
        {
            return this.ScrollViewer != null;
        }

        protected override void WheelPoint(Point p, double delta)
        {
            this.LocationPoint(p);

            if (this.ScrollViewer is ScrollViewerTransfor transfor)
            {
                transfor.Scale = transfor.Scale + delta / 1200.0;
            }
        }
    }


    public static class ScrollViewerLocatorCommands
    {
        public static RoutedCommand FullScreen { get; set; } = new RoutedCommand();
        public static RoutedCommand TrimSize { get; set; } = new RoutedCommand();
        public static RoutedCommand Plus { get; set; } = new RoutedCommand();
        public static RoutedCommand Minus { get; set; } = new RoutedCommand();
        public static RoutedCommand LocatorCenter { get; set; } = new RoutedCommand();
    }
}
