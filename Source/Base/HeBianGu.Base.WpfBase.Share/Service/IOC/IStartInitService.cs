// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace HeBianGu.Base.WpfBase
{
    public interface IStartInitService
    {
        bool Start(params Func<IStartWindow, bool>[] action);
    }

    public interface IStartWindow
    {
        void SetMessage(string message);
    }
}