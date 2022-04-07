// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Service.Animation
{
    public interface IStory
    {
        void Start(UIElement element);

        void Stop();
    }
}
