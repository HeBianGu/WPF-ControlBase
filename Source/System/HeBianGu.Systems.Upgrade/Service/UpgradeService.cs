// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HeBianGu.Systems.Upgrade
{
    public class UpgradeService : IUpgradeService
    {
        public VersionData GetVersion(out string message)
        {
            message = "操作超时";

            List<string> messages = new List<string>();
            messages.Add("1、增加了检验更新和版本下载");
            messages.Add("2、增加了Mvc跳转页面方案");
            messages.Add("3、修改了一些已知BUG");

            //var find = window.ShowDialog();

            //if (find.HasValue && find.Value)
            //{
            //    DownLoadWindow downLoad = new DownLoadWindow();
            //    downLoad.TitleMessage = "正在下载新版本：V3.0.1";
            //    downLoad.Url = @"http://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4";
            //    downLoad.Message = message;
            //    downLoad.ShowDialog();
            //}

            return new VersionData() { Version = "1.0.0", Messages = messages, Uri = @"http://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4" };
        }
    }
}
