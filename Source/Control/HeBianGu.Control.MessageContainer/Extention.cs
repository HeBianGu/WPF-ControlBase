// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.MessageContainer;

namespace System
{
    public static class Extention
    {
        ///// <summary>
        ///// 注册
        ///// </summary>
        ///// <param name="service"></param>
        //public static void AddMessageContainer(this IServiceCollection service)
        //{
        //    service.AddSingleton<IMessageContainerService, MessageContainerService>();
        //}

        ///// <summary>
        ///// 注册
        ///// </summary>  
        //public static IServiceCollection AddNotifyMessage(this IServiceCollection services)
        //{
        //    services.AddSingleton<INotifyMessage, NotifyMessage>();
        //    return services;
        //}

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseMessageContainer(this IApplicationBuilder service, Action<MessageContainerSetting> action)
        {
            action?.Invoke(MessageContainerSetting.Instance);
        }
    }


}
