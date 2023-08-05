// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Service.AppConfig
{
    public interface ISettingPathServiceOption
    {
        string Extention { get; set; }
        string Path { get; set; }
    }

    [Displayer(Name = "配置路径", GroupName = SystemSetting.GroupSystem)]
    public class SettingPathService : ServiceSettingInstance<SettingPathService, ISettingPathService>, ISettingPathService, ISettingPathServiceOption
    {
        public string GetSetting()
        {
            return this.Path;
        }
        public string GetConfigExtention()
        {
            return this.Extention;
        }

        private string _path;
        [Display(Name = "配置文件路径")]
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                RaisePropertyChanged();
            }
        }

        private string _extention;
        [Display(Name = "配置文件路径")]
        public string Extention
        {
            get { return _extention; }
            set
            {
                _extention = value;
                RaisePropertyChanged();
            }
        }

        public override void LoadDefault()
        {
            base.LoadDefault();
            this.Path = SystemPathSetting.Instance.Setting;
            this.Extention = SystemPathSetting.Instance.ConfigExtention;
        }
    }
}
