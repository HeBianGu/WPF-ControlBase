// Copyright ? 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows.Threading;

namespace HeBianGu.Control.Message
{
    /// <summary>
    /// Allows an open dialog to be managed. Use is only permitted during a single display operation.
    /// </summary>
    public class DialogSession
    {
        private readonly DialogHost _owner;

        internal DialogSession(DialogHost owner)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));

            _owner = owner;
        }

        /// <summary>
        /// Indicates if the dialog session has ended.  Once ended no further method calls will be permitted.
        /// </summary>
        /// <remarks>
        /// Client code cannot set this directly, this is internally managed.  To end the dicalog session use <see cref="Close()"/>.
        /// </remarks>
        public bool IsEnded { get; internal set; }

        /// <summary>
        /// Gets the <see cref="DialogHost.DialogContent"/> which is currently displayed, so this could be a view model or a UI element.
        /// </summary>
        public object Content => _owner.DialogContent;

        /// <summary>
        /// Update the currrent content in the dialog.
        /// </summary>
        /// <param name="content"></param>
        public void UpdateContent(object content)
        {
            _owner.AssertTargetableContent();
            _owner.DialogContent = content ?? throw new ArgumentNullException(nameof(content));
            _owner.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                _owner.FocusPopup();
            }));
        }

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the dialog session has ended, or a close operation is currently in progress.</exception>
        public void Close()
        {
            if (IsEnded) throw new InvalidOperationException("Dialog session has ended.");

            _owner.Close(null);
        }

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        /// <param name="parameter">Result parameter which will be returned in <see cref="DialogClosingEventArgs.Parameter"/> or from <see cref="DialogHost.Show(object)"/> method.</param>
        /// <exception cref="InvalidOperationException">Thrown if the dialog session has ended, or a close operation is currently in progress.</exception>
        public void Close(object parameter)
        {
            if (IsEnded) throw new InvalidOperationException("Dialog session has ended.");

            _owner.Close(parameter);
        }
    }
}