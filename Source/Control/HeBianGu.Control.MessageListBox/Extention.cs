// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.MessageListBox;
using HeBianGu.Service.Mvp;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static IServiceCollection AddInfoMessageViewPresenter(this IServiceCollection service, Action<IInfoMessageViewPresenterOption> action = null)
        {
            //service.AddWindowMessageViewPresenter();
            service.AddSingleton<IInfoMessageViewPresenter, InfoMessageViewPresenter>();
            action?.Invoke(InfoMessageViewPresenter.Instance);
            //WindowMessageViewPresenter.Instance.AddPersenter(InfoMessageViewPresenter.Instance);
            SystemSetting.Instance.Add(InfoMessageViewPresenter.Instance);
            return service;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static IServiceCollection AddErrorMessageViewPresenter(this IServiceCollection service, Action<IErrorMessageViewPresenterOption> action = null)
        {
            //service.AddWindowMessageViewPresenter();
            service.AddSingleton<IErrorMessageViewPresenter, ErrorMessageViewPresenter>();
            action?.Invoke(ErrorMessageViewPresenter.Instance);
            //WindowMessageViewPresenter.Instance.AddPersenter(ErrorMessageViewPresenter.Instance);
            SystemSetting.Instance.Add(ErrorMessageViewPresenter.Instance);
            return service;
        }

        ///// <summary>
        ///// 配置
        ///// </summary>
        ///// <param name="service"></param>
        //public static void UsePropertyGrid(this IApplicationBuilder service, Action<Setting> action)
        //{
        //    action?.Invoke(Setting.Instance);
        //}
    }


}
