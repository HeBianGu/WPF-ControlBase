// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 获取ItemsPresenter 中的ItemsPanel </summary>
    public class ItemPanelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ItemsPresenter item = (ItemsPresenter)value;

            System.Collections.Generic.List<Panel> panel = item.GetChildren<Panel>()?.ToList();

            return panel?.FirstOrDefault();
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

            foreach (var current in ic.Items)
            {
                var cItem = ic.ItemContainerGenerator.ContainerFromItem(current) as UIElement;
                if (cItem == item)
                {
                    return true;
                }
                if (cItem.Visibility == Visibility.Visible)
                    return false;
            }

            return false;

            //return ic.ItemContainerGenerator.IndexFromContainer(item) == 0;
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CollpseLastItemInContainerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DependencyObject item = (DependencyObject)value;
            ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);

            bool r = ic.ItemContainerGenerator.IndexFromContainer(item)
                    == ic.Items.Count - 1;

            return r ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
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
            if (ic == null)
                return false;
            return ic.ItemContainerGenerator.IndexFromContainer(item)
                    == ic.Items.Count - 1;
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
