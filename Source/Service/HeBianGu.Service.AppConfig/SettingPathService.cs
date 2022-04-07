// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;

namespace HeBianGu.Service.AppConfig
{
    public class SettingPathService : ISettingPathService
    {
        public string GetSetting()
        {
            return SystemPathSetting.Instance.Setting;
        }
        public string GetConfigExtention()
        {
            return SystemPathSetting.Instance.ConfigExtention;
        }
    }

}
