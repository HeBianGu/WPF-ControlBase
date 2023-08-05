// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.Base.WpfBase
{
    public interface IContainerMessage
    {
        MessageCloseLayerCommand CloseLayerCommand { get; }
        void Show(FrameworkElement element, Predicate<Panel> predicate = null);
        void Close();
    }

    public class MessageCloseLayerCommand : MarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            MessageProxy.Container.Close();
        }
    }

}
