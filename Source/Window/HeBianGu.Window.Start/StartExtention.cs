// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Window.Start;

namespace System
{
    public static class StartExtention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddStart(this IServiceCollection service)
        {
            service.AddSingleton<IStartInitService, StartInitService>();
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseStart(this IApplicationBuilder service, Action<StartSetting> action)
        {
            action?.Invoke(StartSetting.Instance);

            SystemSetting.Instance?.Add(StartSetting.Instance);

        }
    }
}
