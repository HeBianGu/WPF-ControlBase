// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Window.MessageDialog;

namespace System
{
    public static class MessageDialogExtention
    {
        public static void AddMessageDialog(this IServiceCollection service)
        {
            service.AddSingleton<IMessageDialog, MessageDialogService>();
        }
    }
}
