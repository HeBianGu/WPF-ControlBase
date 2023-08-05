// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.Control.Filter
{
    public class FilterBox : ListBox
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(FilterBox), "S.FilterBox.Default");
        public static ComponentResourceKey ClearKey => new ComponentResourceKey(typeof(FilterBox), "S.FilterBox.Clear");
        public static ComponentResourceKey LabelKey => new ComponentResourceKey(typeof(FilterBox), "S.FilterBox.Label");
        public static ComponentResourceKey LabelClearKey => new ComponentResourceKey(typeof(FilterBox), "S.FilterBox.LabelClear");


        static FilterBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FilterBox), new FrameworkPropertyMetadata(typeof(FilterBox)));
        }

        public static RoutedCommand AddItemCommand = new RoutedCommand();

        public FilterBox()
        {
            CommandBinding binding = new CommandBinding(AddItemCommand, (l, k) =>
             {
                 this.FilterItemCollection.Add(this.NewFilterModel.Copy());
             });

            this.CommandBindings.Add(binding);

            this.SelectionChanged += FilterBox_SelectionChanged;
        }

        private void FilterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.RefreshOut();
        }


        public void RefreshOut()
        {
            if (this.DataSource == null) 
                return;
         
            System.Collections.Generic.IEnumerable<IFilter> filters = this.SelectedItems.OfType<IFilter>();
            IList cache = Activator.CreateInstance(this.DataSource.GetType()) as IList;
            foreach (object item in this.DataSource)
            {
                if (filters == null || filters.Count() == 0)
                {
                    cache.Add(item);
                }
                else
                {
                    bool m = filters.All(l => l.IsMatch(item));

                    if (!m) continue;

                    cache.Add(item);
                }
            }

            this.OutSource = cache;
        }


        public IFilter NewFilterModel
        {
            get { return (IFilter)GetValue(NewFilterModelProperty); }
            set { SetValue(NewFilterModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewFilterModelProperty =
            DependencyProperty.Register("NewFilterModel", typeof(IFilter), typeof(FilterBox), new PropertyMetadata(default(IFilter), (d, e) =>
             {
                 FilterBox control = d as FilterBox;

                 if (control == null) return;

                 IFilter config = e.NewValue as IFilter;

             }));

        public FilterItemCollection FilterItemCollection
        {
            get { return (FilterItemCollection)GetValue(FilterItemCollectionProperty); }
            set { SetValue(FilterItemCollectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterItemCollectionProperty =
            DependencyProperty.Register("FilterItemCollection", typeof(FilterItemCollection), typeof(FilterBox), new PropertyMetadata(new FilterItemCollection(), (d, e) =>
             {
                 FilterBox control = d as FilterBox;

                 if (control == null) return;

                 FilterItemCollection config = e.NewValue as FilterItemCollection;

             }));


        public IEnumerable DataSource
        {
            get { return (IEnumerable)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(IEnumerable), typeof(FilterBox), new PropertyMetadata(default(IEnumerable), (d, e) =>
             {
                 FilterBox control = d as FilterBox;

                 if (control == null) return;

                 {
                     IEnumerable config = e.NewValue as IEnumerable;

                     if (config is INotifyCollectionChanged collection)
                     {
                         collection.CollectionChanged += control.Collection_CollectionChanged;
                     }
                 }

                 {
                     IEnumerable config = e.OldValue as IEnumerable;

                     if (config is INotifyCollectionChanged collection)
                     {
                         collection.CollectionChanged -= control.Collection_CollectionChanged;
                     }
                 }

                 control.RefreshSetting();
                 control.RefreshOut();
             }));

        private void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.RefreshSetting();
            this.RefreshOut();
        }

        public IEnumerable OutSource
        {
            get { return (IEnumerable)GetValue(OutSourceProperty); }
            set { SetValue(OutSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutSourceProperty =
            DependencyProperty.Register("OutSource", typeof(IEnumerable), typeof(FilterBox), new PropertyMetadata(default(IEnumerable), (d, e) =>
             {
                 FilterBox control = d as FilterBox;

                 if (control == null) return;

                 IEnumerable config = e.NewValue as IEnumerable;

             }));

        private void RefreshSetting()
        {
            if (this.DataSource == null) return;

            Type ctype = this.DataSource.GetType();
            if (!ctype.IsGenericType) return;
            Type type = ctype.GetGenericArguments()?.FirstOrDefault();
            if (type == null) return;
            if (typeof(IModelViewModel).IsAssignableFrom(type) && type.IsGenericType)
            {
                type = type.GetGenericArguments()?.FirstOrDefault();
            }

            //if (!string.IsNullOrEmpty(this.PropertyPath))
            //{
            //    type = type.GetProperty(this.PropertyPath).PropertyType;
            //}


            System.Collections.Generic.IEnumerable<PropertyInfo> ps = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Where(l => l.PropertyType.IsPrimitive || l.PropertyType.IsEnum || l.PropertyType.Equals(typeof(string)) || l.PropertyType.Equals(typeof(DateTime)));
            ps = ps.Where(l => l.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false).Where(l => l.CanRead);
            this.PropertySource = ps.Select(l => FilterFactory.Create(l, this.DataSource))?.ToObservable();
        }


        public ObservableCollection<IFilter> PropertySource
        {
            get { return (ObservableCollection<IFilter>)GetValue(PropertySourceProperty); }
            set { SetValue(PropertySourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PropertySourceProperty =
            DependencyProperty.Register("PropertySource", typeof(ObservableCollection<IFilter>), typeof(FilterBox), new PropertyMetadata(default(ObservableCollection<IFilter>), (d, e) =>
             {
                 FilterBox control = d as FilterBox;

                 if (control == null) return;

                 ObservableCollection<IFilter> config = e.NewValue as ObservableCollection<IFilter>;

             }));
    }



}
