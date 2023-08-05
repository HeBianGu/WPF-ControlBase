// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class ExpanderKeys
    {
        public static ComponentResourceKey Dynamic => new ComponentResourceKey(typeof(ExpanderKeys), "S.Expander.Dynamic");

        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(ExpanderKeys), "S.Expander.Default");
        public static ComponentResourceKey Single => new ComponentResourceKey(typeof(ExpanderKeys), "S.Expander.Single");
        public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(ExpanderKeys), "S.Expander.Accent");
        public static ComponentResourceKey Clear => new ComponentResourceKey(typeof(ExpanderKeys), "S.Expander.Clear");
        public static ComponentResourceKey Transparent => new ComponentResourceKey(typeof(ExpanderKeys), "S.Expander.Transparent");
    }
}
