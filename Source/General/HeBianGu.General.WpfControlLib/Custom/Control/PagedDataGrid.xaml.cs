using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary> 分页DataGrid </summary>
    public class PagedDataGrid : DataGrid
    {

        static PagedDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PagedDataGrid), new FrameworkPropertyMetadata(typeof(PagedDataGrid)));
        }

        public IEnumerable DataSource
        {
            get { return (IEnumerable)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(IEnumerable), typeof(PagedDataGrid), new PropertyMetadata(default(IEnumerable), (d, e) =>
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

                 control.ItemsSource = e.NewValue as IEnumerable;

                 control.InitData();

             }));

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // handle CollectionChanged on instance-level

            IEnumerable config = sender as IEnumerable;

            this.ItemsSource = config;

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


        public List<int> PageCountSource
        {
            get { return (List<int>)GetValue(PageCountSourceProperty); }
            set { SetValue(PageCountSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageCountSourceProperty =
            DependencyProperty.Register("PageCountSource", typeof(List<int>), typeof(PagedDataGrid), new PropertyMetadata(GetSelect(50)));


        static List<int> GetSelect(int count)
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


        public Visibility PageCountVisible
        {
            get { return (Visibility)GetValue(PageCountVisibleProperty); }
            set { SetValue(PageCountVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageCountVisibleProperty =
            DependencyProperty.Register("PageCountVisible", typeof(Visibility), typeof(PagedDataGrid), new PropertyMetadata(default(Visibility), (d, e) =>
             {
                 PagedDataGrid control = d as PagedDataGrid;

                 if (control == null) return;

                 //Visibility config = e.NewValue as Visibility;

             }));



        public object HeaderContent
        {
            get { return (object)GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderContentProperty =
            DependencyProperty.Register("HeaderContent", typeof(object), typeof(PagedDataGrid), new PropertyMetadata(default(object), (d, e) =>
             {
                 PagedDataGrid control = d as PagedDataGrid;

                 if (control == null) return;

                 object config = e.NewValue as object;

             }));



        public PagedDataGrid()
        {
            this.CommandBindings.Add(new CommandBinding(CommandService.Prev, (le, e) =>
            {
                if (this.PageIndex == 1)
                {
                    Message?.Invoke("已经是第一页！");
                    return;
                }

                this.PageIndex = (this.PageIndex - 1);

                this.RefreshData();
            }));

            this.CommandBindings.Add(new CommandBinding(CommandService.Next, (le, e) =>
            {
                if (this.PageIndex == this.TotalPage)
                {
                    Message?.Invoke("已经是最后一页！");
                    return;
                }

                this.PageIndex = (this.PageIndex + 1);

                this.RefreshData();
            }));

            this.CommandBindings.Add(new CommandBinding(CommandService.First, (le, e) =>
            {
                if (this.PageIndex == 1)
                {
                    Message?.Invoke("已经是第一页！");
                    return;
                }

                this.PageIndex = 1;

                this.RefreshData();
            }));

            this.CommandBindings.Add(new CommandBinding(CommandService.Last, (le, e) =>
            {
                if (this.PageIndex == this.TotalPage)
                {
                    Message?.Invoke("已经是最后一页！");
                    return;
                }

                this.PageIndex = this.TotalPage;

                this.RefreshData();
            }));
        }


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

                 if (control == null) return;

                 string config = e.NewValue as string;


                 control.RefreshData();

             }));


        void RefreshData()
        {

            Predicate<object> filterString = l =>
              {
                  if (string.IsNullOrEmpty(this.FilterString)) return true;

                  return l.GetType().GetProperties().Any(k =>
                  {
                      if (k.GetValue(l) == null) return false;

                      return k.GetValue(l).ToString().Contains(this.FilterString);
                  });
              };

            this.Items.Filter = filterString;

            this.Total = this.Items.Count;

            int min = (this.PageIndex - 1) * this.PageCount;

            int max = min + this.PageCount;

            this.MinValue = this.Total == 0 ? 0 : (min + 1);

            this.MaxValue = max < this.Total ? max : this.Total;

            this.TotalPage = this.Total % this.PageCount == 0 ? this.Total / this.PageCount : this.Total / this.PageCount + 1;

            Predicate<object> match = l =>
              {
                  int index = this.Items.IndexOf(l) + 1;

                  return index > min && index < max;
              };

            this.Items.Filter = match;
        }

        void InitData()
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

            if(diplay.GetOrder().HasValue)
            {
                e.Column.DisplayIndex = diplay.GetOrder().Value;
            }

            e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
    }
}
