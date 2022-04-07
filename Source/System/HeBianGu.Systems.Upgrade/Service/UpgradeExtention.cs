// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Systems.Upgrade;

namespace System
{
    public static class UpgradeExtention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddUpgrade(this IServiceCollection service)
        {
            service.AddSingleton<IUpgradeService, UpgradeService>();

            service.AddSingleton<IUpgradeInitService, UpgradeInitService>();
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseUpgrade(this IApplicationBuilder service, Action<VersionConfig> action)
        {
            action?.Invoke(VersionConfig.Instance);
        }
    }
}
