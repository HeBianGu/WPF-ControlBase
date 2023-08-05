using HeBianGu.Base.WpfBase;
using HeBianGu.Control.FontEditor;

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
            service.AddSingleton<IService, Service>();
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
