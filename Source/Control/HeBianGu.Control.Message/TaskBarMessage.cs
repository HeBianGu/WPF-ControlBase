// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shell;

namespace HeBianGu.Control.Message
{
    public class TaskBarMessage : ITaskBarMessage
    {
        /// <summary> 调用主窗口任务栏缩略图 </summary>
        public async void Show(Action<TaskbarItemInfo> action)
        {
            TaskbarItemInfo bar = Application.Current.MainWindow.TaskbarItemInfo;
            if (bar == null) return;
            bar.ProgressValue = 1;
            await Task.Run(() => action?.Invoke(bar));
        }

        /// <summary> 调用主窗口任务栏缩略图 显示任务完成百分比</summary>
        public async void ShowNormal(Action<TaskbarItemInfo> action)
        {
            TaskbarItemInfo bar = Application.Current.MainWindow.TaskbarItemInfo;
            if (bar == null) return;
            bar.ProgressState = TaskbarItemProgressState.Normal;
            await Task.Run(() => action?.Invoke(bar));
        }

        /// <summary> 调用主窗口任务栏缩略图 显示任务完成百分比</summary>
        public async Task ShowPercent(Action<TaskbarItemInfo> action)
        {
            TaskbarItemInfo bar = Application.Current.MainWindow.TaskbarItemInfo;

            if (bar == null) return;

            bar.ProgressState = TaskbarItemProgressState.Normal;

            await Task.Run(() =>
            {
                TaskbarItemInfo t = Application.Current.Dispatcher.Invoke(() =>
                {
                    return Application.Current.MainWindow.TaskbarItemInfo;
                });

                action?.Invoke(t);
            });

            //bar.ProgressValue = 0;
            bar.ProgressState = TaskbarItemProgressState.None;
        }

        /// <summary> 调用主窗口任务栏缩略图 显示等待效果 </summary>
        public async Task<bool> ShowWaitting(Func<bool> action)
        {
            TaskbarItemInfo bar = Application.Current.MainWindow.TaskbarItemInfo;

            if (bar == null) return false;
            if (action == null) return false;

            bar.ProgressState = TaskbarItemProgressState.Indeterminate;

            bool result = await Task.Run(() => action.Invoke());

            bar.ProgressValue = 1;

            if (result)
            {
                bar.ProgressState = TaskbarItemProgressState.Normal;
            }
            else
            {
                bar.ProgressState = TaskbarItemProgressState.Error;
            }

            Thread.Sleep(1000);

            bar.ProgressState = TaskbarItemProgressState.None;

            return result;
        }


        /// <summary> 调用主窗口任务栏缩略图 </summary>
        public void ShowImage(ImageSource image)
        {
            TaskbarItemInfo bar = Application.Current.MainWindow.TaskbarItemInfo;

            if (bar == null) return;

            bar.Overlay = image;
        }
    }
}
