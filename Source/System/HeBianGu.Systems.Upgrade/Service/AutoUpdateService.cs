// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Linq;
using System.Reflection;

namespace HeBianGu.Systems.Upgrade
{
    public class AutoUpdateService : IUpgradeService
    {
        public VersionData GetVersion(out string message)
        {
            message = null;
            UpdateInfoEventArgs args = XmlWebSerializer.Instance.Load<UpdateInfoEventArgs>(UpgradeSetting.Instance.Uri, out message);

            if (args == null)
            {
                message = "没有获取到更新数据";
                return null;
            }

            bool isUpdate = new Version(args.CurrentVersion) > Assembly.GetEntryAssembly().GetName().Version;
            if (!isUpdate)
            {
                message = "当前已经是最新版本";
                return null;
            }
            return new VersionData() { Version = args.CurrentVersion.ToString(), Messages = args.ChangelogURL?.Split(' ').ToList(), Uri = args.DownloadURL };

        }
    }
}
