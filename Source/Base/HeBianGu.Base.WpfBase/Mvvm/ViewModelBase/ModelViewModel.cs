// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace HeBianGu.Base.WpfBase
{
    public interface IModelViewModel
    {

    }
    //public interface IModelViewModel<T> : IModelViewModel
    //{
    //    T Model { get; set; }
    //}

    public interface IModelViewModel<T> : IModelViewModel
    {
        T Model { get; set; }
        double Value { get; set; }
        bool Visible { get; set; }
    }


    public partial class ModelViewModel<T> : NotifyPropertyChanged, IModelViewModel<T>, ISearchable
    {
        //public ModelViewModel()
        //{
        //    this.Model = new T();
        //}
        public ModelViewModel(T t)
        {
            Model = t;

        }

        private T _model;
        /// <summary> Model层  </summary>
        [Browsable(false)]
        public T Model
        {
            get { return _model; }
            set
            {
                _model = value;
                RaisePropertyChanged("Model");
            }
        }

        private bool _visible = true;
        /// <summary> 是否可见  </summary>
        [Browsable(false)]
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                RaisePropertyChanged("Visible");
            }
        }

        private bool _isEnbled;
        /// <summary> 是否可用  </summary>
        [Browsable(false)]
        public bool IsEnbled
        {
            get { return _isEnbled; }
            set
            {
                _isEnbled = value;
                RaisePropertyChanged("IsEnbled");
            }
        }


        private bool _isBuzy;
        /// <summary> 说明  </summary>
        [Browsable(false)]
        public bool IsBuzy
        {
            get { return _isBuzy; }
            set
            {
                _isBuzy = value;
                RaisePropertyChanged();
            }
        }


        private double _value;
        /// <summary> 说明  </summary>
        [Browsable(false)]
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }


        private string _message;
        [Browsable(false)]
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }


        /// <summary> 将Model数据加载到ViewModel属性上面 通过名称匹配 </summary>
        protected virtual bool LoadValue(out string message)
        {
            message = string.Empty;
            PropertyInfo[] ps = this.GetType().GetProperties();
            if (ps == null)
                return true;
            foreach (PropertyInfo property in ps)
            {
                PropertyInfo find = typeof(T).GetProperty(property.Name);
                if (find == null)
                    continue;
                property.SetValue(this, find.GetValue(this.Model));
            }
            return true;
        }

        /// <summary> 将ViewModel数据加载到Model属性上面 通过名称匹配 </summary>
        protected virtual bool SaveValue(out string message)
        {
            message = string.Empty;
            PropertyInfo[] ps = this.GetType().GetProperties();
            if (ps == null)
                return true;
            foreach (PropertyInfo property in ps)
            {
                PropertyInfo find = typeof(T).GetProperty(property.Name);
                if (find == null)
                    continue;
                find.SetValue(this, property.GetValue(this.Model));
            }
            return true;
        }

        public virtual bool Filter(string txt)
        {
            if (string.IsNullOrEmpty(txt))
                return true;

            string[] ands = txt.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            {
                IEnumerable<PropertyInfo> ps = this.Model.GetType().GetProperties().Where(x => x.CanRead).Where(l => l.PropertyType == typeof(string) || l.PropertyType.IsPrimitive || l.PropertyType == typeof(DateTime));
                ps = ps.Where(x => x.Name != "Item" && x.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false);
                IEnumerable<string> list = ps.Select(x => x.GetValue(this.Model)?.ToString());
                if (ands.All(x => list.Any(l => l?.Contains(x) == true)))
                {
                    return true;
                }

            }
            {
                IEnumerable<PropertyInfo> ps = this.GetType().GetProperties().Where(x => x.CanRead).Where(l => l.PropertyType == typeof(string) || l.PropertyType.IsPrimitive || l.PropertyType == typeof(DateTime));
                ps = ps.Where(x => x.Name != "Item" && x.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false);
                IEnumerable<string> list = ps.Select(x => x.GetValue(this)?.ToString());
                if (ands.All(x => list.Any(l => l?.Contains(x) == true)))
                {
                    return true;
                }
            }
            this.Visible = false;
            return false;
        }
    }
}
