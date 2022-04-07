// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.General.WpfControlLib;
using System.Windows;

namespace HeBianGu.Service.Animation
{
    public class WindowAnimationService : IWindowAnimationService
    {
        public void ShowAnimation(Window window)
        {
            TransitionService.SetIsClose(window, true);
        }

        public void CloseAnimation(Window window)
        {
            TransitionService.SetIsClose(window, false);
        }
    }
}
