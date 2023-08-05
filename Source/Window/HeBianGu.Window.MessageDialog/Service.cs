// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;

namespace HeBianGu.Window.MessageDialog
{
    public class WindowDialogService : IWindowDialogService
    {
        public bool ShowDialog(string messge, string title = null, int closeTime = -1, bool showEffect = true, params Tuple<string, Action<IDialogWindow>>[] acts)
        {
            return MessageDialogWindow.ShowDialog(messge, title, closeTime, showEffect, acts);
        }

        /// <summary> 显示窗口 </summary>
        public int ShowDialogWith(string messge, string title = null, bool showEffect = false, params Tuple<string, Action<IDialogWindow>>[] acts)
        {
            return MessageDialogWindow.ShowDialogWith(messge, title, showEffect, acts);
        }

        /// <summary> 只有确定的按钮 </summary>
        public bool ShowSumit(string messge, string title = null, bool showEffect = false, int closeTime = -1)
        {
            return MessageDialogWindow.ShowSumit(messge, title, showEffect, closeTime);
        }
    }

    public class ObjectWindowDialogService : IPresenterWindowDialogService
    {
        public bool ShowDialog(object content, string title = null, Action<System.Windows.Window> builder = null, int closeTime = -1, bool showEffect = true, params Tuple<string, Action>[] acts)
        {
            return ObjectDialogWindow.ShowDialog(content, title, builder, closeTime, showEffect, acts);
        }

        public int ShowDialogWith(object content, string title = null, Action<System.Windows.Window> builder = null, bool showEffect = false, params Tuple<string, Action<IDialogWindow>>[] acts)
        {
            return ObjectDialogWindow.ShowDialogWith(content, title, builder, showEffect, acts);
        }

        public bool ShowSumit(object content, string title = null, Action<System.Windows.Window> builder = null, bool showEffect = false, int closeTime = -1)
        {
            return ObjectDialogWindow.ShowSumit(content, title, builder, showEffect, closeTime);
        }

        public bool ShowClearly(object content, string title = null, Action<System.Windows.Window> builder = null, int closeTime = -1, bool showEffect = true)
        {
            return ObjectDialogWindow.ShowDialog(content, title, builder, closeTime, showEffect, new Tuple<string, Action>[0]);
        }
    }
}
