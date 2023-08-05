// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{

    public class RadioButtonKeys
    {
        public static ComponentResourceKey Dynamic => new ComponentResourceKey(typeof(RadioButtonKeys), "S.RadioButton.Dynamic");

        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(RadioButtonKeys), "S.RadioButton.Default");
        public static ComponentResourceKey Single => new ComponentResourceKey(typeof(RadioButtonKeys), "S.RadioButton.Single");
        public static ComponentResourceKey Clear => new ComponentResourceKey(typeof(RadioButtonKeys), "S.RadioButton.Clear");
        public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(RadioButtonKeys), "S.RadioButton.Accent");

        public static ComponentResourceKey Box => new ComponentResourceKey(typeof(RadioButtonKeys), "S.RadioButton.Box");
        public static ComponentResourceKey BoxSingle => new ComponentResourceKey(typeof(RadioButtonKeys), "S.RadioButton.Box.Single");
        public static ComponentResourceKey BoxAccent => new ComponentResourceKey(typeof(RadioButtonKeys), "S.RadioButton.Box.Accent");
        public static ComponentResourceKey BoxClear => new ComponentResourceKey(typeof(RadioButtonKeys), "S.RadioButton.Box.Clear");

    }
}
