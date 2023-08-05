// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace HeBianGu.Service.MarkupExtension
{
    public class GetColorMarkupExtension : InvokeMethodExtensionBase
    {
        public IEnumerable<Color> GetStandardColors()
        {
            return ColorFactory.CreateStandardColors();
        }

        public IEnumerable<Color> GetAvailableColors()
        {
            return ColorFactory.CreateAvailableColors();
        }
    }
}
