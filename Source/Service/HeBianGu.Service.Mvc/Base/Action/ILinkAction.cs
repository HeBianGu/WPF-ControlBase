// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;

namespace HeBianGu.Service.Mvc
{
    public interface ILinkAction : IAction
    {
        string Action { get; set; }

        string Controller { get; set; }

        object[] Parameter { get; set; }
    }
}