// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.ObjectModel;

namespace HeBianGu.Systems.Setting
{
    public class SystemSettingConfig : LazyNotifyInstance<SystemSettingConfig>
    {
        private ObservableCollection<ISetting> _settings = new ObservableCollection<ISetting>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ISetting> Settings
        {
            get { return _settings; }
            set
            {
                _settings = value;
                RaisePropertyChanged("Settings");
            }
        }


        private double _titleWidth = double.NaN;
        /// <summary> 说明  </summary>
        public double TitleWidth
        {
            get { return _titleWidth; }
            set
            {
                _titleWidth = value;
                RaisePropertyChanged();
            }
        }

    }
}
