// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HeBianGu.Service.Validation
{

    /// <summary> 拥有根据Model验证特性判断用户输入数据是否合法的绑定模型基类 </summary>
    public class ValidationModelViewModel<T> : ModelViewModel<T>
    {

        public ValidationModelViewModel(T t) : base(t)
        {

        }

        /// <summary> 获取指定名称实体字段数据 </summary>
        protected ValidationProperty<R> CreateModelProperty<R>([CallerMemberName] string propertyName = "")
        {
            ValidationProperty<R> value = new ValidationProperty<R>();

            //  Do ：值改变时检查数据，并更新实体字段
            value.ValueChanged = l =>
            {
                this.RefreshModelProperty(value, propertyName);
            };

            PropertyInfo p = typeof(T).GetProperty(propertyName);

            PropertyInfo p_this = this.GetType().GetProperty(propertyName);

            string display = p.GetCustomAttributes<DisplayAttribute>()?.FirstOrDefault()?.Name;

            string display_this = p_this.GetCustomAttributes<DisplayAttribute>()?.FirstOrDefault()?.Name;

            value.Name = p.Name;

            value.DisplayName = display_this ?? display ?? value.Name;

            value.DisplayName = display ?? value.Name;

            if (!p.CanRead)
            {
                value.Message = "只写字段不支持读取数据";
                return value;
            }

            //  Do ：判断数据验证
            List<ValidationAttribute> collection = p.GetCustomAttributes<ValidationAttribute>()?.ToList();

            if (collection != null)
            {
                foreach (ValidationAttribute r in collection)
                {
                    if (!r.IsValid(value.Value))
                    {
                        value.Message = r.ErrorMessage ?? r.FormatErrorMessage(display ?? p.Name);

                        //  Do ：保存实体验证失败
                        break;
                    }
                }
            }
            List<ValidationAttribute> collection_this = p_this.GetCustomAttributes<ValidationAttribute>()?.ToList();

            if (collection_this != null)
            {
                foreach (ValidationAttribute r in collection_this)
                {

                    if (!r.IsValid(value.Value))
                    {
                        value.Message = r.ErrorMessage ?? r.FormatErrorMessage(value.DisplayName);

                        //  Do ：保存实体验证失败
                        break;
                    }
                }
            }

            value.Flag = p.GetCustomAttributes<RequiredAttribute>()?.FirstOrDefault() != null ? "*" : "";

            value.Value = (R)p.GetValue(this.Model);

            return value;
        }

        /// <summary> 设置指定名称实体字段数据  </summary>
        private bool RefreshModelProperty<R>(ValidationProperty<R> value, [CallerMemberName] string propertyName = "")
        {
            PropertyInfo p = typeof(T).GetProperty(propertyName);

            if (!p.CanWrite)
            {
                value.Message = "只读字段不可以修改";
                return false;
            }

            value.Message = null;

            //  Do ：判断数据验证
            List<ValidationAttribute> collection = p.GetCustomAttributes<ValidationAttribute>()?.ToList();

            if (collection != null)
            {
                foreach (ValidationAttribute r in collection)
                {
                    if (!r.IsValid(value.Value))
                    {
                        string display = p.GetCustomAttributes<DisplayAttribute>()?.FirstOrDefault()?.Name;

                        value.Message = r.ErrorMessage ?? r.FormatErrorMessage(display ?? p.Name);

                        //  Do ：保存实体验证失败
                        return false;
                    }
                }
            }

            p.SetValue(this.Model, value.Value);

            return true;
        }

        /// <summary> 检查数据状态 </summary>
        public bool ModelState(out List<string> errors)
        {
            errors = new List<string>();

            PropertyInfo[] ps = this.GetType().GetProperties();

            foreach (PropertyInfo item in ps)
            {
                if (item.PropertyType.IsSubclassOf(typeof(ValidationPropertyBase)))
                {
                    ValidationPropertyBase v = item.GetValue(this) as ValidationPropertyBase;

                    if (!string.IsNullOrEmpty(v.Message))
                    {
                        errors.Add(v.Message);
                    }
                }
            }

            return errors.Count == 0;
        }
    }

    /// <summary> 用于绑定验证Model的实体，可以应用Model中的验证特性判断输入数据是否合法</summary>
    public class ValidationProperty<V> : ValidationPropertyBase
    {

        private V _value;
        /// <summary> 值  </summary>
        public V Value
        {
            get { return _value; }
            set
            {
                _value = value;

                //  Do ：应用实体验证数据有效性
                this.ValueChanged?.Invoke(value);

                RaisePropertyChanged("Value");
            }
        }


        public Action<V> ValueChanged;
    }

    /// <summary> 验证Model属性的基类 </summary>
    public class ValidationPropertyBase : NotifyPropertyChanged
    {
        #region - 属性 -


        private string _name;
        /// <summary> 实体字段名称  </summary>
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

        private string _flag;
        /// <summary> 必须字段标识  </summary>
        public string Flag
        {
            get { return _flag; }
            set
            {
                _flag = value;
                RaisePropertyChanged("Flag");
            }
        }


        private string _message;
        /// <summary> 验证消息  </summary>
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }

        #endregion

    }
}
