// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace HeBianGu.Control.Filter
{
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

            string display = model.GetCustomAttribute<DisplayAttribute>()?.Name;

            this.DisplayName = display ?? model.Name;
        }

        public PropertyFilterBase(PropertyInfo property, IEnumerable source)
        {
            this.Model = property;

            this.Name = property.Name;

            string display = property.GetCustomAttribute<DisplayAttribute>()?.Name;

            this.DisplayName = display ?? property.Name;

            this.Source.Clear();

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
}
