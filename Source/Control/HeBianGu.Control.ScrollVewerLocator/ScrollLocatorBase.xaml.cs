// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Control.ScrollVewerLocator
{
    [TemplatePart(Name = "PART_MASK", Type = typeof(MaskGrid))]
    [TemplatePart(Name = "PART_Visual", Type = typeof(Canvas))]
    /// <summary> 绑定 ScrollViewer 的鸟瞰图</summary>
    public abstract class ScrollLocatorBase : ContentControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ScrollLocatorBase), "S.ScrollLocatorBase.Default");

        static ScrollLocatorBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollLocatorBase), new FrameworkPropertyMetadata(typeof(ScrollLocatorBase)));
        }

        public ScrollLocatorBase()
        {
            this.SizeChanged += (l, k) =>
            {
                this.RefreshLocation();
            };

            this.Loaded += (l, k) =>
            {
                this.RefreshLocation();
            };

        }

        public Visual Visual
        {
            get { return (Visual)GetValue(VisualProperty); }
            set { SetValue(VisualProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisualProperty =
            DependencyProperty.Register("Visual", typeof(Visual), typeof(ScrollLocatorBase), new PropertyMetadata(default(Visual), (d, e) =>
            {
                ScrollLocatorBase control = d as ScrollLocatorBase;

                if (control == null) return;

                Visual config = e.NewValue as Visual;

            }));


        public Brush CoverBackground
        {
            get { return (Brush)GetValue(CoverBackgroundProperty); }
            set { SetValue(CoverBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CoverBackgroundProperty =
            DependencyProperty.Register("CoverBackground", typeof(Brush), typeof(ScrollLocatorBase), new PropertyMetadata(default(Brush), (d, e) =>
            {
                ScrollLocatorBase control = d as ScrollLocatorBase;

                if (control == null) return;

                Brush config = e.NewValue as Brush;
            }));


        public Brush MaskBackground
        {
            get { return (Brush)GetValue(MaskBackgroundProperty); }
            set { SetValue(MaskBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaskBackgroundProperty =
            DependencyProperty.Register("MaskBackground", typeof(Brush), typeof(ScrollLocatorBase), new FrameworkPropertyMetadata(default(Brush), (d, e) =>
             {
                 ScrollLocatorBase control = d as ScrollLocatorBase;

                 if (control == null) return;

                 if (e.OldValue is Brush o)
                 {

                 }

                 if (e.NewValue is Brush n)
                 {

                 }

             }));


        protected MaskGrid Mask { get; private set; }

        protected Canvas Canvas { get; private set; }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.Mask = Template.FindName("PART_MASK", this) as MaskGrid;

            this.Canvas = Template.FindName("PART_Visual", this) as Canvas;

            this.Mask.LocationChanged += (l, k) =>
            {
                //  Do ：移动滚动条
                if (!this.Checked()) return;

                double left = Canvas.GetLeft(this.Mask);

                double top = Canvas.GetTop(this.Mask);

                this.LocationPoint(new Point(left, top));
            };


            this.Canvas.MouseDown += (l, k) =>
            {
                Point p = k.GetPosition(this.Canvas);
                p.X = p.X - this.Mask.ActualWidth / 2.0;
                p.Y = p.Y - this.Mask.ActualHeight / 2.0;

                this.LocationPoint(p);

                k.Handled = true;
            };

            this.Canvas.PreviewMouseWheel += (l, k) =>
            {
                Point p = k.GetPosition(this.Canvas);
                p.X = p.X - this.Mask.ActualWidth / 2.0;
                p.Y = p.Y - this.Mask.ActualHeight / 2.0;

                this.WheelPoint(p,k.Delta);
            };
        }

        protected abstract void LocationPoint(Point p);

        protected abstract void WheelPoint(Point p,double Delta);

        //  Do ：更新布局
        protected abstract void RefreshLocation();

        protected abstract bool Checked();

    }
}
