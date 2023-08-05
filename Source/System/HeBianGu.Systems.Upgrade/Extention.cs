// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.Upgrade;

namespace System
{
    public static class UpgradeExtention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddUpgrade(this IServiceCollection service, Action<IUpgradeSettingOption> action = null)
        {
            service.AddSingleton<IUpgradeService, UpgradeService>();
            service.AddSingleton<IUpgradeInitService, UpgradeInitService>();
            action?.Invoke(UpgradeSetting.Instance);
            SystemSetting.Instance.Add(UpgradeSetting.Instance);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddUpgradeViewPresenter(this IServiceCollection service, Action<IUpgradeViewPresenterOption> action = null)
        {
            //service.AddMoreViewPresenter();
            service.AddSingleton<IUpgradeViewPresenter, UpgradeViewPresenter>();
            action?.Invoke(UpgradeViewPresenter.Instance);
            SystemSetting.Instance.Add(UpgradeViewPresenter.Instance);
            //MoreViewPresenter.Instance.AddPersenter(UpgradeViewPresenter.Instance);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddAutoUpgrade(this IServiceCollection service, Action<IUpgradeSettingOption> action = null)
        {
            service.AddSingleton<IUpgradeService, AutoUpdateService>();
            service.AddSingleton<IUpgradeInitService, AutoUpdateInitService>();
            action?.Invoke(UpgradeSetting.Instance);
            SystemSetting.Instance.Add(UpgradeSetting.Instance);

        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        [Obsolete("AddUpgrade")]
        public static void UseUpgrade(this IApplicationBuilder service, Action<UpgradeSetting> action)
        {
            action?.Invoke(UpgradeSetting.Instance);
            SystemSetting.Instance.Add(UpgradeSetting.Instance);
        }
    }
}
