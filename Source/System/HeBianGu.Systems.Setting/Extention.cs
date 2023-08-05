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
        public static IServiceCollection AddSetting(this IServiceCollection services, Action<ISystemSettingServiceOpation> option = null)
        {
            services.AddSingleton<ISystemSettingService, SystemSettingService>();
            ServiceRegistry.Instance.GetInstance<ISystemSettingService>();
            option?.Invoke(ServiceRegistry.Instance.GetInstance<ISystemSettingService>());
            return services;
        }

        /// <summary>
        /// 设置系统路径
        /// </summary>  
        public static IServiceCollection AddSettingViewPrenter(this IServiceCollection services, Action<ISettingViewPresenterOption> option = null)
        {
            //services.AddWindowCaptionViewPresenter();
            services.AddSingleton<ISettingViewPresenter, SettingViewPresenter>();
            option?.Invoke(SettingViewPresenter.Instance);
            //WindowCaptionViewPresenter.Instance.AddPersenter(SettingViewPresenter.Instance);
            SystemDisplay.Instance.Settings.Add(SettingViewPresenter.Instance);
            return services;
        }

        /// <summary>
        /// 设置系统路径
        /// </summary>  
        public static IApplicationBuilder UseSetting(this IApplicationBuilder builder, Action<SystemDisplay> systemPath = null)
        {
            systemPath?.Invoke(SystemDisplay.Instance);
            return builder;
        }

        /// <summary>
        /// 设置系统路径
        /// </summary>  
        public static IApplicationBuilder UseSettingDefault(this IApplicationBuilder builder, Action<SystemDisplay> systemPath = null)
        {
            systemPath?.Invoke(SystemDisplay.Instance);
            SystemDisplay.Instance.Settings.Add(LoginSetting.Instance);
            SystemDisplay.Instance.Settings.Add(ViewSetting.Instance);
            SystemDisplay.Instance.Settings.Add(MainSetting.Instance);
            SystemDisplay.Instance.Settings.Add(HotKeySetting.Instance);
            //SystemDisplay.Instance.Settings.Add(UpgradeSetting.Instance);
            SystemDisplay.Instance.Settings.Add(StateSetting.Instance);
            SystemDisplay.Instance.Settings.Add(FileSetting.Instance);
            SystemDisplay.Instance.Settings.Add(NotifySetting.Instance);
            SystemDisplay.Instance.Settings.Add(PasswordSetting.Instance);
            SystemDisplay.Instance.Settings.Add(MessageSetting.Instance);
            SystemDisplay.Instance.Settings.Add(PersonalSetting.Instance);
            return builder;
        }
    }

}
