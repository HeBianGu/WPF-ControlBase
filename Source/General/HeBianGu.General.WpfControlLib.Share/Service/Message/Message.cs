// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class Message : ServiceInstance<IMessageService>
    {

    }

    public class MessageDialog : ServiceInstance<IMessageDialog>
    {

    }

    public static class MessageHost
    {
        public static async Task<bool> ShowResultMessge(string message)
        {
            if (Message.Instance == null)
                return MessageHost.ShowDialog(message);

            return await Message.Instance.ShowResultMessge(message);
        }

        public static bool ShowDialog(string messge, string title = null, int closeTime = -1, bool showEffect = true, params Tuple<string, Action>[] acts)
        {
            if (MessageDialog.Instance == null)
                return MessageBox.Show(messge, title, MessageBoxButton.YesNo) == MessageBoxResult.Yes;

            return MessageDialog.Instance.ShowDialog(messge, title, closeTime, showEffect, acts);
        }
    }
}
