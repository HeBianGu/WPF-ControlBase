// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class CheckBoxKeys
    {
        public static ComponentResourceKey Dynamic => new ComponentResourceKey(typeof(CheckBoxKeys), "S.CheckBox.Dynamic");
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(CheckBoxKeys), "S.CheckBox.Default");
        public static ComponentResourceKey Label => new ComponentResourceKey(typeof(CheckBoxKeys), "S.CheckBox.Label");
        public static ComponentResourceKey Single => new ComponentResourceKey(typeof(CheckBoxKeys), "S.CheckBox.Single");
        public static ComponentResourceKey SingleLabel => new ComponentResourceKey(typeof(CheckBoxKeys), "S.CheckBox.Single.Label");
        public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(CheckBoxKeys), "S.CheckBox.Accent");
        public static ComponentResourceKey AccentLabel => new ComponentResourceKey(typeof(CheckBoxKeys), "S.CheckBox.Accent.Label");
        public static ComponentResourceKey Box => new ComponentResourceKey(typeof(CheckBoxKeys), "S.CheckBox.Box");
        public static ComponentResourceKey BoxSingle => new ComponentResourceKey(typeof(CheckBoxKeys), "S.CheckBox.Box.Single");
        public static ComponentResourceKey BoxAccent => new ComponentResourceKey(typeof(CheckBoxKeys), "S.CheckBox.Box.Accent");
    }
}
