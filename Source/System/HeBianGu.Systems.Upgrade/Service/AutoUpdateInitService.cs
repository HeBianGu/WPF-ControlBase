// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Reflection;

namespace HeBianGu.Systems.Upgrade
{
    public class AutoUpdateInitService : IUpgradeInitService
    {
        public string Name => "版本更新";

        //public bool Init(VersionData data, out string error)
        //{
        //    error = string.Empty;
        //    var ass = Assembly.GetEntryAssembly();

        //    AutoUpdater.Synchronous = true;
        //    AutoUpdater.Start(UpgradeSetting.Instance.Uri, ass);
        //    return true;
        //}

        public bool Load(out string message)
        {
            if (UpgradeSetting.Instance.CheckUpdateOnStart == false)
            {
                message = "已跳过程序启动时检查更新";
                return true;
            }

            return this.CheckUpgrade(out message);
        }

        public bool CheckUpgrade(out string message)
        {
            var versionData = Upgrader.Instance.GetVersion(out message);
            if (versionData == null)
                return false;
            else
            {
                var ass = Assembly.GetEntryAssembly();
                AutoUpdater.Synchronous = true;
                AutoUpdater.Start(UpgradeSetting.Instance.Uri, ass);
                return true;
            }
        }
    }
}