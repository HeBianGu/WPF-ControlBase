using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using System;
using System.Diagnostics;

namespace HeBianGu.Systems.WinTool
{
    public abstract class SpecialFolderViewPresenterBase : InvokePresenterBase
    {
        public override bool Invoke(out string message)
        {
            message = null;
            string path = this.GetSpecialFolderPath();
            Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });

            //Process.Start(path);
            return true;
        }

        protected abstract string GetSpecialFolderPath();
    }


    [Displayer(Name = "我的文档", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开我的文档文件夹")]
    public class MyDocumentsViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
    }

    [Displayer(Name = "应用数据", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开应用数据文件夹")]
    public class ApplicationDataViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }
    }

    [Displayer(Name = "管理工具", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开管理工具文件夹")]
    public class AdminToolsViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.AdminTools);
        }
    }

    [Displayer(Name = "缓存数据", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开混村数据文件夹")]
    public class CookiesViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
        }
    }

    [Displayer(Name = "桌面", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开桌面文件夹")]
    public class DesktopViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }
    }

    [Displayer(Name = "收藏夹", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开收藏夹文件夹")]
    public class FavoritesViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Favorites);
        }
    }

    [Displayer(Name = "字体", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开字体文件夹")]
    public class FontsViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
        }
    }

    [Displayer(Name = "历史数据", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开历史数据文件夹")]
    public class HistoryViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.History);
        }
    }

    [Displayer(Name = "我的音乐", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开我的音乐文件夹")]
    public class MyMusicViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        }
    }

    [Displayer(Name = "我的图片", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开我的图片文件夹")]
    public class MyPicturesViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        }
    }

    [Displayer(Name = "我的视频", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开我的视频文件夹")]
    public class MyVideosViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        }
    }

    [Displayer(Name = "打印机", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开打印机文件夹")]
    public class PrinterShortcutsViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.PrinterShortcuts);
        }
    }

    [Displayer(Name = "Program files", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开【Program files】文件夹")]
    public class ProgramFilesViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        }
    }

    [Displayer(Name = "Program files(x86)", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开【Program files(x86)】文件夹")]
    public class ProgramFilesX86ViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        }
    }

    [Displayer(Name = "用户程序", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开用户程序文件夹")]
    public class ProgramsViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Programs);
        }
    }

    [Displayer(Name = "最近使用", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开最近使用文件夹")]
    public class RecentViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Recent);
        }
    }

    [Displayer(Name = "资源数据", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开资源数据文件夹")]
    public class ResourcesViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Resources);
        }
    }

    [Displayer(Name = "发送目录", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开发送目录文件夹")]
    public class SendToViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.SendTo);
        }
    }

    [Displayer(Name = "开始目录", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开开始目录文件夹")]
    public class StartMenuViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
        }
    }

    [Displayer(Name = "启动程序", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开启动程序文件夹")]
    public class StartupViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        }
    }

    [Displayer(Name = "System", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开【System】文件夹")]
    public class SystemViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.System);
        }
    }

    [Displayer(Name = "System(x86)", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开【System(x86)】文件夹")]
    public class SystemX86ViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.SystemX86);
        }
    }

    [Displayer(Name = "文档模板", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开文档模板文件夹")]
    public class TemplatesViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Templates);
        }
    }

    [Displayer(Name = "用户配置", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开用户配置文件夹")]
    public class UserProfileViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }
    }

    [Displayer(Name = "Windows目录", GroupName = SystemSetting.GroupSystem, Icon = "\xeada", Description = "应用此功能可以打开【Window目录】文件夹")]
    public class WindowsViewPresenter : SpecialFolderViewPresenterBase
    {
        protected override string GetSpecialFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        }
    }
}
