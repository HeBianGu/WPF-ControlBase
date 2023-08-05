// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class ListBoxKeys
    {
        public static ComponentResourceKey Dynamic => new ComponentResourceKey(typeof(ListBoxKeys), "S.ListBox.Dynamic");

        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(ListBoxKeys), "S.ListBox.Default");
        public static ComponentResourceKey Label => new ComponentResourceKey(typeof(ListBoxKeys), "S.ListBox.Label");
        public static ComponentResourceKey Single => new ComponentResourceKey(typeof(ListBoxKeys), "S.ListBox.Single");
        public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(ListBoxKeys), "S.ListBox.Accent");

        public static ComponentResourceKey Clear => new ComponentResourceKey(typeof(ListBoxKeys), "S.ListBox.Clear");

        public static ComponentResourceKey Fluid => new ComponentResourceKey(typeof(ListBoxKeys), "S.ListBox.FluidMoveBehavior");

        public static ComponentResourceKey CheckAll => new ComponentResourceKey(typeof(ListBoxKeys), "S.ListBox.All");

        public static ComponentResourceKey AllowDrag => new ComponentResourceKey(typeof(ListBoxKeys), "S.ListBox.AllowDrag");
        public static ComponentResourceKey AddCloseInner => new ComponentResourceKey(typeof(ListBoxKeys), "S.ListBox.InnerSource.AddClose");
        public static ComponentResourceKey CheckAllInner => new ComponentResourceKey(typeof(ListBoxKeys), "S.ListBox.InnerSource.CheckAll");


        public static ComponentResourceKey CheckBoxWrapPanel => new ComponentResourceKey(typeof(ListBoxKeys), "S.ListBox.WrapPanelSelecter.CheckBox");
        public static ComponentResourceKey WrapPanel => new ComponentResourceKey(typeof(ListBoxKeys), "S.ListBox.WrapPanelSelecter.Default");
    }
}
