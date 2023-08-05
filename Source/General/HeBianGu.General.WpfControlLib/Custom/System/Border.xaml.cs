// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class BorderKeys
    {
        public static ComponentResourceKey Dynamic => new ComponentResourceKey(typeof(BorderKeys), "S.Border.Dynamic");
        public static ComponentResourceKey Binding => new ComponentResourceKey(typeof(BorderKeys), "S.Border.Binding");
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(BorderKeys), "S.Border.Default");
        public static ComponentResourceKey Circle => new ComponentResourceKey(typeof(BorderKeys), "S.Border.Circle");
        public static ComponentResourceKey Single => new ComponentResourceKey(typeof(BorderKeys), "S.Border.Single");
        public static ComponentResourceKey Card => new ComponentResourceKey(typeof(BorderKeys), "S.Border.Card");
        public static ComponentResourceKey CardBorderThickness => new ComponentResourceKey(typeof(BorderKeys), "S.Border.BorderThickness");
        public static ComponentResourceKey CardBorderBrush => new ComponentResourceKey(typeof(BorderKeys), "S.Border.BorderBrush");
        public static ComponentResourceKey CardBackground => new ComponentResourceKey(typeof(BorderKeys), "S.Border.Background");
    }
}
