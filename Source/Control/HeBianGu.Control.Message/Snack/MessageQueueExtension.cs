// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows.Markup;

namespace HeBianGu.Control.Message
{
    /// <summary>
    /// Provides shorthand to initialise a new <see cref="SnackbarMessageQueue"/> for a <see cref="Snackbar"/>.
    /// </summary>
    [MarkupExtensionReturnType(typeof(SnackbarMessageQueue))]
    public class MessageQueueExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new SnackbarMessageQueue();
        }
    }
}