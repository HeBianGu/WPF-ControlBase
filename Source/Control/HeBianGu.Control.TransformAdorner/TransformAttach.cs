// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Documents;

namespace HeBianGu.Control.TransformAdorner
{
    public static class TransformAttach
    {
        /// <summary>
        /// 说明
        /// </summary>
        public static readonly DependencyProperty ThumbStyleProperty = DependencyProperty.RegisterAttached(
            "ThumbStyle", typeof(Style), typeof(TransformAttach), new FrameworkPropertyMetadata(default(Style), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnThumbStyleChanged));

        public static Style GetThumbStyle(DependencyObject d)
        {
            return (Style)d.GetValue(ThumbStyleProperty);
        }

        public static void SetThumbStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(ThumbStyleProperty, value);
        }

        private static void OnThumbStyleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        /// <summary>
        /// 说明
        /// </summary>
        public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.RegisterAttached(
            "IsVisible", typeof(bool), typeof(TransformAttach), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsVisibleChanged));

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

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(element);

            if (layer == null)
            {
                element.Loaded += (l, k) =>
                {
                    OnIsVisibleChanged(sender, args);
                };

                return;

            }

            bool v = (bool)args.NewValue;

            Style style = TransformAttach.GetThumbStyle(element);

            if (v)
            {
                //  Do ：添加
                TransformAdorner adorner = new TransformAdorner(element, style);
                layer.Add(adorner);
            }
            else
            {
                //  Do ：移除
                Adorner[] finds = layer.GetAdorners(element);

                if (finds == null) return;

                foreach (Adorner item in finds)
                {
                    if (item is TransformAdorner transform)
                    {
                        layer.Remove(transform);
                    }
                }

            }
        }

    }
}
