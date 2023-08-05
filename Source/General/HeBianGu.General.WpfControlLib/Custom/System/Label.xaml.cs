// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class LabelKeys
    {
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(LabelKeys), "S.Label.Default");
        public static ComponentResourceKey Label => new ComponentResourceKey(typeof(LabelKeys), "S.Label.Label");
        public static ComponentResourceKey IconRotate => new ComponentResourceKey(typeof(LabelKeys), "S.Label.Icon.Rotate");
        public static ComponentResourceKey Icon => new ComponentResourceKey(typeof(LabelKeys), "S.Label.Icon");
        public static ComponentResourceKey Flash => new ComponentResourceKey(typeof(LabelKeys), "S.Label.Flash");
    }
}
