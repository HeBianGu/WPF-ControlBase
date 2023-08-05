// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Windows;
using System.Windows.Threading;

namespace HeBianGu.Control.Message
{
    public class ContainerMessage : IContainerMessage
    {
        public MessageCloseLayerCommand CloseLayerCommand { get; } = new MessageCloseLayerCommand();

        public void Show(FrameworkElement element, Predicate<System.Windows.Controls.Panel> predicate = null)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
            {
                if (Application.Current.MainWindow is IMainWindow window)
                {
                    window.ShowContainer(element);
                }
            }));
        }

        public void Close()
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
            {
                if (Application.Current.MainWindow is IMainWindow window)
                {
                    window.CloseContainer();
                }
            }));
        }
    }
}
