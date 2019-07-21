using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 类型基类 </summary>
    public class ObjectPropertyItem : NotifyPropertyChanged
    {
        public string Name { get; set; }
        public PropertyInfo PropertyInfo { get; set; }

        public bool ReadOnly { get; set; } 

        public Visibility Visibility { get; set; }

        public object Obj { get; set; }
        public ObjectPropertyItem(PropertyInfo property, object obj)
        {
            PropertyInfo = property;


            var display = property.GetCustomAttribute<DisplayAttribute>();

            Name = display == null ? property.Name : display.Name;

            Obj = obj;


            var hidden = property.GetCustomAttribute<HiddenAttribute>();

            var readyOnly = property.GetCustomAttribute<ReadOnlyAttribute>();

            this.ReadOnly = readyOnly == null ? true : false;

            this.Visibility = hidden == null ? Visibility.Visible : Visibility.Collapsed;
        }


    }

    /// <summary> 泛型类型基类 </summary>
    public class ObjectPropertyItem<T> : ObjectPropertyItem
    {
        private T _value;
        /// <summary> 说明  </summary>
        public T Value
        {
            get { return _value; }
            set
            {

                this.Message = null;

                //  Do：检验数据有效性
                if (Validations != null)
                {
                    foreach (var item in Validations)
                    {
                        if (!item.IsValid(value))
                        {
                            this.Message = item.ErrorMessage;  
                        }
                    }
                }

                _value = value; 

                RaisePropertyChanged("Value");

                this.SetValue(value);
            }
        }

        void SetValue(T value)
        {
            this.PropertyInfo.SetValue(Obj, value);
        }

        List<ValidationAttribute> Validations { get; }

        public ObjectPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
        
            Validations = property.GetCustomAttributes<ValidationAttribute>()?.ToList();

            if(Validations!=null&& Validations.Count>0)
            {
                this.Flag = "*";
            }

            Value = (T)property.GetValue(obj);
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

    }

    /// <summary> 字符串属性类型 </summary>
    public class StringPropertyItem : ObjectPropertyItem<string>
    {
        public StringPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
        }
    }

    /// <summary> 时间属性类型 </summary>
    public class DateTimePropertyItem : ObjectPropertyItem<DateTime>
    {
        public DateTimePropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
        }
    }

    /// <summary> Double属性类型 </summary>
    public class DoublePropertyItem : ObjectPropertyItem<double>
    {
        public DoublePropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
        }
    }

    /// <summary> Int属性类型 </summary>

    public class IntPropertyItem : ObjectPropertyItem<int>
    {
        public IntPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
        }
    }

    /// <summary> Bool属性类型 </summary>
    public class BoolPropertyItem : ObjectPropertyItem<bool>
    {
        public BoolPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
        }
    }
}
