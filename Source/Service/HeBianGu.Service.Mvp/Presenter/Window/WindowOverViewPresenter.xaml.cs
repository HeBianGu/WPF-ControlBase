// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.ComponentModel;
using System.Xml.Serialization;

namespace HeBianGu.Service.Mvp
{
    public interface IWindowOverViewPresenter : IViewPresenter
    {

    }

    public interface IWindowOverViewPresenterOption : ISettingOption
    {

    }

    public class WindowOverViewPresenter : ServiceMvpSettingBase<WindowOverViewPresenter, IWindowOverViewPresenter>, IWindowOverViewPresenter, IWindowOverViewPresenterOption
    {
        private IViewPresenter _persenter;
        [XmlIgnore]
        [Browsable(false)]
        public IViewPresenter Persenter
        {
            get { return _persenter; }
            set
            {
                _persenter = value;
                RaisePropertyChanged();
            }
        }
    }
}
