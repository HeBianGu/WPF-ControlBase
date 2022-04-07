// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;
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
            //  Do ：注册事件
            viewer.ScrollChanged += ScrollViewer_ScrollChanged;

            //viewer.SizeChanged += ScrollViewer_SizeChanged; 
        }

        //private void ScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    if (sender is FrameworkElement elment)
        //    {
        //        if (elment.IsLoaded)
        //        {
        //            this.RefreshLocation();
        //        }
        //    }
        //}


        //  Do ：更新布局
        protected override void RefreshLocation()
        {
            if (this.Canvas == null || this.Mask == null) return;

            this.Canvas.Width = this.GetVisualWidth();
            this.Canvas.Height = this.GetVisualHeight();

            //  Do ：更新大小
            //this._mask.Width = this.GetRelativeWidth(this.ScrollViewer.ViewportWidth);
            //this._mask.Height = this.GetRelativeHeight(this.ScrollViewer.ViewportHeight); 

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
            //  Do ：注册事件
            viewer.ScrollChanged -= ScrollViewer_ScrollChanged;

            //viewer.SizeChanged -= ScrollViewer_SizeChanged;
        }


        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            //  Do ：更新位置 
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
    }
}
