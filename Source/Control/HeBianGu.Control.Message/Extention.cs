// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Message;
using HeBianGu.General.WpfControlLib;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>  
        public static IServiceCollection AddMessageProxy(this IServiceCollection services)
        {
            services.AddSingleton<IMessageService, MessageService>();
            services.AddSingleton<ISystemNotifyMessage, SystemNotifyMessage>();
            services.AddSingleton<ITaskBarMessage, TaskBarMessage>();
            services.AddSingleton<ISnackMessage, SnackMessage>();
            services.AddSingleton<IPresenterMessage, PresenterMessage>();
            services.AddSingleton<IContainerMessage, ContainerMessage>();
            return services;
        }

        /// <summary>
        /// 注册
        /// </summary>  
        public static IServiceCollection AddTaskBarMessage(this IServiceCollection services)
        {
            services.AddSingleton<ITaskBarMessage, TaskBarMessage>();
            return services;
        }
        /// <summary>
        /// 注册
        /// </summary>  
        public static IServiceCollection AddSnackMessage(this IServiceCollection services)
        {
            services.AddSingleton<ISnackMessage, SnackMessage>();
            return services;
        }
        /// <summary>
        /// 注册
        /// </summary>  
        public static IServiceCollection AddPresenterMessage(this IServiceCollection services)
        {
            services.AddSingleton<IPresenterMessage, PresenterMessage>();
            return services;
        }

        /// <summary>
        /// 注册
        /// </summary>  
        public static IServiceCollection AddContainerMessage(this IServiceCollection services)
        {
            services.AddSingleton<IContainerMessage, ContainerMessage>();
            return services;
        }
        /// <summary>
        /// 注册
        /// </summary>  
        public static IServiceCollection AddSystemNotifyMessage(this IServiceCollection services)
        {
            services.AddSingleton<ISystemNotifyMessage, SystemNotifyMessage>();
            return services;
        }

        /// <summary>
        /// 设置
        /// </summary>  
        public static IApplicationBuilder UseMessage(this IApplicationBuilder builder, Action<MessageSetting> systemPath = null)
        {
            systemPath?.Invoke(MessageSetting.Instance);

            SystemSetting.Instance?.Add(MessageSetting.Instance);

            return builder;
        }

        /// <summary>
        /// 设置
        /// </summary>  
        public static IApplicationBuilder UseConentDialog(this IApplicationBuilder builder, Action<ConentDialogSetting> systemPath = null)
        {
            systemPath?.Invoke(ConentDialogSetting.Instance);

            SystemSetting.Instance?.Add(ConentDialogSetting.Instance);

            return builder;
        }
    }

}
