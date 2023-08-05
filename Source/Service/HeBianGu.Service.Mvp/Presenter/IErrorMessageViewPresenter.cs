// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using HeBianGu.Base.WpfBase;

namespace HeBianGu.Service.Mvp
{
    public interface IErrorMessageViewPresenter : IInvokePresenter
    {
        void AddMessage(Exception ex, string data = null, string title = "错误消息");
        void AddMessage(string data, string title = "错误消息");

    }

    public class ErrorMessageViewPresenterProxy : ServiceInstance<IErrorMessageViewPresenter>
    {

    }
}
