// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HeBianGu.Base.WpfBase
{

    public abstract class ButtonBehaviorBase : Behavior<Button>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Click += AssociatedObject_Click;
        }

        private void AssociatedObject_Click(object sender, RoutedEventArgs e)
        {
            this.OnClick();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= AssociatedObject_Click;
        }

        protected abstract void OnClick();
    }

    public abstract class ItemsSourceButtonBehaviorBase : ButtonBehaviorBase
    {
        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IList), typeof(ItemsSourceButtonBehaviorBase), new FrameworkPropertyMetadata(default(IList), (d, e) =>
            {
                ItemsSourceButtonBehaviorBase control = d as ItemsSourceButtonBehaviorBase;

                if (control == null) return;

                if (e.OldValue is IList o)
                {

                }

                if (e.NewValue is IList n)
                {

                }
            }));
    }

    public abstract class AddItemButtonBehaviorBase : ItemsSourceButtonBehaviorBase
    {
        public object DefaultValue
        {
            get { return GetValue(DefaultValueProperty); }
            set { SetValue(DefaultValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultValueProperty =
            DependencyProperty.Register("DefaultValue", typeof(object), typeof(AddItemButtonBehaviorBase), new FrameworkPropertyMetadata(default(object), (d, e) =>
            {
                AddItemButtonBehaviorBase control = d as AddItemButtonBehaviorBase;

                if (control == null) return;

                if (e.OldValue is object o)
                {

                }

                if (e.NewValue is object n)
                {

                }

            }));

        protected object CreateNewItem()
        {
            if (this.DefaultValue == null && this.ItemsSource == null) return null;

            if (this.ItemsSource == null)
            {
                this.ItemsSource = this.DefaultValue.GetType().CreateObservableCollection<IList>();
            }

            if (this.DefaultValue is ICloneable cloneable)
            {
                return cloneable.Clone();
            }

            if (this.ItemsSource.TryCreateItem(out object instance))
            {
                return instance;
            }
            else
            {
                return null;
            }
        }
    }

    public class AddItemButtonBehavior : AddItemButtonBehaviorBase
    {
        protected override void OnClick()
        {
            object addItem = this.CreateNewItem();

            if (addItem == null) return;

            this.ItemsSource.Add(addItem);
        }
    }

    public class InsertItemButtonBehavior : AddItemButtonBehaviorBase
    {

        public int Index
        {
            get { return (int)GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IndexProperty =
            DependencyProperty.Register("Index", typeof(int), typeof(InsertItemButtonBehavior), new FrameworkPropertyMetadata(0, (d, e) =>
             {
                 InsertItemButtonBehavior control = d as InsertItemButtonBehavior;

                 if (control == null) return;

                 if (e.OldValue is int o)
                 {

                 }

                 if (e.NewValue is int n)
                 {

                 }

             }));

        protected override void OnClick()
        {
            object addItem = this.CreateNewItem();

            if (addItem == null) return;

            this.ItemsSource.Insert(this.Index, addItem);
        }
    }

    public class RemoveItemVisualTreeButtonBehavior : ButtonBehaviorBase
    {
        protected override void OnClick()
        {
            ItemsControl itemsControl = this.AssociatedObject.GetParent<ItemsControl>();
            if (itemsControl.ItemsSource is IList list)
                list.Remove(this.AssociatedObject.DataContext);
        }
    }

    public class RemoveItemButtonBehavior : ItemsSourceButtonBehaviorBase
    {
        public object Item
        {
            get { return GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(object), typeof(RemoveItemButtonBehavior), new FrameworkPropertyMetadata(default(object), (d, e) =>
             {
                 RemoveItemButtonBehavior control = d as RemoveItemButtonBehavior;

                 if (control == null) return;

                 if (e.OldValue is object o)
                 {

                 }

                 if (e.NewValue is object n)
                 {

                 }

             }));

        protected override void OnClick()
        {
            if (this.ItemsSource == null) 
                return;
            if (this.Item == null) 
                return;
            this.ItemsSource.Remove(this.Item);
        }
    }


    public class ClearItemButtonBehavior : ItemsSourceButtonBehaviorBase
    {
        protected override void OnClick()
        {
            if (this.ItemsSource == null) 
                return;
            this.ItemsSource.Clear();
        }
    }

    public class RemoveAllCheckedItemButtonBehavior : ItemsSourceButtonBehaviorBase
    {

        public ListBox ListBox
        {
            get { return (ListBox)GetValue(ListBoxProperty); }
            set { SetValue(ListBoxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListBoxProperty =
            DependencyProperty.Register("ListBox", typeof(ListBox), typeof(RemoveAllCheckedItemButtonBehavior), new FrameworkPropertyMetadata(default(ListBox), (d, e) =>
             {
                 RemoveAllCheckedItemButtonBehavior control = d as RemoveAllCheckedItemButtonBehavior;

                 if (control == null) return;

                 if (e.OldValue is ListBox o)
                 {

                 }

                 if (e.NewValue is ListBox n)
                 {

                 }

             }));

        protected override void OnClick()
        {
            if (this.ListBox == null) return;

            List<object> objs = new List<object>();

            foreach (object item in this.ListBox.Items)
            {
                DependencyObject find = this.ListBox.ItemContainerGenerator.ContainerFromItem(item);

                if (find is ListBoxItem listBoxItem)
                {
                    if (!listBoxItem.IsSelected) continue;

                    objs.Add(item);
                }
            }

            if (this.ItemsSource is IList list)
            {
                foreach (object item in objs)
                {
                    list.Remove(item);
                }
            }


        }
    }

    public class SumSourceContentControlBehavior : Behavior<ContentControl>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            this.Refresh();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }


        public IEnumerable Source
        {
            get { return (IEnumerable)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(IEnumerable), typeof(SumSourceContentControlBehavior), new FrameworkPropertyMetadata(default(IEnumerable), (d, e) =>
            {
                SumSourceContentControlBehavior control = d as SumSourceContentControlBehavior;

                if (control == null) return;

                if (e.OldValue is INotifyCollectionChanged o)
                {
                    o.CollectionChanged -= control.O_CollectionChanged;
                }

                if (e.NewValue is INotifyCollectionChanged n)
                {
                    n.CollectionChanged += control.O_CollectionChanged;
                }

                control.Refresh();
            }));

        private void O_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.Refresh();
        }

        public string PropertyName
        {
            get { return (string)GetValue(PropertyNameProperty); }
            set { SetValue(PropertyNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PropertyNameProperty =
            DependencyProperty.Register("PropertyName", typeof(string), typeof(SumSourceContentControlBehavior), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                SumSourceContentControlBehavior control = d as SumSourceContentControlBehavior;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }

                control.Refresh();


            }));



        public IValueConverter Converter
        {
            get { return (IValueConverter)GetValue(ConverterProperty); }
            set { SetValue(ConverterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConverterProperty =
            DependencyProperty.Register("Converter", typeof(IValueConverter), typeof(SumSourceContentControlBehavior), new FrameworkPropertyMetadata(default(IValueConverter), (d, e) =>
            {
                SumSourceContentControlBehavior control = d as SumSourceContentControlBehavior;

                if (control == null) return;

                if (e.OldValue is IValueConverter o)
                {

                }

                if (e.NewValue is IValueConverter n)
                {

                }

                control.Refresh();
            }));

        private void Refresh()
        {
            if (this.Source == null)
                return;

            Type sourceType = this.Source.GetType();

            if (!sourceType.IsGenericType)
                return;

            Type elementType = sourceType.GetGenericArguments()?.FirstOrDefault();

            System.Reflection.PropertyInfo p = elementType.GetProperty(this.PropertyName);

            if (p == null || !p.CanRead)
                return;

            double sum = 0;
            foreach (object item in this.Source)
            {
                sum = sum + (double)Convert.ChangeType(p.GetValue(item), typeof(double));
            }

            if (this.Converter == null)
            {
                this.AssociatedObject.Content = sum;
            }
            else
            {
                this.AssociatedObject.Content = this.Converter.Convert(sum, null, null, CultureInfo.DefaultThreadCurrentCulture);
            }

        }
    }

}