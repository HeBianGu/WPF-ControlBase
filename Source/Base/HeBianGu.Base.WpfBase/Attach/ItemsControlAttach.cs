// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections;
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

        public static void OnInnerSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ItemsControl control = d as ItemsControl;

            IEnumerable n = (IEnumerable)e.NewValue;

            IEnumerable o = (IEnumerable)e.OldValue;

            RefreshInnerSource(control);
        }


        public static object GetTools(DependencyObject obj)
        {
            return obj.GetValue(ToolsProperty);
        }

        public static void SetTools(DependencyObject obj, object value)
        {
            obj.SetValue(ToolsProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty ToolsProperty =
            DependencyProperty.RegisterAttached("Tools", typeof(object), typeof(ItemsControlService), new PropertyMetadata(default(object), OnToolsChanged));

        public static void OnToolsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ItemsControl control = d as ItemsControl;

            object n = e.NewValue;

            object o = e.OldValue;

            RefreshInnerSource(control);
        }


        public static ControlTemplate GetHomeTool(DependencyObject obj)
        {
            return obj.GetValue(HomeToolProperty) as ControlTemplate;
        }

        public static void SetHomeTool(DependencyObject obj, ControlTemplate value)
        {
            obj.SetValue(HomeToolProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty HomeToolProperty =
            DependencyProperty.RegisterAttached("HomeTool", typeof(ControlTemplate), typeof(ItemsControlService), new PropertyMetadata(null, OnHomeToolChanged));

        public static void OnHomeToolChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ItemsControl control = d as ItemsControl;
            object n = e.NewValue;
            object o = e.OldValue;
            RefreshInnerSource(control);
        }


        public static ControlTemplate GetEndTool(DependencyObject obj)
        {
            return obj.GetValue(EndToolProperty) as ControlTemplate;
        }

        public static void SetEndTool(DependencyObject obj, ControlTemplate value)
        {
            obj.SetValue(EndToolProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty EndToolProperty =
            DependencyProperty.RegisterAttached("EndTool", typeof(ControlTemplate), typeof(ItemsControlService), new PropertyMetadata(null, OnEndToolChanged));

        public static void OnEndToolChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ItemsControl control = d as ItemsControl;
            object n = e.NewValue;
            object o = e.OldValue;
            RefreshInnerSource(control);
        }

        private static void RefreshInnerSource(ItemsControl control)
        {
            CompositeCollection compositeCollection = new CompositeCollection();
            CollectionContainer container = new CollectionContainer();
            IEnumerable source = ItemsControlService.GetInnerSource(control);
            container.Collection = source;
            ControlTemplate front = ItemsControlService.GetHomeTool(control) as ControlTemplate;
            if (front != null)
            {
                ContentControl home = new ContentControl();
                home.Template = front;
                home.HorizontalAlignment= HorizontalAlignment.Stretch;
                home.VerticalAlignment = VerticalAlignment.Stretch;
                home.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                home.VerticalContentAlignment = VerticalAlignment.Stretch;
                compositeCollection.Add(home);
            }

            compositeCollection.Add(container);
            ControlTemplate behind = ItemsControlService.GetEndTool(control) as ControlTemplate;
            if (behind != null)
            {
                ContentControl end = new ContentControl();
                end.Template = behind;
                compositeCollection.Add(end);
            }

            control.ItemsSource = compositeCollection;
        }


        public static IList GetSelectedItems(DependencyObject obj)
        {
            return (IList)obj.GetValue(SelectedItemsProperty);
        }

        public static void SetSelectedItems(DependencyObject obj, IList value)
        {
            obj.SetValue(SelectedItemsProperty, value);
        }

        //Using a DependencyProperty as the backing store for SelectedItems.  This enables animation, styling, binding, etc...

        public static readonly DependencyProperty SelectedItemsProperty =

            DependencyProperty.RegisterAttached("SelectedItems", typeof(IList), typeof(ItemsControlService), new FrameworkPropertyMetadata(OnSelectedItemsChanged));

        public static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ListBox listBox = d as ListBox;

            if ((listBox != null) && (listBox.SelectionMode == SelectionMode.Multiple))
            {
                if (e.OldValue != null)
                {
                    listBox.SelectionChanged -= OnlistBoxSelectionChanged;
                }

                IList collection = e.NewValue as IList;
                listBox.SelectedItems.Clear();
                if (collection != null)
                {
                    foreach (object item in collection)
                    {
                        listBox.SelectedItems.Add(item);
                    }
                    listBox.SelectionChanged += OnlistBoxSelectionChanged;
                }
            }
        }

        private static void OnlistBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IList dataSource = GetSelectedItems(sender as DependencyObject);

            IEnumerable inner = ItemsControlService.GetInnerSource(sender as DependencyObject);

            foreach (object item in e.AddedItems)
            {
                if (inner is IList list)
                {
                    if (!list.Contains(item))
                        continue;
                }

                if (dataSource.Contains(item))
                    continue;

                dataSource.Add(item);
            }

            foreach (object item in e.RemovedItems)
            {
                dataSource.Remove(item);
            }
        }
    }


}
