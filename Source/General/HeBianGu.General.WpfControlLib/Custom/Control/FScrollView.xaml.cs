// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.General.WpfControlLib
{
    public class FScrollView : ScrollViewer
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(FScrollView), "S.FScrollView.Default");
        public static ComponentResourceKey ToolKey => new ComponentResourceKey(typeof(FScrollView), "S.FScrollView.Tool");

        static FScrollView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FScrollView), new FrameworkPropertyMetadata(typeof(FScrollView)));
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (this.MouseWheelOrientation == Orientation.Horizontal)
            {
                if (ViewportWidth + HorizontalOffset >= ExtentWidth && e.Delta <= 0)
                    return;
                if (HorizontalOffset == 0 && e.Delta > 0)
                    return;
                if (e.Handled)
                    return;
                if (ScrollInfo != null)
                {
                    if (e.Delta < 0)
                    {
                        ScrollInfo.MouseWheelRight();
                    }
                    else
                    {
                        ScrollInfo.MouseWheelLeft();
                    }
                }
                e.Handled = true;
            }
            else
            {
                if (ViewportHeight + VerticalOffset >= ExtentHeight && e.Delta <= 0)
                    return;
                if (VerticalOffset == 0 && e.Delta > 0)
                    return;
                base.OnMouseWheel(e);
            }
        }

        protected override void OnScrollChanged(ScrollChangedEventArgs e)
        {
            base.OnScrollChanged(e);

            //if (e.HorizontalChange != 0)
            //{ 
            //    this.IsLefHome = e.HorizontalOffset == 0.0;
            //    this.IsRightEnd = e.HorizontalOffset == e.ExtentWidth;
            //}

            //if (e.VerticalChange != 0)
            //{
            //    this.IsTopHome = e.VerticalOffset == 0.0;
            //    this.IsBottomEnd = e.VerticalOffset == e.ExtentHeight;
            //}
        }

        public bool IsLefHome
        {
            get { return (bool)GetValue(IsLefHomeProperty); }
            set { SetValue(IsLefHomeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLefHomeProperty =
            DependencyProperty.Register("IsLefHome", typeof(bool), typeof(FScrollView), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 FScrollView control = d as FScrollView;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }
             }));


        public bool IsRightEnd
        {
            get { return (bool)GetValue(IsRightEndProperty); }
            set { SetValue(IsRightEndProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsRightEndProperty =
            DependencyProperty.Register("IsRightEnd", typeof(bool), typeof(FScrollView), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 FScrollView control = d as FScrollView;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));


        public bool IsTopHome
        {
            get { return (bool)GetValue(IsTopHomeProperty); }
            set { SetValue(IsTopHomeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsTopHomeProperty =
            DependencyProperty.Register("IsTopHome", typeof(bool), typeof(FScrollView), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 FScrollView control = d as FScrollView;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));


        public bool IsBottomEnd
        {
            get { return (bool)GetValue(IsBottomEndProperty); }
            set { SetValue(IsBottomEndProperty, value); }
        }

        public static readonly DependencyProperty IsBottomEndProperty =
            DependencyProperty.Register("IsBottomEnd", typeof(bool), typeof(FScrollView), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 FScrollView control = d as FScrollView;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));


        public Orientation MouseWheelOrientation
        {
            get { return (Orientation)GetValue(MouseWheelOrientationProperty); }
            set { SetValue(MouseWheelOrientationProperty, value); }
        }

        public static readonly DependencyProperty MouseWheelOrientationProperty =
            DependencyProperty.Register("MouseWheelOrientation", typeof(Orientation), typeof(FScrollView), new FrameworkPropertyMetadata(Orientation.Vertical, (d, e) =>
             {
                 FScrollView control = d as FScrollView;

                 if (control == null) return;

                 if (e.OldValue is Orientation o)
                 {

                 }

                 if (e.NewValue is Orientation n)
                 {

                 }
             }));
    }

    public class ScrollViewerService
    {

        public static bool GetUseLeftLine(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseLeftLineProperty);
        }

        public static void SetUseLeftLine(DependencyObject obj, bool value)
        {
            obj.SetValue(UseLeftLineProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseLeftLineProperty =
            DependencyProperty.RegisterAttached("UseLeftLine", typeof(bool), typeof(ScrollViewerService), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, OnUseLeftLineChanged));

        public static void OnUseLeftLineChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static bool GetUseRightLine(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseRightLineProperty);
        }

        public static void SetUseRightLine(DependencyObject obj, bool value)
        {
            obj.SetValue(UseRightLineProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseRightLineProperty =
            DependencyProperty.RegisterAttached("UseRightLine", typeof(bool), typeof(ScrollViewerService), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, OnUseRightLineChanged));

        public static void OnUseRightLineChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static bool GetUseLeftPage(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseLeftPageProperty);
        }

        public static void SetUseLeftPage(DependencyObject obj, bool value)
        {
            obj.SetValue(UseLeftPageProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseLeftPageProperty =
            DependencyProperty.RegisterAttached("UseLeftPage", typeof(bool), typeof(ScrollViewerService), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, OnUseLeftPageChanged));

        public static void OnUseLeftPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static bool GetUseRightPage(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseRightPageProperty);
        }

        public static void SetUseRightPage(DependencyObject obj, bool value)
        {
            obj.SetValue(UseRightPageProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseRightPageProperty =
            DependencyProperty.RegisterAttached("UseRightPage", typeof(bool), typeof(ScrollViewerService), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, OnUseRightPageChanged));

        public static void OnUseRightPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static bool GetUseLeftHome(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseLeftHomeProperty);
        }

        public static void SetUseLeftHome(DependencyObject obj, bool value)
        {
            obj.SetValue(UseLeftHomeProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseLeftHomeProperty =
            DependencyProperty.RegisterAttached("UseLeftHome", typeof(bool), typeof(ScrollViewerService), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, OnUseLeftHomeChanged));

        public static void OnUseLeftHomeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static bool GetUseRightEnd(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseRightEndProperty);
        }

        public static void SetUseRightEnd(DependencyObject obj, bool value)
        {
            obj.SetValue(UseRightEndProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseRightEndProperty =
            DependencyProperty.RegisterAttached("UseRightEnd", typeof(bool), typeof(ScrollViewerService), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, OnUseRightEndChanged));

        public static void OnUseRightEndChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static bool GetUseTopHome(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseTopHomeProperty);
        }

        public static void SetUseTopHome(DependencyObject obj, bool value)
        {
            obj.SetValue(UseTopHomeProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseTopHomeProperty =
            DependencyProperty.RegisterAttached("UseTopHome", typeof(bool), typeof(ScrollViewerService), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, OnUseTopHomeChanged));

        public static void OnUseTopHomeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static bool GetUseBottomEnd(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseBottomEndProperty);
        }

        public static void SetUseBottomEnd(DependencyObject obj, bool value)
        {
            obj.SetValue(UseBottomEndProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseBottomEndProperty =
            DependencyProperty.RegisterAttached("UseBottomEnd", typeof(bool), typeof(ScrollViewerService), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, OnUseBottomEndChanged));

        public static void OnUseBottomEndChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static bool GetUseUpLine(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseUpLineProperty);
        }

        public static void SetUseUpLine(DependencyObject obj, bool value)
        {
            obj.SetValue(UseUpLineProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseUpLineProperty =
            DependencyProperty.RegisterAttached("UseUpLine", typeof(bool), typeof(ScrollViewerService), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, OnUseUpLineChanged));

        public static void OnUseUpLineChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static bool GetUseUpPage(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseUpPageProperty);
        }

        public static void SetUseUpPage(DependencyObject obj, bool value)
        {
            obj.SetValue(UseUpPageProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseUpPageProperty =
            DependencyProperty.RegisterAttached("UseUpPage", typeof(bool), typeof(ScrollViewerService), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, OnUseUpPageChanged));

        public static void OnUseUpPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static bool GetUseDownLine(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseDownLineProperty);
        }

        public static void SetUseDownLine(DependencyObject obj, bool value)
        {
            obj.SetValue(UseDownLineProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseDownLineProperty =
            DependencyProperty.RegisterAttached("UseDownLine", typeof(bool), typeof(ScrollViewerService), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, OnUseDownLineChanged));

        public static void OnUseDownLineChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static bool GetUseDownPage(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseDownPageProperty);
        }

        public static void SetUseDownPage(DependencyObject obj, bool value)
        {
            obj.SetValue(UseDownPageProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseDownPageProperty =
            DependencyProperty.RegisterAttached("UseDownPage", typeof(bool), typeof(ScrollViewerService), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, OnUseDownPageChanged));

        public static void OnUseDownPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }

    }

}
