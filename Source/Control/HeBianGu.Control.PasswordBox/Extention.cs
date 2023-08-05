// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PasswordBox;

namespace System
{
    public static class PasswordExtention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddPasswordDialog(this IServiceCollection service,Action<IPasswordDialogServiceOption> action=null)
        {
            service.AddSingleton<IPasswordDialogService, PasswordDialogService>();
            action?.Invoke(PasswordDialogService.Instance);
            SystemSetting.Instance.Add(PasswordDialogService.Instance);
        }

        ///// <summary>
        ///// 配置
        ///// </summary>
        ///// <param name="service"></param>
        //public static void UsePasswordDialog(this IApplicationBuilder service, Action<PasswordSetting> action)
        //{
        //    action?.Invoke(PasswordSetting.Instance);
        //    SystemSetting.Instance.Add(PasswordSetting.Instance);
        //}
    }


}
