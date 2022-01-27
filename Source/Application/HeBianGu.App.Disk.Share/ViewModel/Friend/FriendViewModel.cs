using HeBianGu.Service.Mvc;
using System;

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

        protected override void Init()
        {
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
