// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace HeBianGu.Control.ScrollVewerLocator
{
    /// <summary> 绑定 ScrollViewer 的鸟瞰图</summary>
    public partial class ScrollBarLocator : ScrollLocatorBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ScrollLocatorBase), "S.ScrollBarLocator.Default");


        static ScrollBarLocator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollBarLocator), new FrameworkPropertyMetadata(typeof(ScrollBarLocator)));
        }

        public ScrollBar HorizontalScrollBar
        {
            get { return (ScrollBar)GetValue(HorizontalScrollBarProperty); }
            set { SetValue(HorizontalScrollBarProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalScrollBarProperty =
            DependencyProperty.Register("HorizontalScrollBar", typeof(ScrollBar), typeof(ScrollBarLocator), new FrameworkPropertyMetadata(default(ScrollBar), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 ScrollBarLocator control = d as ScrollBarLocator;

                 if (control == null) return;

                 ScrollBar config = e.NewValue as ScrollBar;

                 config.ValueChanged -= control.Config_ValueChanged;
                 config.ValueChanged += control.Config_ValueChanged;

                 if (e.OldValue is ScrollBar scroll)
                 {
                     scroll.ValueChanged -= control.Config_ValueChanged;
                 }

             }));


        public ScrollBar VerticalScrollBar
        {
            get { return (ScrollBar)GetValue(VerticalScrollBarProperty); }
            set { SetValue(VerticalScrollBarProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalScrollBarProperty =
            DependencyProperty.Register("VerticalScrollBar", typeof(ScrollBar), typeof(ScrollBarLocator), new FrameworkPropertyMetadata(default(ScrollBar), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 ScrollBarLocator control = d as ScrollBarLocator;

                 if (control == null) return;

                 ScrollBar config = e.NewValue as ScrollBar;

                 config.ValueChanged -= control.Config_ValueChanged;
                 config.ValueChanged += control.Config_ValueChanged;

                 if (e.OldValue is ScrollBar scroll)
                 {
                     scroll.ValueChanged -= control.Config_ValueChanged;
                 }

             }));


        private void Config_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ////  Do ：更新位置 
            //double l = (this.HorizontalScrollBar.Value - this.HorizontalScrollBar.Minimum) / this.GetExtentWidth() * this.ActualWidth;
            //double t = (this.VerticalScrollBar.Value - this.VerticalScrollBar.Minimum) / this.GetExtentHeight() * this.ActualHeight;
            //  Do ：更新位置 
            double h = this.GetRelativeLeft(this.HorizontalScrollBar.Value);
            double v = this.GetRelativeTop(this.VerticalScrollBar.Value);
            Canvas.SetLeft(this.Mask, h);
            Canvas.SetTop(this.Mask, v);
            this.RefreshLocation();

            if (this.Mask.Background is VisualBrush visualBrush)
            {
                double x = (this.HorizontalScrollBar.Value - this.HorizontalScrollBar.Minimum) / this.GetExtentWidth();
                double y = (this.VerticalScrollBar.Value - this.VerticalScrollBar.Minimum) / this.GetExtentHeight();

                double width = this.GetViewportWidth() / this.GetExtentWidth();
                double hight = this.GetViewPortHeigth() / this.GetExtentHeight();

                visualBrush.Viewbox = new Rect(x, y, width, hight);
            }
        }




        protected override bool Checked()
        {
            if (this.HorizontalScrollBar == null) return false;
            if (this.VerticalScrollBar == null) return false;

            return true;
        }
        protected override void LocationPoint(Point p)
        {
            if (p.X.IsNaN() || p.Y.IsNaN()) return;

            double h = this.GetScrollHorizontalOffset(p.X);

            double v = this.GetScrollVerticalOffset(p.Y);

            this.HorizontalScrollBar.Value = h + this.HorizontalScrollBar.Minimum;

            this.VerticalScrollBar.Value = v + this.VerticalScrollBar.Minimum;
        }

        //  Do ：更新布局
        protected override void RefreshLocation()
        {
            if (this.Canvas == null || this.Mask == null) return;

            this.Canvas.Width = this.GetVisualWidth();
            this.Canvas.Height = this.GetVisualHeight();


            this.Mask.Width = this.GetRelativeWidth(this.HorizontalScrollBar.ViewportSize);
            this.Mask.Height = this.GetRelativeHeight(this.VerticalScrollBar.ViewportSize);
        }

        private double GetScrollHorizontalOffset(double h)
        {
            return (h / this.GetVisualWidth()) * this.GetExtentWidth();
        }

        private double GetScrollVerticalOffset(double h)
        {
            return (h / this.GetVisualHeight()) * this.GetExtentHeight();
        }

        private double GetRelativeWidth(double value)
        {
            double v = this.GetVisualWidth();

            return v * (value / this.GetExtentWidth());
        }

        private double GetRelativeHeight(double value)
        {
            double v = this.GetVisualHeight();

            return v * ((value) / this.GetExtentHeight());
        }

        private double GetRelativeLeft(double value)
        {
            double v = this.GetVisualWidth();

            return v * ((value - this.HorizontalScrollBar.Minimum) / this.GetExtentWidth());
        }

        private double GetRelativeTop(double value)
        {
            double v = this.GetVisualHeight();

            return v * ((value - this.VerticalScrollBar.Minimum) / this.GetExtentHeight());
        }



        /// <summary> 计算缩略图的宽度 </summary>
        private double GetVisualWidth()
        {
            double x1 = this.ActualWidth / this.ActualHeight;

            double x2 = this.HorizontalScrollBar.ActualWidth / this.VerticalScrollBar.ActualHeight;



            if (x1 > x2)
            {
                return x2 * (this.ActualWidth / x1);
            }

            return this.ActualWidth;
        }

        /// <summary> 计算缩略图的高度 </summary> 
        private double GetVisualHeight()
        {
            double x1 = this.ActualHeight / this.ActualWidth;

            //double x2 = this.GetExtentHeight() / this.GetExtentWidth();

            double x2 = this.VerticalScrollBar.ActualHeight / this.HorizontalScrollBar.ActualWidth;


            if (x1 > x2)
            {
                return x2 * (this.ActualHeight / x1);
            }

            return this.ActualHeight;
        }

        private double GetExtentWidth()
        {
            return this.HorizontalScrollBar.Maximum - this.HorizontalScrollBar.Minimum + this.HorizontalScrollBar.ViewportSize;
        }

        private double GetExtentHeight()
        {
            return this.VerticalScrollBar.Maximum - this.VerticalScrollBar.Minimum + this.VerticalScrollBar.ViewportSize;
        }

        private double GetViewportWidth()
        {
            return this.HorizontalScrollBar.ViewportSize;
        }

        private double GetViewPortHeigth()
        {
            return this.VerticalScrollBar.ViewportSize;
        }

        protected override void WheelPoint(Point p, double Delta)
        {
           
        }
    }
}
