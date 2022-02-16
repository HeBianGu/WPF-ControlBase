using HeBianGu.Service.Mvc;
using System;
using System.Collections.ObjectModel;

namespace HeBianGu.App.Disk
{
    [ViewModel("Friend")]
    internal class FriendViewModel : MvcViewModelBase
    {
        private LinkAction _selectedItem;
        /// <summary> 说明  </summary>
        public LinkAction SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<LinkAction> _linkAction = new ObservableCollection<LinkAction>();
        /// <summary> 说明  </summary>
        public ObservableCollection<LinkAction> LinkActions
        {
            get { return _linkAction; }
            set
            {
                _linkAction = value;
                RaisePropertyChanged("LinkActions");
            }
        }

        protected override void Init()
        {
            this.LinkActions.Add(new LinkAction() { Action = "Space", Controller = "Loyout", DisplayName = "会话", Logo = "\xe613" });
            this.LinkActions.Add(new LinkAction() { Action = "Share", Controller = "Loyout", DisplayName = "好友", Logo = "\xe764" });
            this.LinkActions.Add(new LinkAction() { Action = "Near", Controller = "Loyout", DisplayName = "群组", Logo = "\xe618" });

            this.SelectedItem = this.LinkActions[1];
        }

        protected override void Loaded(string args)
        {
        }

        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            //  Do：对话消息
            if (command == "Sumit")
            {

            }

            //  Do：等待消息
            else if (command == "Cancel")
            {

            }
        }

    }
}
