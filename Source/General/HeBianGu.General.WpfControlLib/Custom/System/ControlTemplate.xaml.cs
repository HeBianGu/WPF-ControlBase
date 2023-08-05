// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public partial class ControlTemplateKeys
    {
        public static ComponentResourceKey MouseOverOperationCommander => new ComponentResourceKey(typeof(ControlTemplateKeys), "S.ControlTemplate.MouseOver.Operation.Commander");
        public static ComponentResourceKey MouseOverOperationCommand => new ComponentResourceKey(typeof(ControlTemplateKeys), "S.ControlTemplate.MouseOver.Operation.Command");
    }
}
