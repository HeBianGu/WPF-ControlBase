// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace HeBianGu.Control.PropertyGrid
{
    /// <summary> 泛型类型基类 </summary>
    public class ObjectPropertyItem<T> : ObjectPropertyItem, IDataErrorInfo
    {
        public ObjectPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

            List<RequiredAttribute> required = property.GetCustomAttributes<RequiredAttribute>()?.ToList();

            Validations = property.GetCustomAttributes<ValidationAttribute>()?.ToList();

            //  Do ：这两个特性用在通知，本控件默认不可用于验证属性定义
            Validations.RemoveAll(l => l is CustomValidationAttribute);

            Validations.RemoveAll(l => l is CompareAttribute);

            if (required != null && required.Count > 0)
            {
                this.Flag = "*";
            }

            this.LoadValue();
        }

        private T _value;
        /// <summary> 说明  </summary>
        [Browsable(false)]
        public virtual T Value
        {
            get { return _value; }
            set
            {

                this.Message = null;

                _value = value;

                if (!this.CheckType(value, out string error))
                {
                    this.Message = error;

                    return;
                }

                this.Error = null;
                //  Do：检验数据有效性
                if (Validations != null)
                {
                    foreach (ValidationAttribute item in Validations)
                    {
                        if (!item.IsValid(value))
                        {
                            if (item is RequiredAttribute required)
                            {
                                this.Message = item.ErrorMessage ?? this.Name + "数据不能为空";
                            }
                            else
                            {
                                this.Message = item.ErrorMessage ?? this.Name + "数据校验失败";
                            }
                        }
                    }
                }

                //_value = value;

                RaisePropertyChanged("Value");

                if (!this.PropertyInfo.CanWrite)
                {
                    this.ReadOnly = true;
                    return;
                }

                try
                {
                    this.SetValue(value);
                }
                catch (Exception ex)
                {
                    this.Message = ex.Message + $"[{this.PropertyInfo.PropertyType.Name}]";
                    return;
                }

                //  Do ：触发值更改
                this.ValueChanged?.Invoke(value);

            }
        }

        private List<ValidationAttribute> Validations { get; }

        /// <summary> 验证数据类型是否合法 </summary>
        protected virtual bool CheckType(T value, out string error)
        {
            error = null;

            try
            {
                object to = this.ConverToObject(value);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message + $"[{this.PropertyInfo.PropertyType.Name}]";
                return false;
            }
        }

        private object ConverToObject(T value)
        {
            TypeConverterAttribute propertyConvert = this.PropertyInfo.GetCustomAttribute<TypeConverterAttribute>();

            if (propertyConvert != null)
            {
                Type type = Type.GetType(propertyConvert.ConverterTypeName);

                TypeConverter ddd = Activator.CreateInstance(type) as TypeConverter;

                //if(!ddd.CanConvertFrom(typeof(T)))
                //{
                //    return null;
                //}

                return ddd.ConvertFrom(null, System.Globalization.CultureInfo.CurrentUICulture, value);
            }

            TypeConverterAttribute typeConvert = this.PropertyInfo.PropertyType.GetCustomAttribute<TypeConverterAttribute>();

            if (typeConvert != null)
            {
                Type type = Type.GetType(typeConvert.ConverterTypeName);

                ConstructorInfo constructor = type.GetConstructors().FirstOrDefault(l => l.GetParameters().Count() == 0);

                if (constructor != null)
                {
                    TypeConverter instance = Activator.CreateInstance(type) as TypeConverter;

                    if (value != null)
                    {
                        return instance.ConvertFrom(null, System.Globalization.CultureInfo.CurrentUICulture, value?.ToString());
                    }
                }
            }

            if (value is IConvertible convertible)
            {
                return Convert.ChangeType(value, this.PropertyInfo.PropertyType);
            }
            else
            {
                return value;
            }
        }

        /// <summary> 设置值的方法 </summary>
        protected virtual void SetValue(T value)
        {
            object to = this.ConverToObject(value);

            this.PropertyInfo.SetValue(Obj, to);

        }

        /// <summary> 获取值的方法 </summary>
        protected virtual T GetValue()
        {
            return (T)this.PropertyInfo.GetValue(Obj);
        }


        protected R GetValue<R>()
        {
            return (R)this.PropertyInfo.GetValue(this.Obj);
        }

        public override void LoadValue()
        {
            this.Value = this.GetValue();
        }

        private string _message;
        /// <summary> 说明  </summary>
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }

        public string Flag { get; set; }

        public string Error { get; private set; }

        // 验证
        public string this[string columnName]
        {
            get
            {
                return this.Error = this.Message;
            }
        }

    }


    /// <summary> 类型基类 </summary>
    public abstract class ObjectPropertyItem : NotifyPropertyChanged, IPropertyItem
    {
        public string Name { get; set; }

        public string TabGroup { get; set; }

        public string Group { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

        public object Obj { get; set; }

        public bool ReadOnly { get; set; }

        public Visibility Visibility { get; set; }

        public string Description { get; set; }

        public Action<object> ValueChanged { get; set; }

        //~ObjectPropertyItem()
        //{
        //    ValueChanged = null; 
        //}

        public ObjectPropertyItem(PropertyInfo property, object obj)
        {
            PropertyInfo = property;

            DisplayAttribute display = property.GetCustomAttribute<DisplayAttribute>();

            Name = display == null ? property.Name : display.Name;

            Obj = obj;

            this.TabGroup = display?.Prompt;

            this.Group = display?.GroupName;

            this.Description = display?.Description;

            ReadOnlyAttribute readyOnly = property.GetCustomAttribute<ReadOnlyAttribute>();

            //var readyOnly = property.GetCustomAttribute<EditableAttribute>();

            this.ReadOnly = readyOnly == null || readyOnly.IsReadOnly == false ? false : true;

            if (!this.PropertyInfo.CanWrite)
            {
                this.ReadOnly = true;
            }

            //  Do ：用于控制显示和隐藏
            BrowsableAttribute browsable = property.GetCustomAttribute<BrowsableAttribute>();

            this.Visibility = browsable == null || browsable.Browsable ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// 从实体中加载数据到页面
        /// </summary>
        public abstract void LoadValue();
    }













    ///// <summary> Double属性类型 </summary>
    //public class DoubleArrayPropertyItem : ObjectPropertyItem<double[]>
    //{
    //    public DoubleArrayPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    //    {
    //    }
    //}
}
