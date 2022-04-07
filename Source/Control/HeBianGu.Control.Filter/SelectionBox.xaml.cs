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
    public class SelectionBox : ListBox
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(SelectionBox), "S.SelectionBox.Default");
        public static ComponentResourceKey LabelKey => new ComponentResourceKey(typeof(SelectionBox), "S.SelectionBox.Label");


        public static RoutedCommand CheckedAll = new RoutedCommand();
        public static RoutedCommand UnCheckedAll = new RoutedCommand();


        static SelectionBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectionBox), new FrameworkPropertyMetadata(typeof(SelectionBox)));
        }

        public SelectionBox()
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


        public IEnumerable InitSource
        {
            get { return (IEnumerable)GetValue(InitSourceProperty); }
            set { SetValue(InitSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InitSourceProperty =
            DependencyProperty.Register("InitSource", typeof(IEnumerable), typeof(SelectionBox), new PropertyMetadata(default(IEnumerable), (d, e) =>
             {
                 SelectionBox control = d as SelectionBox;

                 if (control == null) return;

                 IEnumerable config = e.NewValue as IEnumerable;

                 NotifyCollectionChangedEventHandler handler = (l, k) =>
                 {
                     control.InitSelection();

                     control.RefreshData();
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


        public IEnumerable InSource
        {
            get { return (IEnumerable)GetValue(InSourceProperty); }
            set { SetValue(InSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InSourceProperty =
            DependencyProperty.Register("InSource", typeof(IEnumerable), typeof(SelectionBox), new PropertyMetadata(default(IEnumerable), (d, e) =>
             {
                 SelectionBox control = d as SelectionBox;

                 if (control == null) return;

                 IEnumerable config = e.NewValue as IEnumerable;

                 control.RefreshData();
             }));


        public IEnumerable OutSource
        {
            get { return (IEnumerable)GetValue(OutSourceProperty); }
            set { SetValue(OutSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutSourceProperty =
            DependencyProperty.Register("OutSource", typeof(IEnumerable), typeof(SelectionBox), new PropertyMetadata(default(IEnumerable), (d, e) =>
             {
                 SelectionBox control = d as SelectionBox;

                 if (control == null) return;

                 IEnumerable config = e.NewValue as IEnumerable;

             }));

        public bool IsUseCheckAll
        {
            get { return (bool)GetValue(IsUseCheckAllProperty); }
            set { SetValue(IsUseCheckAllProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsUseCheckAllProperty =
            DependencyProperty.Register("IsUseCheckAll", typeof(bool), typeof(SelectionBox), new PropertyMetadata(default(bool), (d, e) =>
            {
                SelectionBox control = d as SelectionBox;

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
            DependencyProperty.Register("PropertyName", typeof(string), typeof(SelectionBox), new PropertyMetadata(default(string), (d, e) =>
             {
                 SelectionBox control = d as SelectionBox;

                 if (control == null) return;

                 string config = e.NewValue as string;

                 control.InitSelection();

                 control.RefreshData();

             }));

        /// <summary> 可选择的数据源 </summary>
        public ObservableCollection<string> SelectionSource
        {
            get { return (ObservableCollection<string>)GetValue(SelectionSourceProperty); }
            set { SetValue(SelectionSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectionSourceProperty =
            DependencyProperty.Register("SelectionSource", typeof(ObservableCollection<string>), typeof(SelectionBox), new PropertyMetadata(new ObservableCollection<string>(), (d, e) =>
             {
                 SelectionBox control = d as SelectionBox;

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
            DependencyProperty.Register("SelectedSource", typeof(ObservableCollection<string>), typeof(SelectionBox), new PropertyMetadata(new ObservableCollection<string>(), (d, e) =>
             {
                 SelectionBox control = d as SelectionBox;

                 if (control == null) return;

                 ObservableCollection<string> config = e.NewValue as ObservableCollection<string>;

             }));

        /// <summary> 是否异步通知输出数据 </summary>
        public bool IsAsync
        {
            get { return (bool)GetValue(IsAsyncProperty); }
            set { SetValue(IsAsyncProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAsyncProperty =
            DependencyProperty.Register("IsAsync", typeof(bool), typeof(SelectionBox), new PropertyMetadata(true, (d, e) =>
             {
                 SelectionBox control = d as SelectionBox;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));

        /// <summary> 刷新可选择的数据源 </summary>
        private void InitSelection()
        {
            ObservableCollection<string> source = new ObservableCollection<string>();

            if (this.InitSource == null) return;

            foreach (object item in this.InitSource)
            {
                System.Reflection.PropertyInfo p = item.GetType().GetProperty(this.PropertyName);

                if (p == null) continue;

                string v = p.GetValue(item)?.ToString();

                if (source.Contains(v)) continue;

                source.Add(v);
            }

            this.SelectionSource = new ObservableCollection<string>();

            this.SelectedSource = new ObservableCollection<string>();

            foreach (string item in source)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
                {
                    this.SelectionSource.Add(item);

                    if (this.SelectionSource.Count == source.Count)
                    {
                        this.SelectedSource = source.ToObservable();
                    }
                }));
            }
        }

        /// <summary> 选中后刷新输出数据源 </summary>
        private void RefreshData()
        {
            IList list = new ObservableCollection<object>();

            if (this.InSource == null) return;

            List<string> selectSource = new List<string>();

            if (this.IsAsync)
            {
                this.OutSource = list;

                foreach (object item in this.InSource)
                {
                    System.Reflection.PropertyInfo p = item.GetType().GetProperty(this.PropertyName);

                    if (p == null) continue;

                    string v = p.GetValue(item)?.ToString();

                    if (this.SelectedSource.Any(l => l == v))
                    {
                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                        {
                            list.Add(item);
                        }));
                    }
                }
            }
            else
            {
                foreach (object item in this.InSource)
                {
                    System.Reflection.PropertyInfo p = item.GetType().GetProperty(this.PropertyName);

                    if (p == null) continue;

                    string v = p.GetValue(item)?.ToString();

                    if (this.SelectedSource.Any(l => l == v))
                    {
                        list.Add(item);
                    }
                }

                this.OutSource = list;
            }

        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            this.RefreshData();
        }

    }

}
