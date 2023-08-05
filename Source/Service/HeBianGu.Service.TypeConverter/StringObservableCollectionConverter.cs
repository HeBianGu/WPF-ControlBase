// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using HeBianGu.Base.WpfBase;

namespace HeBianGu.Service.TypeConverter
{
    public class StringObservableCollectionConverter : System.ComponentModel.TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        { 
            if (value is StringObservableCollection obs)
            {
                return string.Join(",", obs);
            }

            return null;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        { 
            if (value is string str)
            { 
                var sss= new StringObservableCollection(str?.Split(','));

                return sss;
            }

            return null;
        }
    }

    [TypeConverter(typeof(StringObservableCollectionConverter))]
    public class StringObservableCollection : ObservableCollection<string>
    {
        public StringObservableCollection(IEnumerable<string> collection) : base(collection)
        {

        }

        public StringObservableCollection()
        {

        }
    }
}
