// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Control.Message;
using HeBianGu.Control.MessageContainer;
using HeBianGu.Control.Panel;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Animation;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Window.Message
{
    [TemplatePart(Name = "PART_SnackBar", Type = typeof(Snackbar))]
    [TemplatePart(Name = "PART_Message", Type = typeof(MessageContainer))]
    [TemplatePart(Name = "PART_LAYERGROUP", Type = typeof(ContainPanel))]
    public partial class MessageWindowBase : MainWindowBase
    {
        public static new ComponentResourceKey DynamicKey => new ComponentResourceKey(typeof(MessageWindowBase), "S.Window.Message.Dynamic");

        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(MessageWindowBase), "S.Window.Message.Default");

        private ContainPanel _layerGroup;
        private MessageContainer _messageContainer;
        private Snackbar _snackbar;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this._layerGroup = Template.FindName("PART_LAYERGROUP", this) as ContainPanel;
            this._messageContainer = Template.FindName("PART_Message", this) as MessageContainer;
            this._snackbar = Template.FindName("PART_SnackBar", this) as Snackbar;
        }

        public override void Show(bool value)
        {
            bool ss = TransitionService.GetIsClose(this);
            TransitionService.SetIsClose(this, value);
        }

        public override void ShowLayer(FrameworkElement element)
        {
            this._layerGroup.Add(element);
        }

        public override void CloseLayer()
        {
            this._layerGroup.Remove();
        }

        public override void ShowWindowNotifyMessage(INotifyMessage message)
        {
            this.Dispatcher.Invoke(() =>
            {
                _messageContainer.Add(message);

            });
        }

        public override void AddSnackMessage(object message)
        {
            SnackbarMessageQueue queue = _snackbar.MessageQueue;

            Task.Run(() => queue.Enqueue(message));
        }

        public override void AddSnackMessage(object message, object actionContent, Action actionHandler)
        {
            SnackbarMessageQueue queue = _snackbar.MessageQueue;

            Task.Run(() => queue.Enqueue(message, actionContent, actionHandler));
        }

        public override void AddSnackMessage<TArgument>(object message, object actionContent, Action<TArgument> actionHandler, TArgument actionArgument)
        {
            SnackbarMessageQueue queue = _snackbar.MessageQueue;

            Task.Run(() => queue.Enqueue(message, actionContent, actionHandler, actionArgument));
        }

        public override void RefreshHide()
        {
            TransitionService.SetIsVisible(this, !TransitionService.GetIsVisible(this));
        }
    }
}
