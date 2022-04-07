// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;

namespace System
{
    [SettingConfig(Name = "系统路径", Group = "基本设置")]
    public class SystemPathSetting : LazySettingInstance<SystemPathSetting>
    {
        public string Company { get; set; } = "HeBianGu";

        public string ConfigExtention { get; set; } = ".xml";

        public SystemPathSetting()
        {
            this.CheckFolder(AppPath);
            this.CheckFolder(DefaultPath);
            this.CheckFolder(DefaultConfig);
        }
        #region - 基础目录 -
        public string Document { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public string AppName => Assembly.GetEntryAssembly()?.GetName()?.Name;
        public string AppPath => Path.Combine(this.Document, Company, this.AppName);
        public string DefaultPath => Path.Combine(this.Document, Company, this.AppName, "Default");
        public string DefaultConfig => Path.Combine(this.DefaultPath, "Config");

        #endregion

        #region - 登录用户目录 -

        public string UserPath => Path.Combine(AppPath, this.GetUserName() ?? Environment.UserName);

        private string GetUserName()
        {
            return ServiceRegistry.Instance.GetInstance<IIdentityService>()?.User?.Name;
        }

        public string Setting => Path.Combine(this.UserPath, nameof(Setting));

        public string Log => Path.Combine(this.UserPath, nameof(Log));

        public string License => Path.Combine(this.UserPath, nameof(License));

        public string Project => Path.Combine(this.UserPath, nameof(Project));

        public string Cache => Path.Combine(this.UserPath, nameof(Cache));

        #endregion

        #region - 程序根目录 -

        public string Module => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Module");

        public string Component => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Component");

        public string Version => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Version");

        #endregion

        [Command]
        [Display(Name = "清空缓存")]
        public RelayCommand ClearCacheCommand => new RelayCommand(ClearCache);

        private void ClearCache(object obj)
        {
            try
            {
                Directory.Delete(Cache, true);
                Message.Instance.ShowSnackMessageWithNotice("操作完成");
            }
            catch (Exception ex)
            {
                Message.Instance.ShowSumitMessge("清空失败,详情请查看日志");
                Logger.Instance?.Error(ex);
            }
        }

        [Command]
        [Display(Name = "清空配置")]
        public RelayCommand ClearSettingCommand => new RelayCommand(ClearSetting);

        private void ClearSetting(object obj)
        {
            try
            {
                Directory.Delete(this.Setting, true);
                Message.Instance.ShowSnackMessageWithNotice("操作完成");
            }
            catch (Exception ex)
            {
                Message.Instance.ShowSumitMessge("清空失败,详情请查看日志");
                Logger.Instance?.Error(ex);
            }
        }

        public void CheckFolder(string folder)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
        }
    }

}
