// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace HeBianGu.Systems.Repository
{
    public class ComboboxRepositoryPropertyItem : ObjectPropertyItem<object>
    {
        public ComboboxRepositoryPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
            Type rType = typeof(IRepository<>).MakeGenericType(property.PropertyType);
            object r = ServiceRegistry.Instance.GetInstance(rType);

            IEnumerable source = rType.GetMethod("GetAll").Invoke(r, new object[] { null }) as IEnumerable;

            this.ItemsSource = source;
            object v = property.GetValue(obj);

            if (v == null && source is IList list)
            {
                this.Value = list.Count > 0 ? list[0] : null;
            }
            this.Value = v ?? source;
        }

        private IEnumerable _itemsSource;
        /// <summary> 说明  </summary>
        public IEnumerable ItemsSource
        {
            get { return _itemsSource; }
            set
            {
                _itemsSource = value;
                RaisePropertyChanged("ItemsSource");
            }
        }
    }

    public class RepositoryTypeConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value == null)
                    return string.Empty;

                DisplayNameAttribute attributes = value.GetType().GetCustomAttribute<DisplayNameAttribute>(false);

                return attributes?.DisplayName ?? string.Empty;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return base.ConvertFrom(context, culture, value);
        }
    }
}
