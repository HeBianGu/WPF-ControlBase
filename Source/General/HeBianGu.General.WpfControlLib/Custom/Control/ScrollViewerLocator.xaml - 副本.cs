using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{


    [TemplatePart(Name = "PART_MASK", Type = typeof(MaskGrid))]
    [TemplatePart(Name = "PART_Visual", Type = typeof(Canvas))]

    /// <summary> 绑定 ScrollViewer 的鸟瞰图</summary>
    public partial class ScrollViewerLocator : ContentControl
    {
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



        public Brush CoverBackground
        {
            get { return (Brush)GetValue(CoverBackgroundProperty); }
            set { SetValue(CoverBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CoverBackgroundProperty =
            DependencyProperty.Register("CoverBackground", typeof(Brush), typeof(ScrollViewerLocator), new PropertyMetadata(default(Brush), (d, e) =>
             {
                 ScrollViewerLocator control = d as ScrollViewerLocator;

                 if (control == null) return;

                 Brush config = e.NewValue as Brush;
             }));


        MaskGrid _mask;

        Canvas _visual;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this._mask = Template.FindName("PART_MASK", this) as MaskGrid;

            this._visual = Template.FindName("PART_Visual", this) as Canvas;

            this._mask.LocationChanged += (l, k) =>
              {
                  //  Do ：移动滚动条
                  if (this.ScrollViewer == null) return;

                  var left = Canvas.GetLeft(this._mask);

                  var top = Canvas.GetTop(this._mask);

                  this.LocationPoint(new Point(left, top));
              };


            _visual.MouseDown += (l, k) =>
              {
                  //  Do ：定位到指定位置
                  Point p = k.GetPosition(_visual);

                  p.X = p.X - this._mask.ActualWidth / 2.0;
                  p.Y = p.Y - this._mask.ActualHeight / 2.0;

                  this.LocationPoint(p);
              };
        }

        void LocationPoint(Point p)
        {
            double h = this.GetScrollHorizontalOffset(p.X);

            double v = this.GetScrollVerticalOffset(p.Y);

            this.ScrollViewer.ScrollToHorizontalOffset(h);

            this.ScrollViewer.ScrollToVerticalOffset(v);
        }

        void OnAttached(ScrollViewer viewer)
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

        public ScrollViewerLocator()
        {
            //this.SizeChanged += ScrollViewer_SizeChanged;

            //this.Loaded += (l, k) =>
            //  {
            //      this.RefreshLocation();
            //  };

        }

        //  Do ：更新布局
        void RefreshLocation()
        {
            if (this._visual == null || this._mask == null) return;

            this._visual.Width = this.GetVisualWidth();
            this._visual.Height = this.GetVisualHeight();

            //  Do ：更新大小
            //this._mask.Width = this.GetRelativeWidth(this.ScrollViewer.ViewportWidth);
            //this._mask.Height = this.GetRelativeHeight(this.ScrollViewer.ViewportHeight);

            if (this.ScrollViewer.ExtentWidth == 0 || this.ScrollViewer.ExtentHeight == 0) return;

            this._mask.Width = this.GetRelativeWidth(this.ScrollViewer.ActualWidth);
            this._mask.Height = this.GetRelativeHeight(this.ScrollViewer.ActualHeight);
        }

        void OnDetaching(ScrollViewer viewer)
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

            Canvas.SetLeft(this._mask, h);
            Canvas.SetTop(this._mask, v);

            this.RefreshLocation();

            if(this._mask.Background is VisualBrush visualBrush)
            {
                double x = this.ScrollViewer.HorizontalOffset / this.ScrollViewer.ExtentWidth;
                double y = this.ScrollViewer.VerticalOffset / this.ScrollViewer.ExtentHeight;
                double width = this.ScrollViewer.ViewportWidth / this.ScrollViewer.ExtentWidth;
                double hight = this.ScrollViewer.ViewportHeight / this.ScrollViewer.ExtentHeight;
                visualBrush.Viewbox = new Rect(x,y, width, hight);
            }
             
        }

        double GetScrollHorizontalOffset(double h)
        {
            return (h / this.GetVisualWidth()) * this.ScrollViewer.ExtentWidth;
        }

        double GetScrollVerticalOffset(double h)
        {
            return (h / this.GetVisualHeight()) * this.ScrollViewer.ExtentHeight;
        }

        double GetRelativeWidth(double value)
        {
            double v = this.GetVisualWidth();

            return v * (value / this.ScrollViewer.ExtentWidth);
        }

        double GetRelativeHeight(double value)
        {
            double v = this.GetVisualHeight();

            return v * (value / this.ScrollViewer.ExtentHeight);
        }

        /// <summary> 计算缩略图的宽度 </summary>
        double GetVisualWidth()
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
        double GetVisualHeight()
        {
            double x1 = this.ActualHeight / this.ActualWidth;

            double x2 = this.ScrollViewer.ExtentHeight / this.ScrollViewer.ExtentWidth;

            if (x1 > x2)
            {
                return x2 * (this.ActualHeight / x1);
            }

            return this.ActualHeight;
        }
    }

    public class MaskGrid : Grid
    {

        //声明和注册路由事件
        public static readonly RoutedEvent LocationChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("LocationChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(MaskGrid));
        //CLR事件包装
        public event RoutedEventHandler LocationChanged
        {
            add { this.AddHandler(LocationChangedRoutedEvent, value); }
            remove { this.RemoveHandler(LocationChangedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        internal void OnLocationChanged()
        {
            RoutedEventArgs args = new RoutedEventArgs(LocationChangedRoutedEvent, this);
            this.RaiseEvent(args);
        }

    }

    /// <summary> 应用到Canvas的拖拽 </summary>
    public class MaskMouseDragElementBehavior : MouseDragElementBehavior
    {
        protected override void ApplyTranslationTransform(double x, double y)
        {
            Canvas.SetLeft(this.AssociatedObject, Canvas.GetLeft(this.AssociatedObject) + x);
            Canvas.SetTop(this.AssociatedObject, Canvas.GetTop(this.AssociatedObject) + y);

            if (this.AssociatedObject is MaskGrid mask)
            {
                //  Do ：触发位置改变事件
                mask.OnLocationChanged();
            }
        }
    }

    [TemplatePart(Name = "PART_ViewBox_Default", Type = typeof(Viewbox))]
    [TemplatePart(Name = "PART_Grid_All", Type = typeof(Grid))]
    /// <summary> 带有缩放功能的滚动条 </summary>
    public class ScrollViewerTransfor : ScrollViewer
    {

        Viewbox _viewBox;

        Grid _gridAll;

        //  Do ：按照这个值放大缩小
        Point _default;

        //  Do ：内容初始值大小，用于计算1:1显示
        Point _contentSize;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this._viewBox = Template.FindName("PART_ViewBox_Default", this) as Viewbox;

            this._gridAll= Template.FindName("PART_Grid_All", this) as Grid;

            this.PreviewMouseWheel += _viewBox_MouseWheel; 
        }

        public ScrollViewerTransfor()
        { 
            this.Loaded += ScrollViewerTransfor_Loaded;
        }

        private void ScrollViewerTransfor_Loaded(object sender, RoutedEventArgs e)
        {
            if (this._viewBox == null) return;

            if (this.Content is FrameworkElement element)
            {
                _contentSize = new Point(element.ActualWidth, element.Height);
            }

            this.SetFull(this.IsFullParent);
          
        }


        void SetFull(bool value)
        {
            if (this._viewBox == null) return;

            if (value)
            {
                this._viewBox.Width = this.ViewportWidth;
                this._viewBox.Height = this.ViewportHeight;

            }
            else
            {
                this._viewBox.Width = _contentSize.X;
                this._viewBox.Height = _contentSize.Y;
            } 

            //_default = new Point(this._viewBox.ActualWidth, this._viewBox.ActualHeight); 

            _default = new Point(this._viewBox.Width, this._viewBox.Height);


        }
        /// <summary> 平铺满控件 </summary>
        public bool IsFullParent
        {
            get { return (bool)GetValue(IsFullParentProperty); }
            set { SetValue(IsFullParentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFullParentProperty =
            DependencyProperty.Register("IsFullParent", typeof(bool), typeof(ScrollViewerTransfor), new PropertyMetadata(default(bool), (d, e) =>
             {
                 ScrollViewerTransfor control = d as ScrollViewerTransfor;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

                 control.SetFull((bool)e.NewValue);

             }));


        private void _viewBox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double v = this.Scale + e.Delta / 2400.0; 


            ////  Do：找出ViewBox的左边距和上边距
            //double x_viewbox_margrn = (this._gridAll.ActualWidth - this._viewBox.ActualWidth) / 2;
            //double y_viewbox_margrn = (this._gridAll.ActualHeight - this._viewBox.ActualHeight) / 2;

            //x_viewbox_margrn = x_viewbox_margrn < 0 ? 0 : x_viewbox_margrn;
            //y_viewbox_margrn = y_viewbox_margrn < 0 ? 0 : y_viewbox_margrn;


            ////  Do：计算边距百分比
            //double x_viewbox_margrn_percent = x_viewbox_margrn / this._gridAll.ActualWidth;
            //double y_viewbox_margrn_percent = y_viewbox_margrn / this._gridAll.ActualHeight;

            //  Do：获取鼠标在绘图区域Canvas的位置的百分比
            var position_canvas = e.GetPosition(this._viewBox);

            double x_position_canvas_percent = position_canvas.X / this._viewBox.ActualWidth;
            double y_position_canvas_percent = position_canvas.Y / this._viewBox.ActualHeight;

            //  Do：获取鼠标站显示窗口GridMouse中的位置
            var position_gridMouse = e.GetPosition(this._viewBox);
            //var position_gridMouse = e.GetPosition(this._gridAll);

            if (v < this.MinScale)
            {
                this.Scale = this.MinScale; 
            } 
            else if (v > this.MaxScale)
            {
                this.Scale = this.MaxScale; 
            }
            else
            {
                this.Scale = v;
            }

            //  Message：根据百分比计算放大后滚轮滚动的位置
            double xvalue = x_position_canvas_percent * this._viewBox.Width - position_gridMouse.X;
            double yvalue = y_position_canvas_percent * this._viewBox.Height - position_gridMouse.Y;

            this.ScrollToHorizontalOffset(xvalue);
            this.ScrollToVerticalOffset(yvalue);

        }


        public double MaxScale
        {
            get { return (double)GetValue(MaxScaleProperty); }
            set { SetValue(MaxScaleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxScaleProperty =
            DependencyProperty.Register("MaxScale", typeof(double), typeof(ScrollViewerTransfor), new PropertyMetadata(5.0, (d, e) =>
             {
                 ScrollViewerTransfor control = d as ScrollViewerTransfor;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));


        public double MinScale
        {
            get { return (double)GetValue(MinScaleProperty); }
            set { SetValue(MinScaleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinScaleProperty =
            DependencyProperty.Register("MinScale", typeof(double), typeof(ScrollViewerTransfor), new PropertyMetadata(0.1, (d, e) =>
             {
                 ScrollViewerTransfor control = d as ScrollViewerTransfor;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));


        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.Register("Scale", typeof(double), typeof(ScrollViewerTransfor), new PropertyMetadata(1.0, (d, e) =>
            {
                ScrollViewerTransfor control = d as ScrollViewerTransfor;

                if (control == null) return;

                double config = (double)e.NewValue;

                control.Scale = config < 0 ? 0 : config;

                control.RefreshScale();

            }));

        internal void RefreshScale()
        {

            this._viewBox.Width = _default.X * this.Scale;
            this._viewBox.Height = _default.Y * this.Scale;
            //GetOffSetRate();

            //if (imgWidth < 0 || imgHeight < 0) return;

            //vb.Width = Scale * imgWidth;
            //vb.Height = Scale * imgHeight;

            //SetOffSetByRate();

            //this.RefreshMarkVisible();
        }

    }
}
