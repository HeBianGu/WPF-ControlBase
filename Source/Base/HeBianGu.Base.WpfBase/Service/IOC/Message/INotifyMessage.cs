// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    public interface INotifyMessage
    {
        void ShowNotifyDialogMessage(string message, string title = null, int closeTime = -1);
        void ShowErrorSystem(string message);
        void ShowFatalSystem(string message);
        void ShowInfoSystem(string message);
        void ShowSystem(INotifyMessageItem message);
        Task<T> ShowProgressSystem<T>(Func<IPercentProgressMessage, T> action);
        Task<T> ShowStringSystem<T>(Func<INotifyMessageItem, T> action);
        void ShowSuccessSystem(string message);
        Task<T> ShowWaittingSystem<T>(Func<INotifyMessageItem, T> action);
        void ShowWarnSystem(string message);
        void ShowError(string message);
        void ShowFatal(string message);
        void ShowInfo(string message);
        void Show(INotifyMessageItem message);
        Task<T> ShowProgress<T>(Func<IPercentProgressMessage, T> action);
        Task<T> ShowString<T>(Func<INotifyMessageItem, T> action);
        void ShowSuccess(string message);
        Task<T> ShowWaitting<T>(Func<INotifyMessageItem, T> action);
        void ShowWarn(string message);
    }

    public interface INotifyMessageItem
    {
        string Message { get; set; }

        string Time { get; set; }
    }

    public interface IPercentProgressMessage : INotifyMessageItem
    {
        double Value { get; set; }
    }
}
