// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;

namespace HeBianGu.Service.Mvp
{
    public class PresenterIsEnabledExtension : System.Windows.Markup.MarkupExtension
    {
        public Type Type { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (!MvpSetting.Instance.UseIsEnabled) return true;

            object find = ServiceRegistry.Instance.GetInstance(this.Type);

            return find != null;
        }
    }
}
