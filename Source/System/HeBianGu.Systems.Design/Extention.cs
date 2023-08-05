using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.Design;
using System.Collections.ObjectModel;

namespace System
{
    public static class Extention
    {
        ///// <summary>
        ///// 注册
        ///// </summary>
        ///// <param name="service"></param>
        //public static void AddPropertyGrid(this IServiceCollection service)
        //{
        //    service.AddSingleton<IService, Service>();
        //}

        ///// <summary>
        ///// 配置
        ///// </summary>
        ///// <param name="service"></param>
        //public static void UsePropertyGrid(this IApplicationBuilder service, Action<Setting> action)
        //{
        //    action?.Invoke(Setting.Instance);
        //}

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddDesginViewPresenter(this IServiceCollection service, Action<IDesginViewPresenterOption> action = null)
        {
            service.AddSingleton<IDesginViewPresenter, DesginViewPresenter>();
            action?.Invoke(DesginViewPresenter.Instance);
            SystemSetting.Instance.Add(DesginViewPresenter.Instance);
        }
    }
}
