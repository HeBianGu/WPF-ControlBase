// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Linq;

namespace HeBianGu.Service.MarkupExtension
{
    public class SpecialFolderExtension : System.Windows.Markup.MarkupExtension
    {
        public Environment.SpecialFolder SpecialFolder { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Environment.GetFolderPath(this.SpecialFolder);
        }
    }
}
