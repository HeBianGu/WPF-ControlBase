// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Systems.Identity;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddIdentity(this IServiceCollection service)
        {
            service.AddSingleton<IIdentityService, IdentityService>();
            service.AddSingleton<IIdentityInitService, IdentityMultiLoginService>();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddXmlIdentity(this IServiceCollection service)
        {
            service.AddSingleton<IIdentityService, XmlIdentityService>();
            service.AddSingleton<IIdentityInitService, IdentityMultiLoginService>();
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseIdentity(this IApplicationBuilder service, Action<IdentifyConfig> action)
        {
            action?.Invoke(IdentifyConfig.Instance);
        }
    }
}
