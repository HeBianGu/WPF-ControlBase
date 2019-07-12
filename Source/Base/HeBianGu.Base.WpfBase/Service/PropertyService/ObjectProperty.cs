using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{

    public class ObjectPropertyItem : NotifyPropertyChanged
    {
        public string Name { get; set; }
        public PropertyInfo PropertyInfo { get; set; }

        public object Obj { get; set; }
        public ObjectPropertyItem(PropertyInfo property, object obj)
        {
            PropertyInfo = property;


            var display = property.GetCustomAttribute<DisplayAttribute>();

            Name = display == null ? property.Name : display.Name;

            Obj = obj;
        }


    }

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
            Value = (T)property.GetValue(obj); 

            Validations = property.GetCustomAttributes<ValidationAttribute>()?.ToList();

            if(Validations!=null&& Validations.Count>0)
            {
                this.Flag = "*";
            }
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

    public class StringPropertyItem : ObjectPropertyItem<string>
    {
        public StringPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
        }
    }

    public class DateTimePropertyItem : ObjectPropertyItem<DateTime>
    {
        public DateTimePropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
        }
    }

    public class DoublePropertyItem : ObjectPropertyItem<double>
    {
        public DoublePropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
        }
    }

    public class IntPropertyItem : ObjectPropertyItem<int>
    {
        public IntPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
        }
    }

    public class BoolPropertyItem : ObjectPropertyItem<bool>
    {
        public BoolPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
        }
    }
}
