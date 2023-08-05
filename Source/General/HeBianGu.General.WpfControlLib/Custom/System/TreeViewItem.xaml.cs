// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class TreeViewItemKeys
    {
        public static ComponentResourceKey Material => new ComponentResourceKey(typeof(TreeViewItemKeys), "S.TreeViewItem.Material");
        public static ComponentResourceKey MaterialMenu => new ComponentResourceKey(typeof(TreeViewItemKeys), "S.TreeViewItem.Material.Menu");
        public static ComponentResourceKey Handy => new ComponentResourceKey(typeof(TreeViewItemKeys), "S.TreeViewItem.Handy");
    }
}
