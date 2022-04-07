// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
            this.Loaded += ScrollViewerTransfor_Loaded;
        }

        private void ScrollViewerTransfor_Loaded(object sender, RoutedEventArgs e)
        {
            if (this._viewBox == null) return;

            if (this.Content is FrameworkElement element)
            {
                _contentSize = new Point(element.ActualWidth, element.ActualHeight);
            }

            this.SetFull(this.IsFullParent);
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
            Point position_canvas = e.GetPosition(this._viewBox);

            double x_position_canvas_percent = position_canvas.X / this._viewBox.ActualWidth;
            double y_position_canvas_percent = position_canvas.Y / this._viewBox.ActualHeight;

            //  Do：获取鼠标站显示窗口GridMouse中的位置
            //var position_gridMouse = e.GetPosition(this._viewBox);
            Point position_gridMouse = e.GetPosition(this._gridAll);

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
        }


        public Visual GetVisual()
        {
            return this._gridCenter;
        }
    }
}
