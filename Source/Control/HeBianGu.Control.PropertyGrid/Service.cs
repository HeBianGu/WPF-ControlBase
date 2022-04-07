// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;

namespace HeBianGu.Control.PropertyGrid
{
    public class PropertyGridService : IPropertyGridService
    {

    }

    public interface IPropertyGridService
    {

    }

    [SettingConfig(Name = "参数设置", Group = "基本设置")]
    public class PropertyGridSetting : LazySettingInstance<PropertyGridSetting>
    {
        private bool _useHistory;
        /// <summary> 说明  </summary>
        public bool UseHistory
        {
            get { return _useHistory; }
            set
            {
                _useHistory = value;
                RaisePropertyChanged();
            }
        }
    }
}
