using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HeBianGu.Base.WpfBase
{
    public static class ItemsControlService
    {
        public static IEnumerable GetInnerSource(DependencyObject obj)
        {
            return (IEnumerable)obj.GetValue(InnerSourceProperty);
        }

        public static void SetInnerSource(DependencyObject obj, IEnumerable value)
        {
            obj.SetValue(InnerSourceProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty InnerSourceProperty =
            DependencyProperty.RegisterAttached("InnerSource", typeof(IEnumerable), typeof(ItemsControlService), new PropertyMetadata(default(IEnumerable), OnInnerSourceChanged));

        static public void OnInnerSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ItemsControl control = d as ItemsControl;

            IEnumerable n = (IEnumerable)e.NewValue;

            IEnumerable o = (IEnumerable)e.OldValue;

            RefreshInnerSource(control);
        }


        public static object GetTools(DependencyObject obj)
        {
            return (object)obj.GetValue(ToolsProperty);
        }

        public static void SetTools(DependencyObject obj, object value)
        {
            obj.SetValue(ToolsProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty ToolsProperty =
            DependencyProperty.RegisterAttached("Tools", typeof(object), typeof(ItemsControlService), new PropertyMetadata(default(object), OnToolsChanged));

        static public void OnToolsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ItemsControl control = d as ItemsControl;

            object n = (object)e.NewValue;

            object o = (object)e.OldValue;

            RefreshInnerSource(control);
        }


        public static object GetHomeTool(DependencyObject obj)
        {
            return (object)obj.GetValue(HomeToolProperty);
        }

        public static void SetHomeTool(DependencyObject obj, object value)
        {
            obj.SetValue(HomeToolProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty HomeToolProperty =
            DependencyProperty.RegisterAttached("HomeTool", typeof(object), typeof(ItemsControlService), new PropertyMetadata(null, OnHomeToolChanged));

        static public void OnHomeToolChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ItemsControl control = d as ItemsControl;

            object n = (object)e.NewValue;

            object o = (object)e.OldValue;

            RefreshInnerSource(control);
        }


        public static object GetEndTool(DependencyObject obj)
        {
            return (object)obj.GetValue(EndToolProperty);
        }

        public static void SetEndTool(DependencyObject obj, object value)
        {
            obj.SetValue(EndToolProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty EndToolProperty =
            DependencyProperty.RegisterAttached("EndTool", typeof(object), typeof(ItemsControlService), new PropertyMetadata(null, OnEndToolChanged));

        static public void OnEndToolChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ItemsControl control = d as ItemsControl;

            object n = (object)e.NewValue;

            object o = (object)e.OldValue;

            RefreshInnerSource(control);
        } 


        static void RefreshInnerSource(ItemsControl control)
        {
            CompositeCollection compositeCollection = new CompositeCollection();

            CollectionContainer container = new CollectionContainer();

            var source = ItemsControlService.GetInnerSource(control);

            container.Collection = source; 

            var front = ItemsControlService.GetHomeTool(control);

            if (front != null)
            {
                compositeCollection.Add(front);
            }

            compositeCollection.Add(container);

            var behind = ItemsControlService.GetEndTool(control);

            if (behind != null)
            {
                compositeCollection.Add(behind);
            }

            control.ItemsSource = compositeCollection;
        }
    }


}
