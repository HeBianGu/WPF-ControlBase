// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Systems.Start;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddStart(this IServiceCollection service, Action<IStartInitServiceOption> action = null)
        {
            service.AddSingleton<IStartInitService, StartInitService>();
            action?.Invoke(StartInitService.Instance);
            SystemSetting.Instance?.Add(StartInitService.Instance);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddCustomLoad(this IServiceCollection service)
        {
            service.AddSingleton<IAppLoadService, CustomLoadService>();
        }


        ///// <summary>
        ///// 配置
        ///// </summary>
        ///// <param name="service"></param>
        //[Obsolete]
        //public static void UseStart(this IApplicationBuilder service, Action<StartSetting> action)
        //{
        //    action?.Invoke(StartSetting.Instance);
        //    SystemSetting.Instance?.Add(StartSetting.Instance);
        //}
    }
}
