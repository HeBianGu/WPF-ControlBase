using HeBianGu.Service.Mvc;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace HeBianGu.App.Disk
{
    [ViewModel("Friend")]
    internal class FriendViewModel : MvcViewModelBase
    {
        protected override void Init()
        {
            LinkActions.Add(new LinkAction() { Action = "Space", Controller = "Loyout", DisplayName = "会话", Logo = "\xe613" });
            LinkActions.Add(new LinkAction() { Action = "Share", Controller = "Loyout", DisplayName = "好友", Logo = "\xe764" });
            LinkActions.Add(new LinkAction() { Action = "Near", Controller = "Loyout", DisplayName = "群组", Logo = "\xe618" });

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                SelectLink = LinkActions[1];
            }));
        }

        protected override void Loaded(string args)
        {
        }


    }
}
