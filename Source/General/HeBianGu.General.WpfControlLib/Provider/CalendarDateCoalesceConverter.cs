// Copyright ? 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// Help us format the content of a header button in a calendar.
    /// </summary>
    /// <remarks>
    /// Expected items, in the following order:
    ///     1) DateTime Calendar.DisplayDate
    ///     2) DateTime? Calendar.SelectedDate
    /// </remarks>
    public class CalendarDateCoalesceConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2) throw new ArgumentException("Unexpected", "values");
            if (!(values[0] is DateTime)) throw new ArgumentException("Unexpected", "values");
            if (values[1] != null && !(values[1] is DateTime?)) throw new ArgumentException("Unexpected", "values");

            DateTime? selectedDate = (DateTime?)values[1];
            Button tt;
            return selectedDate ?? values[0];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}