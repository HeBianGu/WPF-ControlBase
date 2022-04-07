// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;

namespace HeBianGu.Base.WpfBase
{
    public interface IUpgradeService
    {
        VersionData GetVersion(out string message);
    }

    public class VersionData
    {
        public string Version { get; set; }

        public string Uri { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;

        public List<string> Messages { get; set; } = new List<string>();
    }
}