using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Design
{
    [Displayer(Name = "仓储模型浮点数据", GroupName = "[浮点]", Icon = "\xe6e7", Description = "仓储模型浮点数据")]
    public class RepositoryDoublesDataContext<T> : PropertyDoublesDataContext<T> where T : StringEntityBase
    {
        public RepositoryDoublesDataContext() : base(ServiceRegistry.Instance.GetInstance<IStringRepository<T>>().GetList())
        {
            Name = typeof(T).GetCustomAttribute<DisplayerAttribute>()?.Name ?? typeof(T).Name;
        }
    }

    [Displayer(Name = "浮点数据", GroupName = "[浮点]", Icon = "\xe6e7", Description = "浮点数据")]
    public class DoublesDataContext : RangeDesignDataContextBase<IDisplayDoublesData>
    {
        public DoublesDataContext(IEnumerable<double> data)
        {
            Data = data;
        }

        [Browsable(false)]
        [XmlIgnore]
        public IEnumerable<double> Data { get; }

        public override void RefreshPresenter(IDisplayDoublesData presenter)
        {
            presenter.RefreshData(Data.Skip(Skip).Take(Take));
        }
    }

    [Displayer(Name = "模型属性浮点数据", GroupName = "[浮点]", Icon = "\xe6e7", Description = "模型属性浮点数据")]
    public class PropertyDoublesDataContext<T> : RangeDesignDataContextBase<IDisplayDoublesData>
    {
        [Browsable(false)]
        [XmlIgnore]
        public IEnumerable<T> ItemsSource { get; }

        [Browsable(false)]
        [XmlIgnore]
        public IEnumerable<string> PropertyInfos { get; }

        [Browsable(false)]
        [XmlIgnore]
        public IEnumerable<string> DisplayPropertyInfos { get; }

        private string _selectedPropertyInfo;
        [Display(Name = "选择数据字段")]
        [BindingGetSelectSourceProperty(nameof(PropertyInfos))]
        [PropertyItemType(typeof(ComboBoxSelectSourcePropertyItem))]
        public string SelectedPropertyInfo
        {
            get { return _selectedPropertyInfo; }
            set
            {
                _selectedPropertyInfo = value;
                DispatcherRaisePropertyChanged();
            }
        }

        private string _selectedDisplayPropertyInfo;
        [Display(Name = "选择显示字段")]
        [BindingGetSelectSourceProperty(nameof(DisplayPropertyInfos))]
        [PropertyItemType(typeof(ComboBoxSelectSourcePropertyItem))]
        public string SelectedDisplayPropertyInfo
        {
            get { return _selectedDisplayPropertyInfo; }
            set
            {
                _selectedDisplayPropertyInfo = value;
                DispatcherRaisePropertyChanged();
            }
        }

        public PropertyDoublesDataContext(IEnumerable<T> itemsSource)
        {
            ItemsSource = itemsSource;
            PropertyInfos = typeof(T).GetProperties().Where(x => x.PropertyType.IsPrimitive).Select(x => x.Name);
            DisplayPropertyInfos = typeof(T).GetProperties().Select(x => x.Name);
        }

        public PropertyDoublesDataContext(IEnumerable<T> itemsSource, IEnumerable<string> propertyInfos)
        {
            ItemsSource = itemsSource;
            PropertyInfos = propertyInfos;
        }
        public override void RefreshPresenter(IDisplayDoublesData presenter)
        {
            {
                if (!PropertyInfos.Contains(SelectedPropertyInfo))
                    return;
                Func<T, double> func = x => Convert.ToDouble(typeof(T).GetProperty(SelectedPropertyInfo).GetValue(x));
                presenter.RefreshData(ItemsSource.Skip(Skip).Take(Take).Select(func));
            }

            {
                if (!DisplayPropertyInfos.Contains(SelectedDisplayPropertyInfo))
                    return;
                Func<T, string> func = x => typeof(T).GetProperty(SelectedDisplayPropertyInfo).GetValue(x)?.ToString();
                presenter.xDisplay = ItemsSource.Skip(Skip).Take(Take).Select(func).ToObservable();
            }
        }
    }
}
