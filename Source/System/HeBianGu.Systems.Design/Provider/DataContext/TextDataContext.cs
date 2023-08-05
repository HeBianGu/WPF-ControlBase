using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using System;
using System.Configuration;
using System.Reflection;

namespace HeBianGu.Systems.Design
{
    [Displayer(Name = "字段文本", GroupName = "[文本]", Icon = "\xe6e7", Description = "属性字段")]
    public class PropertyTextDataContext : DesignDataContextBase<ITextData>
    {
        object _targetObject;
        string _propertyName;
        public PropertyTextDataContext(object obj, string property)
        {
            _targetObject = obj;
            _propertyName = property;
            Name = property;

        }
        public override void RefreshPresenter(ITextData presenter)
        {
            presenter.Text = _targetObject.GetType().GetProperty(_propertyName).GetValue(_targetObject)?.ToString();
        }

        public override string ToString()
        {
            return _propertyName;
        }
    }

    [Displayer(Name = "属性字段", GroupName = "[文本]", Icon = "\xe6e7", Description = "属性字段")]
    public class SelectPropertysTextDataContext : DesignDataContextBase<ITextData>
    {
        object _targetObject;
        private string _propertyName;
        [Display(Name = "选择数据字段")]
        [BindingGetSelectSourceProperty(nameof(PropertyInfos))]
        [PropertyItemType(typeof(ComboBoxSelectSourcePropertyItem))]
        public string PropertyName
        {
            get { return _propertyName; }
            set
            {
                _propertyName = value;
                OnDispatcherPropertyChanged();
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public IEnumerable<string> PropertyInfos { get; }

        public SelectPropertysTextDataContext(object obj)
        {
            _targetObject = obj;
            this.PropertyInfos = obj.GetType().GetProperties().Where(x => x.PropertyType.IsValueType || x.PropertyType == typeof(string) || x.PropertyType == typeof(DateTime))
                .Select(x => x.Name);

        }

        public SelectPropertysTextDataContext(object obj, Func<PropertyInfo, bool> where)
        {
            _targetObject = obj;
            this.PropertyInfos = obj.GetType().GetProperties().Where(where).Select(x => x.Name);
        }

        public override void RefreshPresenter(ITextData presenter)
        {
            if (!this.PropertyInfos.Contains(this.PropertyName))
                return;
            presenter.Text = _targetObject.GetType().GetProperty(PropertyName).GetValue(_targetObject)?.ToString();
        }
    }
}
