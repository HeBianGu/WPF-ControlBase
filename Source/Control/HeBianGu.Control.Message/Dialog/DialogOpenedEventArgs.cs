// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;

namespace HeBianGu.Control.Message
{
    public class DialogClosedEventArgs : RoutedEventArgs
    {
        public DialogClosedEventArgs(DialogSession session, RoutedEvent routedEvent)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));

            Session = session;
            RoutedEvent = routedEvent;
        }

        /// <summary>
        /// Allows interation with the current dialog session.
        /// </summary>
        public DialogSession Session { get; }
    }
}