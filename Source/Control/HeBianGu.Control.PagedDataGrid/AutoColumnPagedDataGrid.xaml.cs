// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HeBianGu.Control.PagedDataGrid
{
    public partial class AutoColumnPagedDataGrid : PagedDataGrid
    {
        public static ComponentResourceKey SumitKey => new ComponentResourceKey(typeof(AutoColumnPagedDataGrid), "S.AutoColumnPagedDataGrid.Default.WithSumit");

        public Type ModelType
        {
            get { return (Type)GetValue(ModelTypeProperty); }
            set { SetValue(ModelTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModelTypeProperty =
            DependencyProperty.Register("ModelType", typeof(Type), typeof(AutoColumnPagedDataGrid), new FrameworkPropertyMetadata(default(Type), (d, e) =>
            {
                AutoColumnPagedDataGrid control = d as AutoColumnPagedDataGrid;

                if (control == null) return;

                if (e.OldValue is Type o)
                {

                }

                if (e.NewValue is Type n)
                {

                }

                control.GenerateColunms();
            }));



        public string BindingPath
        {
            get { return (string)GetValue(BindingPathProperty); }
            set { SetValue(BindingPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BindingPathProperty =
            DependencyProperty.Register("BindingPath", typeof(string), typeof(AutoColumnPagedDataGrid), new FrameworkPropertyMetadata("{0}", (d, e) =>
            {
                AutoColumnPagedDataGrid control = d as AutoColumnPagedDataGrid;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }

                control.GenerateColunms();
            }));
        public ObservableCollection<DataGridColumn> HomeColumns { get; } = new ObservableCollection<DataGridColumn>();

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            if (this.ModelType == null)
            {
                this.ModelType = this.DataSource.GetGenericArgumentType();
            }

            this.GenerateColunms();
        }

        protected virtual void GenerateColunms()
        {
            if (this.IsInitialized == false) return;

            this.Columns.Clear();

            foreach (DataGridColumn item in this.HomeColumns)
            {
                this.Columns.Add(item);
            }

            if (this.ModelType == null) return;

            PropertyInfo[] ps = this.ModelType.GetProperties();

            foreach (PropertyInfo p in ps)
            {
                BrowsableAttribute browsable = p.GetCustomAttribute<BrowsableAttribute>();

                if (browsable?.Browsable == false) continue;

                DisplayAttribute display = p.GetCustomAttribute<DisplayAttribute>();

                if (browsable?.Browsable == false) continue;

                ReadOnlyAttribute readOnly = p.GetCustomAttribute<ReadOnlyAttribute>();

                DataGridColumnAttribute columnAttribute = p.GetCustomAttribute<DataGridColumnAttribute>();

                DataGridColumn column = columnAttribute == null ? this.GetDataGridColumn(p)
                    : columnAttribute.GetDataGridColumn(p);

                column.Header = display?.Name ?? p.Name;

                if (column is DataGridBoundColumn bound)
                {
                    Binding binding = new Binding();
                    string path = string.Format(this.BindingPath, string.Format(columnAttribute?.PropertyPath ?? "{0}", p.Name));
                    binding.Path = new PropertyPath(path);
                    binding.Mode = readOnly?.IsReadOnly == true ? BindingMode.OneWay : BindingMode.TwoWay;
                    bound.Binding = binding;
                }

                this.Columns.Add(column);
            }

            foreach (DataGridColumn item in this.EndColumns)
            {
                this.Columns.Add(item);
            }
        }
        public DataGridColumn GetDataGridColumn(PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType == typeof(bool))
            {
                return new CheckBoxDataGridColumn() { Width = new DataGridLength(1, DataGridLengthUnitType.Star) };
            }
            else if (propertyInfo.PropertyType.IsEnum)
            {
                return new DataGridComboBoxColumn() { Width = new DataGridLength(1, DataGridLengthUnitType.Star) };
            }
            else
            {
                return new DataGridTextColumn() { Width = new DataGridLength(1, DataGridLengthUnitType.Star) };
            }
        }
    }


    public partial class AutoColumnPagedDataGrid
    {
        public AutoColumnPagedDataGrid()
        {
            this.BindCommand(CommandService.Sure, (l, k) =>
            {
                this.Result = true;

                this.OnSumit();
            });

            this.BindCommand(CommandService.Close, (l, k) =>
            {
                this.OnClose();
            });
        }

        public object BottomContent
        {
            get { return GetValue(BottomContentProperty); }
            set { SetValue(BottomContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomContentProperty =
            DependencyProperty.Register("BottomContent", typeof(object), typeof(AutoColumnPagedDataGrid), new PropertyMetadata(default(object), (d, e) =>
            {
                AutoColumnPagedDataGrid control = d as AutoColumnPagedDataGrid;

                if (control == null) return;

                object config = e.NewValue;

            }));
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(AutoColumnPagedDataGrid), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                AutoColumnPagedDataGrid control = d as AutoColumnPagedDataGrid;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }

            }));


        public bool Result { get; set; }


        //声明和注册路由事件
        public static readonly RoutedEvent SumitRoutedEvent =
            EventManager.RegisterRoutedEvent("Sumit", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(AutoColumnPagedDataGrid));
        //CLR事件包装
        public event RoutedEventHandler Sumit
        {
            add { this.AddHandler(SumitRoutedEvent, value); }
            remove { this.RemoveHandler(SumitRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnSumit()
        {
            RoutedEventArgs args = new RoutedEventArgs(SumitRoutedEvent, this);
            this.RaiseEvent(args);
        }


        //声明和注册路由事件
        public static readonly RoutedEvent CloseRoutedEvent =
            EventManager.RegisterRoutedEvent("Close", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(AutoColumnPagedDataGrid));
        //CLR事件包装
        public event RoutedEventHandler Close
        {
            add { this.AddHandler(CloseRoutedEvent, value); }
            remove { this.RemoveHandler(CloseRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnClose()
        {
            RoutedEventArgs args = new RoutedEventArgs(CloseRoutedEvent, this);
            this.RaiseEvent(args);
        }

        private static ManualResetEvent _asyncShowWaitHandle = new ManualResetEvent(false);

        /// <summary> 显示蒙版 </summary>
        public static async Task<bool> ShowSource<T>(T value, Predicate<T> match = null, string title = null, int layerIndex = 0) where T : IEnumerable
        {
            bool result = false;

            await Application.Current.Dispatcher.Invoke(async () =>
            {
                if (Application.Current.MainWindow is IMainWindow window)
                {
                    AutoColumnPagedDataGrid form = new AutoColumnPagedDataGrid();

                    form.Title = title;

                    form.Style = Application.Current.FindResource(AutoColumnPagedDataGrid.SumitKey) as Style;

                    form.DataSource = value;

                    form.Close += (l, k) =>
                    {
                        HeBianGu.General.WpfControlLib.Message.Instance.CloseLayer();
                        _asyncShowWaitHandle.Set();
                        result = false;
                    };

                    form.Sumit += (l, k) =>
                    {
                        if (form.DataSource is IEnumerable<object> objs)
                        {
                            List<string> errors = null;

                            if (!objs.Any(o => o.ModelState(out errors)))
                            {
                                HeBianGu.General.WpfControlLib.Message.Instance.ShowSnackMessageWithNotice(errors?.FirstOrDefault());
                                return;
                            }
                        }

                        if (match != null && !match(value))
                        {
                            return;
                        }

                        HeBianGu.General.WpfControlLib.Message.Instance.CloseLayer();
                        _asyncShowWaitHandle.Set();
                        result = true;

                    };

                    window.ShowLayer(form);

                    _asyncShowWaitHandle.Reset();

                    Task task = new Task(() =>
                    {
                        _asyncShowWaitHandle.WaitOne();
                    });

                    task.Start();

                    await task;
                }
            });

            return result;

        }

    }

    /// <summary> 说明</summary>
    public class DataGridColumnSetItem : NotifyPropertyChangedBase
    {
        private DataGridColumn _dataGridColumn;

        public DataGridColumnSetItem(DataGridColumn dataGridColumn)
        {
            _dataGridColumn = dataGridColumn;
        }

        [DataGridColumn("*", Template = typeof(TextBoxDataGridColumn))]
        [Display(Name = "标题")]
        public string Header
        {
            get
            {
                if (_dataGridColumn.Header == null) return null;

                Type type = _dataGridColumn.Header.GetType();

                if (type == typeof(string))
                    return _dataGridColumn.Header.ToString();

                if (typeof(ContentControl).IsAssignableFrom(type))
                    return (_dataGridColumn.Header as ContentControl).Content?.ToString();

                return "--";
            }
            set
            {
                if (_dataGridColumn.Header == null)
                {
                    _dataGridColumn.Header = value;
                    return;
                }

                Type type = _dataGridColumn.Header.GetType();

                if (type == typeof(string))
                {
                    _dataGridColumn.Header = value;
                    return;
                }

                if (typeof(ContentControl).IsAssignableFrom(type))
                    (_dataGridColumn.Header as ContentControl).Content = value;
            }
        }

        [Display(Name = "是否可见")]
        public bool Visible
        {
            get { return _dataGridColumn.Visibility == Visibility.Visible; }
            set
            {
                _dataGridColumn.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
                RaisePropertyChanged();
            }
        }
        [DataGridColumn("*", Template = typeof(TextBoxDataGridColumn))]
        [Display(Name = "宽度")]
        public DataGridLength Width
        {
            get { return _dataGridColumn.Width; }
            set
            {
                _dataGridColumn.Width = value;
                RaisePropertyChanged();
            }
        }

    }

    public class DataGridColumnSet : NotifyPropertyChangedBase
    {
        public DataGridColumnSet(IEnumerable<DataGridColumn> dataGridColumns)
        {
            this.ColumnSetItems = dataGridColumns.Select(l => new DataGridColumnSetItem(l))?.ToList();
        }
        public List<DataGridColumnSetItem> ColumnSetItems { get; private set; }

    }
}
