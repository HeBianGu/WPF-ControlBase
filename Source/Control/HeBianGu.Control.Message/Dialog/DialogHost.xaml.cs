﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace HeBianGu.Control.Message
{
    /// <summary>
    /// Defines how a data context is sourced for a dialog if a <see cref="FrameworkElement"/>
    /// is passed as the command parameter when using <see cref="DialogHost.OpenDialogCommand"/>.
    /// </summary>
    public enum DialogHostOpenDialogCommandDataContextSource
    {
        /// <summary>
        /// The data context from the sender element (typically a <see cref="Button"/>) 
        /// is applied to the content.
        /// </summary>
        SenderElement,
        /// <summary>
        /// The data context from the <see cref="DialogHost"/> is applied to the content.
        /// </summary>
        DialogHostInstance,
        /// <summary>
        /// The data context is explicitly set to <c>null</c>.
        /// </summary>
        None
    }

    [TemplatePart(Name = PopupPartName, Type = typeof(Popup))]
    [TemplatePart(Name = PopupPartName, Type = typeof(ContentControl))]
    [TemplatePart(Name = ContentCoverGridName, Type = typeof(Grid))]
    [TemplateVisualState(GroupName = "PopupStates", Name = OpenStateName)]
    [TemplateVisualState(GroupName = "PopupStates", Name = ClosedStateName)]
    public class DialogHost : ContentControl
    {
        public const string PopupPartName = "PART_Popup";
        public const string PopupContentPartName = "PART_PopupContentElement";
        public const string ContentCoverGridName = "PART_ContentCoverGrid";
        public const string OpenStateName = "Open";
        public const string ClosedStateName = "Closed";

        /// <summary>
        /// Routed command to be used somewhere inside an instance to trigger showing of the dialog. Content can be passed to the dialog via a <see cref="Button.CommandParameter"/>.
        /// </summary>
        public static RoutedCommand OpenDialogCommand = new RoutedCommand();
        /// <summary>
        /// Routed command to be used inside dialog content to close a dialog. Use a <see cref="Button.CommandParameter"/> to indicate the result of the parameter.
        /// </summary>
        public static RoutedCommand CloseDialogCommand = new RoutedCommand();

        private static readonly HashSet<DialogHost> LoadedInstances = new HashSet<DialogHost>();

        private readonly ManualResetEvent _asyncShowWaitHandle = new ManualResetEvent(false);
        private DialogOpenedEventHandler _asyncShowOpenedEventHandler;
        private DialogClosingEventHandler _asyncShowClosingEventHandler;

        private Popup _popup;
        private ContentControl _popupContentControl;
        private Grid _contentCoverGrid;
        private DialogSession _session;
        private DialogOpenedEventHandler _attachedDialogOpenedEventHandler;
        private DialogClosingEventHandler _attachedDialogClosingEventHandler;
        private object _closeDialogExecutionParameter;
        private IInputElement _restoreFocusDialogClose;
        private IInputElement _restoreFocusWindowReactivation;
        private Action _currentSnackbarMessageQueueUnPauseAction = null;
        private Action _closeCleanUp = () => { };

        static DialogHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogHost), new FrameworkPropertyMetadata(typeof(DialogHost)));
        }

        #region .Show overloads

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static async Task<object> Show(object content)
        {
            return await Show(content, null, null);
        }

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>        
        /// <param name="openedEventHandler">Allows access to opened event which would otherwise have been subscribed to on a instance.</param>        
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static async Task<object> Show(object content, DialogOpenedEventHandler openedEventHandler)
        {
            return await Show(content, null, openedEventHandler, null);
        }

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>
        /// <param name="closingEventHandler">Allows access to closing event which would otherwise have been subscribed to on a instance.</param>
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static async Task<object> Show(object content, DialogClosingEventHandler closingEventHandler)
        {
            return await Show(content, null, null, closingEventHandler);
        }

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>        
        /// <param name="openedEventHandler">Allows access to opened event which would otherwise have been subscribed to on a instance.</param>
        /// <param name="closingEventHandler">Allows access to closing event which would otherwise have been subscribed to on a instance.</param>
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static async Task<object> Show(object content, DialogOpenedEventHandler openedEventHandler, DialogClosingEventHandler closingEventHandler)
        {
            return await Show(content, null, openedEventHandler, closingEventHandler);
        }

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>
        /// <param name="dialogIdentifier"><see cref="Identifier"/> of the instance where the dialog should be shown. Typically this will match an identifer set in XAML. <c>null</c> is allowed.</param>
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static async Task<object> Show(object content, object dialogIdentifier)
        {
            return await Show(content, dialogIdentifier, null, null);
        }

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>
        /// <param name="dialogIdentifier"><see cref="Identifier"/> of the instance where the dialog should be shown. Typically this will match an identifer set in XAML. <c>null</c> is allowed.</param>
        /// <param name="openedEventHandler">Allows access to opened event which would otherwise have been subscribed to on a instance.</param>
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static Task<object> ShowWithOpen(object content, object dialogIdentifier, DialogOpenedEventHandler openedEventHandler)
        {
            return Show(content, dialogIdentifier, openedEventHandler, null);
        }

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>
        /// <param name="dialogIdentifier"><see cref="Identifier"/> of the instance where the dialog should be shown. Typically this will match an identifer set in XAML. <c>null</c> is allowed.</param>        
        /// <param name="closingEventHandler">Allows access to closing event which would otherwise have been subscribed to on a instance.</param>
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static Task<object> ShowWithClose(object content, object dialogIdentifier, DialogClosingEventHandler closingEventHandler)
        {
            return Show(content, dialogIdentifier, null, closingEventHandler);
        }

        /// <summary>
        /// Shows a modal dialog. To use, a <see cref="DialogHost"/> instance must be in a visual tree (typically this may be specified towards the root of a Window's XAML).
        /// </summary>
        /// <param name="content">Content to show (can be a control or view model).</param>
        /// <param name="dialogIdentifier"><see cref="Identifier"/> of the instance where the dialog should be shown. Typically this will match an identifer set in XAML. <c>null</c> is allowed.</param>
        /// <param name="openedEventHandler">Allows access to opened event which would otherwise have been subscribed to on a instance.</param>
        /// <param name="closingEventHandler">Allows access to closing event which would otherwise have been subscribed to on a instance.</param>
        /// <returns>Task result is the parameter used to close the dialog, typically what is passed to the <see cref="CloseDialogCommand"/> command.</returns>
        public static async Task<object> Show(object content, object dialogIdentifier, DialogOpenedEventHandler openedEventHandler, DialogClosingEventHandler closingEventHandler)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            if (LoadedInstances.Count == 0)
                throw new InvalidOperationException("No loaded DialogHost instances.");
            LoadedInstances.First().Dispatcher.VerifyAccess();

            List<DialogHost> targets = LoadedInstances.Where(dh => dialogIdentifier == null || Equals(dh.Identifier, dialogIdentifier)).ToList();
            if (targets.Count == 0)
                throw new InvalidOperationException("No loaded DialogHost have an Identifier property matching dialogIndetifier argument.");
            if (targets.Count > 1)
                throw new InvalidOperationException("Multiple viable DialogHosts.  Specify a unique Identifier on each DialogHost, especially where multiple Windows are a concern.");

            return await targets[0].ShowInternal(content, openedEventHandler, closingEventHandler);
        }

        internal async Task<object> ShowInternal(object content, DialogOpenedEventHandler openedEventHandler, DialogClosingEventHandler closingEventHandler)
        {
            if (IsOpen)
                throw new InvalidOperationException("DialogHost is already open.");

            AssertTargetableContent();
            DialogContent = content;
            _asyncShowOpenedEventHandler = openedEventHandler;
            _asyncShowClosingEventHandler = closingEventHandler;
            SetCurrentValue(IsOpenProperty, true);

            Task task = new Task(() =>
            {
                _asyncShowWaitHandle.WaitOne();
            });
            task.Start();

            await task;

            _asyncShowOpenedEventHandler = null;
            _asyncShowClosingEventHandler = null;

            return _closeDialogExecutionParameter;
        }

        #endregion

        public DialogHost()
        {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;

            CommandBindings.Add(new CommandBinding(CloseDialogCommand, CloseDialogHandler, CloseDialogCanExecute));
            CommandBindings.Add(new CommandBinding(OpenDialogCommand, OpenDialogHandler));
        }

        public static readonly DependencyProperty IdentifierProperty = DependencyProperty.Register(
            nameof(Identifier), typeof(object), typeof(DialogHost), new PropertyMetadata(default(object)));

        /// <summary>
        /// Identifier which is used in conjunction with <see cref="Show(object)"/> to determine where a dialog should be shown.
        /// </summary>
        public object Identifier
        {
            get { return GetValue(IdentifierProperty); }
            set { SetValue(IdentifierProperty, value); }
        }

        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
            nameof(IsOpen), typeof(bool), typeof(DialogHost), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsOpenPropertyChangedCallback));

        private static void IsOpenPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            DialogHost dialogHost = (DialogHost)dependencyObject;

            if (dialogHost._popupContentControl != null)
                ValidationAssist.SetSuppress(dialogHost._popupContentControl, !dialogHost.IsOpen);
            VisualStateManager.GoToState(dialogHost, dialogHost.SelectState(), !TransitionAssist.GetDisableTransitions(dialogHost));

            if (dialogHost.IsOpen)
            {
                WatchWindowActivation(dialogHost);
                dialogHost._currentSnackbarMessageQueueUnPauseAction = dialogHost.SnackbarMessageQueue?.Pause();
            }
            else
            {
                dialogHost._asyncShowWaitHandle.Set();
                dialogHost._attachedDialogClosingEventHandler = null;
                if (dialogHost._currentSnackbarMessageQueueUnPauseAction != null)
                {
                    dialogHost._currentSnackbarMessageQueueUnPauseAction();
                    dialogHost._currentSnackbarMessageQueueUnPauseAction = null;
                }
                dialogHost._session.IsEnded = true;
                dialogHost._session = null;
                dialogHost._closeCleanUp();

                // Don't attempt to Invoke if _restoreFocusDialogClose hasn't been assigned yet. Can occur
                // if the MainWindow has started up minimized. Even when Show() has been called, this doesn't
                // seem to have been set.
                dialogHost.Dispatcher.InvokeAsync(() => dialogHost._restoreFocusDialogClose?.Focus(), DispatcherPriority.Input);

                return;
            }

            dialogHost._asyncShowWaitHandle.Reset();
            dialogHost._session = new DialogSession(dialogHost);
            Window window = Window.GetWindow(dialogHost);
            dialogHost._restoreFocusDialogClose = window != null ? FocusManager.GetFocusedElement(window) : null;

            //multiple ways of calling back that the dialog has opened:
            // * routed event
            // * the attached property (which should be applied to the button which opened the dialog
            // * straight forward dependency property 
            // * handler provided to the async show method
            DialogClosedEventArgs dialogOpenedEventArgs = new DialogClosedEventArgs(dialogHost._session, DialogOpenedEvent);
            dialogHost.OnDialogOpened(dialogOpenedEventArgs);
            dialogHost._attachedDialogOpenedEventHandler?.Invoke(dialogHost, dialogOpenedEventArgs);
            dialogHost.DialogOpenedCallback?.Invoke(dialogHost, dialogOpenedEventArgs);
            dialogHost._asyncShowOpenedEventHandler?.Invoke(dialogHost, dialogOpenedEventArgs);


            dialogHost.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                CommandManager.InvalidateRequerySuggested();
                UIElement child = dialogHost.FocusPopup();

                if (child != null)
                {
                    //https://github.com/ButchersBoy/MaterialDesignInXamlToolkit/issues/187
                    //totally not happy about this, but on immediate validation we can get some weird looking stuff...give WPF a kick to refresh...
                    Task.Delay(300).ContinueWith(t => child.Dispatcher.BeginInvoke(new Action(() => child.InvalidateVisual())));
                }
            }));
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly DependencyProperty DialogContentProperty = DependencyProperty.Register(
            nameof(DialogContent), typeof(object), typeof(DialogHost), new PropertyMetadata(default(object)));

        public object DialogContent
        {
            get { return GetValue(DialogContentProperty); }
            set { SetValue(DialogContentProperty, value); }
        }

        public static readonly DependencyProperty DialogContentTemplateProperty = DependencyProperty.Register(
            nameof(DialogContentTemplate), typeof(DataTemplate), typeof(DialogHost), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate DialogContentTemplate
        {
            get { return (DataTemplate)GetValue(DialogContentTemplateProperty); }
            set { SetValue(DialogContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty DialogContentTemplateSelectorProperty = DependencyProperty.Register(
            nameof(DialogContentTemplateSelector), typeof(DataTemplateSelector), typeof(DialogHost), new PropertyMetadata(default(DataTemplateSelector)));

        public DataTemplateSelector DialogContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(DialogContentTemplateSelectorProperty); }
            set { SetValue(DialogContentTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty DialogContentStringFormatProperty = DependencyProperty.Register(
            nameof(DialogContentStringFormat), typeof(string), typeof(DialogHost), new PropertyMetadata(default(string)));

        public string DialogContentStringFormat
        {
            get { return (string)GetValue(DialogContentStringFormatProperty); }
            set { SetValue(DialogContentStringFormatProperty, value); }
        }

        public static readonly DependencyProperty DialogMarginProperty = DependencyProperty.Register(
            "DialogMargin", typeof(Thickness), typeof(DialogHost), new PropertyMetadata(default(Thickness)));

        public Thickness DialogMargin
        {
            get { return (Thickness)GetValue(DialogMarginProperty); }
            set { SetValue(DialogMarginProperty, value); }
        }

        public static readonly DependencyProperty OpenDialogCommandDataContextSourceProperty = DependencyProperty.Register(
            nameof(OpenDialogCommandDataContextSource), typeof(DialogHostOpenDialogCommandDataContextSource), typeof(DialogHost), new PropertyMetadata(default(DialogHostOpenDialogCommandDataContextSource)));

        /// <summary>
        /// Defines how a data context is sourced for a dialog if a <see cref="FrameworkElement"/>
        /// is passed as the command parameter when using <see cref="DialogHost.OpenDialogCommand"/>.
        /// </summary>
        public DialogHostOpenDialogCommandDataContextSource OpenDialogCommandDataContextSource
        {
            get { return (DialogHostOpenDialogCommandDataContextSource)GetValue(OpenDialogCommandDataContextSourceProperty); }
            set { SetValue(OpenDialogCommandDataContextSourceProperty, value); }
        }

        public static readonly DependencyProperty CloseOnClickAwayProperty = DependencyProperty.Register(
            "CloseOnClickAway", typeof(bool), typeof(DialogHost), new PropertyMetadata(default(bool)));

        /// <summary>
        /// Indicates whether the dialog will close if the user clicks off the dialog, on the obscured background.
        /// </summary>
        public bool CloseOnClickAway
        {
            get { return (bool)GetValue(CloseOnClickAwayProperty); }
            set { SetValue(CloseOnClickAwayProperty, value); }
        }

        public static readonly DependencyProperty CloseOnClickAwayParameterProperty = DependencyProperty.Register(
            "CloseOnClickAwayParameter", typeof(object), typeof(DialogHost), new PropertyMetadata(default(object)));

        /// <summary>
        /// Parameter to provide to close handlers if an close due to click away is instigated.
        /// </summary>
        public object CloseOnClickAwayParameter
        {
            get { return GetValue(CloseOnClickAwayParameterProperty); }
            set { SetValue(CloseOnClickAwayParameterProperty, value); }
        }

        public static readonly DependencyProperty SnackbarMessageQueueProperty = DependencyProperty.Register(
            "SnackbarMessageQueue", typeof(SnackbarMessageQueue), typeof(DialogHost), new PropertyMetadata(default(SnackbarMessageQueue), SnackbarMessageQueuePropertyChangedCallback));

        private static void SnackbarMessageQueuePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            DialogHost dialogHost = (DialogHost)dependencyObject;
            if (dialogHost._currentSnackbarMessageQueueUnPauseAction != null)
            {
                dialogHost._currentSnackbarMessageQueueUnPauseAction();
                dialogHost._currentSnackbarMessageQueueUnPauseAction = null;
            }

            if (!dialogHost.IsOpen) return;
            SnackbarMessageQueue snackbarMessageQueue = dependencyPropertyChangedEventArgs.NewValue as SnackbarMessageQueue;
            dialogHost._currentSnackbarMessageQueueUnPauseAction = snackbarMessageQueue?.Pause();
        }

        /// <summary>
        /// Allows association of a snackbar, so that notifications can be paused whilst a dialog is being displayed.
        /// </summary>
        public SnackbarMessageQueue SnackbarMessageQueue
        {
            get { return (SnackbarMessageQueue)GetValue(SnackbarMessageQueueProperty); }
            set { SetValue(SnackbarMessageQueueProperty, value); }
        }

        public static readonly DependencyProperty PopupStyleProperty = DependencyProperty.Register(
            nameof(PopupStyle), typeof(System.Windows.Style), typeof(DialogHost), new PropertyMetadata(default(System.Windows.Style)));

        public System.Windows.Style PopupStyle
        {
            get { return (System.Windows.Style)GetValue(PopupStyleProperty); }
            set { SetValue(PopupStyleProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            if (_contentCoverGrid != null)
                _contentCoverGrid.MouseLeftButtonUp -= ContentCoverGridOnMouseLeftButtonUp;

            _popup = GetTemplateChild(PopupPartName) as Popup;
            _popupContentControl = GetTemplateChild(PopupContentPartName) as ContentControl;
            _contentCoverGrid = GetTemplateChild(ContentCoverGridName) as Grid;

            if (_contentCoverGrid != null)
                _contentCoverGrid.MouseLeftButtonUp += ContentCoverGridOnMouseLeftButtonUp;

            VisualStateManager.GoToState(this, SelectState(), false);

            base.OnApplyTemplate();
        }

        #region open dialog events/callbacks

        public static readonly RoutedEvent DialogOpenedEvent =
            EventManager.RegisterRoutedEvent(
                "DialogOpened",
                RoutingStrategy.Bubble,
                typeof(DialogOpenedEventHandler),
                typeof(DialogHost));

        /// <summary>
        /// Raised when a dialog is opened.
        /// </summary>
        public event DialogOpenedEventHandler DialogOpened
        {
            add { AddHandler(DialogOpenedEvent, value); }
            remove { RemoveHandler(DialogOpenedEvent, value); }
        }

        /// <summary>
        /// Attached property which can be used on the <see cref="Button"/> which instigated the <see cref="OpenDialogCommand"/> to process the event.
        /// </summary>
        public static readonly DependencyProperty DialogOpenedAttachedProperty = DependencyProperty.RegisterAttached(
            "DialogOpenedAttached", typeof(DialogOpenedEventHandler), typeof(DialogHost), new PropertyMetadata(default(DialogOpenedEventHandler)));

        public static void SetDialogOpenedAttached(DependencyObject element, DialogOpenedEventHandler value)
        {
            element.SetValue(DialogOpenedAttachedProperty, value);
        }

        public static DialogOpenedEventHandler GetDialogOpenedAttached(DependencyObject element)
        {
            return (DialogOpenedEventHandler)element.GetValue(DialogOpenedAttachedProperty);
        }

        public static readonly DependencyProperty DialogOpenedCallbackProperty = DependencyProperty.Register(
            nameof(DialogOpenedCallback), typeof(DialogOpenedEventHandler), typeof(DialogHost), new PropertyMetadata(default(DialogOpenedEventHandler)));

        /// <summary>
        /// Callback fired when the <see cref="DialogOpened"/> event is fired, allowing the event to be processed from a binding/view model.
        /// </summary>
        public DialogOpenedEventHandler DialogOpenedCallback
        {
            get { return (DialogOpenedEventHandler)GetValue(DialogOpenedCallbackProperty); }
            set { SetValue(DialogOpenedCallbackProperty, value); }
        }

        protected void OnDialogOpened(DialogClosedEventArgs eventArgs)
        {
            RaiseEvent(eventArgs);
        }

        #endregion

        #region close dialog events/callbacks

        public static readonly RoutedEvent DialogClosingEvent =
            EventManager.RegisterRoutedEvent(
                "DialogClosing",
                RoutingStrategy.Bubble,
                typeof(DialogClosingEventHandler),
                typeof(DialogHost));

        /// <summary>
        /// Raised just before a dialog is closed.
        /// </summary>
        public event DialogClosingEventHandler DialogClosing
        {
            add { AddHandler(DialogClosingEvent, value); }
            remove { RemoveHandler(DialogClosingEvent, value); }
        }

        /// <summary>
        /// Attached property which can be used on the <see cref="Button"/> which instigated the <see cref="OpenDialogCommand"/> to process the closing event.
        /// </summary>
        public static readonly DependencyProperty DialogClosingAttachedProperty = DependencyProperty.RegisterAttached(
            "DialogClosingAttached", typeof(DialogClosingEventHandler), typeof(DialogHost), new PropertyMetadata(default(DialogClosingEventHandler)));

        public static void SetDialogClosingAttached(DependencyObject element, DialogClosingEventHandler value)
        {
            element.SetValue(DialogClosingAttachedProperty, value);
        }

        public static DialogClosingEventHandler GetDialogClosingAttached(DependencyObject element)
        {
            return (DialogClosingEventHandler)element.GetValue(DialogClosingAttachedProperty);
        }

        public static readonly DependencyProperty DialogClosingCallbackProperty = DependencyProperty.Register(
            nameof(DialogClosingCallback), typeof(DialogClosingEventHandler), typeof(DialogHost), new PropertyMetadata(default(DialogClosingEventHandler)));

        /// <summary>
        /// Callback fired when the <see cref="DialogClosing"/> event is fired, allowing the event to be processed from a binding/view model.
        /// </summary>
        public DialogClosingEventHandler DialogClosingCallback
        {
            get { return (DialogClosingEventHandler)GetValue(DialogClosingCallbackProperty); }
            set { SetValue(DialogClosingCallbackProperty, value); }
        }

        protected void OnDialogClosing(DialogClosingEventArgs eventArgs)
        {
            RaiseEvent(eventArgs);
        }

        #endregion

        internal void AssertTargetableContent()
        {
            BindingExpression existindBinding = BindingOperations.GetBindingExpression(this, DialogContentProperty);
            if (existindBinding != null)
                throw new InvalidOperationException(
                    "Content cannot be passed to a dialog via the OpenDialog if DialogContent already has a binding.");
        }

        internal void Close(object parameter)
        {
            DialogClosingEventArgs dialogClosingEventArgs = new DialogClosingEventArgs(_session, parameter, DialogClosingEvent);

            _session.IsEnded = true;

            //multiple ways of calling back that the dialog is closing:
            // * routed event
            // * the attached property (which should be applied to the button which opened the dialog
            // * straight forward dependency property 
            // * handler provided to the async show method
            OnDialogClosing(dialogClosingEventArgs);
            _attachedDialogClosingEventHandler?.Invoke(this, dialogClosingEventArgs);
            DialogClosingCallback?.Invoke(this, dialogClosingEventArgs);
            _asyncShowClosingEventHandler?.Invoke(this, dialogClosingEventArgs);

            if (!dialogClosingEventArgs.IsCancelled)
                SetCurrentValue(IsOpenProperty, false);
            else
                _session.IsEnded = false;

            _closeDialogExecutionParameter = parameter;
        }

        /// <summary>
        /// Attempts to focus the content of a popup.
        /// </summary>
        /// <returns>The popup content.</returns>
        internal UIElement FocusPopup()
        {
            UIElement child = _popup?.Child;
            if (child == null) return null;

            CommandManager.InvalidateRequerySuggested();
            UIElement focusable = child.VisualDepthFirstTraversal().OfType<UIElement>().FirstOrDefault(ui => ui.Focusable && ui.IsVisible);
            focusable?.Dispatcher.InvokeAsync(() =>
            {
                if (!focusable.Focus()) return;
                focusable.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
            }, DispatcherPriority.Background);

            return child;
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            Window window = Window.GetWindow(this);
            if (window != null && !window.IsActive)
                window.Activate();
            base.OnPreviewMouseDown(e);
        }

        private void ContentCoverGridOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (CloseOnClickAway)
                Close(CloseOnClickAwayParameter);
        }

        private void OpenDialogHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            if (executedRoutedEventArgs.Handled) return;

            DependencyObject dependencyObject = executedRoutedEventArgs.OriginalSource as DependencyObject;
            if (dependencyObject != null)
            {
                _attachedDialogOpenedEventHandler = GetDialogOpenedAttached(dependencyObject);
                _attachedDialogClosingEventHandler = GetDialogClosingAttached(dependencyObject);
            }

            if (executedRoutedEventArgs.Parameter != null)
            {
                AssertTargetableContent();

                if (_popupContentControl != null)
                {
                    switch (OpenDialogCommandDataContextSource)
                    {
                        case DialogHostOpenDialogCommandDataContextSource.SenderElement:
                            _popupContentControl.DataContext =
                                (executedRoutedEventArgs.OriginalSource as FrameworkElement)?.DataContext;
                            break;
                        case DialogHostOpenDialogCommandDataContextSource.DialogHostInstance:
                            _popupContentControl.DataContext = DataContext;
                            break;
                        case DialogHostOpenDialogCommandDataContextSource.None:
                            _popupContentControl.DataContext = null;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                DialogContent = executedRoutedEventArgs.Parameter;
            }

            SetCurrentValue(IsOpenProperty, true);

            executedRoutedEventArgs.Handled = true;
        }

        private void CloseDialogCanExecute(object sender, CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            canExecuteRoutedEventArgs.CanExecute = _session != null;
        }

        private void CloseDialogHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            if (executedRoutedEventArgs.Handled) return;

            Close(executedRoutedEventArgs.Parameter);

            executedRoutedEventArgs.Handled = true;
        }

        private string SelectState()
        {
            return IsOpen ? OpenStateName : ClosedStateName;
        }

        private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            LoadedInstances.Remove(this);
            SetCurrentValue(IsOpenProperty, false);
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            LoadedInstances.Add(this);
        }

        private static void WatchWindowActivation(DialogHost dialogHost)
        {
            Window window = Window.GetWindow(dialogHost);
            if (window != null)
            {
                window.Activated += dialogHost.WindowOnActivated;
                window.Deactivated += dialogHost.WindowOnDeactivated;
                dialogHost._closeCleanUp = () =>
                {
                    window.Activated -= dialogHost.WindowOnActivated;
                    window.Deactivated -= dialogHost.WindowOnDeactivated;
                };
            }
            else
            {
                dialogHost._closeCleanUp = () => { };
            }
        }

        private void WindowOnDeactivated(object sender, EventArgs eventArgs)
        {
            _restoreFocusWindowReactivation = _popup != null ? FocusManager.GetFocusedElement((Window)sender) : null;
        }

        private void WindowOnActivated(object sender, EventArgs eventArgs)
        {
            if (_restoreFocusWindowReactivation != null)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    Keyboard.Focus(_restoreFocusWindowReactivation);
                }));
            }
        }

        public static void CloseIndenfer(string dialogIdentifier)
        {
            List<DialogHost> targets = LoadedInstances.Where(dh => dialogIdentifier == null || Equals(dh.Identifier, dialogIdentifier)).ToList();

            if (targets.Count == 1)
            {
                DialogHost.CloseDialogCommand.Execute(targets[0], targets[0]);
            }
        }

        public static void ShowIndenfer(string dialogIdentifier)
        {
            List<DialogHost> targets = LoadedInstances.Where(dh => dialogIdentifier == null || Equals(dh.Identifier, dialogIdentifier)).ToList();

            if (targets.Count == 1)
            {
                DialogHost.OpenDialogCommand.Execute(targets[0], null);
            }
        }

        public static bool IsOpened()
        {
            return LoadedInstances.Any(l => l.IsOpen);

        }
    }
}
