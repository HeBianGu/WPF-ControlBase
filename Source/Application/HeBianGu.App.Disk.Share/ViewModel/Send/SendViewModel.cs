using HeBianGu.Service.Mvc;
using System;
using System.Collections.ObjectModel;

namespace HeBianGu.App.Disk
{
    [ViewModel("Send")]
    internal class SendViewModel : MvcViewModelBase
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
            this.LinkActions.Add(new LinkAction() { Action = "Down", Controller = "Send", DisplayName = "正在下载", Logo = "\xe6f3" });
            this.LinkActions.Add(new LinkAction() { Action = "Down", Controller = "Send", DisplayName = "正在上传", Logo = "\xe6fe" });
            this.LinkActions.Add(new LinkAction() { Action = "Down", Controller = "Send", DisplayName = "传输完成", Logo = "\xe613" });
            this.LinkActions.Add(new LinkAction() { Action = "Down", Controller = "Send", DisplayName = "文件快传", Logo = "\xe764" });

            this.SelectedItem = this.LinkActions[0];
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
