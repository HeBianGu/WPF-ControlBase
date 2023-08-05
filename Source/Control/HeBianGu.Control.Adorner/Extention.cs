// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Adorner;

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

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseAdorner(this IApplicationBuilder service, Action<AdornerSetting> action=null)
        {
            action?.Invoke(AdornerSetting.Instance);
            SystemSetting.Instance?.Add(AdornerSetting.Instance);
        }
    }


}
