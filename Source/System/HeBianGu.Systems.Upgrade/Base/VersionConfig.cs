// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace HeBianGu.Systems.Upgrade
{
    public class VersionConfig
    {
        public static VersionConfig Instance = new Lazy<VersionConfig>(() => new VersionConfig()).Value;

        public string Uri { get; set; }

        public string SavePath { get; set; } = SystemPathSetting.Instance.Version;

        public string LoadFormat { get; set; } = "正在下载...{0},共计{1}";
    }
}
