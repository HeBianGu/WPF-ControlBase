// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;

namespace System
{
    public static class Extention
    {

        /// <summary>
        /// 设置
        /// </summary>  
        public static IServiceCollection AddMvp(this IServiceCollection builder)
        {
            builder.AddSingleton<IPresenterService, PresenterService>();
            return builder;
        }

        /// <summary>
        /// 设置
        /// </summary>  
        public static IServiceCollection AddWindowCaptionViewPresenter(this IServiceCollection builder, Action<IWindowCaptionViewPresenterOption> action = null)
        {
            builder.AddSingleton<IWindowCaptionViewPresenter, WindowCaptionViewPresenter>();
            action?.Invoke(WindowCaptionViewPresenter.Instance);
            return builder;
        }

        /// 设置
        /// </summary>  
        public static IServiceCollection AddHideWindowViewPresenter(this IServiceCollection builder)
        {
            //builder.AddWindowCaptionViewPresenter();
            builder.AddSingleton<IHideWindowViewPresenter, HideWindowViewPresenter>();
            return builder;
        }

        /// <summary>
        /// 设置
        /// </summary>  
        public static IServiceCollection AddMoreViewPresenter(this IServiceCollection builder, Action<IMoreViewPresenterOption> action = null)
        {
            //builder.AddWindowCaptionViewPresenter();
            builder.AddSingleton<IMoreViewPresenter, MoreViewPresenter>();
            //WindowCaptionViewPresenter.Instance.AddPersenter(MoreViewPresenter.Instance);
            action?.Invoke(MoreViewPresenter.Instance);
            SystemSetting.Instance.Add(MoreViewPresenter.Instance);
            return builder;
        }

        /// <summary>
        /// 设置
        /// </summary>  
        public static IServiceCollection AddWindowStatusViewPresenter(this IServiceCollection builder, Action<IWindowStatusViewPresenterOption> action = null)
        {
            builder.AddSingleton<IWindowStatusViewPresenter, WindowStatusViewPresenter>();
            action?.Invoke(WindowStatusViewPresenter.Instance);
            SystemSetting.Instance.Add(WindowStatusViewPresenter.Instance);
            return builder;
        }

        /// <summary>
        /// 设置
        /// </summary>  
        public static IServiceCollection AddWindowToolbarViewPresenter(this IServiceCollection builder, Action<IWindowToolbarViewPresenterOption> action = null)
        {
            builder.AddSingleton<IWindowToolbarViewPresenter, WindowToolbarViewPresenter>();
            action?.Invoke(WindowToolbarViewPresenter.Instance);
            SystemSetting.Instance.Add(WindowToolbarViewPresenter.Instance);
            return builder;
        }

        /// <summary>
        /// 设置
        /// </summary>  
        public static IServiceCollection AddWindowMessageViewPresenter(this IServiceCollection builder, Action<IWindowMessageViewPresenterOption> action = null)
        {
            builder.AddSingleton<IWindowMessageViewPresenter, WindowMessageViewPresenter>();
            action?.Invoke(WindowMessageViewPresenter.Instance);
            SystemSetting.Instance.Add(WindowMessageViewPresenter.Instance);
            return builder;
        }


        /// <summary>
        /// 设置
        /// </summary>  
        public static IServiceCollection AddWindowMessageToolViewPresenter(this IServiceCollection builder, Action<IWindowMessageViewPresenterOption> action = null)
        {
            //builder.AddWindowStatusViewPresenter();
            builder.AddSingleton<IWindowMessageToolViewPresenter, WindowMessageToolViewPresenter>();
            //WindowStatusViewPresenter.Instance.AddPersenter(WindowMessageToolViewPresenter.Instance);
            return builder;
        }

        /// <summary>
        /// 设置
        /// </summary>  
        public static IServiceCollection AddWindowMenuViewPresenter(this IServiceCollection builder, Action<IWindowMenuViewPresenterOption> action = null)
        {
            builder.AddSingleton<IWindowMenuViewPresenter, WindowMenuViewPresenter>();
            action?.Invoke(WindowMenuViewPresenter.Instance);
            SystemSetting.Instance.Add(WindowMenuViewPresenter.Instance);
            return builder;
        }

        /// <summary>
        /// 设置
        /// </summary>  
        public static IServiceCollection AddWindowTreeViewPresenter(this IServiceCollection builder, Action<IWindowTreeViewPresenterOption> action = null)
        {
            builder.AddSingleton<IWindowTreeViewPresenter, WindowTreeViewPresenter>();
            action?.Invoke(WindowTreeViewPresenter.Instance);
            SystemSetting.Instance.Add(WindowTreeViewPresenter.Instance);
            return builder;
        }

