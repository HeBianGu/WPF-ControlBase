// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Guide;
using HeBianGu.Service.Mvp;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddGuideAdorner(this IServiceCollection service)
        {
            service.AddSingleton<IGuideAdornerService, GuideAdornerService>();
        }

        /// <summary>
        /// 设置显示新手向导
        /// </summary>  
        public static IServiceCollection AddGuideViewPresenter(this IServiceCollection services, Action<IGuideViewPresenterOption> option = null)
        {
            //services.AddWindowCaptionViewPresenter();
            services.AddSingleton<IGuideViewPresenter, GuideViewPresenter>();
            option?.Invoke(GuideViewPresenter.Instance);
            SystemSetting.Instance.Add(GuideViewPresenter.Instance);
            //WindowCaptionViewPresenter.Instance.AddPersenter(GuideViewPresenter.Instance);
            return services;
        }
    }
}
