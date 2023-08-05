using HeBianGu.Service.Mvc;
using System;

namespace HeBianGu.App.Menu
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

        ///// <summary> 命令通用方法 </summary>
        //protected override async void RelayMethod(object obj)

        //{
        //    string command = obj?.ToString();

        //    //  Do：对话消息
        //    if (command == "Sumit")
        //    {

        //    }

        //    //  Do：等待消息
        //    else if (command == "Cancel")
        //    {

        //    }
        //}

    }
}
