// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
{
    public class PropertyGridEditMessageCommand : MarkupCommandBase
    {
        public override async void Execute(object parameter)
        {
            await MessageProxy.PropertyGrid.ShowEdit(parameter, null, "编辑", x => x.MinWidth = 600);
        }

        public override bool CanExecute(object parameter)
        {
            return parameter != null;
        }
    }

    public class PropertyGridViewMessageCommand : MarkupCommandBase
    {
        public override async void Execute(object parameter)
        {
            await MessageProxy.PropertyGrid.ShowView(parameter, null, "查看", x => x.MinWidth = 600);
        }

        public override bool CanExecute(object parameter)
        {
            return parameter != null;
        }
    }

    public class CopyTextCommand : MarkupCommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return parameter != null;
        }

        public override void Execute(object parameter)
        {
            Clipboard.SetText(parameter.ToString());
            MessageProxy.Snacker.ShowTime("复制成功");
        }
    }
}