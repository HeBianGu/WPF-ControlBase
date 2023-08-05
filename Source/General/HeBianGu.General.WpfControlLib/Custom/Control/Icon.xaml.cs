// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
{
    public class Icon : TextBlock
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Dialog), "S.Icon.Default");

        static Icon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Icon), new FrameworkPropertyMetadata(typeof(Icon)));
        }
    }
}
