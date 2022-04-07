// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.ObjectModel;
using System.Windows;

namespace HeBianGu.Control.Filter
{
    public static class ControlFilterService
    {

        public static ControlFilterCollection GetFilterBoxs(DependencyObject obj)
        {
            return (ControlFilterCollection)obj.GetValue(FilterBoxsProperty);
        }

        public static void SetFilterBoxs(DependencyObject obj, ControlFilterCollection value)
        {
            obj.SetValue(FilterBoxsProperty, value);
        }

        public static readonly DependencyProperty FilterBoxsProperty =
            DependencyProperty.RegisterAttached("FilterBoxs", typeof(ControlFilterCollection), typeof(ControlFilterService), new PropertyMetadata(new ControlFilterCollection(), OnFilterBoxsChanged));

        public static void OnFilterBoxsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;
            ControlFilterCollection n = (ControlFilterCollection)e.NewValue;
            ControlFilterCollection o = (ControlFilterCollection)e.OldValue;
        }
    }

    public class ControlFilterCollection : ObservableCollection<IControlFilter>
    {

    }

    public interface IControlFilter
    {
        bool IsMatch(object obj);

        event RoutedEventHandler Changed;
    }

}
