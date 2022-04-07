// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.ThemeSet;
using HeBianGu.Service.Mvp;

namespace System
{
    public static class ThemeSetExtention
    {
        /// <summary>
        /// 用本地保存的配置初始化主题，如果本地没有保存业务则使用默认主题
        /// </summary>
        /// <param name="builder"> 主程序构建对象 </param>
        /// <param name="useDefaultTheme">  默认主题 </param>
        /// <returns></returns>
        public static IApplicationBuilder UseLocalTheme(this IApplicationBuilder builder, Action<ThemeViewModel> useDefaultTheme, int version = 0)
        {
            ThemeConfig local = ThemeConfig.Instance;

            if ((!local.IsInit()) && local.Version == version)
            {
                local.Load();

                //  Do：设置默认主题
                builder.UseTheme(l =>
                {
                    ThemeViewModel.Current.LoadFrom(local);

                    ThemeViewModel.Current.Version = version;
                });

                return builder;
            }

            useDefaultTheme?.Invoke(ThemeViewModel.Current);

            return builder;

        }

        public static void AddTheme(this IServiceCollection service)
        {
            service.AddSingleton<IThemeViewPresenter, ThemeViewPresenter>();
            service.AddSingleton<IThemeSaveService, ThemeSaveService>();
        }

        /// <summary>
        /// 设置主题
        /// </summary>
        /// <param name="builder"> 主程序构建对象 </param>
        /// <param name="theme"> 主题 </param>
        /// <returns></returns>
        public static IApplicationBuilder UseTheme(this IApplicationBuilder builder, Action<ThemeSetting> setting)
        {
            setting?.Invoke(ThemeSetting.Instance);

            SystemSetting.Instance?.Add(ThemeSetting.Instance);

            return builder;
        }
    }
}
