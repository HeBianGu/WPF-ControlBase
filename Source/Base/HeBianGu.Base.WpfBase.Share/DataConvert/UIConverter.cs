using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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

    /// <summary> 布尔为true转不可用 </summary>
    [ValueConversion(typeof(Visibility), typeof(bool))]
    public class FalseToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //将bool值转换为什么呢？自己在这里定义
            return (bool)value ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //反转换方法，就是对照上面的把男女再转换回去
            return (Visibility)value == Visibility.Collapsed;
        }
    }

    /// <summary> 布尔为true转不可用 </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class VisibilityToBoolenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //将bool值转换为什么呢？自己在这里定义
            return (Visibility)value == Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //反转换方法，就是对照上面的把男女再转换回去
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
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

    /// <summary> 匹配文本本不可用 </summary>
    [ValueConversion(typeof(Visibility), typeof(string))]
    public class VisibilityWithOutStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Visibility.Collapsed;

            if (parameter == null) return Visibility.Visible;

            if (value.ToString().Trim() == parameter.ToString().Trim()) return Visibility.Collapsed;

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
            var v = (bool)value;
            return !v;
        }
    }

    /// <summary>
    /// 这是一个颠倒黑白的世界 其中有一个为true则反馈false
    /// </summary>
    public sealed class TrueToFalseMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (var item in value)
            {
                var v = (bool)item;

                if (v)
                {
                    return false;

                }
            }
            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
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

    /// <summary> 乘除法转换计算 </summary>
    public class MathMultiplicationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double from = System.Convert.ToDouble(value?.ToString());
            double param = System.Convert.ToDouble(parameter?.ToString());

            return from * param;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    /// <summary> 加减法转换计算 </summary>
    public class MathAdditionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double from = (double)value;
            double param = (double)parameter;

            return from + param;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
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

    public class BrushRoundConverter : IValueConverter
    {
        public System.Windows.Media.Brush HighValue { get; set; } = System.Windows.Media.Brushes.White;

        public System.Windows.Media.Brush LowValue { get; set; } = System.Windows.Media.Brushes.Black;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var solidColorBrush = value as SolidColorBrush;

            if (solidColorBrush == null) return null;

            var color = solidColorBrush.Color;

            var brightness = 0.3 * color.R + 0.59 * color.G + 0.11 * color.B;

            return brightness < 123 ? LowValue : HighValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    public enum MathOperation
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public sealed class MathMultipleConverter : IMultiValueConverter
    {
        public MathOperation Operation { get; set; }

        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.Length < 2 || value[0] == null || value[1] == null) return Binding.DoNothing;

            if (!double.TryParse(value[0].ToString(), out double value1) || !double.TryParse(value[1].ToString(), out double value2))
                return 0;

            switch (Operation)
            {
                default:
                    // (case MathOperation.Add:)
                    return value1 + value2;
                case MathOperation.Divide:
                    return value1 / value2;
                case MathOperation.Multiply:
                    return value1 * value2;
                case MathOperation.Subtract:
                    return value1 - value2;
            }

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DrawerOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var d = value as double? ?? 0;
            if (double.IsInfinity(d) || double.IsNaN(d)) d = 0;

            var dock = (parameter is Dock) ? (Dock)parameter : Dock.Left;
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

    /// <summary> 分割字符串取第一个值 </summary>
    public class StringSplitFirstConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            if (parameter == null) return value.ToString().Split(' ')[0];

            return value.ToString().Split(parameter.ToString().ToCharArray())[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary> 替换字符串 </summary>
    public class StringReplaceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            if (parameter == null) return value;

            return value.ToString().Replace(value.ToString().Split(' ')[0], value.ToString().Split(' ')[1]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary> 替换字符串 </summary>
    public class IsEqualConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return false;
            bool result= value.Equals(parameter);

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return parameter;
            }

            return null;
        }
    }

    public class IsMultiValueEqualConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return false;

            if (values.Length < 2) return false;


            return values[0] == values[1];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    /// <summary> 替换字符串 </summary>
    public class ByteToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            byte[] byteArray = System.Convert.FromBase64String(value.ToString());

            try
            {
                BitmapImage bmp = null;

                bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(byteArray);
                bmp.EndInit();

                return bmp;
            }
            catch
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return parameter;
            }

            return null;
        }
    }

    public class Number2PercentageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length != 2) return .0;

            var obj1 = values[0];
            var obj2 = values[1];

            if (obj1 == null || obj2 == null) return .0;

            var str1 = values[0].ToString();
            var str2 = values[1].ToString();

            var v1 = double.Parse(str1);
            var v2 = double.Parse(str2);

            if (Math.Abs(v2) < 1E-06) return 100.0;

            return Math.Round(v1 / v2 * 100, 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsLastItemInContainerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DependencyObject item = (DependencyObject)value;
            ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);

            return ic.ItemContainerGenerator.IndexFromContainer(item)
                    == ic.Items.Count - 1;
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsFirstItemInContainerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DependencyObject item = (DependencyObject)value;
            ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);

            return ic.ItemContainerGenerator.IndexFromContainer(item)
                    == 0;
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary> 获取ItemsPresenter 中的ItemsPanel </summary>
    public class ItemPanelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ItemsPresenter item = (ItemsPresenter)value;

            var panel = item.GetChildren<Panel>()?.ToList();

            return panel?.FirstOrDefault();
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CircleProgressBarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double v = double.Parse(value.ToString());

            return 182 - v * (182 - 172.5) / 100;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DoubleRoundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            parameter = parameter ?? 0;

            int param = int.Parse(parameter.ToString());

            double v = double.Parse(value.ToString());

            return Math.Round(v, param);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class OpacityProgressBarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            parameter = parameter ?? "0.1-1";
            double param1 = double.Parse(parameter.ToString().Split('-')[0]);
            double param2 = double.Parse(parameter.ToString().Split('-')[1]);

            double v = 100 - double.Parse(value.ToString());

            return param2 - v * (param2 - param1) / 100;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 区间范围筛选
    /// </summary>
    public class MultiComboboxSelectConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return null;

            //  Message：起始杆号可选项删选
            if (values.Length != 2) return null;

            if (values[1] == null) return null;

            int selectIndex = (int)values[0];

            IEnumerable<object> enumerable = values[1] as IEnumerable<object>;

            return enumerable.Skip(selectIndex).ToList();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary> 匹配文本本不可用 </summary>
    public class VisibilityContainWithOutStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Visibility.Collapsed;

            if (parameter == null) return Visibility.Visible;

            IList collection = parameter as IList;

            if (collection.Contains(value.ToString().Trim()))
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception();
        }
    }

    /// <summary> 匹配文本本不可用 </summary>
    public class VisibilityContainWithStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Visibility.Collapsed;

            if (parameter == null) return Visibility.Visible;

            IList collection = parameter as IList;

            if (collection.Contains(value.ToString().Trim()))
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception();
        }
    }


    public class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            var d = double.Parse(value.ToString());

            var sp = TimeSpan.FromMilliseconds((long)d);

            return sp.ToString().Split('.')[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TimeSpanSplitPointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            return value.ToString().Split('.')[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary> 值为空转false </summary>
    [ValueConversion(typeof(string), typeof(bool))]
    public class NullOrEmptyTofalseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.IsNullOrEmpty(value?.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    /// <summary> 值为空转false </summary> 
    public class NullToEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value == null ? string.Empty : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }


    /// <summary> 绑定图标转换 </summary>
    [ValueConversion(typeof(Icon), typeof(ImageSource))]
    public class IconToImageSourceConverter : IValueConverter
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

    /// <summary>
    /// 并运算
    /// </summary>
    public class BoolAndConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return null;

            var result = values.OfType<bool>()?.ToList();

            return result.TrueForAll(l => l == true);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    /// <summary>
    /// 或运算
    /// </summary>
    public class BoolOrConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return null;

            var result = values.OfType<bool>()?.ToList();

            return result.Any(l => l == true);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary> 转换成GB MB KB 显示 </summary> 
    public class ByteSizeDisplayConverter : IValueConverter
    {
        const int GB = 1024 * 1024 * 1024;

        const int MB = 1024 * 1024;

        const int KB = 1024;


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (int.TryParse(value?.ToString(), out int KSize))
            {
                if (KSize / GB >= 1)


                    return (Math.Round(KSize / (float)GB, 2)).ToString() + "GB";
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

    /// <summary> 显示类型的Display名称 </summary>
    public class GetTypeDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var display = value.GetType().GetCustomAttributes(typeof(DescriptionAttribute), false)?.FirstOrDefault() as DescriptionAttribute;

            return display == null ? value.GetType().Name : display.Description;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ToTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            return value.GetType();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FindResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var find= Application.Current.TryFindResource(value);

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


    /// <summary> 设置文本框PropertyChanged 可以输入小数点 </summary>
    public class DoubleTextConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value.ToString();

            if (str.EndsWith(".")) return ".";

            if (str.Contains(".") && str.EndsWith("0")) return ".";

            return value;
        }
    }
    /// <summary> 根据映射转换显示 </summary>
    public class DisplayMapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IEnumerable array = parameter as IEnumerable;

            foreach (var item in array.Cast<DisplayMap>())
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
                var find = ServiceRegistry.Instance.GetService(type);

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
