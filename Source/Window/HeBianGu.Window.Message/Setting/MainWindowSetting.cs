// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.Service.Animation;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Window.Message
{

    [SettingConfig(Name = "窗口样式", Group = "页面显示")]
    public class MainWindowSetting : RefreshDefaultSettingInstance<MainWindowSetting>
    {
        private TransitionsHost _transitionHost;
        /// <summary> 说明  </summary>
        [Display(Name = "入场效果")]
        [Property(Type = typeof(TransitionPropertyItem))]
        public TransitionsHost TransitionHost
        {
            get { return _transitionHost; }
            set
            {
                _transitionHost = value;
                RaisePropertyChanged();
            }
        }
    }
}
