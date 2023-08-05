// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.TypeConverter;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace System
{
    //[Displayer(Name = "启动", GroupName = SystemSetting.GroupSystem)]
    //public class StartSetting : LazySettingInstance<StartSetting>
    //{
    //    public override void LoadDefault()
    //    {
    //        base.LoadDefault();
    //        this._title = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyTitleAttribute>()?.Title;
    //        this._company = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCompanyAttribute>()?.Company;
    //        this._copyright = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;
    //        this._version = Assembly.GetEntryAssembly().GetName().Version.ToString();
    //        this._product = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyProductAttribute>()?.Product;

    //    }
    //    private string _ImagePath;

    //    [DefaultValue(@"/HeBianGu.General.WpfControlLib;component/Resources/logo.png")]
    //    [Display(Name = "图片路径")]
    //    public string ImagePath
    //    {
    //        get { return _ImagePath; }
    //        set
    //        {
    //            _ImagePath = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    private string _title;
    //    [Required]
    //    [Display(Name = "标题")]
    //    public string Title
    //    {
    //        get { return _title; }
    //        set
    //        {
    //            _title = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    private string _product;
    //    [Required]
    //    [Display(Name = "产品")]
    //    public string Product
    //    {
    //        get { return _product; }
    //        set
    //        {
    //            _product = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    private string _version;
    //    [Required]
    //    [Display(Name = "版本")]
    //    public string Version
    //    {
    //        get { return _version; }
    //        set
    //        {
    //            _version = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    private string _company;
    //    [Required]
    //    [Display(Name = "公司信息")]
    //    public string Company
    //    {
    //        get { return _company; }
    //        set
    //        {
    //            _company = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    private string _copyright;
    //    [Required]
    //    [Display(Name = "版权")]
    //    public string Copyright
    //    {
    //        get { return _copyright; }
    //        set
    //        {
    //            _copyright = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    private int _productFontSize = 80;
    //    [DefaultValue(80)]
    //    [Display(Name = "产品名称字体大小")]
    //    [Range(10, 100)]
    //    public int ProductFontSize
    //    {
    //        get { return _productFontSize; }
    //        set
    //        {
    //            _productFontSize = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    private WindowType _type;
    //    [DefaultValue(WindowType.Message)]
    //    [Display(Name = "启动窗口类型")]
    //    public WindowType Type
    //    {
    //        get { return _type; }
    //        set
    //        {
    //            _type = value;
    //            RaisePropertyChanged();
    //        }
    //    }
    //}


}
