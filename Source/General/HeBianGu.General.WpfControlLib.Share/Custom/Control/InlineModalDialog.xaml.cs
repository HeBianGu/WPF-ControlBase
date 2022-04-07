// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// Provides a modal dialog that uses inline <see cref="UIElement"/>s
    /// instead of a top-level window.
    /// </summary>
    public class InlineModalDialog : HeaderedContentControl
    {
        #region Fields

        private bool _isOpen;
        private DispatcherFrame _dispatcherFrame;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes the <see cref="InlineModalDialog"/> class.
        /// </summary>
        static InlineModalDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InlineModalDialog), new FrameworkPropertyMetadata(typeof(InlineModalDialog)));
            IsTabStopProperty.OverrideMetadata(typeof(InlineModalDialog), new FrameworkPropertyMetadata(false));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineModalDialog"/> class.
        /// </summary>
        public InlineModalDialog()
        {
            CommandBindings.Add(new CommandBinding(CloseCommand, HandleCloseCommand));

            KeyboardNavigation.SetTabNavigation(this, KeyboardNavigationMode.Local);
            KeyboardNavigation.SetDirectionalNavigation(this, KeyboardNavigationMode.Local);

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnLoaded;

            Focus();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Identifies the <see cref="DialogIntroAnimation"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DialogIntroAnimationProperty = DependencyProperty.Register("DialogIntroAnimation", typeof(Storyboard), typeof(InlineModalDialog));

        /// <summary>
        /// Gets or sets the animation that runs when this dialog is shown.
        /// </summary>
        public Storyboard DialogIntroAnimation
        {
            get { return (Storyboard)GetValue(DialogIntroAnimationProperty); }
            set { SetValue(DialogIntroAnimationProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="DialogOutroAnimation"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DialogOutroAnimationProperty = DependencyProperty.Register("DialogOutroAnimation", typeof(Storyboard), typeof(InlineModalDialog));

        /// <summary>
        /// Gets or sets the animation that runs when this dialog is closed.
        /// </summary>
        public Storyboard DialogOutroAnimation
        {
            get { return (Storyboard)GetValue(DialogOutroAnimationProperty); }
            set { SetValue(DialogOutroAnimationProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show the blurrer.
        /// </summary>
        /// <value><c>true</c> if the blurrer is shown; otherwise, <c>false</c>.</value>
        public bool ShowBlurrer
        {
            get { return (bool)GetValue(ShowBlurrerProperty); }
            set { SetValue(ShowBlurrerProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="ShowBlurrer"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ShowBlurrerProperty =
            DependencyProperty.Register("ShowBlurrer", typeof(bool), typeof(InlineModalDialog), new UIPropertyMetadata(true));

        /// <summary>
        /// Gets or sets the owner.
        /// <remarks>
        /// This value is used to retrieve the corresponding <see cref="InlineModalDecorator"/>.
        /// </remarks>
        /// </summary>
        /// <value>The owner.</value>
        public DependencyObject Owner
        {
            get { return (DependencyObject)GetValue(OwnerProperty); }
            set { SetValue(OwnerProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="Owner"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OwnerProperty =
            DependencyProperty.Register("Owner", typeof(DependencyObject), typeof(InlineModalDialog), new FrameworkPropertyMetadata());

        private static InlineModalDecorator GetModalDecorator(DependencyObject obj)
        {
            return (InlineModalDecorator)obj.GetValue(ModalDecoratorProperty);
        }

        internal static void SetModalDecorator(DependencyObject obj, InlineModalDecorator value)
        {
            obj.SetValue(ModalDecoratorProperty, value);
        }

        internal static void ClearModalDecorator(DependencyObject obj)
        {
            obj.ClearValue(ModalDecoratorProperty);
        }

        private static readonly DependencyProperty ModalDecoratorProperty =
            DependencyProperty.RegisterAttached("ModalDecorator", typeof(InlineModalDecorator), typeof(InlineModalDialog), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.OverridesInheritanceBehavior));

        /// <summary>
        /// Gets or sets the dialog result.
        /// </summary>
        /// <value>The dialog result.</value>
        public bool? DialogResult { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// Identifies the <c>Close</c> routed command, which can be used to close the dialog.
        /// </summary>
        public static readonly RoutedCommand CloseCommand = new RoutedCommand("Close", typeof(InlineModalDialog));

        #endregion

        #region Routed Events

        /// <summary>
        /// Identifies the <see cref="E:Opened"/> routed event.
        /// </summary>
        public static readonly RoutedEvent OpenedEvent = EventManager.RegisterRoutedEvent("Opened", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(InlineModalDialog));

        /// <summary>
        /// Fires when the dialog is opened.
        /// </summary>
        public event RoutedEventHandler Opened
        {
            add { AddHandler(OpenedEvent, value); }
            remove { RemoveHandler(OpenedEvent, value); }
        }

        /// <summary>
        /// Raises the <see cref="Opened"/> routed event.
        /// </summary>
        protected virtual void OnOpened()
        {
            RaiseEvent(new RoutedEventArgs(OpenedEvent));
        }

        /// <summary>
        /// Identifies the <see cref="E:Closed"/> routed event.
        /// </summary>
        public static readonly RoutedEvent ClosedEvent = EventManager.RegisterRoutedEvent("Closed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(InlineModalDialog));

        /// <summary>
        /// Fires when the dialog closes.
        /// </summary>
        public event RoutedEventHandler Closed
        {
            add { AddHandler(ClosedEvent, value); }
            remove { RemoveHandler(ClosedEvent, value); }
        }

        /// <summary>
        /// Raises the <see cref="Closed"/> routed event.
        /// </summary>
        protected virtual void OnClosed()
        {
            RaiseEvent(new RoutedEventArgs(ClosedEvent));
        }

        /// <summary>
        /// Identifies the <see cref="E:Closing"/> routed event.
        /// </summary>
        public static readonly RoutedEvent ClosingEvent = EventManager.RegisterRoutedEvent("Closing", RoutingStrategy.Bubble, typeof(CancelRoutedEventHandler), typeof(InlineModalDialog));

        /// <summary>
        /// Fires when the dialog is about to close.
        /// </summary>
        public event CancelRoutedEventHandler Closing
        {
            add { AddHandler(ClosingEvent, value); }
            remove { RemoveHandler(ClosingEvent, value); }
        }

        /// <summary>
        /// Raises the <see cref="Closing"/> routed event.
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnClosing(CancelRoutedEventArgs args)
        {
            RaiseEvent(args);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Shows the modal dialog.
        /// </summary>
        public void Show()
        {
            ValidateOwner();

            InlineModalDecorator panel = GetModalDecorator(Owner);

            if (panel != null)
            {
                panel.AddModal(this, ShowBlurrer);

                if (AnimatorService.IsAnimationEnabled)
                {
                    Storyboard dialogAnim = DialogIntroAnimation;
                    if (dialogAnim != null)
                    {
                        if (dialogAnim.IsFrozen)
                        {
                            dialogAnim = dialogAnim.Clone();
                        }

                        dialogAnim.AttachCompletedEventHandler(OnOpenAnimationCompleted);
                        dialogAnim.Begin(this);
                    }
                }
                else
                {
                    OpenCompleted();
                }

                _dispatcherFrame = new DispatcherFrame();
                Dispatcher.PushFrame(_dispatcherFrame);

                _isOpen = false;
            }
        }

        /// <summary>
        /// Closes the modal dialog.
        /// </summary>
        public void Close()
        {
            if (_isOpen)
            {
                ValidateOwner();

                InlineModalDecorator panel = GetModalDecorator(Owner);

                if (panel != null)
                {
                    CancelRoutedEventArgs cancelArgs = new CancelRoutedEventArgs(ClosingEvent);
                    OnClosing(cancelArgs);
                    if (!cancelArgs.Cancel)
                    {
                        if (AnimatorService.IsAnimationEnabled)
                        {
                            Storyboard dialogAnim = DialogOutroAnimation;
                            if (dialogAnim != null)
                            {
                                if (dialogAnim.IsFrozen)
                                {
                                    dialogAnim = dialogAnim.Clone();
                                }

                                // Add a handler so we know when the dialog can be closed.
                                dialogAnim.AttachCompletedEventHandler(OnCloseAnimationCompleted);
                                dialogAnim.Begin(this);
                            }
                        }
                        else
                        {
                            CloseDialog(panel);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Attempts to close the front-most dialog of the provided owner.
        /// </summary>
        /// <param name="owner"></param>
        public static void CloseCurrent(DependencyObject owner)
        {
            InlineModalDecorator panel = GetModalDecorator(owner);
            if (panel != null)
            {
                InlineModalDialog current = panel.TopmostModal as InlineModalDialog;
                if (current != null)
                {
                    current.Close();
                }
            }
        }

        #endregion

        #region Private Methods

        private void ValidateOwner()
        {
            if (Owner == null)
            {
                throw new InvalidOperationException("Owner must be set.");
            }
        }

        /// <summary>
        /// Handler for the close command.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleCloseCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (DialogResult == null)
            {
                Button source = e.OriginalSource as Button;
                if (source != null)
                {
                    if (source.IsDefault)
                    {
                        DialogResult = true;
                    }
                    else if (source.IsCancel)
                    {
                        DialogResult = false;
                    }
                }
            }
            e.Handled = true;
            Close();
        }

        private void OnOpenAnimationCompleted(object sender, EventArgs e)
        {
            OpenCompleted();
        }

        private void OpenCompleted()
        {
            _isOpen = true;

            OnOpened();
        }

        /// <summary>
        /// Handler for the window close animation completion, this delays
        /// actually closing the window until all outro animations have completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCloseAnimationCompleted(object sender, EventArgs e)
        {
            InlineModalDecorator panel = GetModalDecorator(Owner);
            Debug.Assert(panel != null);
            CloseDialog(panel);
        }

        private void CloseDialog(InlineModalDecorator panel)
        {
            panel.RemoveModal(this, ShowBlurrer);

            OnClosed();

            UIElement topMost = panel.TopmostModal;
            if (topMost != null)
            {
                topMost.Focus();
            }

            if (_dispatcherFrame != null)
            {
                _dispatcherFrame.Continue = false;
                _dispatcherFrame = null;
            }

            _isOpen = false;
        }

        #endregion
    }

    /// <summary>
    /// Represents a delegate used for routed events that support cancellation.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void CancelRoutedEventHandler(object sender, CancelRoutedEventArgs e);

    /// <summary>
    /// Represents routed events arguments that support cancellation.
    /// </summary>
    public class CancelRoutedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CancelRoutedEventArgs"/>.
        /// </summary>
        public CancelRoutedEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CancelRoutedEventArgs"/>.
        /// </summary>
        public CancelRoutedEventArgs(RoutedEvent routedEvent)
            : base(routedEvent)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CancelRoutedEventArgs"/>.
        /// </summary>
        public CancelRoutedEventArgs(RoutedEvent routedEvent, object source)
            : base(routedEvent, source)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether to cancel the event.
        /// </summary>
        public bool Cancel { get; set; }
    }


    /// <summary>
    /// Provides a host for <see cref="InlineModalDialog"/>s.
    /// </summary>
    [StyleTypedProperty(Property = "BlurrerStyle", StyleTargetType = typeof(Border))]
    public class InlineModalDecorator : Decorator
    {
        #region Fields

        private const int DefaultChildIndex = 1;
        private const int BlurrerAnimationDurationMs = 500;

        private readonly Grid _panel;
        private readonly Border _blurrer;

        private bool _hasChild;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineModalDecorator"/> class.
        /// </summary>
        public InlineModalDecorator()
        {
            _panel = new Grid();
            AddVisualChild(_panel);
            AddLogicalChild(_panel);

            _blurrer = new Border { Style = BlurrerStyle, Visibility = Visibility.Collapsed };

            _panel.Children.Add(_blurrer);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Identifies the <see cref="Target"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(
            "Target", typeof(UIElement), typeof(InlineModalDecorator), new FrameworkPropertyMetadata(OnTargetChanged));

        private static void OnTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement oldValue = (UIElement)e.OldValue;
            if (oldValue != null)
            {
                InlineModalDialog.ClearModalDecorator(oldValue);
            }
            UIElement newValue = (UIElement)e.NewValue;
            if (newValue != null)
            {
                InlineModalDialog.SetModalDecorator(newValue, (InlineModalDecorator)d);
            }
        }

        /// <summary>
        /// Gets or sets the dialog decorator target.
        /// This element will be marked as the root element under which inline dialogs can be used.
        /// </summary>
        public UIElement Target
        {
            get { return (UIElement)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        /// <summary>
        /// Gets the current modal count.
        /// </summary>
        /// <value>The modal count.</value>
        public int ModalCount
        {
            get { return (int)GetValue(ModalCountProperty); }
            private set { SetValue(ModalCountPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey ModalCountPropertyKey =
            DependencyProperty.RegisterReadOnly("ModalCount", typeof(int), typeof(InlineModalDecorator), new FrameworkPropertyMetadata(0));

        /// <summary>
        /// Identifies the <see cref="ModalCount"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ModalCountProperty = ModalCountPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets or sets the blurrer style.
        /// <remarks>
        /// The blurrer is a <see cref="Border"/>.
        /// </remarks>
        /// </summary>
        /// <value>The blurrer style.</value>
        public Style BlurrerStyle
        {
            get { return (Style)GetValue(BlurrerStyleProperty); }
            set { SetValue(BlurrerStyleProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="BlurrerStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty BlurrerStyleProperty =
            DependencyProperty.Register("BlurrerStyle", typeof(Style), typeof(InlineModalDecorator), new FrameworkPropertyMetadata(GetDefaultBlurrerStyle()));

        private static Style GetDefaultBlurrerStyle()
        {
            Style style = new Style(typeof(Border));
            style.Setters.Add(new Setter(Border.BackgroundProperty, Brushes.Black));
            style.Setters.Add(new Setter(OpacityProperty, 0.2D));
            style.Seal();
            return style;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets or sets the child element.
        /// </summary>
        public override UIElement Child
        {
            get
            {
                return _hasChild ? _panel.Children[DefaultChildIndex] : null;
            }
            set
            {
                if (_hasChild)
                {
                    _panel.Children.RemoveAt(DefaultChildIndex);
                }

                if (value != null)
                {
                    Panel.SetZIndex(value, -1);
                    _panel.Children.Insert(DefaultChildIndex, value);
                    _hasChild = true;
                }
                else
                {
                    _hasChild = false;
                }
            }
        }

        /// <summary>
        /// Gets the logical children.
        /// </summary>
        protected override System.Collections.IEnumerator LogicalChildren
        {
            get { yield return _panel; }
        }

        /// <summary>
        /// Gets the visual children count.
        /// </summary>
        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        /// <summary>
        /// Gets the visual child at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override Visual GetVisualChild(int index)
        {
            if (index > 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return _panel;
        }

        /// <summary>
        /// Measures the element.
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size constraint)
        {
            _panel.Measure(constraint);
            return _panel.DesiredSize;
        }

        /// <summary>
        /// Arranges the element.
        /// </summary>
        /// <param name="arrangeSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            _panel.Arrange(new Rect(new Point(), arrangeSize));
            return _panel.RenderSize;
        }

        #endregion

        #region Internal Methods

        internal void AddModal(UIElement newElement)
        {
            AddModal(newElement, true);
        }

        internal void AddModal(UIElement newElement, bool showBlurrer)
        {
            int childCount = _panel.Children.Count;

            if (childCount > 1)
            {
                UIElement child = PeekChild();
                child.IsHitTestVisible = false;
            }

            if (childCount <= 2 && showBlurrer)
            {
                _blurrer.InvalidateArrange();
                AnimatorService.AnimatePropertyFromTo(_blurrer, OpacityProperty, 0, null, BlurrerAnimationDurationMs);
                _blurrer.Visibility = Visibility.Visible;
            }

            _panel.Children.Add(CreateDecorator(newElement));
            UpdateModalCount();
        }

        internal void RemoveModal(UIElement closingElement)
        {
            RemoveModal(closingElement, true);
        }

        internal void RemoveModal(UIElement closingElement, bool hideBlurrer)
        {
            if (_panel.Children.Count > 1)
            {
                Decorator decorator = PeekChild() as Decorator;
                if (decorator != null)
                {
                    UIElement element = decorator.Child;
                    if (ReferenceEquals(element, closingElement))
                    {
                        _panel.Children.Remove(decorator);
                        int childCount = _panel.Children.Count;
                        if (childCount > 0)
                        {
                            UIElement child = PeekChild();
                            child.IsHitTestVisible = true;

                            if (childCount <= 2 && hideBlurrer)
                            {
                                AnimatorService.AnimatePropertyFromTo(_blurrer, OpacityProperty, null, 0, BlurrerAnimationDurationMs, HideBlurrer);
                            }
                        }
                        UpdateModalCount();
                    }
                }
            }
        }

        internal void RemoveTopmostModal()
        {
            if (_panel.Children.Count > 1)
            {
                Decorator decorator = PeekChild() as Decorator;
                if (decorator != null)
                {
                    RemoveModal(decorator.Child);
                }
            }
        }

        internal UIElement TopmostModal
        {
            get
            {
                Decorator decorator = PeekChild() as Decorator;
                if (decorator != null)
                {
                    return decorator.Child;
                }

                return null;
            }
        }

        #endregion

        #region Private Methods

        private static Decorator CreateDecorator(UIElement element)
        {
            Decorator decorator = new Decorator
            {
                Child = element,
            };

            KeyboardNavigation.SetTabNavigation(decorator, KeyboardNavigationMode.Cycle);
            KeyboardNavigation.SetControlTabNavigation(decorator, KeyboardNavigationMode.Cycle);
            KeyboardNavigation.SetDirectionalNavigation(decorator, KeyboardNavigationMode.Cycle);

            return decorator;
        }

        private UIElement PeekChild()
        {
            return _panel.Children[_panel.Children.Count - 1];
        }

        private void UpdateModalCount()
        {
            ModalCount = _panel.Children.Count - 2;
        }

        private void HideBlurrer(object sender, EventArgs e)
        {
            _blurrer.Visibility = Visibility.Collapsed;
        }

        #endregion
    }
}
