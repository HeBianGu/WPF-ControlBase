using System;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
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