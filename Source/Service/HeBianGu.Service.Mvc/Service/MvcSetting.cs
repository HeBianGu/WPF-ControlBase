// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;

namespace HeBianGu.Service.Mvc
{
    public class MvcSetting : LazySettingInstance<MvcSetting>
    {
        public string[] ModulePath { get; set; } = { "Module" };
        public bool IsUseModule { get; set; } = true;
        public bool IsUseApp { get; set; } = true;
        public bool UseAutoMap { get; set; } = true;
    }

    public class Mvc : ServiceInstance<IMvcService>
    {

    }
}
