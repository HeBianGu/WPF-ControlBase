using System;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public interface IMainWindow
    {
        /// <summary> 输出消息 </summary>
        void AddSnackMessage(object message);

        /// <summary> 输出消息和操作按钮 </summary>
        void AddSnackMessage(object message, object actionContent, Action actionHandler);

        /// <summary> 输出消息、按钮和参数 </summary>
        void AddSnackMessage<TArgument>(object message, object actionContent, Action<TArgument> actionHandler, TArgument actionArgument);

        void ShowLayer(FrameworkElement element);

        /// <summary> 关闭蒙版 </summary>
        void CloseLayer();

        /// <summary> 显示气泡消息 </summary>
        void ShowNotifyMessage(string tipTitle, string tipText, NotifyBalloonIcon tipIcon = NotifyBalloonIcon.Info, int timeout = 1000);

        /// <summary> 显示气泡消息 </summary>
        void ShowWindowNotifyMessage(INotifyMessage message);

    }

    public interface INotifyMessage
    {
       
    }
}
