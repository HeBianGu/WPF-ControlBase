using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.Notification;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddNotificationViewPresenter(this IServiceCollection service, Action<INotificationViewPresenterOption> action = null)
        {
            //service.AddWindowStatusViewPresenter();
            service.AddSingleton<INotificationViewPresenter, NotificationViewPresenter>();
            action?.Invoke(NotificationViewPresenter.Instance);
            //WindowStatusViewPresenter.Instance.AddPersenter(NotificationViewPresenter.Instance);
            SystemSetting.Instance.Add(NotificationViewPresenter.Instance);
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UsePropertyGrid(this IApplicationBuilder service, Action<Setting> action)
        {
            action?.Invoke(Setting.Instance);
        }
    }


}
