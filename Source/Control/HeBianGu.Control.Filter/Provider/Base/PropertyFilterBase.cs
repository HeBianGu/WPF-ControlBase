// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows.Media.Animation;
using System.Xml.Serialization;

namespace HeBianGu.Control.Filter
{
    public interface IPropertyFilter : IFilter
    {
        string DisplayName { get; set; }
        bool IsSelected { get; set; }
        string Name { get; set; }
        FilterOperate Operate { get; set; }
        PropertyInfo PropertyInfo { get; set; }
        //void SetValue(object value);
        //object GetValue();
    }

    /// <summary> 说明</summary>
    public abstract class PropertyFilterBase<T> : NotifyPropertyChanged, IFilter, IPropertyFilter
    {
        public PropertyFilterBase()
        {

        }

        private ObservableCollection<T> _source = new ObservableCollection<T>();
        [XmlIgnore]
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
        [XmlIgnore]
        public ObservableCollection<T> SelectedSource
        {
            get { return _selectedSource; }
            set
            {
                _selectedSource = value;
                RaisePropertyChanged("SelectedSource");
            }
        }
     
        private PropertyInfo _propertyInfo;
        [XmlIgnore]
        public PropertyInfo PropertyInfo
        {
            get { return _propertyInfo; }
            set
            {
                _propertyInfo = value;
                RaisePropertyChanged();
            }
        }


        public PropertyFilterBase(PropertyInfo propertyInfo)
        {
            this.PropertyInfo = propertyInfo;

            this.Name = propertyInfo.Name;

            string display = propertyInfo.GetCustomAttribute<DisplayAttribute>()?.Name;

            this.DisplayName = display ?? propertyInfo.Name;
        }

        public PropertyFilterBase(PropertyInfo property, IEnumerable source)
        {
            this.PropertyInfo = property;
            this.Name = property.Name;
            string display = property.GetCustomAttribute<DisplayAttribute>()?.Name;
            this.DisplayName = display ?? property.Name;
            this.Source.Clear();
            if (source == null)
                return;
            List<T> finds = new List<T>();
            foreach (object item in source)
            {
                T v = (T)property.GetValue(item);
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
        [XmlIgnore]
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

        //public void SetValue(object value)
        //{
        //    this.Value = value.TryChangeType<T>();
        //}
    }
}
