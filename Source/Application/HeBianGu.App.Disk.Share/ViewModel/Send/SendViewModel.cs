using HeBianGu.Service.Mvc;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace HeBianGu.App.Disk
{
    [ViewModel("Send")]
    internal class SendViewModel : MvcViewModelBase
    {
        protected override void Init()
        {
            LinkActions.Add(new LinkAction() { Action = "Down", Controller = "Send", DisplayName = "正在下载", Logo = "\xe6f3" });
            LinkActions.Add(new LinkAction() { Action = "Down", Controller = "Send", DisplayName = "正在上传", Logo = "\xe6fe" });
            LinkActions.Add(new LinkAction() { Action = "Down", Controller = "Send", DisplayName = "传输完成", Logo = "\xe613" });
            LinkActions.Add(new LinkAction() { Action = "Down", Controller = "Send", DisplayName = "文件快传", Logo = "\xe764" });


            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                SelectLink = LinkActions[0];
            }));
        }

        protected override void Loaded(string args)
        {
        }
    }
}
