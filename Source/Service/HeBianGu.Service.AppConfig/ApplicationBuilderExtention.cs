// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.AppConfig;

namespace System
{
    public static class ApplicationBuilderExtention
    {

        /// <summary>
        /// 设置
        /// </summary>  
        public static IServiceCollection AddSettingPath(this IServiceCollection builder)
        {
            builder.AddSingleton<ISettingPathService, SettingPathService>();

            return builder;
        }

        /// <summary>
        /// 配置
        /// </summary>  
        public static IApplicationBuilder UsePath(this IApplicationBuilder builder, Action<SystemPathSetting> systemPath)
        {
            systemPath?.Invoke(SystemPathSetting.Instance);

            SystemSetting.Instance?.Add(SystemPathSetting.Instance);

            return builder;
        }
    }

}
