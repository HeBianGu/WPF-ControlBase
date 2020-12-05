using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{
    public class FilterBox : ListBox
    {
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
            if (this.DataSource == null) return;
            //  Do ：刷新输出数据源
            var filters = this.SelectedItems.OfType<IFilter>();

            var cache = Activator.CreateInstance(this.DataSource.GetType()) as IList;

            foreach (var item in this.DataSource)
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

                 IEnumerable config = e.NewValue as IEnumerable;

                 control.RefreshSetting();

             }));


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


        void RefreshSetting()
        {
            if (this.DataSource == null) return;

            var ctype = this.DataSource.GetType();

            if (!ctype.IsGenericType) return;

            var type = ctype.GetGenericArguments()?.FirstOrDefault();

            if (type == null) return;

            var ps = type.GetProperties();

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

    public interface IFilter
    {
        bool IsMatch(object obj);

        IFilter Copy();
    }

    /// <summary> 说明</summary>
    public abstract class PropertyFilterBase<T> : NotifyPropertyChanged, IFilter
    {
        public PropertyFilterBase()
        {

        }

        private ObservableCollection<T> _source = new ObservableCollection<T>();
        /// <summary> 选项资源  </summary>
        public ObservableCollection<T> Source
        {
            get { return _source; }
            set
            {
                _source = value;
                RaisePropertyChanged("Source");
            }
        }

        private ObservableCollection<T> _selectedSource = new ObservableCollection<T>();
        /// <summary> 选中资源  </summary>
        public ObservableCollection<T> SelectedSource
        {
            get { return _selectedSource; }
            set
            {
                _selectedSource = value;
                RaisePropertyChanged("SelectedSource");
            }
        }


        public PropertyInfo Model { get; set; }

        public PropertyFilterBase(PropertyInfo model)
        {
            this.Model = model;

            this.Name = model.Name;

            var display = model.GetCustomAttribute<DisplayAttribute>()?.Name;

            this.DisplayName = display ?? model.Name;
        }

        public PropertyFilterBase(PropertyInfo model, IEnumerable source)
        {
            this.Model = model;

            this.Name = model.Name;

            var display = model.GetCustomAttribute<DisplayAttribute>()?.Name;

            this.DisplayName = display ?? model.Name;

            this.Source.Clear();

            List<T> finds = new List<T>();

            foreach (var item in source)
            {
                T v = (T)model.GetValue(item);

                finds.Add(v);
            }

            this.Source = finds.Distinct().ToObservable();
        }

        #region - 属性 -

        private string _name;
        /// <summary> 说明  </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }


        private string _displayName;
        /// <summary> 说明  </summary>
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                RaisePropertyChanged("DisplayName");
            }
        }

        private FilterOperate _operate;
        /// <summary> 说明  </summary>
        [TypeConverter(typeof(EnumConverter))]
        public FilterOperate Operate
        {
            get { return _operate; }
            set
            {
                _operate = value;
                RaisePropertyChanged("Operate");
            }
        }

        private T _value;
        /// <summary> 说明  </summary>
        public T Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");
            }
        }

        private bool _isSelected;
        /// <summary> 说明  </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

        #endregion

        #region - 命令 -

        #endregion

        #region - 方法 -

        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {


            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }

        #endregion

        public abstract bool IsMatch(object obj);

        public abstract IFilter Copy();
    }

    public class StringFilter : PropertyFilterBase<string>
    {
        public StringFilter()
        {

        }
        public StringFilter(PropertyInfo property) : base(property)
        {
            this.Operate = FilterOperate.SelectSource;
        }

        public StringFilter(PropertyInfo property, IEnumerable source) : base(property, source)
        {
            this.Operate = FilterOperate.SelectSource;
        }

        public override IFilter Copy()
        {
            var result = new StringFilter(this.Model) { Operate = this.Operate, Value = this.Value, Source = this.Source, SelectedSource = new ObservableCollection<string>(this.SelectedSource) };

            if (this.Operate == FilterOperate.SelectSource)
            {
                result.Value = $"<{string.Join(",", this.SelectedSource)}>";
            }

            return result;
        }

        public override bool IsMatch(object obj)
        {
            var p = obj.GetType().GetProperty(this.Name);

            if (p == null || !p.CanRead) return false;

            var v = p.GetValue(obj)?.ToString();

            if (this.Operate == FilterOperate.Contain)
            {
                return v.Contains(this.Value);
            }
            else if (this.Operate == FilterOperate.UnContain)
            {
                return !v.Contains(this.Value);
            }
            else if (this.Operate == FilterOperate.Equals)
            {
                return v == this.Value;
            }
            else if (this.Operate == FilterOperate.UnEquals)
            {
                return v != this.Value;
            }
            else if (this.Operate == FilterOperate.SelectSource)
            {
                return this.SelectedSource.Any(l => l == v);
            }
            else if (this.Operate == FilterOperate.Setted)
            {
                return v != null;
            }
            else if (this.Operate == FilterOperate.Unset)
            {
                return v == null;
            }
            else
            {
                return false;
            }
        }
    }

    public abstract class MathValueFilter<T> : PropertyFilterBase<T> where T : IComparable
    {
        public MathValueFilter()
        {

        }
        public MathValueFilter(PropertyInfo property) : base(property)
        {
            this.Operate = FilterOperate.Equals;
        }

        public override bool IsMatch(object obj)
        {
            var p = obj.GetType().GetProperty(this.Name);

            if (p == null || !p.CanRead) return false;

            var v = (T)p.GetValue(obj);

            if (this.Operate == FilterOperate.Equals)
            {
                return v.CompareTo(this.ConvertValue()) == 0;
            }
            else if (this.Operate == FilterOperate.UnEquals)
            {
                return v.CompareTo(this.ConvertValue()) != 0;
            }
            else if (this.Operate == FilterOperate.Greater)
            {
                return v.CompareTo(this.ConvertValue()) > 0;
            }
            else if (this.Operate == FilterOperate.Less)
            {
                return v.CompareTo(this.ConvertValue()) < 0;
            }
            else if (this.Operate == FilterOperate.GreaterAndEqual)
            {
                return v.CompareTo(this.ConvertValue()) >= 0;
            }
            else if (this.Operate == FilterOperate.LessAndEqual)
            {
                return v.CompareTo(this.ConvertValue()) <= 0;
            }
            else
            {
                return false;
            }
        }

        public virtual T ConvertValue()
        {
            return this.Value;
        }
    }

    public class IntFilter : MathValueFilter<int>
    {
        public IntFilter()
        {

        }

        public IntFilter(PropertyInfo property) : base(property)
        {

        }

        public override IFilter Copy()
        {
            return new IntFilter(this.Model) { Operate = this.Operate, Value = this.Value };
        }
    }

    public class DoubleFilter : MathValueFilter<double>
    {
        public DoubleFilter()
        {

        }
        public DoubleFilter(PropertyInfo property) : base(property)
        {

        }

        public override IFilter Copy()
        {
            return new DoubleFilter(this.Model) { Operate = this.Operate, Value = this.Value };
        }
    }

    public class BooleanFilter : PropertyFilterBase<bool>
    {
        public BooleanFilter()
        {

        }
        public BooleanFilter(PropertyInfo property) : base(property)
        {

        }

        public override IFilter Copy()
        {
            return new BooleanFilter(this.Model) { Operate = this.Operate, Value = this.Value };
        }

        public override bool IsMatch(object obj)
        {
            var p = obj.GetType().GetProperty(this.Name);

            if (p == null || !p.CanRead) return false;

            var v = (bool)p.GetValue(obj);

            return v == this.Value;
        }
    }

    public class DateTimeFilter : MathValueFilter<DateTime>
    {
        public DateTimeFilter()
        {

        }
        public DateTimeFilter(PropertyInfo property) : base(property)
        {
            this.Value = DateTime.Now;
        }


        private bool _onlyDate = true;
        /// <summary> 仅比较日期 </summary>
        public bool OnlyDate
        {
            get { return _onlyDate; }
            set
            {
                _onlyDate = value;
                RaisePropertyChanged("OnlyDate");
            }
        }

        public override IFilter Copy()
        {
            return new DateTimeFilter(this.Model) { Operate = this.Operate, Value = this.Value };
        }

        public override DateTime ConvertValue()
        {
            return this.OnlyDate ? this.Value.Date : this.Value;
        }

    }

    public static class FilterFactory
    {
        public static IFilter Create(PropertyInfo property, IEnumerable source)
        {
            if (property == null) return null;

            if (!property.CanRead) return null;

            if (property.PropertyType.FullName == typeof(string).FullName)
            {
                return new StringFilter(property, source);
            }
            else if (property.PropertyType.FullName == typeof(int).FullName)
            {
                return new IntFilter(property);
            }
            else if (property.PropertyType.FullName == typeof(double).FullName)
            {
                return new DoubleFilter(property);
            }
            else if (property.PropertyType.FullName == typeof(bool).FullName)
            {
                return new BooleanFilter(property);
            }
            else if (property.PropertyType.FullName == typeof(DateTime).FullName)
            {
                return new DateTimeFilter(property);
            }

            throw new NotImplementedException($"{property.PropertyType.FullName} 类型没有实现，请先实现该类型方法");
        }
    }

    public class FilterItemCollection : ObservableCollection<IFilter>
    {
        
    }

    [TypeConverter(typeof(DisplayConverter))]
    public enum FilterOperate
    {
        [Display(GroupName = "String",Name ="选择数据源")]
        SelectSource = 0,
        [Display(GroupName = "String,Double,Int,DateTime", Name = "等于")]
        Equals ,
        [Display(GroupName = "String,Double,Int,DateTime", Name = "不等于")]
        UnEquals,
        [Display(GroupName = "String", Name = "包含")]
        Contain,
        [Display(GroupName = "String", Name = "不包含")]
        UnContain,
        [Display(GroupName = "String", Name = "已设置")]
        Setted,
        [Display(GroupName = "String", Name = "未设置")]
        Unset,
        [Display(GroupName = "Double,Int,DateTime", Name = "大于")]
        Greater,
        [Display(GroupName = "Double,Int,DateTime", Name = "小于")]
        Less,
        [Display(GroupName = "Double,Int,DateTime", Name = "大于等于")]
        GreaterAndEqual,
        [Display(GroupName = "Double,Int,DateTime", Name = "小于等于")]
        LessAndEqual
    }
}
