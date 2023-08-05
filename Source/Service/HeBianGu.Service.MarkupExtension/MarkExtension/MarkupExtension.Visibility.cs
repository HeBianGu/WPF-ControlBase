// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Service.MarkupExtension
{
    public class GetVisibilityExtension : GetValueExtensionBase
    { 
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Enum.TryParse<Visibility>(this.Value, out Visibility result))
            {
                return result;
            }
            return Visibility.Visible;
        }
    } 
}
