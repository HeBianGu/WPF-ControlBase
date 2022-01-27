using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace HeBianGu.App.Touch
{
    public class LastLinkActionsToCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ObservableCollection<LinkActionEntity> LinkActions = value as ObservableCollection<LinkActionEntity>;

            LinkActionEntity select = parameter as LinkActionEntity;

            if (LinkActions == null) return Visibility.Collapsed;

            if (parameter == null) return Visibility.Visible;

            return LinkActions.LastOrDefault() == parameter ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return (Visibility)value == Visibility.Collapsed;
        }
    }
}
