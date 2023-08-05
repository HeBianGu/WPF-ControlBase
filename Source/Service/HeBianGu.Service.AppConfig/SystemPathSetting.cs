// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;

namespace HeBianGu.Service.AppConfig
{
    [Displayer(Name = "路径", GroupName = SystemSetting.GroupSystem)]
    public class SystemPathSetting : LazySettingInstance<SystemPathSetting>
    {
        public string Company { get; set; } = "HeBianGu";

        public string ConfigExtention { get; set; } = ".xml";

        public SystemPathSetting()
        {
            this.CheckFolder(AppPath);
            this.CheckFolder(Default);
            this.CheckFolder(Config);
            this.CheckFolder(Data);
            this.CheckFolder(Setting);
            this.CheckFolder(License);
            this.CheckFolder(Log);
            this.CheckFolder(Project);
            this.CheckFolder(Template);
            this.CheckFolder(Setting);
            this.CheckFolder(Cache);
        }
        #region - 基础目录 -
        public string Document { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public string AppName => Assembly.GetEntryAssembly()?.GetName()?.Name;
        public string AppPath => Path.Combine(this.Document, Company, this.AppName);
        public string Default => Path.Combine(this.Document, Company, this.AppName, nameof(Default));
        public string Config => Path.Combine(this.Default, nameof(Config));
        public string Data => Path.Combine(this.Default, nameof(Data));
        public string RegistryPath => Path.Combine("SOFTWARE", this.AppName);
        public string Setting => Path.Combine(this.Default, nameof(Setting));
        public string License => Path.Combine(this.Default, nameof(License));
        public string Log => Path.Combine(this.Default, nameof(Log));
        public string Project => Path.Combine(this.Default, nameof(Project));
        public string Template => Path.Combine(this.Default, nameof(Template));
        public string Cache => Path.Combine(this.Default, nameof(Cache));
        #endregion

        #region - 登录用户目录 -

        public string UserPath => Path.Combine(AppPath, this.GetUserName() ?? Environment.UserName);

        private string GetUserName()
        {
            return Loginner.Instance?.User?.Account;
        }
        
        public string UserData=> Path.Combine(this.UserPath, nameof(Data));
        public string UserSetting => Path.Combine(this.UserPath, nameof(Setting));
        public string UserProject => Path.Combine(this.UserPath, nameof(Project));
        public string UserTemplate => Path.Combine(this.UserPath, nameof(Template));
        public string UserCache => Path.Combine(this.UserPath, nameof(Cache));
        public string UserLicense => Path.Combine(this.Default, nameof(License));
        public string UserLog => Path.Combine(this.Default, nameof(Log));
        #endregion

        #region - 程序根目录 -

        public string Module => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Module");

        public string Component => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Component");

        public string Version => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Version");

        #endregion

        [Display(Name = "清空缓存")]
        public RelayCommand ClearCacheCommand => new RelayCommand(ClearCache);

        private void ClearCache(object obj)
        {
            try
            {
                Directory.Delete(Cache, true);
                MessageProxy.Snacker.ShowTime("操作完成");
            }
            catch (Exception ex)
            {
                MessageProxy.Messager.ShowSumit("清空失败,详情请查看日志");
                Logger.Instance?.Error(ex);
            }
        }

        [Display(Name = "清空配置")]
        public RelayCommand ClearSettingCommand => new RelayCommand(ClearSetting);

        private void ClearSetting(object obj)
        {
            try
            {
                Directory.Delete(this.Setting, true);
                Directory.Delete(this.UserSetting, true);
                MessageProxy.Snacker.ShowTime("操作完成");
            }
            catch (Exception ex)
            {
                MessageProxy.Messager.ShowSumit("清空失败,详情请查看日志");
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
