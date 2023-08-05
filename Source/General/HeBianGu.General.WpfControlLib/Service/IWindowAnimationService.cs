// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public interface IWindowAnimationService
    {
        void CloseAnimation(Window window);
        void ShowAnimation(Window window);
    }
}