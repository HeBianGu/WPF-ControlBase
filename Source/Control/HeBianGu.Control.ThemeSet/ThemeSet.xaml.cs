// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Control.ThemeSet
{
    public partial class ThemeSet : System.Windows.Controls.Control
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ThemeSet), "S.ThemeSet.Default");

        static ThemeSet()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemeSet), new FrameworkPropertyMetadata(typeof(ThemeSet)));
        }
    }
}
