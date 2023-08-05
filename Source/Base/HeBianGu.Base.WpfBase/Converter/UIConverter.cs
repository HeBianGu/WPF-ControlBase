// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HeBianGu.Base.WpfBase
{

    ///// <summary> 绑定图标转换 </summary>
    //[ValueConversion(typeof(Icon), typeof(ImageSource))]
    //public class IconConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {

    //        if (value == null) return null;

    //        Icon icon = (Icon)value;

    //        ImageSource imageSource =
    //            System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
    //            icon.Handle,
    //            Int32Rect.Empty,
    //            BitmapSizeOptions.FromEmptyOptions());

    //        return imageSource;
    //    }
    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class FindResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                object find = Application.Current.TryFindResource(value);

                return find;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DrawerOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double d = value as double? ?? 0;
            if (double.IsInfinity(d) || double.IsNaN(d)) d = 0;

            Dock dock = (parameter is Dock) ? (Dock)parameter : Dock.Left;
            switch (dock)
            {
                case Dock.Top:
                    return new Thickness(0, 0 - d, 0, 0);
                case Dock.Bottom:
                    return new Thickness(0, 0, 0, 0 - d);
                case Dock.Right:
                    return new Thickness(0, 0, 0 - d, 0);
                case Dock.Left:
                default:
                    return new Thickness(0 - d, 0, 0, 0);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary> 显示类型的Display名称 </summary>
    public class WriteLineObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine(value);
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    /// <summary> 根据映射转换显示 </summary>
    public class DisplayMapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IEnumerable array = parameter as IEnumerable;

            foreach (DisplayMap item in array.Cast<DisplayMap>())
            {
                if (item.Key.ToString() == value.ToString())
                {
                    return item.Value;
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DisplayMap
    {
        public object Key { get; set; }

        public object Value { get; set; }
    }

    public class ServiceRegistryVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Type type)
            {
                object find = ServiceRegistry.Instance.GetService(type);

                return find == null ? Visibility.Collapsed : Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
