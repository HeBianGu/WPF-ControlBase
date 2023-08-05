// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace HeBianGu.Base.WpfBase
{
    public interface ISnackMessage
    {
        void Show(object message);
        void Show(object message, object actionContent, Action actionHandler);
        void Show<TArgument>(string message, object actionContent, Action<TArgument> actionHandler, TArgument actionArgument);
        void ShowTime(object message);
    }
}
