// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace HeBianGu.Control.ScrollVewerLocator
{
    [TemplatePart(Name = "PART_ViewBox_Default", Type = typeof(Viewbox))]
    [TemplatePart(Name = "PART_Grid_All", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_Grid_Center", Type = typeof(Grid))]
    /// <summary> 带有缩放功能的滚动条 </summary>
    public class ScrollViewerTransfor : ScrollViewer
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ScrollViewerTransfor), "S.ScrollViewerTransfor.Default");

        static ScrollViewerTransfor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollViewerTransfor), new FrameworkPropertyMetadata(typeof(ScrollViewerTransfor)));
        }

        //  Do ：这个东西控制缩放
        private Viewbox _viewBox;

        //  Do ：用来计算滚动的位置
        private Grid _gridAll;

        //  Do ：这个用来控制内容平铺整个窗口
        private Grid _gridCenter;

        //  Do ：按照这个值放大缩小
        private Point _default;

        //  Do ：内容初始值大小，用于计算1:1显示
        private Point _contentSize;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this._viewBox = Template.FindName("PART_ViewBox_Default", this) as Viewbox;

            this._gridAll = Template.FindName("PART_Grid_All", this) as Grid;

            this._gridCenter = Template.FindName("PART_Grid_Center", this) as Grid;

            this.PreviewMouseWheel += _viewBox_MouseWheel;
        }

        public ScrollViewerTransfor()
        {
            this.Loaded += (s, e) => this.Init();
            //this.DataContextChanged += ScrollViewerTransfor_DataContextChanged;
            this.SizeChanged += (s, e) =>
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
                          {
                              this.Init();
                          }));
            };
            DependencyPropertyDescriptor.FromProperty(FrameworkElement.DataContextProperty, typeof(FrameworkElement))
                .AddValueChanged(this, (sender, args) =>
                {
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
                    {
                        this.LocateCenterCallBack = x => this.LocatorCenter(x);
                        this.LocateRectCallBack = x => this.LocatorRect(x);
                    }));
                });
        }

        //private void ScrollViewerTransfor_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    this.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
        //               {
        //                   this.LocateCenterCallBack = x => this.LocatorCenter(x);
        //                   this.LocateRectCallBack = x => this.LocatorRect(x);
        //               }));

        //}


        public void Init()
        {
            if (this._viewBox == null) return;

            if (this.Content is FrameworkElement element)
            {
                //element.InvalidateMeasure();
                //element.InvalidateArrange(); 20220523
                //_contentSize = new Point(element.ActualWidth, element.ActualHeight);

                _contentSize = new Point(element.DesiredSize.Width, element.DesiredSize.Height);
            }

            this.SetFull(this.IsFullParent);

            if (this.UseDefaultCenter)
            {
                this.LocatorCenter();
            }

            //this.LocateCenterCallBack = x => this.LocatorCenter(x);
            //this.LocateRectCallBack = x => this.LocatorRect(x);
        }


        private void ScrollViewerTransfor_Loaded(object sender, RoutedEventArgs e)
        {
            this.Init();
            //if (this._viewBox == null) return;

            //if (this.Content is FrameworkElement element)
            //{
            //    //element.InvalidateMeasure();
            //    //element.InvalidateArrange(); 20220523
            //    //_contentSize = new Point(element.ActualWidth, element.ActualHeight);

            //    _contentSize = new Point(element.DesiredSize.Width, element.DesiredSize.Height);
            //}

            //this.SetFull(this.IsFullParent);

            //if (this.UseDefaultCenter)
            //{
            //    this.LocatorCenter();
            //}

            ////this.LocateCenterCallBack = x => this.LocatorCenter(x);
            ////this.LocateRectCallBack = x => this.LocatorRect(x);
        }

        private void SetFull(bool value)
        {
            if (this._viewBox == null) return;

            if (value)
            {
                this._gridCenter.Width = this.ViewportWidth;
                this._gridCenter.Height = this.ViewportHeight;

                this._viewBox.Width = this.ViewportWidth;
                this._viewBox.Height = this.ViewportHeight;
            }
            else
            {

                this._gridCenter.Width = _contentSize.X;
                this._gridCenter.Height = _contentSize.Y;

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
            Point position_canvas = Mouse.GetPosition(this._viewBox);
            this.SetScale(position_canvas, e.Delta);
        }

        public void SetScale(Point viewBoxPosition, double delta)
        {
            double v = this.Scale + delta / 1200.0;
            double x_position_canvas_percent = viewBoxPosition.X / this._viewBox.ActualWidth;
            double y_position_canvas_percent = viewBoxPosition.Y / this._viewBox.ActualHeight;
            Point position_gridMouse = this._viewBox.TranslatePoint(viewBoxPosition, this._gridAll);
            //if (v < this.MinScale)
            //{
            //    this.Scale = this.MinScale;
            //    MessageProxy.Snacker?.ShowTime("已经设置到最小");
            //}
            //else if (v > this.MaxScale)
            //{
            //    this.Scale = this.MaxScale;
            //    MessageProxy.Snacker?.ShowTime("已经设置到最大");
            //}
            //else
            //{
            //    this.Scale = v;
            //}

            this.Scale = this.CheckScale(v);

            //  Message：根据百分比计算放大后滚轮滚动的位置
            double xvalue = x_position_canvas_percent * this._viewBox.Width - position_gridMouse.X;
            double yvalue = y_position_canvas_percent * this._viewBox.Height - position_gridMouse.Y;

            this.ScrollToHorizontalOffset(xvalue);
            this.ScrollToVerticalOffset(yvalue);
        }

        double CheckScale(double scale)
        {
            if (scale < this.MinScale)
            {
                MessageProxy.Snacker?.ShowTime("已经设置到最小");
                return this.MinScale;
            }
            else if (scale > this.MaxScale)
            {

                MessageProxy.Snacker?.ShowTime("已经设置到最大");
                return this.MaxScale;
            }
            else
            {
                return scale;
            }
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
        }


        //public Rect ZoomRect
        //{
        //    get { return (Rect)GetValue(ZoomRectProperty); }
        //    set { SetValue(ZoomRectProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ZoomRectProperty =
        //    DependencyProperty.Register("ZoomRect", typeof(Rect), typeof(ScrollViewerTransfor), new FrameworkPropertyMetadata(default(Rect), (d, e) =>
        //     {
        //         ScrollViewerTransfor control = d as ScrollViewerTransfor;

        //         if (control == null) return;

        //         if (e.OldValue is Rect o)
        //         {

        //         }

        //         if (e.NewValue is Rect n)
        //         {
        //             //if (n == Rect.Empty)
        //             //    return;
        //             control.LocatorRect(n);
        //             //control.ZoomRect = new Rect(0,0,0,0);
        //         }

        //     }));


        public Action<Point> LocateCenterCallBack
        {
            get { return (Action<Point>)GetValue(LocateCenterCallBackProperty); }
            set { SetValue(LocateCenterCallBackProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LocateCenterCallBackProperty =
            DependencyProperty.Register("LocateCenterCallBack", typeof(Action<Point>), typeof(ScrollViewerTransfor), new FrameworkPropertyMetadata(default(Action<Point>), (d, e) =>
             {
                 ScrollViewerTransfor control = d as ScrollViewerTransfor;

                 if (control == null) return;

                 if (e.OldValue is Action<Point> o)
                 {

                 }

                 if (e.NewValue is Action<Point> n)
                 {

                 }

             }));


        public Action<Rect> LocateRectCallBack
        {
            get { return (Action<Rect>)GetValue(LocateRectCallBackProperty); }
            set { SetValue(LocateRectCallBackProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LocateRectCallBackProperty =
            DependencyProperty.Register("LocateRectCallBack", typeof(Action<Rect>), typeof(ScrollViewerTransfor), new FrameworkPropertyMetadata(default(Action<Rect>), (d, e) =>
             {
                 ScrollViewerTransfor control = d as ScrollViewerTransfor;

                 if (control == null) return;

                 if (e.OldValue is Action<Rect> o)
                 {

                 }

                 if (e.NewValue is Action<Rect> n)
                 {

                 }

             }));






        /// <summary>
        /// 以当前点为中心点显示，不缩放
        /// </summary>
        /// <param name="center"></param>
        public void LocatorCenter(Point center)
        {
            Point tranCenter = new Point((center.X / this._default.X) * this.ExtentWidth, (center.Y / this._default.Y) * this.ExtentHeight);
            this.ScrollToHorizontalOffset(tranCenter.X - this.ViewportWidth / 2);
            this.ScrollToVerticalOffset(tranCenter.Y - this.ViewportHeight / 2);
        }

        /// <summary>
        /// 定位到当前区间，有缩放
        /// </summary>
        /// <param name="rect"></param>
        public void LocatorRect(Rect rect)
        {
            if (this.IsLoaded == false)
                return;
            if (rect.Width == 0 || rect.Height == 0)
                return;
            double extendX = this.ViewportWidth / (rect.Width / this._default.X);
            double scaleX = extendX / this._default.X;
            double extendY = this.ViewportHeight / (rect.Height / this._default.Y);
            double scaleY = extendY / this._default.Y;
            double scale = Math.Min(scaleX, scaleY);
            this.Scale = this.CheckScale(scale);
            this.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
            {
                Point center = new Point(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
                this.LocatorCenter(center);
            }));
        }

        public Visual GetVisual()
        {
            return this._gridCenter;
        }

        public void SetScaleToViewCenter(double delta)
        {
            Point center = new Point(this._viewBox.ActualWidth / 2.0, this._viewBox.ActualHeight / 2.0);
            this.SetScale(center, delta);
        }

        public void LocatorCenter()
        {
            Point center = new Point(this.ExtentWidth / 2.0 - this.ActualWidth / 2, this.ExtentHeight / 2.0 - this.ActualHeight / 2);
            this.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
            {
                this.ScrollToHorizontalOffset(center.X);
                this.ScrollToVerticalOffset(center.Y);
            }));
        }

        //public void AnimationLocation(Point location)
        //{
        //    Storyboard storyboard = new Storyboard();
        //    PointAnimation animation = new PointAnimation();
        //    animation.To = location;
        //    animation.Duration = new Duration(TimeSpan.FromSeconds(1));
        //    Storyboard.SetTarget(animation, this);
        //    Storyboard.SetTargetProperty(
        //        animation, new PropertyPath(ScrollViewerTransfor.LocationProperty));
        //    storyboard.Children.Add(animation);
        //    storyboard.Begin();
        //}

        //public Point CenterLocation
        //{
        //    get { return (Point)GetValue(CenterLocationProperty); }
        //    set { SetValue(CenterLocationProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty CenterLocationProperty =
        //    DependencyProperty.Register("CenterLocation", typeof(Point), typeof(ScrollViewerTransfor), new FrameworkPropertyMetadata(default(Point), (d, e) =>
        //     {
        //         ScrollViewerTransfor control = d as ScrollViewerTransfor;

        //         if (control == null) return;

        //         if (e.OldValue is Point o)
        //         {

        //         }

        //         if (e.NewValue is Point n)
        //         {
        //             control.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
        //                        {
        //                            Point center = new Point(n.X - control.ActualWidth / 2, n.Y - control.ActualHeight / 2);
        //                            control.Location = center;
        //                        }));

        //         }

        //     }));

        /// <summary>
        /// 左上点
        /// </summary>
        public Point Location
        {
            get { return (Point)GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LocationProperty =
            DependencyProperty.Register("Location", typeof(Point), typeof(ScrollViewerTransfor), new FrameworkPropertyMetadata(default(Point), (d, e) =>
             {
                ScrollViewerTransfor control = d as ScrollViewerTransfor;

                 if (control == null) return;

                 if (e.OldValue is Point o)
                 {

                 }

                 if (e.NewValue is Point n)
                 {
                     control.ScrollToHorizontalOffset(n.X);
                     control.ScrollToVerticalOffset(n.Y);
                 }
             }));

        protected override void OnScrollChanged(ScrollChangedEventArgs e)
        {
            base.OnScrollChanged(e);
            this.Location = new Point(this.HorizontalOffset, this.VerticalOffset);
            //this.AnimationLocation(new Point(this.HorizontalOffset, this.VerticalOffset));
        }

        public bool UseDefaultCenter
        {
            get { return (bool)GetValue(UseDefaultCenterProperty); }
            set { SetValue(UseDefaultCenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseDefaultCenterProperty =
            DependencyProperty.Register("UseDefaultCenter", typeof(bool), typeof(ScrollViewerTransfor), new FrameworkPropertyMetadata(true, (d, e) =>
             {
                 ScrollViewerTransfor control = d as ScrollViewerTransfor;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {
                     if (n)
                         control.LocatorCenter();
                 }
             }));

    }
}
