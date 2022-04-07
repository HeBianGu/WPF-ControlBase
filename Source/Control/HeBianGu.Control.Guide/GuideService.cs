// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace HeBianGu.Control.Guide
{
    public static class GuideService
    {
        /// <summary>
        /// 说明
        /// </summary>
        public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.RegisterAttached(
            "IsVisible", typeof(bool), typeof(GuideService), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsVisibleChanged));

        public static bool GetIsVisible(DependencyObject d)
        {
            return (bool)d.GetValue(IsVisibleProperty);
        }

        public static void SetIsVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsVisibleProperty, value);
        }

        private static void OnIsVisibleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement element = sender as FrameworkElement;
            UIElement parent = GuideService.GetGuideParent(element);
            if (parent == null) return;

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(parent);
            if (layer == null) return;

            bool v = (bool)args.NewValue;

            Brush stroke = GuideService.GetStroke(element);
            Brush fill = GuideService.GetFill(element);
            string text = GuideService.GetText(element);
            Point offset = GuideService.GetTextOffset(element);

            if (v)
            {
                //  Do ：添加向导
                GuideAdorner adorner = new GuideAdorner(parent, element)
                {
                    Stroke = stroke,
                    Fill = fill,
                    Text = text,
                    TextOffset = offset
                };

                layer.Add(adorner);
            }
            else
            {
                //  Do ：移除向导 
                Adorner[] finds = layer.GetAdorners(parent);

                if (finds == null) return;

                foreach (Adorner item in finds)
                {
                    if (item is GuideAdorner toolTip)
                    {
                        if (toolTip.Child == element)
                        {
                            layer.Remove(toolTip);
                            return;
                        }
                    }
                }
            }
        }



        /// <summary>
        /// 说明
        /// </summary>
        public static readonly DependencyProperty GuideParentProperty = DependencyProperty.RegisterAttached(
            "GuideParent", typeof(UIElement), typeof(GuideService), new FrameworkPropertyMetadata(default(UIElement), OnGuideParentChanged));

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
        public static readonly DependencyProperty StrokeProperty = DependencyProperty.RegisterAttached(
            "Stroke", typeof(Brush), typeof(GuideService), new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnStrokeChanged));

        public static Brush GetStroke(DependencyObject d)
        {
            return (Brush)d.GetValue(StrokeProperty);
        }

        public static void SetStroke(DependencyObject obj, Brush value)
        {
            obj.SetValue(StrokeProperty, value);
        }

        private static void OnStrokeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        /// <summary>
        /// 说明
        /// </summary>
        public static readonly DependencyProperty FillProperty = DependencyProperty.RegisterAttached(
            "Fill", typeof(Brush), typeof(GuideService), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black) { Opacity = 0.5 }, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnFillChanged));

        public static Brush GetFill(DependencyObject d)
        {
            return (Brush)d.GetValue(FillProperty);
        }

        public static void SetFill(DependencyObject obj, Brush value)
        {
            obj.SetValue(FillProperty, value);
        }

        private static void OnFillChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        /// <summary>
        /// 说明
        /// </summary>
        public static readonly DependencyProperty TextOffsetProperty = DependencyProperty.RegisterAttached(
            "TextOffset", typeof(Point), typeof(GuideService), new FrameworkPropertyMetadata(new Point(0, -30), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextOffsetChanged));

        public static Point GetTextOffset(DependencyObject d)
        {
            return (Point)d.GetValue(TextOffsetProperty);
        }

        public static void SetTextOffset(DependencyObject obj, Point value)
        {
            obj.SetValue(TextOffsetProperty, value);
        }

        private static void OnTextOffsetChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }

        /// <summary>
        /// 说明
        /// </summary>
        public static readonly DependencyProperty TextForegroundProperty = DependencyProperty.RegisterAttached(
            "TextForeground", typeof(Brush), typeof(GuideService), new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextForegroundChanged));

        public static Brush GetTextForeground(DependencyObject d)
        {
            return (Brush)d.GetValue(TextForegroundProperty);
        }

        public static void SetTextForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(TextForegroundProperty, value);
        }

        private static void OnTextForegroundChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }



        /// <summary>
        /// 说明
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached(
            "Text", typeof(string), typeof(GuideService), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextChanged));

        public static string GetText(DependencyObject d)
        {
            return (string)d.GetValue(TextProperty);
        }

        public static void SetText(DependencyObject obj, string value)
        {
            obj.SetValue(TextProperty, value);
        }

        private static void OnTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        public static bool GetUseGuide(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseGuideProperty);
        }

        public static void SetUseGuide(DependencyObject obj, bool value)
        {
            obj.SetValue(UseGuideProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseGuideProperty =
            DependencyProperty.RegisterAttached("UseGuide", typeof(bool), typeof(GuideService), new PropertyMetadata(false, OnUseGuideChanged));

        public static void OnUseGuideChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;
            bool n = (bool)e.NewValue;
            bool o = (bool)e.OldValue;
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
            DependencyProperty.RegisterAttached("BeginGuide", typeof(bool), typeof(GuideService), new PropertyMetadata(false, OnBeginGuideChanged));

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

            System.Collections.Generic.IEnumerable<FrameworkElement> children = parent.GetChildren<FrameworkElement>(l => GuideService.GetUseGuide(l));

            if (children == null || children.Count() == 0) return;
            FrameworkElement child = children.FirstOrDefault();
            Brush stroke = GuideService.GetStroke(child);
            Brush fill = GuideService.GetFill(child);
            string text = GuideService.GetText(child);
            Point offset = GuideService.GetTextOffset(child);

            //  Do ：添加向导
            GuideAdorner adorner = new GuideAdorner(adornerElement, child)
            {
                Stroke = stroke,
                Fill = fill,
                Text = text,
                TextOffset = offset
            };

            layer.Add(adorner);
        }


        public static object GetData(DependencyObject obj)
        {
            return obj.GetValue(DataProperty);
        }

        public static void SetData(DependencyObject obj, object value)
        {
            obj.SetValue(DataProperty, value);
        }


        public static string GetTitle(DependencyObject obj)
        {
            return (string)obj.GetValue(TitleProperty);
        }

        public static void SetTitle(DependencyObject obj, string value)
        {
            obj.SetValue(TitleProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.RegisterAttached("Title", typeof(string), typeof(GuideService), new PropertyMetadata(null, OnTitleChanged));

        public static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            string n = (string)e.NewValue;

            string o = (string)e.OldValue;
        }


        public static string GetParentTitle(DependencyObject obj)
        {
            return (string)obj.GetValue(ParentTitleProperty);
        }

        public static void SetParentTitle(DependencyObject obj, string value)
        {
            obj.SetValue(ParentTitleProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty ParentTitleProperty =
            DependencyProperty.RegisterAttached("ParentTitle", typeof(string), typeof(GuideService), new PropertyMetadata(null, OnParentTitleChanged));

        public static void OnParentTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            string n = (string)e.NewValue;

            string o = (string)e.OldValue;
        }



        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.RegisterAttached("Data", typeof(object), typeof(GuideService), new PropertyMetadata(null, OnDataChanged));

        public static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            object n = e.NewValue;

            object o = e.OldValue;
        }


        public static DataTemplate GetDataTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(DataTemplateProperty);
        }

        public static void SetDataTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(DataTemplateProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty DataTemplateProperty =
            DependencyProperty.RegisterAttached("DataTemplate", typeof(DataTemplate), typeof(GuideService), new PropertyMetadata(null, OnDataTemplateChanged));

        public static void OnDataTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            DataTemplate n = (DataTemplate)e.NewValue;

            DataTemplate o = (DataTemplate)e.OldValue;
        }



        public static bool GetIsGuide(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsGuideProperty);
        }

        public static void SetIsGuide(DependencyObject obj, bool value)
        {
            obj.SetValue(IsGuideProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty IsGuideProperty =
            DependencyProperty.RegisterAttached("IsGuide", typeof(bool), typeof(GuideService), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsGuideChanged));

        public static void OnIsGuideChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            bool n = (bool)e.NewValue;
            bool o = (bool)e.OldValue;
        }


        public static bool GetUseClick(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseClickProperty);
        }

        public static void SetUseClick(DependencyObject obj, bool value)
        {
            obj.SetValue(UseClickProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseClickProperty =
            DependencyProperty.RegisterAttached("UseClick", typeof(bool), typeof(GuideService), new PropertyMetadata(false, OnUseClickChanged));

        public static void OnUseClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }

    }
}
