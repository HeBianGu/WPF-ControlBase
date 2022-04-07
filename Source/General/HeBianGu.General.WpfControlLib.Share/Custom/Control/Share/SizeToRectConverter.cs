// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HeBianGu.General.WpfControlLib
{
    public class CardClipConverter : IMultiValueConverter
    {
        /// <summary>
        /// 1 - Content presenter render size,
        /// 2 - Clipping border padding (main control padding)
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2 || !(values[0] is Size) || !(values[1] is Thickness))
                return Binding.DoNothing;

            Size size = (Size)values[0];
            Point farPoint = new Point(
                Math.Max(0, size.Width),
                Math.Max(0, size.Height));
            Thickness padding = (Thickness)values[1];
            farPoint.Offset(padding.Left + padding.Right, padding.Top + padding.Bottom);

            return new Rect(
                new Point(),
                new Point(farPoint.X, farPoint.Y));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
