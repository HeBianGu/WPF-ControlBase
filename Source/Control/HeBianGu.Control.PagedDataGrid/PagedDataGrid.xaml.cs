// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace HeBianGu.Control.PagedDataGrid
{
    public class PagedDataGrid : DataGrid
    {
        public static ComponentResourceKey DynamicKey => new ComponentResourceKey(typeof(PagedDataGrid), "S.PagedDataGrid.Dynamic");
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(PagedDataGrid), "S.PagedDataGrid.Default");
        public static ComponentResourceKey SingleKey => new ComponentResourceKey(typeof(PagedDataGrid), "S.PagedDataGrid.Single");
        public static ComponentResourceKey AccentKey => new ComponentResourceKey(typeof(PagedDataGrid), "S.PagedDataGrid.Accet");
        public static ComponentResourceKey ClearKey => new ComponentResourceKey(typeof(PagedDataGrid), "S.PagedDataGrid.Clear");

        static PagedDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PagedDataGrid), new FrameworkPropertyMetadata(typeof(PagedDataGrid)));
        }

        public PagedDataGrid()
        {
            this.CommandBindings.Add(new CommandBinding(Commander.Prev, (le, e) =>
            {
                if (this.PageIndex == 1)
                {
                    Message?.Invoke("已经是第一页！");
                    return;
                }

                this.PageIndex = (this.PageIndex - 1);

                this.RefreshData();
            }));

            this.CommandBindings.Add(new CommandBinding(Commander.Next, (le, e) =>
            {
                if (this.PageIndex == this.TotalPage)
                {
                    Message?.Invoke("已经是最后一页！");
                    return;
                }

                this.PageIndex = (this.PageIndex + 1);

                this.RefreshData();
            }));

            this.CommandBindings.Add(new CommandBinding(Commander.First, (le, e) =>
            {
                if (this.PageIndex == 1)
                {
                    Message?.Invoke("已经是第一页！");
                    return;
                }

                this.PageIndex = 1;

                this.RefreshData();
            }));

            this.CommandBindings.Add(new CommandBinding(Commander.Last, (le, e) =>
            {
                if (this.PageIndex == this.TotalPage)
                {
                    Message?.Invoke("已经是最后一页！");
                    return;
                }

                this.PageIndex = this.TotalPage;

                this.RefreshData();
            }));

            {
                CommandBinding binding = new CommandBinding(Commander.CheckAll);
                binding.Executed += (l, k) =>
                {
                    bool v = (bool)k.Parameter;

                    foreach (var item in this.DataSource.OfType<ISelectViewModel>())
                    {
                        item.IsSelected = v;
                    }
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = this.DataSource != null && this.DataSource.Count > 0;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(Commander.Delete);
                binding.Executed += (l, k) =>
                {
                    this.DataSource.Remove(k.Parameter);
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = this.DataSource.Contains(k.Parameter);
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(Commander.DeleteAll);
                binding.Executed += (l, k) =>
                {
                    this.DataSource.Clear();
                };
                binding.CanExecute += (l, k) =>
            {
                k.CanExecute = this.DataSource != null && this.DataSource.Count > 0;
            };

                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(Commander.DeleteAllChecked);
                binding.Executed += (l, k) =>
                {
                    foreach (var item in this.DataSource.OfType<ISelectViewModel>().Where(x => x.IsSelected).ToList())
                    {
                        this.DataSource.Remove(item);
                    }
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = this.DataSource != null && this.DataSource.OfType<ISelectViewModel>().Any(x => x.IsSelected);
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(Commander.View);
                binding.Executed += (l, k) =>
                {
                    MessageProxy.PropertyGrid.ShowView(k.Parameter, null, "查看", x => x.MinWidth = 600);
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = this.DataSource.Contains(k.Parameter);
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(Commander.Edit);
                binding.Executed += (l, k) =>
                {
                    MessageProxy.PropertyGrid.ShowEdit(k.Parameter, null, "编辑", x => x.MinWidth = 600);
                };
                binding.CanExecute += (l, k) =>
                {
                    k.CanExecute = this.DataSource.Contains(k.Parameter);
                };
                this.CommandBindings.Add(binding);

            }

            {
                CommandBinding binding = new CommandBinding(Commander.Setting);
                binding.Executed += (l, k) =>
                {
                    MessageProxy.Presenter.Show(this.ColumnSet, null, "列头设置");
                };
                this.CommandBindings.Add(binding);

            }
        }

        public IList DataSource
        {
            get { return (IList)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(IList), typeof(PagedDataGrid), new PropertyMetadata(default(IList), (d, e) =>
             {
                 PagedDataGrid control = d as PagedDataGrid;

                 if (control == null) return;

                 if (e.OldValue is INotifyCollectionChanged notify)
                 {
                     if (notify != null)
                         notify.CollectionChanged -= control.CollectionChanged;
                 }

                 if (e.NewValue is INotifyCollectionChanged notify1)
                 {
                     if (notify1 != null)
                         notify1.CollectionChanged += control.CollectionChanged;

                 }

                 //control.ItemsSource = e.NewValue as IEnumerable;

                 control.InitData();
                 control.OnDataSourceChanged();

             }));

        protected virtual void OnDataSourceChanged()
        {

        }


        public bool UseDoubleClickShowView
        {
            get { return (bool)GetValue(UseDoubleClickShowViewProperty); }
            set { SetValue(UseDoubleClickShowViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseDoubleClickShowViewProperty =
            DependencyProperty.Register("UseDoubleClickShowView", typeof(bool), typeof(PagedDataGrid), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                PagedDataGrid control = d as PagedDataGrid;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (this.UseDoubleClickShowView)
                MessageProxy.PropertyGrid?.ShowView(this.SelectedItem);
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // handle CollectionChanged on instance-level

            //IEnumerable config = sender as IEnumerable;

            //this.ItemsSource = config;

            this.InitData();
        }

        /// <summary> 数据总条数 </summary>
        public int Total
        {
            get { return (int)GetValue(TotalProperty); }
            set { SetValue(TotalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalProperty =
            DependencyProperty.Register("Total", typeof(int), typeof(PagedDataGrid), new PropertyMetadata(0, (d, e) =>
             {
                 PagedDataGrid control = d as PagedDataGrid;

                 if (control == null) return;


             }));


        /// <summary> 当前页索引 </summary>
        public int PageIndex
        {
            get { return (int)GetValue(PageIndexProperty); }
            set { SetValue(PageIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageIndexProperty =
            DependencyProperty.Register("PageIndex", typeof(int), typeof(PagedDataGrid), new PropertyMetadata(1, (d, e) =>
             {
                 PagedDataGrid control = d as PagedDataGrid;

                 if (control == null) return;

                 control.RefreshData();

                 //int config = e.NewValue as int;

             }));

        /// <summary> 总页数 </summary>
        public int TotalPage
        {
            get { return (int)GetValue(TotalPageProperty); }
            set { SetValue(TotalPageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalPageProperty =
            DependencyProperty.Register("TotalPage", typeof(int), typeof(PagedDataGrid), new PropertyMetadata(0, (d, e) =>
             {
                 PagedDataGrid control = d as PagedDataGrid;

                 if (control == null) return;

                 //int config = e.NewValue as int;

             }));

        public int PageCount
        {
            get { return (int)GetValue(PageCountProperty); }
            set { SetValue(PageCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageCountProperty =
            DependencyProperty.Register("PageCount", typeof(int), typeof(PagedDataGrid), new PropertyMetadata(20, (d, e) =>
             {
                 PagedDataGrid control = d as PagedDataGrid;

                 if (control == null) return;

                 //int config = e.NewValue as int;

                 control.RefreshData();

             }));


        public bool UsePageCount
        {
            get { return (bool)GetValue(UsePageCountProperty); }
            set { SetValue(UsePageCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UsePageCountProperty =
            DependencyProperty.Register("UsePageCount", typeof(bool), typeof(PagedDataGrid), new FrameworkPropertyMetadata(true, (d, e) =>
             {
                 PagedDataGrid control = d as PagedDataGrid;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));


        public bool UseSearch
        {
            get { return (bool)GetValue(UseSearchProperty); }
            set { SetValue(UseSearchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseSearchProperty =
            DependencyProperty.Register("UseSearch", typeof(bool), typeof(PagedDataGrid), new FrameworkPropertyMetadata(true, (d, e) =>
             {
                 PagedDataGrid control = d as PagedDataGrid;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));


        public List<int> PageCountSource
        {
            get { return (List<int>)GetValue(PageCountSourceProperty); }
            set { SetValue(PageCountSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageCountSourceProperty =
            DependencyProperty.Register("PageCountSource", typeof(List<int>), typeof(PagedDataGrid), new PropertyMetadata(GetSelect(50)));

        private static List<int> GetSelect(int count)
        {

            List<int> result = new List<int>();

            for (int i = 1; i <= count; i++)
            {
                result.Add(i);
            }

            return result;
        }


        public Action<string> Message { get; set; }



        /// <summary> 当前页最小索引 </summary>
        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(int), typeof(PagedDataGrid), new PropertyMetadata(default(int), (d, e) =>
             {
                 PagedDataGrid control = d as PagedDataGrid;

                 if (control == null) return;

                 //string config = e.NewValue as string;

             }));

        /// <summary> 当前页最大索引 </summary>
        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(int), typeof(PagedDataGrid), new PropertyMetadata(default(int), (d, e) =>
             {
                 PagedDataGrid control = d as PagedDataGrid;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public Visibility TopVisibility
        {
            get { return (Visibility)GetValue(TopVisibilityProperty); }
            set { SetValue(TopVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopVisibilityProperty =
            DependencyProperty.Register("TopVisibility", typeof(Visibility), typeof(PagedDataGrid), new PropertyMetadata(default(Visibility), (d, e) =>
             {
                 PagedDataGrid control = d as PagedDataGrid;

                 if (control == null) return;

                 //Visibility config = e.NewValue as Visibility;

             }));


        //public Visibility PageCountVisible
        //{
        //    get { return (Visibility)GetValue(PageCountVisibleProperty); }
        //    set { SetValue(PageCountVisibleProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PageCountVisibleProperty =
        //    DependencyProperty.Register("PageCountVisible", typeof(Visibility), typeof(PagedDataGrid), new PropertyMetadata(default(Visibility), (d, e) =>
        //     {
        //         PagedDataGrid control = d as PagedDataGrid;

        //         if (control == null) return;

        //         //Visibility config = e.NewValue as Visibility;

        //     }));



        public object HeaderContent
        {
            get { return GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderContentProperty =
            DependencyProperty.Register("HeaderContent", typeof(object), typeof(PagedDataGrid), new PropertyMetadata(default(object), (d, e) =>
             {
                 PagedDataGrid control = d as PagedDataGrid;

                 if (control == null) return;

                 object config = e.NewValue;

             }));




        public string FilterString
        {
            get { return (string)GetValue(FilterStringProperty); }
            set { SetValue(FilterStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterStringProperty =
            DependencyProperty.Register("FilterString", typeof(string), typeof(PagedDataGrid), new PropertyMetadata(default(string), (d, e) =>
             {
                 PagedDataGrid control = d as PagedDataGrid;
                 if (control == null)
                     return;
                 string config = e.NewValue as string;
                 control.RefreshData();
             }));




        public bool UseAsync
        {
            get { return (bool)GetValue(UseAsyncProperty); }
            set { SetValue(UseAsyncProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseAsyncProperty =
            DependencyProperty.Register("UseAsync", typeof(bool), typeof(PagedDataGrid), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                PagedDataGrid control = d as PagedDataGrid;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));

        private bool _isRefreshing;

        private void RefreshData()
        {
            if (this.DataSource == null)
                return;

            Predicate<object> filterString = l =>
              {
                  if (string.IsNullOrEmpty(this.FilterString))
                      return true;

                  if (l is ISearchable searchable)
                  {
                      return searchable.Filter(this.FilterString);
                  }

                  return l.GetType().GetProperties().Any(k =>
                  {
                      if (k.GetMethod.Name == "get_Item")
                          return false;
                      if (k.GetValue(l) == null)
                          return false;
                      if (k.CanRead == false)
                          return false;
                      if (k.PropertyType == typeof(string) || k.PropertyType.IsPrimitive || k.PropertyType == typeof(DateTime))
                      {
                          return k.GetValue(l).ToString().Contains(this.FilterString);
                      }
                      return false;
                  });
              };

            if (_isRefreshing)
                return;
            _isRefreshing = true;

            this.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
            {
                _isRefreshing = false;
                if (this.DataSource == null)
                    return;

                var list = this.DataSource.OfType<object>().ToList();
                var filter = list.Where(x => filterString(x)).ToList();
                this.Total = filter.Count();
                int min = (this.PageIndex - 1) * this.PageCount;
                int max = min + this.PageCount;
                this.MinValue = this.Total == 0 ? 0 : (min + 1);
                this.MaxValue = max < this.Total ? max : this.Total;
                this.TotalPage = this.Total % this.PageCount == 0 ? this.Total / this.PageCount : this.Total / this.PageCount + 1;

                Predicate<object> match = l =>
                {
                    //int index = filter.IndexOf(l) + 1;
                    int index = filter.IndexOf(l);
                    return index >= min && index < max;
                };
                filter = filter.Where(x => match(x)).ToList();

                if (this.UseAsync)
                {
                    ObservableCollection<object> observable = new ObservableCollection<object>();
                    this.ItemsSource = observable;
                    foreach (var item in filter)
                    {
                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
                        {
                            if (_isRefreshing)
                                return;
                            observable.Add(item);
                        }));
                    }
                }
                else
                {
                    this.ItemsSource = filter;
                }
            }));
        }

        protected virtual void InitData()
        {
            this.PageIndex = 1;
            this.RefreshData();
        }


        protected override void OnAutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            base.OnAutoGeneratingColumn(e);

            if (this.DataSource == null) return;

            Type type = this.DataSource.GetType();

            if (!type.IsGenericType) return;

            Type t = type.GetGenericArguments()?.FirstOrDefault();

            if (t == null) return;

            DisplayAttribute diplay = t.GetProperty(e.PropertyName)?.GetCustomAttribute<DisplayAttribute>();

            if (diplay == null) return;

            e.Column.Header = diplay.Name;

            if (diplay.GetOrder().HasValue)
            {
                e.Column.DisplayIndex = diplay.GetOrder().Value;
            }

            DataGridColumnAttribute column = t.GetProperty(e.PropertyName)?.GetCustomAttribute<DataGridColumnAttribute>();

            e.Column.Width = column?.Width ?? new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        protected override void OnAutoGeneratedColumns(EventArgs e)
        {
            base.OnAutoGeneratedColumns(e);
            this.EndColumns.Clear();
            foreach (DataGridColumn item in EndColumns)
            {
                this.Columns.Add(item);
            }
        }

        public ObservableCollection<DataGridColumn> EndColumns { get; } = new ObservableCollection<DataGridColumn>();

        public DataGridColumnSet ColumnSet => new DataGridColumnSet(this.Columns);
    }
}
