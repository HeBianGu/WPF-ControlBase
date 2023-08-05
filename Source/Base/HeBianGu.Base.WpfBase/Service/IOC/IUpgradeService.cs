// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Input;

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

    public class Upgrader : ServiceInstance<IUpgradeService>
    {
        public static string UpgradeVersion { get; set; } = Instance?.GetVersion(out string message)?.Version;

        public static bool HasNewVersion { get; set; } = Instance?.GetVersion(out string message) != null;

        public static string CurrentVersion { get; set; } = Assembly.GetEntryAssembly().GetName().Version.ToString();
    }
}