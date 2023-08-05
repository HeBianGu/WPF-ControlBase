// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Panel;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddPropertyGrid(this IServiceCollection service)
        {
            service.AddSingleton<IService, Service>();
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseContainPanel(this IApplicationBuilder service, Action<ContainPanelSetting> action)
        {
            action?.Invoke(ContainPanelSetting.Instance);
        }
    }


}
