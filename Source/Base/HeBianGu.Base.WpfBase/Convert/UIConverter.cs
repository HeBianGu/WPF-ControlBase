#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/21 15:37:56 
 * 文件名：UIConverter 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 布尔转不可用 </summary>
    [ValueConversion(typeof(Visibility), typeof(bool))]
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //将bool值转换为什么呢？自己在这里定义
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //反转换方法，就是对照上面的把男女再转换回去
            return (Visibility)value == Visibility.Visible;
        }
    }

    /// <summary> 空文本不可用 </summary>
    [ValueConversion(typeof(Visibility), typeof(string))]
    public class VisibilityEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Visibility.Hidden;

            if (string.IsNullOrEmpty(value.ToString().Trim())) return Visibility.Hidden;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception();
        }
    }

    /// <summary> 匹配文本本不可用 </summary>
    [ValueConversion(typeof(Visibility), typeof(string))]
    public class VisibilityStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Visibility.Collapsed;

            if (parameter == null) return Visibility.Visible;

            if (value.ToString().Trim() != parameter.ToString().Trim()) return Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception();
        }
    }

    /// <summary> 警告级别转换 </summary>
    [ValueConversion(typeof(string), typeof(int))]
    public class LevelToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Colors.Gray;

            if (string.IsNullOrEmpty(value.ToString().Trim())) return Colors.Gray;

            int v = int.Parse(value.ToString());

            switch (v)
            {
                case 1:
                    return Colors.Purple.ToString();
                case 2:
                    return Colors.Red.ToString();
                case 3:
                    return Colors.Yellow.ToString();
                case 4:
                    return Colors.Blue.ToString();
                case 5:
                    return Colors.Gray.ToString();
                default:
                    return Colors.Gray.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception();
        }
    }


    /// <summary> 绑定图标转换 </summary>
    [ValueConversion(typeof(Icon), typeof(ImageSource))]
    public class IconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null) return null;

            Icon icon = (Icon)value;

            ImageSource imageSource =
                System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return imageSource;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary> 绑定图标转换 </summary>
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null) return null;

            System.Windows.Media.Color color = (System.Windows.Media.Color)value;


            return new SolidColorBrush(color);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush brush = value as SolidColorBrush;

            if (brush == null) return null;

            return brush.Color;
        }
    }

    /// <summary> 根据背景色获取前景色。当然也可反着用 </summary>
    public class BackgroundToForegroundConverter : IValueConverter
    {
        private System.Windows.Media.Color IdealTextColor(System.Windows.Media.Color bg)
        {
            const int nThreshold = 105;
            var bgDelta = System.Convert.ToInt32((bg.R * 0.299) + (bg.G * 0.587) + (bg.B * 0.114));
            var foreColor = (255 - bgDelta < nThreshold) ? Colors.Black : Colors.White;
            return foreColor;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush)
            {
                var idealForegroundColor = this.IdealTextColor(((SolidColorBrush)value).Color);
                var foreGroundBrush = new SolidColorBrush(idealForegroundColor);
                foreGroundBrush.Freeze();
                return foreGroundBrush;
            }
            return System.Windows.Media.Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }


    /// <summary> 百分比转换为角度值 </summary>
    public class PercentToAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var percent = double.Parse(value.ToString());
            if (percent >= 1) return 360.0D;
            return percent * 360;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 获取Thickness固定值double
    /// </summary>
    public class ThicknessToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var thickness = (Thickness)value;
            return thickness.Left;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    /// <summary>
    /// 这是一个颠倒黑白的世界
    /// </summary>
    public sealed class TrueToFalseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (bool)value;
            return !v;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary> 按下 </summary>

    [ValueConversion(typeof(double), typeof(double))]
    public class PressedSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return double.Parse(value.ToString()) - 5;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return double.Parse(value.ToString()) + 5;
        }
    }
    /// <summary> 鼠标在上面 </summary>

    [ValueConversion(typeof(double), typeof(double))]
    public class MouseOverSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return double.Parse(value.ToString()) + 5;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return double.Parse(value.ToString()) - 5;
        }
    }

    public class CornerRadiusToDouble : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                return ((CornerRadius)value).TopLeft * System.Convert.ToDouble(parameter);
            }
            return ((CornerRadius)value).TopLeft;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                return new CornerRadius((double)value / System.Convert.ToDouble(parameter));
            }
            return new CornerRadius((double)value);
        }
    }

    /// <summary> 布尔转不可用 </summary>
    public class ComboBoxAutoSelectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            IEnumerable collection = value as IEnumerable;

            foreach (var item in collection)
            {
                if (item == parameter) return item;
            }

            return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

}
