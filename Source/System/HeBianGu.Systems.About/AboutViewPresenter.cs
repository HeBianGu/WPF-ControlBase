
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Serialization;

namespace HeBianGu.Systems.About
{
    public interface IAboutViewPresenter : IInvokePresenter
    {

    }

    public interface IAboutViewPresenterOption : IMvpSettingOption
    {
        string Agreement { get; set; }
        string Company { get; set; }
        string Configuration { get; set; }
        string Contact { get; set; }
        string Copyright { get; set; }
        string Culture { get; set; }
        DialogMode DialogMode { get; set; }
        string FileVersion { get; set; }
        string Privacy { get; set; }
        string ProductDescription { get; set; }
        string Title { get; set; }
        string Trademark { get; set; }
        string Version { get; set; }
        string WebSet { get; set; }
    }


    [Displayer(Name = "关于", GroupName = SystemSetting.GroupSystem, Icon = IconAll.QuetionCircle, Description = "这是一个关于页面的信息")]
    public class AboutViewPresenter : ServiceMvpSettingBase<AboutViewPresenter, IAboutViewPresenter>, IAboutViewPresenter, IAboutViewPresenterOption
    {
        public override void LoadDefault()
        {
            base.LoadDefault();
            this._title = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyTitleAttribute>()?.Title;
            this._productDescription = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;
            this._company = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCompanyAttribute>()?.Company;
            this._culture = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCultureAttribute>()?.Culture;
            this._trademark = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyTrademarkAttribute>()?.Trademark;
            this._configuration = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration;
            this._copyright = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;
            this._version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyVersionAttribute>()?.Version;
            this._fileVersion = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;
        }
        #region - 配置 -
        private string _title;
        [XmlIgnore]
        [ReadOnly(true)]
        [Display(Name = "产品名称")]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        private string _productDescription;
        [XmlIgnore]
        [ReadOnly(true)]
        [Display(Name = "产品信息")]
        public string ProductDescription
        {
            get { return _productDescription; }
            set
            {
                _productDescription = value;
                RaisePropertyChanged();
            }
        }

        //private string _authors;
        //[XmlIgnore]
        //[ReadOnly(true)]
        //[Display(Name = "作者")]
        //public string Authors
        //{
        //    get { return _authors; }
        //    set
        //    {
        //        _authors = value;
        //        RaisePropertyChanged();
        //    }
        //}

        private string _company;
        [XmlIgnore]
        [ReadOnly(true)]
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
    
        
        private string _culture;
        [XmlIgnore]
        [ReadOnly(true)]
        [Display(Name = "产品区域")]
        public string Culture
        {
            get { return _culture; }
            set
            {
                _culture = value;
                RaisePropertyChanged();
            }
        }

        private string _trademark;
        [XmlIgnore]
        [ReadOnly(true)]
        [Display(Name = "产品商标")]
        public string Trademark
        {
            get { return _trademark; }
            set
            {
                _trademark = value;
                RaisePropertyChanged();
            }
        }

        private string _configuration;
        [XmlIgnore]
        [ReadOnly(true)]
        [Display(Name = "产品配置")]
        public string Configuration
        {
            get { return _configuration; }
            set
            {
                _configuration = value;
                RaisePropertyChanged();
            }
        }

        private string _privacy;
        [XmlIgnore]
        [ReadOnly(true)]
        [DefaultValue("https://github.com/HeBianGu/WPF-ControlBase")]
        [Display(Name = "隐私政策")]
        public string Privacy
        {
            get { return _privacy; }
            set
            {
                _privacy = value;
                RaisePropertyChanged();
            }
        }

        private string _agreement;
        [XmlIgnore]
        [ReadOnly(true)]
        [DefaultValue("https://github.com/HeBianGu/WPF-ControlBase")]
        [Display(Name = "服务协议")]
        public string Agreement
        {
            get { return _agreement; }
            set
            {
                _agreement = value;
                RaisePropertyChanged();
            }
        }

        private string _copyright;
        [XmlIgnore]
        [ReadOnly(true)]
        [Display(Name = "许可证书")]
        public string Copyright
        {
            get { return _copyright; }
            set
            {
                _copyright = value;
                RaisePropertyChanged();
            }
        }

        private string _version;
        [ReadOnly(true)]
        [XmlIgnore]
        [Display(Name = "产品版本")]
        public string Version
        {
            get { return _version; }
            set
            {
                _version = value;
                RaisePropertyChanged();
            }
        }

        private string _fileVersion;
        [XmlIgnore]
        [ReadOnly(true)]
        [Display(Name = "文件版本")]
        public string FileVersion
        {
            get { return _fileVersion; }
            set
            {
                _fileVersion = value;
                RaisePropertyChanged();
            }
        }

        private string _webSet;
        [XmlIgnore]
        [ReadOnly(true)]
        [DefaultValue("https://github.com/HeBianGu/WPF-ControlBase")]
        [Display(Name = "官方网站")]
        public string WebSet
        {
            get { return _webSet; }
            set
            {
                _webSet = value;
                RaisePropertyChanged();
            }
        }

        private string _contact;
        [XmlIgnore]
        [ReadOnly(true)]
        [DefaultValue("QQ:908293466")]
        [Display(Name = "联系方式")]
        public string Contact
        {
            get { return _contact; }
            set
            {
                _contact = value;
                RaisePropertyChanged();
            }
        }

        private DialogMode _dialogMode;
        [Browsable(false)]
        [DefaultValue(DialogMode.Control)]
        [Display(Name = "显示模式")]
        public DialogMode DialogMode
        {
            get { return _dialogMode; }
            set
            {
                _dialogMode = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public override bool Invoke(out string message)
        {
            message = null;
            if (this.DialogMode == DialogMode.Window)
            {
                MessageProxy.WindowPresenter.ShowSumit(this, this.Name);
            }
            else
            {
                MessageProxy.Presenter.Show(this, null, this.Name, x =>
                 {
                     //x.Width = 600;
                     x.Margin = new System.Windows.Thickness(0);
                     x.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                 });

                //PropertyView.Show(AboutSetting.Instance);
            }
            //AboutWindow aboutWindow = new AboutWindow();
            //aboutWindow.ShowDialog();
            return true;
        }
    }
}
