// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class TabItemKeys
    {
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(TabItemKeys), "S.TabItem.Default");
        public static ComponentResourceKey Close => new ComponentResourceKey(typeof(TabItemKeys), "S.TabItem.Close");
        public static ComponentResourceKey Icon => new ComponentResourceKey(typeof(TabItemKeys), "S.TabItem.Icon");

        public static ComponentResourceKey Line => new ComponentResourceKey(typeof(TabItemKeys), "S.TabItem.Line.Default");
        public static ComponentResourceKey Single => new ComponentResourceKey(typeof(TabItemKeys), "S.TabItem.Single");
        public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(TabItemKeys), "S.TabItem.Accent");

        //public static ComponentResourceKey Office => new ComponentResourceKey(typeof(TabItemKeys), "S.TabItem.Office");
    }
}
