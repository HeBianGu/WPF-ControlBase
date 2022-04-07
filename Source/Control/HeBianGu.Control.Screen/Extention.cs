// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Screen;

namespace System
{
    public static class ScreenExtention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddScreen(this IServiceCollection service)
        {
            service.AddSingleton<IScreenService, ScreenService>();
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UsePropertyGrid(this IApplicationBuilder service, Action<ScreenSetting> action)
        {
            action?.Invoke(ScreenSetting.Instance);
        }
    }


}
