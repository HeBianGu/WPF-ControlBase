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
    }
}
