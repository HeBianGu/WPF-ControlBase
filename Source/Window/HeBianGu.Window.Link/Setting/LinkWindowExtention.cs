// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Window.Link;
using SystemSetting = HeBianGu.Base.WpfBase.SystemSetting;

namespace System
{
    public static class LinkWindowExtention
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseLinkGroupsManagerWindowSetting(this IApplicationBuilder service, Action<LinkGroupsManagerWindowSetting> action = null)
        {
            action?.Invoke(LinkGroupsManagerWindowSetting.Instance);

            SystemSetting.Instance?.Add(LinkGroupsManagerWindowSetting.Instance);
        }
    }


}
