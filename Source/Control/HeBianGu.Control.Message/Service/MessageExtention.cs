// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Message;
using HeBianGu.General.WpfControlLib;

namespace System
{
    public static class MessageExtention
    {
        /// <summary>
        /// 设置系统路径
        /// </summary>  
        public static IServiceCollection AddMessage(this IServiceCollection services)
        {
            services.AddSingleton<IMessageService, MessageService>();

            return services;
        }

        /// <summary>
        /// 设置系统路径
        /// </summary>  
        public static IApplicationBuilder UseMessage(this IApplicationBuilder builder, Action<MessageSetting> systemPath = null)
        {
            systemPath?.Invoke(MessageSetting.Instance);

            SystemSetting.Instance?.Add(MessageSetting.Instance);

            return builder;
        }

    }

}
