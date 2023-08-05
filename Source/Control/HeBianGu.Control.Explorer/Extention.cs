// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Explorer;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddWindowExplorer(this IServiceCollection service)
        {
            service.AddSingleton<IExplorerService, WindowExplorerService>();
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
