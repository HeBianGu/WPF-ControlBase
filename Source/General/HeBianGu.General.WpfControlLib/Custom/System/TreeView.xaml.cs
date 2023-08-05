// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{

    public class TreeViewKeys
    {
        public static ComponentResourceKey MaterialDynamic => new ComponentResourceKey(typeof(TreeViewKeys), "S.TreeView.Material.Dynamic");
        public static ComponentResourceKey Material => new ComponentResourceKey(typeof(TreeViewKeys), "S.TreeView.Material");
        public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(TreeViewKeys), "S.TreeView.Material.Accent");
        public static ComponentResourceKey Single => new ComponentResourceKey(typeof(TreeViewKeys), "S.TreeView.Material.Single");
        public static ComponentResourceKey MaterialMenu => new ComponentResourceKey(typeof(TreeViewKeys), "S.TreeView.Material.Menu");

        public static ComponentResourceKey ComboBox => new ComponentResourceKey(typeof(TreeViewKeys), "S.TreeView.ComboBox");


        public static ComponentResourceKey HandyDynamic => new ComponentResourceKey(typeof(TreeViewKeys), "S.TreeView.Handy.Dynamic");
        public static ComponentResourceKey Handy => new ComponentResourceKey(typeof(TreeViewKeys), "S.TreeView.Handy");
        public static ComponentResourceKey HandyAccent => new ComponentResourceKey(typeof(TreeViewKeys), "S.TreeView.Handy.Accent");
        public static ComponentResourceKey HandySingle => new ComponentResourceKey(typeof(TreeViewKeys), "S.TreeView.Handy.Single");
    }
}
