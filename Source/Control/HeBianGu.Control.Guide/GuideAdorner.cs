// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace HeBianGu.Control.Guide
{
    public partial class GuideAdorner : BorderAdorner
    {
        public FrameworkElement Child { get; private set; }

        public string Text { get; set; }

        public GuideAdorner(UIElement adornedElement, FrameworkElement child) : base(adornedElement)
        {
            this.Fill = new SolidColorBrush(Colors.Green) { Opacity = 0.5 };

            this.Child = child;
        }

        public Brush Foreground { get; set; } = Brushes.White;

        public Point TextOffset { get; set; } = new Point(0, -50);

        private Button NextButton { get; set; } = new Button();

        protected override void OnRender(DrawingContext dc)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(this.AdornedElement);
            RectangleGeometry outRect = new RectangleGeometry(new Rect(layer.RenderSize));
            Point point = this.Child.TranslatePoint(new Point(0, 0), this.AdornedElement);
            RectangleGeometry childRect = new RectangleGeometry(new Rect(point, this.Child.RenderSize), 2, 2);
            CombinedGeometry combinedGeometry = new CombinedGeometry(GeometryCombineMode.Exclude, outRect, childRect);

            //Rect rect = new Rect(layer.RenderSize);

            dc.DrawGeometry(this.Fill, new Pen(this.Stroke, this.StrokeThickness), combinedGeometry);
            FormattedText text = new FormattedText(this.Text ?? String.Empty, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                new Typeface("宋体"), 20, this.Foreground);
            dc.DrawText(text, point + new Vector(TextOffset.X, TextOffset.Y));

            //dc.draw
        }
    }


    public partial class GuideAdorner
    {
        /// <summary>
        /// 说明
        /// </summary>
        public static readonly DependencyProperty IsShowProperty = DependencyProperty.RegisterAttached(
            "IsShow", typeof(bool), typeof(GuideAdorner), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsShowChanged));

        public static bool GetIsShow(DependencyObject d)
        {
            return (bool)d.GetValue(IsShowProperty);
        }

        public static void SetIsShow(DependencyObject obj, bool value)
        {
            obj.SetValue(IsShowProperty, value);
        }

        private static void OnIsShowChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement element = sender as FrameworkElement;

            bool v = (bool)args.NewValue;
            if (v)
            {
                GuideAdornerProxyer.Instance.ShowAdorner(element);
            }
            else
            {
                GuideAdornerProxyer.Instance.HideAdorner(element);
            }

            //UIElement parent = Cattach.GetGuideParent(element);
            //if (parent == null) return;

            //AdornerLayer layer = AdornerLayer.GetAdornerLayer(parent);
            //if (layer == null) return;

            //bool v = (bool)args.NewValue;

            //Brush stroke = Cattach.GetGuideStroke(element);
            //Brush fill = Cattach.GetGuideFill(element);
            //string text = Cattach.GetGuideText(element);
            //Point offset = Cattach.GetGuideTextOffset(element);

            //if (v)
            //{
            //    //  Do ：添加向导
            //    GuideAdorner adorner = new GuideAdorner(parent, element)
            //    {
            //        Stroke = stroke,
            //        Fill = fill,
            //        Text = text,
            //        TextOffset = offset
            //    };

            //    layer.Add(adorner);
            //}
            //else
            //{
            //    //  Do ：移除向导 
            //    Adorner[] finds = layer.GetAdorners(parent);

            //    if (finds == null) return;

            //    foreach (Adorner item in finds)
            //    {
            //        if (item is GuideAdorner toolTip)
            //        {
            //            if (toolTip.Child == element)
            //            {
            //                layer.Remove(toolTip);
            //                return;
            //            }
            //        }
            //    }
            //}
        }


        public static bool GetBeginGuide(DependencyObject obj)
        {
            return (bool)obj.GetValue(BeginGuideProperty);
        }

        public static void SetBeginGuide(DependencyObject obj, bool value)
        {
            obj.SetValue(BeginGuideProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty BeginGuideProperty =
            DependencyProperty.RegisterAttached("BeginGuide", typeof(bool), typeof(GuideAdorner), new PropertyMetadata(false, OnBeginGuideChanged));

        public static void OnBeginGuideChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement parent = d as FrameworkElement;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;

            FrameworkElement adornerElement = parent;

            if (parent is MainWindowBase main)
            {
                adornerElement = main.GetAdornerElement();
            }

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(adornerElement);

            if (layer == null) return;

            if (!n)
            {
                Adorner[] finds = layer.GetAdorners(parent);
                if (finds == null) return;

                foreach (Adorner item in finds)
                {
                    if (item is GuideAdorner toolTip)
                    {
                        layer.Remove(toolTip);
                    }
                }
                return;
            }

            System.Collections.Generic.IEnumerable<FrameworkElement> children = parent.GetChildren<FrameworkElement>(l => Cattach.GetUseGuide(l));

            if (children == null || children.Count() == 0) return;
            FrameworkElement child = children.FirstOrDefault();
            Brush stroke = GuideAdorner.GetGuideStroke(child);
            Brush fill = GuideAdorner.GetGuideFill(child);
            object text = Cattach.GetTitle(child);
            Point offset = GuideAdorner.GetGuideTextOffset(child);

            //  Do ：添加向导
            GuideAdorner adorner = new GuideAdorner(adornerElement, child)
            {
                Stroke = stroke,
                Fill = fill,
                Text = text?.ToString(),
                TextOffset = offset
            };

            layer.Add(adorner);
        }

        /// <summary>
        /// 说明
        /// </summary>
        public static readonly DependencyProperty GuideParentProperty = DependencyProperty.RegisterAttached(
            "GuideParent", typeof(UIElement), typeof(GuideAdorner), new FrameworkPropertyMetadata(default(UIElement), OnGuideParentChanged));

        public static UIElement GetGuideParent(DependencyObject d)
        {
            return (UIElement)d.GetValue(GuideParentProperty);
        }

        public static void SetGuideParent(DependencyObject obj, UIElement value)
        {
            obj.SetValue(GuideParentProperty, value);
        }

        private static void OnGuideParentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        /// <summary>
        /// 说明
        /// </summary>
        public static readonly DependencyProperty GuidTextForegroundProperty = DependencyProperty.RegisterAttached(
            "GuidTextForeground", typeof(Brush), typeof(GuideAdorner), new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnGuidTextForegroundChanged));

        public static Brush GetGuidTextForeground(DependencyObject d)
        {
            return (Brush)d.GetValue(GuidTextForegroundProperty);
        }

        public static void SetGuidTextForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(GuidTextForegroundProperty, value);
        }

        private static void OnGuidTextForegroundChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }

        /// <summary>
        /// 说明
        /// </summary>
        public static readonly DependencyProperty GuideTextOffsetProperty = DependencyProperty.RegisterAttached(
            "GuideTextOffset", typeof(Point), typeof(GuideAdorner), new FrameworkPropertyMetadata(new Point(0, -30), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnGuideTextOffsetChanged));

        public static Point GetGuideTextOffset(DependencyObject d)
        {
            return (Point)d.GetValue(GuideTextOffsetProperty);
        }

        public static void SetGuideTextOffset(DependencyObject obj, Point value)
        {
            obj.SetValue(GuideTextOffsetProperty, value);
        }

        private static void OnGuideTextOffsetChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        /// <summary>
        /// 说明
        /// </summary>
        public static readonly DependencyProperty GuideFillProperty = DependencyProperty.RegisterAttached(
            "GuideFill", typeof(Brush), typeof(GuideAdorner), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black) { Opacity = 0.5 }, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnGuideFillChanged));

        public static Brush GetGuideFill(DependencyObject d)
        {
            return (Brush)d.GetValue(GuideFillProperty);
        }

        public static void SetGuideFill(DependencyObject obj, Brush value)
        {
            obj.SetValue(GuideFillProperty, value);
        }

        private static void OnGuideFillChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }

        /// <summary>
        /// 说明
        /// </summary>
        public static readonly DependencyProperty GuideStrokeProperty = DependencyProperty.RegisterAttached(
            "GuideStroke", typeof(Brush), typeof(GuideAdorner), new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnGuideStrokeChanged));

        public static Brush GetGuideStroke(DependencyObject d)
        {
            return (Brush)d.GetValue(GuideStrokeProperty);
        }

        public static void SetGuideStroke(DependencyObject obj, Brush value)
        {
            obj.SetValue(GuideStrokeProperty, value);
        }

        private static void OnGuideStrokeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }
    }
}
