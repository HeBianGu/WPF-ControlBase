// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Window.Notify;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddNotifyMessage(this IServiceCollection service)
        {
            service.AddSingleton<INotifyMessage, NotifyMessage>();
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseIdentity(this IApplicationBuilder service, Action<NotifySetting> action)
        {
            action?.Invoke(NotifySetting.Instance);
        }
    }
}
