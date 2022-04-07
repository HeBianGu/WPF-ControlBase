// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;

namespace HeBianGu.General.WpfControlLib
{
    public static class Extention
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseButton(this IApplicationBuilder service, Action<ButtonSetting> action = null)
        {
            action?.Invoke(ButtonSetting.Instance);

            SystemSetting.Instance?.Add(ButtonSetting.Instance);
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseScrollViewer(this IApplicationBuilder service, Action<ScrollViewerSetting> action = null)
        {
            action?.Invoke(ScrollViewerSetting.Instance);

            SystemSetting.Instance?.Add(ScrollViewerSetting.Instance);
        }
    }
}
