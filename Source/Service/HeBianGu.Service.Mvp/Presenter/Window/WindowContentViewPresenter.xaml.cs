// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace HeBianGu.Service.Mvp
{
    public interface IWindowContentViewPresenter : IItemsViewPresenter
    {
        //IPresenter Persenter { get; set; }
    }

    public interface IWindowContentViewPresenterOption : IItemsSettingOption
    {
        //IPresenter Persenter { get; set; }
    }

    [Displayer(Name = "窗口主内容区域", GroupName = SystemSetting.GroupSystem, Icon = Icons.Eye_slash, Description = "显示窗口主内容区域")]
    public class WindowContentViewPresenter : ItemsViewPresenterBase<WindowContentViewPresenter, IWindowContentViewPresenter>, IWindowContentViewPresenter, IWindowContentViewPresenterOption
    {
        //private IPresenter _persenter;
        //[XmlIgnore]
        //[Browsable(false)]
        //public IPresenter Persenter
        //{
        //    get { return _persenter; }
        //    set
        //    {
        //        _persenter = value;
        //        RaisePropertyChanged();
        //    }
        //}


        private bool _useTab;
        [DefaultValue(true)]
        [Display(Name ="使用分页")]
        public bool UseTab
        {
            get { return _useTab; }
            set
            {
                _useTab = value;
                RaisePropertyChanged();
            }
        }


        private bool _useTabSearch;
        [DefaultValue(true)]
        [Display(Name = "使用分页搜索")]
        public bool UseTabSearch
        {
            get { return _useTabSearch; }
            set
            {
                _useTabSearch = value;
                RaisePropertyChanged();
            }
        }



        private bool _useTitle;
        [DefaultValue(false)]
        [Display(Name = "使用标题")]
        public bool UseTitle
        {
            get { return _useTitle; }
            set
            {
                _useTitle = value;
                RaisePropertyChanged();
            }
        }



    }
}
