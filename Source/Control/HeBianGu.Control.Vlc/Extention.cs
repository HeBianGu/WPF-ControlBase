// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Vlc;

namespace System
{
    public static class PropertyGridExtention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddVlc(this IServiceCollection service)
        {
            service.AddSingleton<IService, Service>();
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseVlc(this IApplicationBuilder service, Action<VlcSetting> action)
        {
            action?.Invoke(VlcSetting.Instance);
            SystemSetting.Instance?.Add(VlcSetting.Instance);
        }
    }


}
