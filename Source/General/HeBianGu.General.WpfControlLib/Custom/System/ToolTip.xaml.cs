// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class ToolTipkKeys
    {
        public static ComponentResourceKey Dynamic => new ComponentResourceKey(typeof(ToolTipkKeys), "S.ToolTip.Dynamic");
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(ToolTipkKeys), "S.ToolTip.Default");
        public static ComponentResourceKey Single => new ComponentResourceKey(typeof(ToolTipkKeys), "S.ToolTip.Single");
        public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(ToolTipkKeys), "S.ToolTip.Accent");
    }
}
