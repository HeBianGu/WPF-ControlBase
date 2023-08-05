using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HeBianGu.Service.Converter
{
    public class GetSolidColorBrushFromColorConverter : MarkupValueConverterBase
    {
        public double Opacity { get; set; } = 1;
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            if (value is Color color)
            {
                return new SolidColorBrush(color) { Opacity = this.Opacity };
            }
            return this.DefaultValue;

        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush brush = value as SolidColorBrush;
            if (brush == null) return this.DefaultBackValue;
            return brush.Color;
        }
    }

    public class GetColorFromSolidColorBrushConverter : MarkupValueConverterBase
    {
        public double Opacity { get; set; } = 1;
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            if (value is SolidColorBrush brush)
            {
                return brush.Color;
            }
            return this.DefaultValue;

        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush brush = value as SolidColorBrush;
            if (brush == null) return this.DefaultBackValue;
            return brush.Color;
        }
    }
    /// <summary>
    /// 取相反色
    /// </summary>
    public class GetRevertSolidColorBrushConverter : MarkupValueConverterBase
    {
        private Color IdealTextColor(Color bg)
        {
            const int nThreshold = 105;
            int bgDelta = System.Convert.ToInt32((bg.R * 0.299) + (bg.G * 0.587) + (bg.B * 0.114));
            Color foreColor = (255 - bgDelta < nThreshold) ? Colors.Black : Colors.White;
            return foreColor;
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush)
            {
                Color idealForegroundColor = this.IdealTextColor(((SolidColorBrush)value).Color);
                SolidColorBrush foreGroundBrush = new SolidColorBrush(idealForegroundColor);
                foreGroundBrush.Freeze();
                return foreGroundBrush;
            }
            return this.DefaultValue;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush)
            {
                Color idealForegroundColor = this.IdealTextColor(((SolidColorBrush)value).Color);
                SolidColorBrush foreGroundBrush = new SolidColorBrush(idealForegroundColor);
                foreGroundBrush.Freeze();
                return foreGroundBrush;
            }
            return this.DefaultValue;
        }
    }
}
