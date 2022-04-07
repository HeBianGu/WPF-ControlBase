// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Animation;

namespace System
{
    public static class WindowAnimationExtention
    {
        public static void AddWindowAnimation(this IServiceCollection service)
        {
            service.AddSingleton<IWindowAnimationService, WindowAnimationService>();
        }
    }
}
