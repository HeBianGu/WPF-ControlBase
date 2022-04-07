// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.Service.Command
{
    public static class MoveCommands
    {
        public static MovePreviousCommand MovePrevious { get; set; } = new MovePreviousCommand();
        public static MoveNextCommand MoveNext { get; set; } = new MoveNextCommand();
        public static MoveTopCommand MoveTop { get; set; } = new MoveTopCommand();
        public static MoveBottomCommand MoveBottom { get; set; } = new MoveBottomCommand();
    }

    public abstract class MoveCommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ListBox listbox)
            {
                object current = listbox.SelectedItem;
                this.Move(listbox.ItemsSource as IList, current);
                listbox.SelectedItem = current;
                return;
            }

            if (parameter is ListBoxItem d)
            {
                object current = d.DataContext;
                ListBox listBox = d.GetParent<ListBox>();
                IList source = listBox.ItemsSource as IList;
                this.Move(source, d.DataContext);
                listBox.SelectedItem = current;
            }
        }

        protected abstract void Move(IList list, object item);
    }

    public class MovePreviousCommand : MoveCommandBase
    {
        protected override void Move(IList list, object item)
        {
            int index = list.IndexOf(item);
            list.RemoveAt(index);
            index = Math.Max(0, index - 1);
            list.Insert(index, item);
        }
    }

    public class MoveNextCommand : MoveCommandBase
    {
        protected override void Move(IList list, object item)
        {
            int index = list.IndexOf(item);
            list.RemoveAt(index);
            index = Math.Min(list.Count, index + 1);
            list.Insert(index, item);
        }
    }

    public class MoveBottomCommand : MoveCommandBase
    {
        protected override void Move(IList list, object item)
        {
            int index = list.IndexOf(item);
            list.RemoveAt(index);
            list.Add(item);
        }
    }

    public class MoveTopCommand : MoveCommandBase
    {
        protected override void Move(IList list, object item)
        {
            int index = list.IndexOf(item);
            list.RemoveAt(index);
            list.Insert(0, item);
        }
    }
}
