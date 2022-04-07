// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Window.Message;
using SystemSetting = HeBianGu.Base.WpfBase.SystemSetting;

namespace System
{
    public static class MessageWindowExtention
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseMainWindowSetting(this IApplicationBuilder service, Action<MainWindowSetting> action = null)
        {
            action?.Invoke(MainWindowSetting.Instance);

            SystemSetting.Instance?.Add(MainWindowSetting.Instance);
        }
    }


}
