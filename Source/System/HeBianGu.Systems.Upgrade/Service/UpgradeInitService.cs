// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Reflection;

namespace HeBianGu.Systems.Upgrade
{
    internal class UpgradeInitService : IUpgradeInitService
    {
        public bool Init(VersionData data, out string error)
        {
            error = string.Empty;

            string version = Assembly.GetEntryAssembly().GetName().Version.ToString();

            if (data == null) return false;

            if (data.Version == version) return true;

            UpgradeWindow window = new UpgradeWindow();
            window.TitleMessage = "发现新版本：V" + data.Version;
            window.Message = data.Messages;

            bool? find = window.ShowDialog();

            if (find.HasValue && find.Value)
            {
                DownLoadWindow downLoad = new DownLoadWindow();
                downLoad.TitleMessage = "正在下载新版本：V" + data.Version;
                downLoad.Url = data.Uri;
                downLoad.Message = data.Messages;
                downLoad.ShowDialog();
                return true;
            }


            System.Diagnostics.Debug.WriteLine("说明");


            error = "用户取消下载更新";
            return false;
        }
    }
}