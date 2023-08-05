// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
{
    public class DeleteItemCommand : MarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            FrameworkElement elemnt = parameter as FrameworkElement;
            var items = ItemsControl.ItemsControlFromItemContainer(elemnt);
            if (items == null)
                return;
            if (items.ItemsSource is IList list)
                list.Remove(elemnt.DataContext);
        }

        public override bool CanExecute(object parameter)
        {
            return parameter is FrameworkElement;
        }
    }
}