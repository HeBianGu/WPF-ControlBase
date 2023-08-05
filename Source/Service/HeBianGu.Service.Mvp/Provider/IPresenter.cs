// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Service.Mvp
{
    public interface IViewPresenter
    {
        string Name { get; set; }
    }

    public abstract class ViewPresenterBase : SettingBase, IViewPresenter
    {
        private bool _isVisible;
        [DefaultValue(true)]
        [Display(Name = "设置显示此功能")]
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                RaisePropertyChanged();
            }
        }

        private bool _isEnabled;
        [DefaultValue(true)]
        [Display(Name = "设置启用此功能")]
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                RaisePropertyChanged();
            }
        }
    }

}
