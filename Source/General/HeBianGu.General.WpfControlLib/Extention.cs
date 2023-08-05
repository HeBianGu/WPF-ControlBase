// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;

namespace HeBianGu.General.WpfControlLib
{
    public static partial class Extention
    {
        public static void UseDialog(this IApplicationBuilder service, Action<DialogSetting> action = null)
        {
            action?.Invoke(DialogSetting.Instance);
            SystemSetting.Instance?.Add(DialogSetting.Instance);
        }
    }
}
