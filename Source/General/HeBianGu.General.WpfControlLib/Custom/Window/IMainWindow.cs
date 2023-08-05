// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
{
    public interface IMainWindow
    {
        void AddSnackMessage(object message);
        void AddSnackMessage(object message, object actionContent, Action actionHandler);
        void AddSnackMessage<TArgument>(object message, object actionContent, Action<TArgument> actionHandler, TArgument actionArgument);
        void ShowContainer(FrameworkElement element, Predicate<Panel> predicate = null);
        void CloseContainer();
        void ShowNotify(string tipTitle, string tipText, NotifyBalloonIcon tipIcon = NotifyBalloonIcon.Info, int timeout = 1000);
        void ShowWindowNotifyMessage(INotifyMessageItem message);
    }
}
