// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Service.TypeConverter
{
    public class ObservableCollectionTypeConverter<Item, ItemConverter> : System.ComponentModel.TypeConverter where ItemConverter : System.ComponentModel.TypeConverter, new()
    {
        public ItemConverter ItemTypeConverter => new ItemConverter();

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is ObservableCollection<Item> obs)
                return string.Join(" ", obs.Select(x => ItemTypeConverter.ConvertToString(x)));
            return null;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var result = new ObservableCollection<Item>();
            if (value is string str)
            {
                if (string.IsNullOrEmpty(str))
                    return result;
                foreach (var item in str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    result.Add((Item)ItemTypeConverter.ConvertFrom(item));
                }
                return result;
            }
            return result;
        }
    }
}
