// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class ListBoxItemKeys
    {
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(ListBoxItemKeys), "S.ListBoxItem.Default");
        public static ComponentResourceKey LeftAccent => new ComponentResourceKey(typeof(ListBoxItemKeys), "S.ListBoxItem.LeftAccent");
        public static ComponentResourceKey BottomAccent => new ComponentResourceKey(typeof(ListBoxItemKeys), "S.ListBoxItem.BottomAccent");
        public static ComponentResourceKey WithBorder => new ComponentResourceKey(typeof(ListBoxItemKeys), "S.ListBoxItem.WithBorder");

        public static ComponentResourceKey BorderBrush => new ComponentResourceKey(typeof(ListBoxItemKeys), "S.ListBoxItem.BorderBrush");
        public static ComponentResourceKey Clear => new ComponentResourceKey(typeof(ListBoxItemKeys), "S.ListBoxItem.Clear");
        public static ComponentResourceKey Single => new ComponentResourceKey(typeof(ListBoxItemKeys), "S.ListBoxItem.Single");
        public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(ListBoxItemKeys), "S.ListBoxItem.Accent");
        public static ComponentResourceKey AccentBack => new ComponentResourceKey(typeof(ListBoxItemKeys), "S.ListBoxItem.AccentBack");
        public static ComponentResourceKey Office => new ComponentResourceKey(typeof(ListBoxItemKeys), "S.ListBoxItem.Office");
        public static ComponentResourceKey CheckAll => new ComponentResourceKey(typeof(ListBoxItemKeys), "S.ListBoxItem.All");

        public static ComponentResourceKey CheckBox => new ComponentResourceKey(typeof(ListBoxItemKeys), "S.ListBoxItem.CheckBox.Default");
        public static ComponentResourceKey CheckBoxBox => new ComponentResourceKey(typeof(ListBoxItemKeys), "S.ListBoxItem.CheckBox.Box");
        public static ComponentResourceKey RadioButton => new ComponentResourceKey(typeof(ListBoxItemKeys), "S.ListBoxItem.RadioButton.Default");
    }
}
