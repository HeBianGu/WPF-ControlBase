// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// 新增功能向导
    /// </summary>
    public static partial class Cattach
    {
        public static bool GetUseNewGuide(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseNewGuideProperty);
        }

        public static void SetUseNewGuide(DependencyObject obj, bool value)
        {
            obj.SetValue(UseNewGuideProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseNewGuideProperty =
            DependencyProperty.RegisterAttached("UseNewGuide", typeof(bool), typeof(Cattach), new PropertyMetadata(false, OnUseNewGuideChanged));

        public static void OnUseNewGuideChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;
            bool n = (bool)e.NewValue;
            bool o = (bool)e.OldValue;
        }


        public static object GetNewGuideTitle(DependencyObject obj)
        {
            return (object)obj.GetValue(NewGuideTitleProperty);
        }

        public static void SetNewGuideTitle(DependencyObject obj, object value)
        {
            obj.SetValue(NewGuideTitleProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty NewGuideTitleProperty =
            DependencyProperty.RegisterAttached("NewGuideTitle", typeof(object), typeof(Cattach), new PropertyMetadata(null, OnNewGuideTitleChanged));

        public static void OnNewGuideTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            object n = (object)e.NewValue;

            object o = (object)e.OldValue;
        }


        public static string GetNewGuideParentTitle(DependencyObject obj)
        {
            return (string)obj.GetValue(NewGuideParentTitleProperty);
        }

        public static void SetNewGuideParentTitle(DependencyObject obj, string value)
        {
            obj.SetValue(NewGuideParentTitleProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty NewGuideParentTitleProperty =
            DependencyProperty.RegisterAttached("NewGuideParentTitle", typeof(string), typeof(Cattach), new PropertyMetadata(null, OnNewGuideParentTitleChanged));

        public static void OnNewGuideParentTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            string n = (string)e.NewValue;

            string o = (string)e.OldValue;
        }


        public static object GetNewGuideData(DependencyObject obj)
        {
            return (object)obj.GetValue(NewGuideDataProperty);
        }

        public static void SetNewGuideData(DependencyObject obj, object value)
        {
            obj.SetValue(NewGuideDataProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty NewGuideDataProperty =
            DependencyProperty.RegisterAttached("NewGuideData", typeof(object), typeof(Cattach), new PropertyMetadata(null, OnNewGuideDataChanged));

        public static void OnNewGuideDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            object n = e.NewValue;

            object o = e.OldValue;
        }


        public static DataTemplate GetNewGuideDataTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(NewGuideDataTemplateProperty);
        }

        public static void SetNewGuideDataTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(NewGuideDataTemplateProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty NewGuideDataTemplateProperty =
            DependencyProperty.RegisterAttached("NewGuideDataTemplate", typeof(DataTemplate), typeof(Cattach), new PropertyMetadata(null, OnNewGuideDataTemplateChanged));

        public static void OnNewGuideDataTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            DataTemplate n = (DataTemplate)e.NewValue;

            DataTemplate o = (DataTemplate)e.OldValue;
        }


        public static bool GetIsNewGuide(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsNewGuideProperty);
        }

        public static void SetIsNewGuide(DependencyObject obj, bool value)
        {
            obj.SetValue(IsNewGuideProperty, value);
        }

        public static readonly DependencyProperty IsNewGuideProperty =
            DependencyProperty.RegisterAttached("IsNewGuide", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsNewGuideChanged));

        public static void OnIsNewGuideChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            bool n = (bool)e.NewValue;
            bool o = (bool)e.OldValue;
        }
    }
}
