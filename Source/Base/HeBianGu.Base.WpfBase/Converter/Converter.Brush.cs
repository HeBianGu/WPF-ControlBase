// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace HeBianGu.Base.WpfBase
{
    public class BrushRoundConverter : IValueConverter
    {
        public System.Windows.Media.Brush HighValue { get; set; } = System.Windows.Media.Brushes.White;

        public System.Windows.Media.Brush LowValue { get; set; } = System.Windows.Media.Brushes.Black;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush solidColorBrush = value as SolidColorBrush;

            if (solidColorBrush == null) return null;

            Color color = solidColorBrush.Color;

            double brightness = 0.3 * color.R + 0.59 * color.G + 0.11 * color.B;

            return brightness < 123 ? LowValue : HighValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
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
            int bgDelta = System.Convert.ToInt32((bg.R * 0.299) + (bg.G * 0.587) + (bg.B * 0.114));
            Color foreColor = (255 - bgDelta < nThreshold) ? Colors.Black : Colors.White;
            return foreColor;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush)
            {
                Color idealForegroundColor = this.IdealTextColor(((SolidColorBrush)value).Color);
                SolidColorBrush foreGroundBrush = new SolidColorBrush(idealForegroundColor);
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
}
