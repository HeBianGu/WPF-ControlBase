// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;

namespace System
{
    public static class MvpExtention
    {

        /// <summary>
        /// 设置
        /// </summary>  
        public static IServiceCollection AddMvp(this IServiceCollection builder)
        {
            builder.AddSingleton<IPresenterService, PresenterService>();

            return builder;
        }

        /// <summary>
        /// 配置
        /// </summary>  
        public static IApplicationBuilder UseMvp(this IApplicationBuilder builder, Action<MvpSetting> setting)
        {
            setting?.Invoke(MvpSetting.Instance);

            SystemSetting.Instance?.Add(MvpSetting.Instance);

            return builder;
        }
    }

}
