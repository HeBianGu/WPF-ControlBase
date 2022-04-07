// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Control.Message
{
    public class SnackbarMessageEventArgs : RoutedEventArgs
    {
        public SnackbarMessageEventArgs(SnackbarMessage message)
        {
            Message = message;
        }

        public SnackbarMessageEventArgs(RoutedEvent routedEvent, SnackbarMessage message) : base(routedEvent)
        {
            Message = message;
        }

        public SnackbarMessageEventArgs(RoutedEvent routedEvent, object source, SnackbarMessage message) : base(routedEvent, source)
        {
            Message = message;
        }

        public SnackbarMessage Message { get; }
    }
}