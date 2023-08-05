// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.AppConfig;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Identity
{

    [Displayer(Name = "登录", GroupName = SystemSetting.GroupSystem)]
    public class LoginSetting : LazySettingInstance<LoginSetting>, ILoginOption
    {
        public override void LoadDefault()
        {
            base.LoadDefault();
            //this._title = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyTitleAttribute>()?.Title;
            this.Company = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCompanyAttribute>()?.Company;
            this.Copyright = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;
            this.Version = "V" + Assembly.GetEntryAssembly().GetName().Version.ToString();
            this.Product = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyProductAttribute>()?.Product;
        }

        protected override string GetDefaultFolder()
        {
            return SystemPathSetting.Instance.Config;
        }
        private string _title;
        [Display(Name = "登录标题")]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        private string _product;
        [Display(Name = "登录标题")]
        public string Product
        {
            get { return _product; }
            set
            {
                _product = value;
                RaisePropertyChanged();
            }
        }

        private string _userName = "HeBianGu";
        [Required]
        [Display(Name = "用户")]
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged();
            }
        }

        private string _password = "123456";
        [Required]
        [Display(Name = "密码")]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        private string _lastUserName = "hebiangu";
        [Required]
        [Display(Name = "上次登录用户")]
        public string LastUserName
        {
            get { return _lastUserName; }
            set
            {
                _lastUserName = value;
                RaisePropertyChanged();
            }
        }

        private string _lastPassword = "123456";
        [Required]
        [Display(Name = "上次登录密码")]
        public string LastPassword
        {
            get { return _lastPassword; }
            set
            {
                _lastPassword = value;
                RaisePropertyChanged();
            }
        }

        private bool _remember = true;
        [Display(Name = "记住密码")]
        public bool Remember
        {
            get { return _remember; }
            set
            {
                _remember = value;
                RaisePropertyChanged();
            }
        }

        private string _passwordRegular;
        [Display(Name = "密码规则")]
        public string PasswordRegular
        {
            get { return _passwordRegular; }
            set
            {
                _passwordRegular = value;
                RaisePropertyChanged();
            }
        }

        private string _logo;
        [DefaultValue("pack://application:,,,/HeBianGu.General.WpfControlLib;component/Resources/logo.png")]
        [Display(Name = "登录图标")]
        public string Logo
        {
            get { return _logo; }
            set
            {
                _logo = value;
                RaisePropertyChanged();
            }
        }

        private string _version;
        [ReadOnly(true)]
        [XmlIgnore]
        [Required]
        [Display(Name = "版本")]
        public string Version
        {
            get { return _version; }
            set
            {
                _version = value;
                RaisePropertyChanged();
            }
        }

        private string _company;
        [Required]
        [Display(Name = "公司信息")]
        public string Company
        {
            get { return _company; }
            set
            {
                _company = value;
                RaisePropertyChanged();
            }
        }

        private string _copyright;
        [Required]
        [Display(Name = "版权")]
        public string Copyright
        {
            get { return _copyright; }
            set
            {
                _copyright = value;
                RaisePropertyChanged();
            }
        }

        private int _productFontSize;
        [DefaultValue(30)]
        [Display(Name = "产品名称字体大小")]
        [Range(10, 100)]
        public int ProductFontSize
        {
            get { return _productFontSize; }
            set
            {
                _productFontSize = value;
                RaisePropertyChanged();
            }
        }
    }
}
