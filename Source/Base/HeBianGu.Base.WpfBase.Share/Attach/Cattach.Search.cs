using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Base.WpfBase
{

    static partial class Cattach
    {

        public static bool GetUseSearch(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseSearchProperty);
        }

        public static void SetUseSearch(DependencyObject obj, bool value)
        {
            obj.SetValue(UseSearchProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseSearchProperty =
            DependencyProperty.RegisterAttached("UseSearch", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(false, OnUseSearchChanged));

        static public void OnUseSearchChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Cattach control = d as Cattach;

            //bool n = e.NewValue as bool;

            //bool o = e.OldValue as bool;
        }

        [TypeConverter(typeof(LengthConverter))]
        public static double GetSearchWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(SearchWidthProperty);
        }
        [TypeConverter(typeof(LengthConverter))]
        public static void SetSearchWidth(DependencyObject obj, double value)
        {
            obj.SetValue(SearchWidthProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty SearchWidthProperty =
            DependencyProperty.RegisterAttached("SearchWidth", typeof(double), typeof(Cattach), new FrameworkPropertyMetadata(150.0, OnSearchWidthChanged));

        static public void OnSearchWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Cattach control = d as Cattach;

            //double n = e.NewValue as double;

            //double o = e.OldValue as double;
        }


        public static Dock GetSearchDock(DependencyObject obj)
        {
            return (Dock)obj.GetValue(SearchDockProperty);
        }

        public static void SetSearchDock(DependencyObject obj, Dock value)
        {
            obj.SetValue(SearchDockProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty SearchDockProperty =
            DependencyProperty.RegisterAttached("SearchDock", typeof(Dock), typeof(Cattach), new FrameworkPropertyMetadata(Dock.Right, OnSearchDockChanged));

        static public void OnSearchDockChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            Dock n = (Dock)e.NewValue;

            Dock o = (Dock)e.OldValue;
        }


        public static bool GetSearchUseHistory(DependencyObject obj)
        {
            return (bool)obj.GetValue(SearchUseHistoryProperty);
        }

        public static void SetSearchUseHistory(DependencyObject obj, bool value)
        {
            obj.SetValue(SearchUseHistoryProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty SearchUseHistoryProperty =
            DependencyProperty.RegisterAttached("SearchUseHistory", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(false, OnSearchUseHistoryChanged));

        static public void OnSearchUseHistoryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


    }
}
