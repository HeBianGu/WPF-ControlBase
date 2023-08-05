// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;

namespace System
{
    public static class PropertyGridExtention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddPropertyGridMessage(this IServiceCollection service, Action<IPropertyGridMessageOption> action=null)
        {
            service.AddSingleton<IPropertyGridMessage, PropertyGridMessage>();
            action?.Invoke(PropertyGridMessage.Instance);
            //SystemSetting.Instance.Add(PropertyGridMessage.Instance);
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UsePropertyGrid(this IApplicationBuilder service, Action<PropertyGridSetting> action)
        {
            action?.Invoke(PropertyGridSetting.Instance);
        }

        /// <summary>
        /// 设置
        /// </summary>  
        public static IServiceCollection AddPropertyGridViewPresenter(this IServiceCollection builder, Action<IPropertyViewPresenterOption> action = null)
        {
            builder.AddWindowSideEditViewPresenter();
            builder.AddSingleton<IPropertyGridViewPresenter, PropertyGridViewPresenter>();
            WindowSideEditViewPresenter.Instance.AddPersenter(PropertyGridViewPresenter.Instance);
            action?.Invoke(PropertyGridViewPresenter.Instance);
            SystemSetting.Instance.Add(PropertyGridViewPresenter.Instance);
            return builder;
        }

        /// <summary>
        /// 设置
        /// </summary>  
        public static IServiceCollection AddPropertyViewViewPresenter(this IServiceCollection builder, Action<IPropertyViewPresenterOption> action = null)
        {
            builder.AddWindowSideEditViewPresenter();
            builder.AddSingleton<IPropertyViewViewPresenter, PropertyViewViewPresenter>();
            WindowSideEditViewPresenter.Instance.AddPersenter(PropertyViewViewPresenter.Instance);
            action?.Invoke(PropertyViewViewPresenter.Instance);
            SystemSetting.Instance.Add(PropertyViewViewPresenter.Instance);
            return builder;
        }
    }
}
