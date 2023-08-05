using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.About;
using System.Windows.Input;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddAboutViewPresenter(this IServiceCollection service, Action<IAboutViewPresenterOption> action = null)
        {
            //service.AddMoreViewPresenter();
            service.AddSingleton<IAboutViewPresenter, AboutViewPresenter>();
            action?.Invoke(AboutViewPresenter.Instance);
            //MoreViewPresenter.Instance.AddPersenter(AboutViewPresenter.Instance);
            SystemSetting.Instance?.Add(AboutViewPresenter.Instance);
            //AuthorityProxy.Instance?.Add(AboutViewPresenter.Instance);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddAboutViewPresenterOnCaption(this IServiceCollection service, Action<IAboutViewPresenterOption> action = null)
        {
            service.AddWindowCaptionViewPresenter();
            service.AddSingleton<IAboutViewPresenter, AboutViewPresenter>();
            action?.Invoke(AboutViewPresenter.Instance);
            WindowCaptionViewPresenter.Instance.AddPersenter(AboutViewPresenter.Instance);
            SystemSetting.Instance?.Add(AboutViewPresenter.Instance);
        }

        ///// <summary>
        ///// 配置
        ///// </summary>
        ///// <param name="service"></param>
        //public static void UseAbout(this IApplicationBuilder service, Action<AboutSetting> action = null)
        //{
        //    action?.Invoke(AboutSetting.Instance);
        //    SystemSetting.Instance?.Add(AboutSetting.Instance);
        //}
    }
}
