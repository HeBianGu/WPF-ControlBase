// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Threading;

namespace HeBianGu.Systems.XmlSerialize
{
    [Displayer(Name = "元配置数据", GroupName = SystemSetting.GroupData)]
    public class MetaSetting : LazySettingInstance<MetaSetting>
    {
        [DefaultValue(DispatcherPriority.SystemIdle)]
        [Display(Name = "元数据配置保存的时机")]
        public DispatcherPriority DispatcherPriority { get; set; }
    }
}
