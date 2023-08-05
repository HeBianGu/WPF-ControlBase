// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class TabControlKeys
    {
        public static ComponentResourceKey Dynamic => new ComponentResourceKey(typeof(TabControlKeys), "S.TabControl.Dynamic");
        public static ComponentResourceKey Icon => new ComponentResourceKey(typeof(TabControlKeys), "S.TabControl.Icon");

        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(TabControlKeys), "S.TabControl.Default");

        public static ComponentResourceKey Office => new ComponentResourceKey(typeof(TabControlKeys), "S.TabControl.Office");
        public static ComponentResourceKey Single => new ComponentResourceKey(typeof(TabControlKeys), "S.TabControl.Single");
        public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(TabControlKeys), "S.TabControl.Accent");
        public static ComponentResourceKey Line => new ComponentResourceKey(typeof(TabControlKeys), "S.TabControl.Line");

    }
}
