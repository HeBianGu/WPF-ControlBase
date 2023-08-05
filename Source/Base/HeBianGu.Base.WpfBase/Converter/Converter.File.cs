// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace HeBianGu.Base.WpfBase
{
    public class GetDirectoryNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            return System.IO.Path.GetDirectoryName(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class GetFileNameWithoutExtensionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            return System.IO.Path.GetFileNameWithoutExtension(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class GetExtensionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            return System.IO.Path.GetExtension(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class GetFileLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            if (!File.Exists(value.ToString()))
                return 0;

            FileInfo info = new FileInfo(value.ToString());
            return info.Length;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class GetFileLengthDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            if (!File.Exists(value.ToString()))
                return null;

            FileInfo info = new FileInfo(value.ToString());

            return XConverter.ByteSizeDisplayConverter.Convert(info.Length, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary> 转换成GB MB KB 显示 </summary> 
    public class ByteSizeDisplayConverter : IValueConverter
    {
        private const double TB = 1024 * 1024 * 1024 * 1024.0;
        private const int GB = 1024 * 1024 * 1024;
        private const int MB = 1024 * 1024;
        private const int KB = 1024;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (long.TryParse(value?.ToString(), out long KSize))
            {
                if (KSize / TB >= 1)
                    return (Math.Round(KSize / (float)TB, 2)).ToString() + "T";
                else if (KSize / GB >= 1)
                    return (Math.Round(KSize / (float)GB, 2)).ToString() + "G";
                else if (KSize / MB >= 1)

                    return (Math.Round(KSize / (float)MB, 2)).ToString() + "MB";
                else if (KSize / KB >= 1)

                    return (Math.Round(KSize / (float)KB, 2)).ToString() + "KB";
                else
                    return KSize.ToString() + "Byte";
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
