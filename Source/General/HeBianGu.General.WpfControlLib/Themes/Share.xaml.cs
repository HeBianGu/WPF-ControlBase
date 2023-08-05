// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class ControlKeys
    {
        public static ComponentResourceKey Dynamic => new ComponentResourceKey(typeof(ControlKeys), "S.Control.Dynamic");
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(ControlKeys), "S.Control.Default");
        public static ComponentResourceKey ForegroundChecked => new ComponentResourceKey(typeof(ControlKeys), "S.Control.Foreground.Checked");
        public static ComponentResourceKey ForegroundSelected => new ComponentResourceKey(typeof(ControlKeys), "S.Control.Foreground.Selected");
        public static ComponentResourceKey ForegroundDynamic => new ComponentResourceKey(typeof(ControlKeys), "S.Control.Foreground.Dynamic");
        public static ComponentResourceKey ButtonBase => new ComponentResourceKey(typeof(ControlKeys), "S.ButtonBase.Dynamic");
        public static ComponentResourceKey ToggleButton => new ComponentResourceKey(typeof(ControlKeys), "S.ToggleButton.Dynamic");
        public static ComponentResourceKey ProgressBar => new ComponentResourceKey(typeof(ControlKeys), "S.ProgressBar.Dynamic");
        public static ComponentResourceKey Range => new ComponentResourceKey(typeof(ControlKeys), "S.Range.Dynamic");
        public static ComponentResourceKey ScrollViewer => new ComponentResourceKey(typeof(ControlKeys), "S.ScrollViewer.Dynamic");
        public static ComponentResourceKey Window => new ComponentResourceKey(typeof(ControlKeys), "S.Window.Dynamic");
        public static ComponentResourceKey TextBoxBase => new ComponentResourceKey(typeof(ControlKeys), "S.TextBoxBase.Dynamic");
        public static ComponentResourceKey Item => new ComponentResourceKey(typeof(ControlKeys), "S.Item.Dynamic");
    }

    public class SelectorKeys
    {
        public static ComponentResourceKey Dynamic => new ComponentResourceKey(typeof(SelectorKeys), "S.Selector.Dynamic");
    }
}
