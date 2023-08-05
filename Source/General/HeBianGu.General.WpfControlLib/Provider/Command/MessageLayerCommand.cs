// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Windows.Input;
using System.Windows.Markup;

namespace HeBianGu.General.WpfControlLib
{
    public class MessageLayerCloseCommand : MarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            MessageProxy.Container.Close();
        }
    }

    public class MessageLayerCommand : MarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            MessageProxy.Container.Close();
        }
    }
}