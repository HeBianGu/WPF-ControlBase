using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;

namespace HeBianGu.App.Tool
{
    [ViewModel("Custom")]
    internal class CustomViewModel : MvcViewModelBase
    {

        protected override void Init()
        {

        }

        protected override void Loaded(string args)
        {

        }

        public RelayCommand RelayCommand => new RelayCommand(RelayMethod);

        /// <summary> 命令通用方法 </summary>
        protected async void RelayMethod(object obj)

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
