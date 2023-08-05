// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Xml.Serialization;

namespace HeBianGu.Base.WpfBase
{
    public class DisplayRoutedUICommand : RoutedUICommand
    {
        public string Description { get; set; }
        public string Icon { get; set; }
        public string Group { get; set; }
        public int Order { get; set; }
    }
}