        /// <summary>
        /// 设置
        /// </summary>  
        public static IServiceCollection AddHomeViewPresenter(this IServiceCollection builder, Action<IHomeViewPresenterOption> action = null)
        {
            builder.AddSingleton<IHomeViewPresenter, HomeViewPresenter>();
            action?.Invoke(HomeViewPresenter.Instance);
            SystemSetting.Instance.Add(HomeViewPresenter.Instance);
            return builder;
        }

        /// <summary>
        /// 设置
        /// </summary>  
        public static IServiceCollection AddWindowSideMenuPresenter(this IServiceCollection builder, Action<IWindowSideMenuPresenterOption> action = null)
        {
            builder.AddSingleton<IWindowSideMenuPresenter, WindowSideMenuPresenter>();
            action?.Invoke(WindowSideMenuPresenter.Instance);
            SystemSetting.Instance.Add(WindowSideMenuPresenter.Instance);
            return builder;
        }

        /// <summary>
        /// 设置
        /// </summary>  
        public static IServiceCollection AddWindowSideEditViewPresenter(this IServiceCollection builder, Action<IWindowSideEditViewPresenterOption> action = null)
        {
            builder.AddSingleton<IWindowSideEditViewPresenter, WindowSideEditViewPresenter>();
            action?.Invoke(WindowSideEditViewPresenter.Instance);
            SystemSetting.Instance.Add(WindowSideEditViewPresenter.Instance);
            return builder;
        }

        /// <summary>
        /// 设置
        /// </summary>  
        public static IServiceCollection AddWindowContentViewPresenter(this IServiceCollection builder, Action<IWindowContentViewPresenterOption> action = null)
        {
            builder.AddSingleton<IWindowContentViewPresenter, WindowContentViewPresenter>();
            action?.Invoke(WindowContentViewPresenter.Instance);
            SystemSetting.Instance.Add(WindowContentViewPresenter.Instance);
            return builder;
        }

        /// <summary>
        /// 设置
        /// </summary>  
        public static IServiceCollection AddWindowOverViewPresenter(this IServiceCollection builder, Action<IWindowOverViewPresenterOption> action = null)
        {
            builder.AddSingleton<IWindowOverViewPresenter, WindowOverViewPresenter>();
            //WindowSideEditViewPresenter.Instance.AddPersenter(WindowOverViewPresenter.Instance);
            action?.Invoke(WindowOverViewPresenter.Instance);
            SystemSetting.Instance.Add(WindowOverViewPresenter.Instance);
            return builder;
        }

        /// <summary>
        /// 设置
        /// </summary>  
        public static IServiceCollection AddCopyRightViewPresenter(this IServiceCollection builder, Action<ICopyRightViewPresenterOption> action = null)
        {
            //builder.AddWindowStatusViewPresenter();
            builder.AddSingleton<ICopyRightViewPresenter, CopyRightViewPresenter>();
            //WindowStatusViewPresenter.Instance.AddPersenter(CopyRightViewPresenter.Instance);
            action?.Invoke(CopyRightViewPresenter.Instance);
            SystemSetting.Instance.Add(CopyRightViewPresenter.Instance);
            return builder;
        }

        /// <summary>
        /// 设置
        /// </summary>  
        public static IServiceCollection AddAsnycProgressViewPresenter(this IServiceCollection builder, Action<IAsnycProgressViewPresenterOption> action = null)
        {
            builder.AddSingleton<IAsnycProgressViewPresenter, AsnycProgressViewPresenter>();
            action?.Invoke(AsnycProgressViewPresenter.Instance);
            SystemSetting.Instance.Add(AsnycProgressViewPresenter.Instance);
            return builder;
        }

        /// <summary>
        /// 设置
        /// </summary>  
        public static IServiceCollection AddWebsiteViewPresenter(this IServiceCollection builder, Action<IWebsiteViewPresenter> action = null)
        {
            builder.AddSingleton<IWebsiteViewPresenter, WebsiteViewPresenter>();
            action?.Invoke(WebsiteViewPresenter.Instance);
            SystemSetting.Instance.Add(WebsiteViewPresenter.Instance);
            return builder;
        }


        /// <summary>
        /// 配置
        /// </summary>  
        public static IApplicationBuilder UseMvp(this IApplicationBuilder builder, Action<MvpSetting> setting)
        {
            setting?.Invoke(MvpSetting.Instance);
            SystemSetting.Instance?.Add(MvpSetting.Instance);
            return builder;
        }
    }

}
