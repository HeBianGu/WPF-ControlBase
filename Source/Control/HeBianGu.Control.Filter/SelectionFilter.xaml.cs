// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace HeBianGu.Control.Filter
{
    public class SelectionFilter : ListBox, IControlFilter
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(SelectionFilter), "S.SelectionFilter.Default");
        public static ComponentResourceKey LabelKey => new ComponentResourceKey(typeof(SelectionFilter), "S.SelectionFilter.Label");

        public static ComponentResourceKey CheckAllKey => new ComponentResourceKey(typeof(SelectionFilter), "S.CheckBox.CheckAll");

        public static RoutedCommand CheckedAll = new RoutedCommand();
        public static RoutedCommand UnCheckedAll = new RoutedCommand();


        static SelectionFilter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectionFilter), new FrameworkPropertyMetadata(typeof(SelectionFilter)));
        }

        public SelectionFilter()
        {
            CommandBinding selectAll = new CommandBinding(CheckedAll, (l, k) =>
              {
                  this.SelectedSource = this.SelectionSource.ToObservable();
              });

            CommandBinding unselectAll = new CommandBinding(UnCheckedAll, (l, k) =>
            {
                this.SelectedSource = new ObservableCollection<string>();
            });

            this.CommandBindings.Add(selectAll);
            this.CommandBindings.Add(unselectAll);
        }


        public string InitProperty
        {
            get { return (string)GetValue(InitPropertyProperty); }
            set { SetValue(InitPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InitPropertyProperty =
            DependencyProperty.Register("InitProperty", typeof(string), typeof(SelectionFilter), new FrameworkPropertyMetadata(default(string), (d, e) =>
             {
                 SelectionFilter control = d as SelectionFilter;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }

                 control.RefreshItems();
             }));


        public IEnumerable InitItemsSource
        {
            get { return (IEnumerable)GetValue(InitItemsSourceProperty); }
            set { SetValue(InitItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InitItemsSourceProperty =
            DependencyProperty.Register("InitItemsSource", typeof(IEnumerable), typeof(SelectionFilter), new FrameworkPropertyMetadata(default(IEnumerable), (d, e) =>
             {
                 SelectionFilter control = d as SelectionFilter;

                 if (control == null) return;

                 if (e.OldValue is INotifyCollectionChanged o)
                 {
                     o.CollectionChanged -= control.CollectionChanged;
                 }

                 if (e.NewValue is INotifyCollectionChanged n)
                 {
                     n.CollectionChanged -= control.CollectionChanged;
                     n.CollectionChanged += control.CollectionChanged;
                 }

                 control.RefreshItems();
             }));

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.RefreshItems();
        }

        public void RefreshItems()
        {
            if (this.InitItemsSource == null)
                return;


            var listType = this.InitItemsSource.GetType();

            if (!listType.IsGenericType)
            {
                listType = listType.BaseType;
            }

            var arguments = listType.GetGenericArguments();

            if (arguments.Length != 1)
            {
                if (this.InitItemsSource.GetType().IsArray)
                {
                    var source = this.InitItemsSource.Cast<object>().Distinct().Select(x => x.ToString()).ToObservable();

                    if (source == null)
                        return;

                    this.SelectionSource = source.ToObservable();
                    this.SelectedSource = source.ToObservable();
                }
                else
                {
                    this.SelectionSource?.Clear();
                    this.SelectedSource?.Clear();
                }
                return;
            }

            var type = arguments.FirstOrDefault();

            ObservableCollection<string> strs = null;

            if (type.IsPrimitive || type == typeof(string))
            {
                strs = this.InitItemsSource.Cast<object>().Distinct().Select(x => x.ToString()).ToObservable();
            }
            else
            {
                var p = type.GetProperty(this.InitProperty);

                if (p != null)
                    strs = this.InitItemsSource.Cast<object>().Select(x => p.GetValue(x).ToString()).ToObservable();
            }

            if (strs == null)
                return;

            this.SelectionSource = strs.ToObservable();
            this.SelectedSource = strs.ToObservable();
        }

        public IEnumerable InitSource
        {
            get { return (IEnumerable)GetValue(InitSourceProperty); }
            set { SetValue(InitSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InitSourceProperty =
            DependencyProperty.Register("InitSource", typeof(IEnumerable), typeof(SelectionFilter), new PropertyMetadata(default(IEnumerable), (d, e) =>
             {
                 SelectionFilter control = d as SelectionFilter;

                 if (control == null) return;

                 IEnumerable config = e.NewValue as IEnumerable;

                 NotifyCollectionChangedEventHandler handler = (l, k) =>
                 {
                     control.InitSelection();
                 };

                 if (e.OldValue is INotifyCollectionChanged notify_old)
                 {
                     notify_old.CollectionChanged -= handler;
                 }

                 if (config is INotifyCollectionChanged notify_new)
                 {
                     notify_new.CollectionChanged -= handler;
                     notify_new.CollectionChanged += handler;
                 }

                 handler?.Invoke(null, null);

             }));

        public bool IsUseCheckAll
        {
            get { return (bool)GetValue(IsUseCheckAllProperty); }
            set { SetValue(IsUseCheckAllProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsUseCheckAllProperty =
            DependencyProperty.Register("IsUseCheckAll", typeof(bool), typeof(SelectionFilter), new PropertyMetadata(default(bool), (d, e) =>
            {
                SelectionFilter control = d as SelectionFilter;
                if (control == null) return;
                //bool config = e.NewValue as bool;

            }));




        public string PropertyName
        {
            get { return (string)GetValue(PropertyNameProperty); }
            set { SetValue(PropertyNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PropertyNameProperty =
            DependencyProperty.Register("PropertyName", typeof(string), typeof(SelectionFilter), new PropertyMetadata(default(string), (d, e) =>
             {
                 SelectionFilter control = d as SelectionFilter;

                 if (control == null) return;

                 string config = e.NewValue as string;

                 control.InitSelection();

                 //control.RefreshData();

             }));

        /// <summary> 可选择的数据源 </summary>
        public ObservableCollection<string> SelectionSource
        {
            get { return (ObservableCollection<string>)GetValue(SelectionSourceProperty); }
            set { SetValue(SelectionSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectionSourceProperty =
            DependencyProperty.Register("SelectionSource", typeof(ObservableCollection<string>), typeof(SelectionFilter), new PropertyMetadata(new ObservableCollection<string>(), (d, e) =>
             {
                 SelectionFilter control = d as SelectionFilter;

                 if (control == null) return;

                 ObservableCollection<string> config = e.NewValue as ObservableCollection<string>;

             }));

        /// <summary> 选中的数据源 </summary>
        public ObservableCollection<string> SelectedSource
        {
            get { return (ObservableCollection<string>)GetValue(SelectedSourceProperty); }
            set { SetValue(SelectedSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedSourceProperty =
            DependencyProperty.Register("SelectedSource", typeof(ObservableCollection<string>), typeof(SelectionFilter), new PropertyMetadata(new ObservableCollection<string>(), (d, e) =>
             {
                 SelectionFilter control = d as SelectionFilter;
                 if (control == null) return;
                 ObservableCollection<string> config = e.NewValue as ObservableCollection<string>;
             }));

        /// <summary> 刷新可选择的数据源 </summary>
        protected virtual void InitSelection()
        {
            ObservableCollection<string> source = new ObservableCollection<string>();

            if (this.InitSource == null) return;

            foreach (object item in this.InitSource)
            {
                System.Reflection.PropertyInfo p = item.GetType().GetProperty(this.PropertyName);

                if (p == null) continue;

                string v = p.GetValue(item)?.ToString();

                if (string.IsNullOrEmpty(v))
                    continue;

                if (source.Contains(v))
                    continue;

                source.Add(v);
            }

            this.SelectionSource = new ObservableCollection<string>();
            this.SelectedSource = new ObservableCollection<string>();
            this.SelectionSource = source?.ToObservable();
            this.SelectedSource = source?.ToObservable();
        }


        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1 && this.IsUseCheckAll)
                if (e.AddedItems[0] == this.Items[0])
                    return;

            if (e.RemovedItems.Count == 1 && this.IsUseCheckAll)
                if (e.RemovedItems[0] == this.Items[0])
                    return;

            base.OnSelectionChanged(e);
            if (this.IsLoaded)
                this.OnChanged();

            if (this.IsUseCheckAll)
            {
                if (this.ItemContainerGenerator.ContainerFromIndex(0) is ListBoxItem item)
                {
                    item.IsSelected = this.SelectedSource.Count == this.SelectionSource.Count;
                }
            }
        }

        public virtual bool IsMatch(object obj)
        {
            if (this.SelectedSource.Count == this.SelectionSource.Count)
                return true;

            if (this.SelectionSource == null || this.SelectionSource.Count == 0)
                return true;

            object value = obj.GetType().GetProperty(this.PropertyName)?.GetValue(obj);
            return this.SelectedSource.Any(l => l == value?.ToString());
        }
        public static readonly RoutedEvent ChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("Changed", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(SelectionFilter));

        public event RoutedEventHandler Changed
        {
            add { this.AddHandler(ChangedRoutedEvent, value); }
            remove { this.RemoveHandler(ChangedRoutedEvent, value); }
        }
        protected void OnChanged()
        {
            RoutedEventArgs args = new RoutedEventArgs(ChangedRoutedEvent, this);
            this.RaiseEvent(args);
        }


        public FilterHost BindFilterHost
        {
            get { return (FilterHost)GetValue(BindFilterHostProperty); }
            set { SetValue(BindFilterHostProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BindFilterHostProperty =
            DependencyProperty.Register("BindFilterHost", typeof(FilterHost), typeof(SelectionFilter), new FrameworkPropertyMetadata(default(FilterHost), (d, e) =>
             {
                 SelectionFilter control = d as SelectionFilter;

                 if (control == null) return;

                 if (e.OldValue is FilterHost o)
                 {
                     o.RemoveFilter(control);
                 }

                 if (e.NewValue is FilterHost n)
                 {
                     n.AddFilter(control);
                 }
             }));
    }

    public class ContainFilter : SelectionFilter, IControlFilter
    {
        public string[] SplitValue
        {
            get { return (string[])GetValue(SplitValueProperty); }
            set { SetValue(SplitValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitValueProperty =
            DependencyProperty.Register("SplitValue", typeof(string[]), typeof(ContainFilter), new FrameworkPropertyMetadata(new string[] { ",", "，", " " }, (d, e) =>
            {
                ContainFilter control = d as ContainFilter;

                if (control == null) return;

                if (e.OldValue is string[] o)
                {

                }

                if (e.NewValue is string[] n)
                {

                }

            }));

        static ContainFilter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ContainFilter), new FrameworkPropertyMetadata(typeof(ContainFilter)));
        }

        protected override void InitSelection()
        {
            ObservableCollection<string> source = new ObservableCollection<string>();

            if (this.InitSource == null) return;

            if (this.PropertyName == null) return;

            foreach (object item in this.InitSource)
            {
                System.Reflection.PropertyInfo p = item.GetType().GetProperty(this.PropertyName);

                if (p == null) continue;

                string v = p.GetValue(item)?.ToString();

                if (string.IsNullOrEmpty(v))
                    continue;

                string[] array = v.Split(this.SplitValue, StringSplitOptions.RemoveEmptyEntries);

                foreach (string a in array)
                {
                    if (source.Contains(a))
                        continue;

                    source.Add(a);
                }
            }

            this.SelectionSource = new ObservableCollection<string>();

            this.SelectedSource = new ObservableCollection<string>();

            //foreach (string item in source)
            //{
            //    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            //    {
            //        this.SelectionSource.Add(item);

            //        if (this.SelectionSource.Count == source.Count)
            //        {
            //            this.SelectedSource = source.ToObservable();
            //        }
            //    }));
            //}

            this.SelectionSource = source.ToObservable();
            this.SelectedSource = source.ToObservable();
        }

        public override bool IsMatch(object obj)
        {
            if (this.SelectedSource.Count == this.SelectionSource.Count)
                return true;

            if (this.SelectionSource == null || this.SelectionSource.Count == 0)
                return true;

            object value = obj.GetType().GetProperty(this.PropertyName)?.GetValue(obj);

            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return false;

            string[] array = value.ToString().Split(this.SplitValue, StringSplitOptions.RemoveEmptyEntries);

            return this.SelectedSource.Any(l => array.Any(k => k == l));
        }
    }

    public class FilterHost : ContentControl
    {
        private List<IControlFilter> _filters = new List<IControlFilter>();

        public void AddFilter(IControlFilter filter)
        {
            this._filters.Add(filter);
            filter.Changed += Filter_Changed;
        }

        bool _isRefreshing;

        private void Filter_Changed(object sender, RoutedEventArgs e)
        {
            if (_isRefreshing)
                return;

            this._isRefreshing = true;

            this.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                       {
                           this.RefreshData();
                           this._isRefreshing = false;
                       }));

        }

        public void RemoveFilter(IControlFilter filter)
        {
            this._filters.Remove(filter);
            filter.Changed -= Filter_Changed;
        }

        public bool IsAsync
        {
            get { return (bool)GetValue(IsAsyncProperty); }
            set { SetValue(IsAsyncProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAsyncProperty =
            DependencyProperty.Register("IsAsync", typeof(bool), typeof(FilterHost), new FrameworkPropertyMetadata(true, (d, e) =>
             {
                 FilterHost control = d as FilterHost;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));


        public int ToTakeCount
        {
            get { return (int)GetValue(ToTakeCountProperty); }
            set { SetValue(ToTakeCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToTakeCountProperty =
            DependencyProperty.Register("ToTakeCount", typeof(int), typeof(FilterHost), new FrameworkPropertyMetadata(-1, (d, e) =>
             {
                 FilterHost control = d as FilterHost;

                 if (control == null) return;

                 if (e.OldValue is int o)
                 {
                     
                 }

                 if (e.NewValue is int n)
                 {

                 }

             }));

        public void RefreshData()
        {

            //System.Diagnostics.Debug.WriteLine("RefreshData");

            ObservableCollection<object> list = new ObservableCollection<object>();

            if (this.FromSource == null)
                return;

            List<string> selectSource = new List<string>();

            if (this.IsAsync)
            {
                this.ToSource = list;

                foreach (object item in this.FromSource)
                {
                    bool match = this._filters.All(l => l.IsMatch(item));
                    if (!match)
                        continue;

                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                    {
                        if (this.ToTakeCount > 0)
                        {
                            if (list.Count > this.ToTakeCount)
                            {
                                return;
                            }
                        }

                        list.Add(item);

                        this.OnChanged();
                    }));
                }
            }
            else
            {

                foreach (object item in this.FromSource)
                {
                    bool match = this._filters.All(l => l.IsMatch(item));
                    if (!match)
                        continue;

                    list.Add(item);
                }

                if (this.ToTakeCount < 0)
                {
                    this.ToSource = list;
                }
                else
                {
                    this.ToSource = list.Take(this.ToTakeCount).ToObservable();
                }

                this.OnChanged();
            }
        }

        public IEnumerable FromSource
        {
            get { return (IEnumerable)GetValue(FromSourceProperty); }
            set { SetValue(FromSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FromSourceProperty =
            DependencyProperty.Register("FromSource", typeof(IEnumerable), typeof(FilterHost), new FrameworkPropertyMetadata(default(IEnumerable), (d, e) =>
             {
                 FilterHost control = d as FilterHost;

                 if (control == null) return;

                 if (e.OldValue is INotifyCollectionChanged o)
                 {
                     o.CollectionChanged -= control.CollectionChanged;
                 }

                 if (e.NewValue is INotifyCollectionChanged n)
                 {
                     n.CollectionChanged += control.CollectionChanged;
                 }

                 control.RefreshData();
             }));

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.RefreshData();
        }

        public IEnumerable ToSource
        {
            get { return (IEnumerable)GetValue(ToSourceProperty); }
            set { SetValue(ToSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToSourceProperty =
            DependencyProperty.Register("ToSource", typeof(IEnumerable), typeof(FilterHost), new FrameworkPropertyMetadata(default(IEnumerable), (d, e) =>
             {
                 FilterHost control = d as FilterHost;

                 if (control == null) return;

                 if (e.OldValue is IEnumerable o)
                 {

                 }

                 if (e.NewValue is IEnumerable n)
                 {

                 }

             }));



        //声明和注册路由事件
        public static readonly RoutedEvent ChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("Changed", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(FilterHost));
        //CLR事件包装
        public event RoutedEventHandler Changed
        {
            add { this.AddHandler(ChangedRoutedEvent, value); }
            remove { this.RemoveHandler(ChangedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnChanged()
        {
            RoutedEventArgs args = new RoutedEventArgs(ChangedRoutedEvent, this);
            this.RaiseEvent(args);
        }

    }

}
