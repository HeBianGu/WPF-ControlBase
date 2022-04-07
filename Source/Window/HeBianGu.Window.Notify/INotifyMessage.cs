// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.MessageContainer;
using System;
using System.Threading.Tasks;

namespace HeBianGu.Window.Notify
{
    public interface INotifyMessage
    {
        void ShowNotifyDialogMessage(string message, string title = null, int closeTime = -1);
        void ShowSysErrorMessage(string message);
        void ShowSysFatalMessage(string message);
        void ShowSysInfoMessage(string message);
        void ShowSysMessage(MessageBase message);
        Task<T> ShowSysProgressBarMessage<T>(Func<PercentProgressMessage, T> action);
        Task<T> ShowSysProgressStrMessage<T>(Func<StringProgressMessage, T> action);
        void ShowSysSuccessMessage(string message);
        Task<T> ShowSysWaittingMessage<T>(Func<WaittingMessage, T> action);
        void ShowSysWarnMessage(string message);
        void ShowWinErrorMessage(string message);
        void ShowWinFatalMessage(string message);
        void ShowWinInfoMessage(string message);
        void ShowWinMessage(MessageBase message);
        Task<T> ShowWinProgressBarMessage<T>(Func<PercentProgressMessage, T> action);
        Task<T> ShowWinProgressStrMessage<T>(Func<StringProgressMessage, T> action);
        void ShowWinSuccessMessage(string message);
        Task<T> ShowWinWaittingMessage<T>(Func<WaittingMessage, T> action);
        void ShowWinWarnMessage(string message);
    }

    public class Notify : ServiceInstance<INotifyMessage>
    {

    }

    public class NotifySetting : LazySettingInstance<NotifySetting>
    {

    }
}