// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System.Windows;

namespace HeBianGu.Control.Message
{
    public class SystemNotifyMessage : ISystemNotifyMessage
    {
        /// <summary> 显示气泡消息 </summary>
        public void Show(string message, string title = null, NotifyBalloonIcon tipIcon = NotifyBalloonIcon.Info, int timeout = 1000)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IMainWindow window = Application.Current.MainWindow as IMainWindow;

                if (window != null)
                {
                    window.ShowNotify(title, message, tipIcon, timeout);
                }
            });

        }
    }
}
