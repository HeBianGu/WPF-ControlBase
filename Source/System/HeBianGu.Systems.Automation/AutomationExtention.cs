// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.Automation;

namespace System
{
    public static class AutomationExtention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddAutomation(this IServiceCollection service)
        {
            service.AddSingleton<IAutomationPersenter, AutomationPersenter>();
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseProject(this IApplicationBuilder service, Action<AutomationSetting> action)
        {
            action?.Invoke(AutomationSetting.Instance);
        }
    }
}
