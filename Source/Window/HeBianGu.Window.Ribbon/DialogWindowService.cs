// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using Microsoft.Win32;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HeBianGu.Window.Ribbon
{
    //public class DialogWindowService : IDialogWindowService
    //{
    //    //public bool ShowDialog(string messge, string title = null, int closeTime = -1, bool showEffect = true, params Tuple<string, Action>[] acts)
    //    //{
    //    //    return DialogWindow.ShowDialog(messge, title, closeTime, showEffect, acts);
    //    //}

    //    ///// <summary> 显示窗口 </summary>
    //    //public int ShowDialogWith(string messge, string title = null, bool showEffect = false, params Tuple<string, Action<IDialogWindow>>[] acts)
    //    //{
    //    //    return DialogWindow.ShowDialogWith(messge, title, showEffect, acts);
    //    //}

    //    ///// <summary> 只有确定的按钮 </summary>
    //    //public bool ShowSumit(string messge, string title = null, bool showEffect = false, int closeTime = -1)
    //    //{
    //    //    return DialogWindow.ShowSumit(messge, title, showEffect, closeTime);
    //    //}

    //}

    public static class Commander
    {
        public static ChangedImageCommand ChangedImage { get; set; } = new ChangedImageCommand();

        public static MovePreviousCommand MovePrevious { get; set; } = new MovePreviousCommand();

        public static MoveNextCommand MoveNext { get; set; } = new MoveNextCommand();
    }

    public class ChangedImageCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ControlData data)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory; //设置初始路径
                openFileDialog.Filter = "png文件(*.png)|*.png|jpg文件(*.jpg)|*.jpg|所有文件(*.*)|*.*"; //设置“另存为文件类型”或“文件类型”框中出现的选择内容
                openFileDialog.FilterIndex = 2; //设置默认显示文件类型为Csv文件(*.csv)|*.csv
                openFileDialog.Title = "打开文件"; //获取或设置文件对话框标题
                openFileDialog.RestoreDirectory = true; //设置对话框是否记忆上次打开的目录
                openFileDialog.Multiselect = false;//设置多选
                if (openFileDialog.ShowDialog() != true) return;

                Uri uri = new Uri(openFileDialog.FileName, UriKind.RelativeOrAbsolute);
                //data.LargeImage = uri;
            }
        }
    }

    public class MovePreviousCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is FrameworkElement d)
            {
                IEnumerable source = d.GetParent<ItemsControl>().GetParent<ItemsControl>()?.ItemsSource;

                object item = d.DataContext;

                if (source is IList list)
                {
                    int index = list.IndexOf(item);

                    list.RemoveAt(index);

                    index = Math.Max(0, index - 1);

                    list.Insert(index, item);
                }
            }
        }
    }

    public class MoveNextCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is FrameworkElement d)
            {
                IEnumerable source = d.GetParent<ItemsControl>().GetParent<ItemsControl>()?.ItemsSource;

                object item = d.DataContext;

                if (source is IList list)
                {
                    int index = list.IndexOf(item);

                    list.RemoveAt(index);

                    index = Math.Min(list.Count, index + 1);

                    list.Insert(index, item);
                }
            }

            CommandManager.InvalidateRequerySuggested();
        }
    }

    public static class VisualTreeExtention
    {
        public static T GetParent<T>(this DependencyObject element) where T : DependencyObject
        {
            if (element == null) return null;
            DependencyObject parent = VisualTreeHelper.GetParent(element);
            while ((parent != null) && !(parent is T))
            {
                DependencyObject newVisualParent = VisualTreeHelper.GetParent(parent);
                if (newVisualParent != null)
                {
                    parent = newVisualParent;
                }
                else
                {
                    // try to get the logical parent ( e.g. if in Popup)
                    if (parent is FrameworkElement)
                    {
                        parent = (parent as FrameworkElement).Parent;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return parent as T;
        }
    }

    public static class ExtendRibbonCommands
    {
        public static RoutedUICommand ReorderQuickAccessToolBarCommand { get; set; } = new RoutedUICommand();
    }
}
