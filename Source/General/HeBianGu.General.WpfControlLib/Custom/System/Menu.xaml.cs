// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class MenuKeys
    {
        public static ComponentResourceKey Dynamic => new ComponentResourceKey(typeof(MenuKeys), "S.Menu.Dynamic");
        public static ComponentResourceKey Window => new ComponentResourceKey(typeof(MenuKeys), "S.Menu.Window");
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(MenuKeys), "S.Menu.Default");
        public static ComponentResourceKey Handy => new ComponentResourceKey(typeof(MenuKeys), "S.Menu.Handy");
        public static ComponentResourceKey Single => new ComponentResourceKey(typeof(MenuKeys), "S.Menu.Single");
        public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(MenuKeys), "S.Menu.Accent");
    }

    public class MenuItemKeys
    {
        //public static ComponentResourceKey Default => new ComponentResourceKey(typeof(MenuItemKeys), "S.MenuItem.Default");
        public static ComponentResourceKey Handy => new ComponentResourceKey(typeof(MenuItemKeys), "S.MenuItem.Handy");
        //public static ComponentResourceKey Single => new ComponentResourceKey(typeof(MenuItemKeys), "S.MenuItem.Single");
        //public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(MenuItemKeys), "S.MenuItem.Accent");
        public static ComponentResourceKey Icon => new ComponentResourceKey(typeof(MenuItemKeys), "S.MenuItem.Icon");

        public static ComponentResourceKey Caption => new ComponentResourceKey(typeof(MenuItemKeys), "S.MenuItem.Caption");

    }


    public class ContextMenuKeys
    {
        public static ComponentResourceKey Dynamic => new ComponentResourceKey(typeof(ContextMenuKeys), "S.ContextMenu.Dynamic");
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(ContextMenuKeys), "S.ContextMenu.Default");
        public static ComponentResourceKey Handy => new ComponentResourceKey(typeof(ContextMenuKeys), "S.ContextMenu.Handy");
        public static ComponentResourceKey Single => new ComponentResourceKey(typeof(ContextMenuKeys), "S.ContextMenu.Single");
        public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(ContextMenuKeys), "S.ContextMenu.Accent");
    }
}
