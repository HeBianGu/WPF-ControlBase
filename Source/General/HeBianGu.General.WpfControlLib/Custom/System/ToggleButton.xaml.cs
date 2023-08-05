// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class ToggleButtonKeys
    {
        public static ComponentResourceKey Dynamic => new ComponentResourceKey(typeof(ToggleButtonKeys), "S.ToggleButton.Dynamic");
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(ToggleButtonKeys), "S.ToggleButton.Default");
        public static ComponentResourceKey Single => new ComponentResourceKey(typeof(ToggleButtonKeys), "S.ToggleButton.Single");
        public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(ToggleButtonKeys), "S.ToggleButton.Accent");
        public static ComponentResourceKey Clear => new ComponentResourceKey(typeof(ToggleButtonKeys), "S.ToggleButton.Clear");

        public static ComponentResourceKey BrushFIconChecked => new ComponentResourceKey(typeof(ToggleButtonKeys), "S.ToggleButton.BrushFIconChecked");
        public static ComponentResourceKey BrushFIconCheckedRightToLeft => new ComponentResourceKey(typeof(ToggleButtonKeys), "S.ToggleButton.BrushFIconChecked.RightToLeft");
        public static ComponentResourceKey DoubleFIconChecked => new ComponentResourceKey(typeof(ToggleButtonKeys), "S.ToggleButton.DoubleFIconChecked");
        public static ComponentResourceKey DoubleFIconCheckedSingle => new ComponentResourceKey(typeof(ToggleButtonKeys), "S.ToggleButton.DoubleFIconChecked.Single");
        public static ComponentResourceKey FIconSingle => new ComponentResourceKey(typeof(ToggleButtonKeys), "S.ToggleButton.FIconSingle");
        public static ComponentResourceKey Rotate90 => new ComponentResourceKey(typeof(ToggleButtonKeys), "S.ToggleButton.Rotate.90");
        public static ComponentResourceKey Tree => new ComponentResourceKey(typeof(ToggleButtonKeys), "S.ToggleButton.Tree");
        public static ComponentResourceKey ComboBox => new ComponentResourceKey(typeof(ToggleButtonKeys), "S.ToggleButton.ComboBox");

        public static ComponentResourceKey Content => new ComponentResourceKey(typeof(ToggleButtonKeys), "S.ToggleButton.Content");

    }
}
