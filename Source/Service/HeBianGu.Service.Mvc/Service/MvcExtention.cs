// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;

namespace System
{
    public static class MvcExtention
    {
        /// <summary>
        /// 设置
        /// </summary>  
        public static IServiceCollection AddMvc(this IServiceCollection builder, Action<IMvcSettingOption> init = null)
        {
            init?.Invoke(MvcSetting.Instance);
            builder.AddSingleton<IMvcService, MvcService>();
            Mvc.Instance.Init();
            return builder;
        }

        ///// <summary> 配置 </summary>
        //public static IApplicationBuilder UseMvc(this IApplicationBuilder builder, Action<IMvcSettingOption> init = null)
        //{
        //    init?.Invoke(MvcSetting.Instance);
        //    if (Mvc.Instance == null)
        //        throw new Exception("Please Register IMvcService Or AddMvc First");
        //    Mvc.Instance.Init();
        //    return builder;
        //}
    }


}
