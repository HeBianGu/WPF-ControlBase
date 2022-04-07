// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;

namespace System
{
    public static class PropertyGridExtention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddPropertyGrid(this IServiceCollection service)
        {
            service.AddSingleton<IPropertyGridService, PropertyGridService>();
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UsePropertyGrid(this IApplicationBuilder service, Action<PropertyGridSetting> action)
        {
            action?.Invoke(PropertyGridSetting.Instance);
        }
    }


}
