// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.Setting;

namespace System
{
    public static class SystemSettingExtention
    {

        /// <summary>
        /// 设置系统路径
        /// </summary>  
        public static IServiceCollection AddSetting(this IServiceCollection services, Action<SystemSettingService> systemPath = null)
        {
            services.AddSingleton<ISystemSettingService, SystemSettingService>();

            return services;
        }

        /// <summary>
        /// 设置系统路径
        /// </summary>  
        public static IServiceCollection AddSettingViewPrenter(this IServiceCollection services, Action<SystemSettingService> systemPath = null)
        {
            services.AddSingleton<ISettingViewPresenter, SettingViewPresenter>();

            return services;
        }

        /// <summary>
        /// 设置系统路径
        /// </summary>  
        public static IApplicationBuilder UseSetting(this IApplicationBuilder builder, Action<SystemSettingConfig> systemPath = null)
        {
            systemPath?.Invoke(SystemSettingConfig.Instance);

            return builder;
        }

        /// <summary>
        /// 设置系统路径
        /// </summary>  
        public static IApplicationBuilder UseSettingDefault(this IApplicationBuilder builder, Action<SystemSettingConfig> systemPath = null)
        {
            systemPath?.Invoke(SystemSettingConfig.Instance);

            SystemSettingConfig.Instance.Settings.Add(LoginSetting.Instance);
            SystemSettingConfig.Instance.Settings.Add(ViewSetting.Instance);
            SystemSettingConfig.Instance.Settings.Add(MainSetting.Instance);
            SystemSettingConfig.Instance.Settings.Add(HotKeySetting.Instance);
            SystemSettingConfig.Instance.Settings.Add(UpgradeSetting.Instance);
            SystemSettingConfig.Instance.Settings.Add(StateSetting.Instance);
            SystemSettingConfig.Instance.Settings.Add(FileSetting.Instance);
            SystemSettingConfig.Instance.Settings.Add(NotifySetting.Instance);
            SystemSettingConfig.Instance.Settings.Add(PasswordSetting.Instance);
            SystemSettingConfig.Instance.Settings.Add(MessageSetting.Instance);
            SystemSettingConfig.Instance.Settings.Add(PersonalSetting.Instance);
            return builder;
        }
    }

}
