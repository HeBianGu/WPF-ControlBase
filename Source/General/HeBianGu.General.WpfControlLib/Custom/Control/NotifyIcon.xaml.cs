// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    ///     Specifies a component that creates an icon in the notification area.
    /// </summary>
    [ContentProperty("Text")]
    [DefaultEvent("MouseDoubleClick")]
    [UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
    public sealed class NotifyIcon : FrameworkElement, IDisposable, IAddChild, ICommandSource
    {
        #region Fields

        private static readonly int TaskbarCreatedWindowMessage;

        private static readonly UIPermission _allWindowsPermission = new UIPermission(UIPermissionWindow.AllWindows);
        private static int _nextId;

        private readonly object _syncObj = new object();

        private NotifyIconHwndSource _hwndSource;

        private readonly int _id = _nextId++;

        private bool _iconCreated;

        private bool _doubleClick;

        #endregion

        #region Events

        /// <summary>
        ///     Identifies the <see cref="BalloonTipClick" /> routed event.
        /// </summary>
        public static readonly RoutedEvent BalloonTipClickEvent = EventManager.RegisterRoutedEvent("BalloonTipClick",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NotifyIcon));

        /// <summary>
        ///     Occurs when the balloon tip is clicked.
        /// </summary>
        public event RoutedEventHandler BalloonTipClick
        {
            add { AddHandler(BalloonTipClickEvent, value); }
            remove { RemoveHandler(BalloonTipClickEvent, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="BalloonTipClosed" /> routed event.
        /// </summary>
        public static readonly RoutedEvent BalloonTipClosedEvent = EventManager.RegisterRoutedEvent("BalloonTipClosed",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NotifyIcon));

        /// <summary>
        ///     Occurs when the balloon tip is closed by the user.
        /// </summary>
        public event RoutedEventHandler BalloonTipClosed
        {
            add { AddHandler(BalloonTipClosedEvent, value); }
            remove { RemoveHandler(BalloonTipClosedEvent, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="BalloonTipShown" /> routed event.
        /// </summary>
        public static readonly RoutedEvent BalloonTipShownEvent = EventManager.RegisterRoutedEvent("BalloonTipShown",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NotifyIcon));

        /// <summary>
        ///     Occurs when the balloon tip is displayed on the screen.
        /// </summary>
        public event RoutedEventHandler BalloonTipShown
        {
            add { AddHandler(BalloonTipShownEvent, value); }
            remove { RemoveHandler(BalloonTipShownEvent, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="Click" /> routed event.
        /// </summary>
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(NotifyIcon));

        /// <summary>
        ///     Occurs when the user clicks the icon in the notification area.
        /// </summary>
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="DoubleClick" /> routed event.
        /// </summary>
        public static readonly RoutedEvent DoubleClickEvent = EventManager.RegisterRoutedEvent("DoubleClick",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NotifyIcon));

        /// <summary>
        ///     Occurs when the user double-clicks the icon in the notification area of the taskbar.
        /// </summary>
        public event RoutedEventHandler DoubleClick
        {
            add { AddHandler(DoubleClickEvent, value); }
            remove { RemoveHandler(DoubleClickEvent, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="MouseClick" /> routed event.
        /// </summary>
        public static readonly RoutedEvent MouseClickEvent = EventManager.RegisterRoutedEvent("MouseClick",
            RoutingStrategy.Bubble, typeof(MouseButtonEventHandler), typeof(NotifyIcon));

        /// <summary>
        ///     Occurs when the user clicks a <see cref="NotifyIcon" /> with the mouse.
        /// </summary>
        public event MouseButtonEventHandler MouseClick
        {
            add { AddHandler(MouseClickEvent, value); }
            remove { RemoveHandler(MouseClickEvent, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="MouseDoubleClick" /> routed event.
        /// </summary>
        public static readonly RoutedEvent MouseDoubleClickEvent = EventManager.RegisterRoutedEvent("MouseDoubleClick",
            RoutingStrategy.Bubble, typeof(MouseButtonEventHandler), typeof(NotifyIcon));

        /// <summary>
        ///     Occurs when the user double-clicks the <see cref="NotifyIcon" /> with the mouse.
        /// </summary>
        public event MouseButtonEventHandler MouseDoubleClick
        {
            add { AddHandler(MouseDoubleClickEvent, value); }
            remove { RemoveHandler(MouseDoubleClickEvent, value); }
        }

        #endregion

        #region Constructor/Destructor

        /// <summary>
        ///     Initializes the <see cref="NotifyIcon" /> class.
        /// </summary>
        [SecurityCritical]
        static NotifyIcon()
        {
            TaskbarCreatedWindowMessage = NativeMethods.RegisterWindowMessage("TaskbarCreated");

            VisibilityProperty.OverrideMetadata(typeof(NotifyIcon), new FrameworkPropertyMetadata(OnVisibilityChanged));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NotifyIcon" /> class.
        /// </summary>
        [SecurityCritical]
        public NotifyIcon()
        {
            IsVisibleChanged += OnIsVisibleChanged;

            //this.MouseDoubleClick += NotifyIcon_MouseDoubleClick;
        }


        /// <summary>
        ///     Releases unmanaged resources and performs other cleanup operations before the
        ///     <see cref="NotifyIcon" /> is reclaimed by garbage collection.
        /// </summary>
        [SecuritySafeCritical]
        ~NotifyIcon()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        [SecuritySafeCritical]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [SecurityCritical]
        private void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (_hwndSource != null)
                {
                    UpdateIcon(true);
                    _hwndSource.Dispose();
                }
            }
            else if (_hwndSource != null)
            {
                NativeMethods.PostMessage(new HandleRef(_hwndSource, _hwndSource.Handle),
                    NativeMethods.WindowMessage.Close, IntPtr.Zero, IntPtr.Zero);

                _hwndSource?.Dispose();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Displays a balloon tip in the taskbar for the specified time period.
        /// </summary>
        /// <param name="timeout">The time period, in milliseconds, the balloon tip should display.</param>
        [SecurityCritical]
        public void ShowBalloonTip(int timeout)
        {
            ShowBalloonTip(timeout, BalloonTipTitle, BalloonTipText, BalloonTipIcon);
        }

        /// <summary>
        ///     Displays a balloon tip with the specified title, text, and icon in the taskbar for the specified time period.
        /// </summary>
        /// <param name="timeout">The time period, in milliseconds, the balloon tip should display.</param>
        /// <param name="tipTitle">The title to display on the balloon tip.</param>
        /// <param name="tipText">The text to display on the balloon tip.</param>
        /// <param name="tipIcon">One of the <see cref="NotifyBalloonIcon" /> values.</param>
        [SecurityCritical]
        public void ShowBalloonTip(int timeout, string tipTitle, string tipText, NotifyBalloonIcon tipIcon)
        {
            if (timeout < 0)
            {
                throw new ArgumentOutOfRangeException("timeout", timeout, "");
            }
            ArgumentValidator.NotNullOrEmptyString(tipText, "tipText");
            ArgumentValidator.EnumValueIsDefined(typeof(NotifyBalloonIcon), tipIcon, "tipIcon");

            if (true)
            {
                _allWindowsPermission.Demand();

                NativeMethods.NOTIFYICONDATA pnid = new NativeMethods.NOTIFYICONDATA
                {
                    hWnd = _hwndSource.Handle,
                    uID = _id,
                    uFlags = NativeMethods.NotifyIconFlags.Balloon,
                    uTimeoutOrVersion = timeout,
                    szInfoTitle = tipTitle,
                    szInfo = tipText,
                    dwInfoFlags = (int)tipIcon
                };
                NativeMethods.Shell_NotifyIcon(1, pnid);
            }
        }


        public void ShowFlash()
        {

        }

        #endregion

        #region Private Methods

        [SecurityCritical]
        private void ShowContextMenu()
        {
            if (ContextMenu != null)
            {
                NativeMethods.SetForegroundWindow(new HandleRef(_hwndSource, _hwndSource.Handle));

                ContextMenuService.SetPlacement(ContextMenu, PlacementMode.MousePoint);
                ContextMenu.IsOpen = true;
            }
        }

        /// <summary>
        ///     Shows or hides the icon according to the <see cref="P:IsVisible" /> and <see cref="P:Visibility" /> properties.
        /// </summary>
        [SecurityCritical]
        private void UpdateIconForVisibility()
        {
            if (IconVisibility == NotifyIconVisibility.UseControlVisibility)
            {
                UpdateIcon();
            }
        }

        [SecurityCritical]
        private void UpdateIcon(bool forceDestroy = false)
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;

            NotifyIconVisibility iconVisibility = IconVisibility;
            bool showIconInTray = !forceDestroy &&
                                  (iconVisibility == NotifyIconVisibility.Visible ||
                                   (iconVisibility == NotifyIconVisibility.UseControlVisibility && IsVisible));

            lock (_syncObj)
            {
                IntPtr iconHandle = IntPtr.Zero;

                try
                {
                    _allWindowsPermission.Demand();

                    if (showIconInTray && _hwndSource == null)
                    {
                        _hwndSource = new NotifyIconHwndSource(this);
                    }

                    if (_hwndSource != null)
                    {
                        _hwndSource.LockReference(showIconInTray);

                        NativeMethods.NOTIFYICONDATA pnid = new NativeMethods.NOTIFYICONDATA
                        {
                            uCallbackMessage = (int)NativeMethods.WindowMessage.TrayMouseMessage,
                            uFlags = NativeMethods.NotifyIconFlags.Message | NativeMethods.NotifyIconFlags.ToolTip,
                            hWnd = _hwndSource.Handle,
                            uID = _id,
                            szTip = Text
                        };
                        if (Icon != null)
                        {
                            iconHandle = NativeMethods.GetHIcon(Icon);

                            pnid.uFlags |= NativeMethods.NotifyIconFlags.Icon;
                            pnid.hIcon = iconHandle;
                        }

                        if (showIconInTray && iconHandle != IntPtr.Zero)
                        {
                            if (!_iconCreated)
                            {
                                NativeMethods.Shell_NotifyIcon(0, pnid);
                                _iconCreated = true;
                            }
                            else
                            {
                                NativeMethods.Shell_NotifyIcon(1, pnid);
                            }
                        }
                        else if (_iconCreated)
                        {
                            NativeMethods.Shell_NotifyIcon(2, pnid);
                            _iconCreated = false;
                        }
                    }
                }
                finally
                {
                    if (iconHandle != IntPtr.Zero)
                    {
                        NativeMethods.DestroyIcon(iconHandle);
                    }
                }
            }
        }

        [SecurityCritical]
        private static void OnVisibilityChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((NotifyIcon)o).UpdateIconForVisibility();
        }

        [SecurityCritical]
        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateIconForVisibility();
        }

        #endregion

        #region WndProc Methods

        private void WmMouseDown(MouseButton button, int clicks)
        {
            MouseButtonEventArgs args;

            if (clicks == 2)
            {
                RaiseEvent(new RoutedEventArgs(DoubleClickEvent));

                args = new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, 0, button)
                {
                    RoutedEvent = MouseDoubleClickEvent
                };
                RaiseEvent(args);

                _doubleClick = true;
            }

            args = new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, 0, button)
            {
                RoutedEvent = MouseDownEvent
            };
            RaiseEvent(args);
        }

        private void WmMouseMove()
        {
            MouseEventArgs args = new MouseEventArgs(InputManager.Current.PrimaryMouseDevice, 0) { RoutedEvent = MouseMoveEvent };
            RaiseEvent(args);
        }

        private void WmMouseUp(MouseButton button)
        {
            MouseButtonEventArgs args = new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, 0, button)
            {
                RoutedEvent = MouseUpEvent
            };
            RaiseEvent(args);

            if (!_doubleClick)
            {
                RaiseEvent(new RoutedEventArgs(ClickEvent));

                args = new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, 0, button)
                {
                    RoutedEvent = MouseClickEvent
                };
                RaiseEvent(args);
            }

            _doubleClick = false;
        }

        [SecurityCritical]
        private void WmTaskbarCreated()
        {
            _iconCreated = false;
            UpdateIcon();
        }

        [SecurityCritical]
        private void WndProc(int message, IntPtr lParam, out bool handled)
        {
            handled = true;

            if (message <= (int)NativeMethods.WindowMessage.MeasureItem)
            {
                if (message == (int)NativeMethods.WindowMessage.Destroy)
                {
                    UpdateIcon(true);
                    return;
                }
            }
            else
            {
                if (message != (int)NativeMethods.WindowMessage.TrayMouseMessage)
                {
                    if (message == TaskbarCreatedWindowMessage)
                    {
                        WmTaskbarCreated();
                    }
                    handled = false;
                    return;
                }
                switch ((NativeMethods.WindowMessage)lParam)
                {
                    case NativeMethods.WindowMessage.MouseMove:
                        WmMouseMove();
                        return;
                    case NativeMethods.WindowMessage.MouseDown:
                        WmMouseDown(MouseButton.Left, 1);
                        return;
                    case NativeMethods.WindowMessage.LButtonUp:
                        WmMouseUp(MouseButton.Left);
                        return;
                    case NativeMethods.WindowMessage.LButtonDblClk:
                        WmMouseDown(MouseButton.Left, 2);
                        return;
                    case NativeMethods.WindowMessage.RButtonDown:
                        WmMouseDown(MouseButton.Right, 1);
                        return;
                    case NativeMethods.WindowMessage.RButtonUp:
                        ShowContextMenu();
                        WmMouseUp(MouseButton.Right);
                        return;
                    case NativeMethods.WindowMessage.RButtonDblClk:
                        WmMouseDown(MouseButton.Right, 2);
                        return;
                    case NativeMethods.WindowMessage.MButtonDown:
                        WmMouseDown(MouseButton.Middle, 1);
                        return;
                    case NativeMethods.WindowMessage.MButtonUp:
                        WmMouseUp(MouseButton.Middle);
                        return;
                    case NativeMethods.WindowMessage.MButtonDblClk:
                        WmMouseDown(MouseButton.Middle, 2);
                        return;
                }
                switch ((NativeMethods.NotifyIconMessage)lParam)
                {
                    case NativeMethods.NotifyIconMessage.BalloonShow:
                        RaiseEvent(new RoutedEventArgs(BalloonTipShownEvent));
                        return;
                    case NativeMethods.NotifyIconMessage.BalloonHide:
                    case NativeMethods.NotifyIconMessage.BalloonTimeout:
                        RaiseEvent(new RoutedEventArgs(BalloonTipClosedEvent));
                        return;
                    case NativeMethods.NotifyIconMessage.BalloonUserClick:
                        RaiseEvent(new RoutedEventArgs(BalloonTipClickEvent));
                        return;
                }
                return;
            }
            if (message == TaskbarCreatedWindowMessage)
            {
                WmTaskbarCreated();
            }
            handled = false;
        }

        [SecurityCritical]
        // ReSharper disable once RedundantAssignment
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            WndProc(msg, lParam, out handled);

            return IntPtr.Zero;
        }

        #endregion

        #region Properties

        #region IconVisibility

        /// <summary>
        /// Identifies the <see cref="IconVisibility"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IconVisibilityProperty = DependencyProperty.Register(
            "IconVisibility", typeof(NotifyIconVisibility), typeof(NotifyIcon), new FrameworkPropertyMetadata(OnIconVisibilityChanged));

        [SecurityCritical]
        private static void OnIconVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NotifyIcon notifyIcon = ((NotifyIcon)d);
            notifyIcon.UpdateIcon();
        }

        /// <summary>
        /// Gets or sets the notify icon's visibility.
        /// </summary>
        public NotifyIconVisibility IconVisibility
        {
            get { return (NotifyIconVisibility)GetValue(IconVisibilityProperty); }
            set { SetValue(IconVisibilityProperty, value); }
        }

        #endregion

        #region BalloonTipIcon

        /// <summary>
        ///     Gets or sets the icon to display on the balloon tip.
        /// </summary>
        /// <value>The balloon tip icon.</value>
        public NotifyBalloonIcon BalloonTipIcon
        {
            get { return (NotifyBalloonIcon)GetValue(BalloonTipIconProperty); }
            set { SetValue(BalloonTipIconProperty, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="BalloonTipIcon" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty BalloonTipIconProperty =
            DependencyProperty.Register("BalloonTipIcon", typeof(NotifyBalloonIcon), typeof(NotifyIcon),
                new FrameworkPropertyMetadata(NotifyBalloonIcon.None));

        #endregion

        #region BalloonTipText

        /// <summary>
        ///     Gets or sets the text to display on the balloon tip.
        /// </summary>
        /// <value>The balloon tip text.</value>
        public string BalloonTipText
        {
            get { return (string)GetValue(BalloonTipTextProperty); }
            set { SetValue(BalloonTipTextProperty, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="BalloonTipText" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty BalloonTipTextProperty =
            DependencyProperty.Register("BalloonTipText", typeof(string), typeof(NotifyIcon),
                new FrameworkPropertyMetadata());

        #endregion

        #region BalloonTipTitle

        /// <summary>
        ///     Gets or sets the title of the balloon tip.
        /// </summary>
        /// <value>The balloon tip title.</value>
        public string BalloonTipTitle
        {
            get { return (string)GetValue(BalloonTipTitleProperty); }
            set { SetValue(BalloonTipTitleProperty, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="BalloonTipTitle" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty BalloonTipTitleProperty =
            DependencyProperty.Register("BalloonTipTitle", typeof(string), typeof(NotifyIcon),
                new FrameworkPropertyMetadata());

        #endregion

        #region Text

        /// <summary>
        ///     Gets or sets the tooltip text displayed when the mouse pointer rests on a notification area icon.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="Text" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(NotifyIcon),
                new FrameworkPropertyMetadata(OnTextPropertyChanged, OnCoerceTextProperty), ValidateTextPropety);

        private static bool ValidateTextPropety(object baseValue)
        {
            string value = (string)baseValue;

            return value == null || value.Length <= 0x3f;
        }

        [SecurityCritical]
        private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NotifyIcon notifyIcon = (NotifyIcon)d;

            notifyIcon.UpdateIcon();
        }

        private static object OnCoerceTextProperty(DependencyObject d, object baseValue)
        {
            string value = (string)baseValue;

            if (value == null)
            {
                value = string.Empty;
            }

            return value;
        }

        #endregion

        #region Icon

        /// <summary>
        ///     Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="Icon" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            Window.IconProperty.AddOwner(typeof(NotifyIcon),
                new FrameworkPropertyMetadata(OnNotifyIconChanged) { Inherits = true });

        [SecurityCritical]
        private static void OnNotifyIconChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            NotifyIcon notifyIcon = (NotifyIcon)o;

            notifyIcon.UpdateIcon();
        }

        #endregion

        #endregion

        #region IAddChild Members

        void IAddChild.AddChild(object value)
        {
            throw new InvalidOperationException();
        }

        void IAddChild.AddText(string text)
        {
            Text = text;
        }

        #endregion

        #region NotifyIconNativeWindow Class

        private class NotifyIconHwndSource : HwndSource
        {
            private readonly NotifyIcon _reference;
            private GCHandle _rootRef;

            [SecurityCritical]
            internal NotifyIconHwndSource(NotifyIcon component)
                : base(0, 0, 0, 0, 0, null, IntPtr.Zero)
            {
                _reference = component;

                AddHook(_reference.WndProc);
            }

            [SecuritySafeCritical]
            ~NotifyIconHwndSource()
            {
                if (Handle != IntPtr.Zero)
                {
                    NativeMethods.PostMessage(new HandleRef(this, Handle), NativeMethods.WindowMessage.Close,
                        IntPtr.Zero, IntPtr.Zero);
                }
            }

            [SecuritySafeCritical]
            public void LockReference(bool locked)
            {
                if (locked)
                {
                    if (!_rootRef.IsAllocated)
                    {
                        _rootRef = GCHandle.Alloc(_reference, GCHandleType.Normal);
                    }
                }
                else if (_rootRef.IsAllocated)
                {
                    _rootRef.Free();
                }
            }
        }

        #endregion 

        #region - Command -


        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(NotifyIcon));



        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(NotifyIcon));



        public IInputElement CommandTarget
        {
            get { return (IInputElement)GetValue(NotifyClassProperty); }
            set { SetValue(NotifyClassProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NotifyClassProperty =
            DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(NotifyIcon));


        private void NotifyIcon_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Command?.Execute(this.CommandParameter);

            MainWindowBase window = (MainWindowBase)Application.Current.MainWindow;

            if (window != null)
            {
                //window.ShowWindow = !window.ShowWindow;

                window.RefreshHide();
            }

        }


        #endregion

    }

    /// <summary>
    /// Defines the icon visibility modes of <see cref="NotifyIcon"/>.
    /// </summary>
    public enum NotifyIconVisibility
    {
        /// <summary>
        /// The icon is not shown.
        /// </summary>
        Hidden,
        /// <summary>
        /// The icon is shown.
        /// </summary>
        Visible,
        /// <summary>
        /// The icon is shown according to <see cref="UIElement.IsVisible"/>.
        /// </summary>
        UseControlVisibility,
    }



    internal static class NativeMethods
    {
        #region Kernel

        [SecurityCritical, SuppressUnmanagedCodeSecurity,
         DllImport("kernel32", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern void SetLastError(int dwErrorCode);

        #endregion

        #region User

        #region Enums

        public enum WindowLongValue
        {
            WndProc = -4,
            HInstace = -6,
            HwndParent = -8,
            Style = -16,
            ExtendedStyle = -20,
            UserData = -21,
            ID = -12,
        }

        [Flags]
        public enum WindowStyles
        {
            SysMemu = 0x80000,
            MinimizeBox = 0x20000,
            MaximizeBox = 0x10000,
            ThickFrame = 0x40000,
        }

        [Flags]
        public enum WindowExStyles
        {
            DlgModalFrame = 0x1,
        }

        public enum WindowMessage
        {
            Destroy = 0x2,
            Close = 0x10,
            SetIcon = 0x80,
            MeasureItem = 0x2c,
            MouseMove = 0x200,
            MouseDown = 0x201,
            LButtonUp = 0x0202,
            LButtonDblClk = 0x0203,
            RButtonDown = 0x0204,
            RButtonUp = 0x0205,
            RButtonDblClk = 0x0206,
            MButtonDown = 0x0207,
            MButtonUp = 0x0208,
            MButtonDblClk = 0x0209,
            TrayMouseMessage = 0x800,
        }

        public enum NotifyIconMessage
        {
            BalloonShow = 0x402,
            BalloonHide = 0x403,
            BalloonTimeout = 0x404,
            BalloonUserClick = 0x405,
            PopupOpen = 0x406,
            PopupClose = 0x407,
        }

        public enum SystemMenu
        {
            Size = 0xF000,
            Close = 0xF060,
            Restore = 0xF120,
            Minimize = 0xF020,
            Maximize = 0xF030,
        }

        #endregion

        [SecurityCritical, SuppressUnmanagedCodeSecurity,
         DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessage(HandleRef hWnd, WindowMessage msg, IntPtr wParam, IntPtr lParam);

        [SecurityCritical, SuppressUnmanagedCodeSecurity, DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool PostMessage(HandleRef hwnd, WindowMessage msg, IntPtr wparam, IntPtr lparam);

        [SecurityCritical, SuppressUnmanagedCodeSecurity,
         DllImport("user32", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetForegroundWindow(HandleRef hWnd);

        [SecurityCritical, SuppressUnmanagedCodeSecurity, DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int RegisterWindowMessage(string msg);

        [SecurityCritical, SuppressUnmanagedCodeSecurity, DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool DestroyIcon(IntPtr hIcon);

        [SecurityCritical, SuppressUnmanagedCodeSecurity,
         DllImport("user32", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int IntGetWindowLong(HandleRef hWnd, int nIndex);

        [SecurityCritical, SuppressUnmanagedCodeSecurity,
         DllImport("user32", EntryPoint = "GetWindowLongPtr", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr IntGetWindowLongPtr(HandleRef hWnd, int nIndex);

        [DllImport("user32", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int IntSetWindowLong(HandleRef hWnd, int nIndex, int dwNewLong);

        [DllImport("user32", EntryPoint = "SetWindowLongPtr", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr IntSetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

        [SecurityCritical]
        public static int GetWindowLong(HandleRef hWnd, WindowLongValue nIndex)
        {
            int result = 0;
            int error = 0;
            SetLastError(0);
            if (IntPtr.Size == 4)
            {
                result = IntGetWindowLong(hWnd, (int)nIndex);
                error = Marshal.GetLastWin32Error();
            }
            else
            {
                IntPtr resultPtr = IntGetWindowLongPtr(hWnd, (int)nIndex);
                error = Marshal.GetLastWin32Error();
                result = IntPtrToInt32(resultPtr);
            }
            return result;
        }

        [SecuritySafeCritical]
        public static IntPtr SetWindowLong(HandleRef hWnd, WindowLongValue nIndex, IntPtr dwNewLong)
        {
            int error = 0;
            IntPtr result = IntPtr.Zero;
            SetLastError(0);
            if (IntPtr.Size == 4)
            {
                int intResult = IntSetWindowLong(hWnd, (int)nIndex, IntPtrToInt32(dwNewLong));
                error = Marshal.GetLastWin32Error();
                result = new IntPtr(intResult);
            }
            else
            {
                result = IntSetWindowLongPtr(hWnd, (int)nIndex, dwNewLong);
                error = Marshal.GetLastWin32Error();
            }
            return result;
        }

        [SecurityCritical, DllImport("user32", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern bool EnableMenuItem(HandleRef hMenu, SystemMenu UIDEnabledItem, int uEnable);

        [SecurityCritical, DllImport("user32", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetSystemMenu(HandleRef hWnd, bool bRevert);

        [SecurityCritical]
        public static void SetSystemMenuItems(HandleRef hwnd, bool isEnabled, params SystemMenu[] menus)
        {
            if (menus != null && menus.Length > 0)
            {
                HandleRef hMenu = new HandleRef(null, GetSystemMenu(hwnd, false));

                foreach (SystemMenu menu in menus)
                {
                    SetMenuItem(hMenu, menu, isEnabled);
                }
            }
        }

        [SecurityCritical]
        public static void SetMenuItem(HandleRef hMenu, SystemMenu menu, bool isEnabled)
        {
            EnableMenuItem(hMenu, menu, (isEnabled) ? ~1 : 1);
        }

        #endregion

        #region Shell

        #region Structures and Enums

        [StructLayout(LayoutKind.Sequential)]
        public struct BROWSEINFO
        {
            /// <summary>
            ///     Handle to the owner window for the dialog box.
            /// </summary>
            public IntPtr HwndOwner;

            /// <summary>
            ///     Pointer to an item identifier list (PIDL) specifying the
            ///     location of the root folder from which to start browsing.
            /// </summary>
            public IntPtr Root;

            /// <summary>
            ///     Address of a buffer to receive the display name of the
            ///     folder selected by the user.
            /// </summary>
            [MarshalAs(UnmanagedType.LPStr)] public string DisplayName;

            /// <summary>
            ///     Address of a null-terminated string that is displayed
            ///     above the tree view control in the dialog box.
            /// </summary>
            [MarshalAs(UnmanagedType.LPStr)] public string Title;

            /// <summary>
            ///     Flags specifying the options for the dialog box.
            /// </summary>
            public uint Flags;

            /// <summary>
            ///     Address of an application-defined function that the
            ///     dialog box calls when an event occurs.
            /// </summary>
            [MarshalAs(UnmanagedType.FunctionPtr)] public WndProc Callback;

            /// <summary>
            ///     Application-defined value that the dialog box passes to
            ///     the callback function
            /// </summary>
            public int LParam;

            /// <summary>
            ///     Variable to receive the image associated with the selected folder.
            /// </summary>
            public int Image;
        }

        [Flags]
        public enum FolderBrowserOptions
        {
            /// <summary>
            ///     None.
            /// </summary>
            None = 0,

            /// <summary>
            ///     For finding a folder to start document searching
            /// </summary>
            FolderOnly = 0x0001,

            /// <summary>
            ///     For starting the Find Computer
            /// </summary>
            FindComputer = 0x0002,

            /// <summary>
            ///     Top of the dialog has 2 lines of text for BROWSEINFO.lpszTitle and
            ///     one line if this flag is set.  Passing the message
            ///     BFFM_SETSTATUSTEXTA to the hwnd can set the rest of the text.
            ///     This is not used with BIF_USENEWUI and BROWSEINFO.lpszTitle gets
            ///     all three lines of text.
            /// </summary>
            ShowStatusText = 0x0004,
            ReturnAncestors = 0x0008,

            /// <summary>
            ///     Add an editbox to the dialog
            /// </summary>
            ShowEditBox = 0x0010,

            /// <summary>
            ///     insist on valid result (or CANCEL)
            /// </summary>
            ValidateResult = 0x0020,

            /// <summary>
            ///     Use the new dialog layout with the ability to resize
            ///     Caller needs to call OleInitialize() before using this API
            /// </summary>
            UseNewStyle = 0x0040,
            UseNewStyleWithEditBox = (UseNewStyle | ShowEditBox),

            /// <summary>
            ///     Allow URLs to be displayed or entered. (Requires BIF_USENEWUI)
            /// </summary>
            AllowUrls = 0x0080,

            /// <summary>
            ///     Add a UA hint to the dialog, in place of the edit box. May not be
            ///     combined with BIF_EDITBOX.
            /// </summary>
            ShowUsageHint = 0x0100,

            /// <summary>
            ///     Do not add the "New Folder" button to the dialog.  Only applicable
            ///     with BIF_NEWDIALOGSTYLE.
            /// </summary>
            HideNewFolderButton = 0x0200,

            /// <summary>
            ///     don't traverse target as shortcut
            /// </summary>
            GetShortcuts = 0x0400,

            /// <summary>
            ///     Browsing for Computers.
            /// </summary>
            BrowseComputers = 0x1000,

            /// <summary>
            ///     Browsing for Printers.
            /// </summary>
            BrowsePrinters = 0x2000,

            /// <summary>
            ///     Browsing for Everything
            /// </summary>
            BrowseFiles = 0x4000,

            /// <summary>
            ///     sharable resources displayed (remote shares, requires BIF_USENEWUI)
            /// </summary>
            BrowseShares = 0x8000
        }

        #endregion

        #region Notify Icon

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class NOTIFYICONDATA
        {
            public int cbSize = Marshal.SizeOf(typeof(NOTIFYICONDATA));
            public IntPtr hWnd;
            public int uID;
            public NotifyIconFlags uFlags;
            public int uCallbackMessage;
            public IntPtr hIcon;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x80)] public string szTip;
            public int dwState;
            public int dwStateMask;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)] public string szInfo;
            public int uTimeoutOrVersion;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x40)] public string szInfoTitle;
            public int dwInfoFlags;
        }

        [Flags]
        public enum NotifyIconFlags
        {
            /// <summary>
            ///     The hIcon member is valid.
            /// </summary>
            Icon = 2,

            /// <summary>
            ///     The uCallbackMessage member is valid.
            /// </summary>
            Message = 1,

            /// <summary>
            ///     The szTip member is valid.
            /// </summary>
            ToolTip = 4,

            /// <summary>
            ///     The dwState and dwStateMask members are valid.
            /// </summary>
            State = 8,

            /// <summary>
            ///     Use a balloon ToolTip instead of a standard ToolTip. The szInfo, uTimeout, szInfoTitle, and dwInfoFlags members are
            ///     valid.
            /// </summary>
            Balloon = 0x10,
        }

        [SecurityCritical, DllImport("shell32", CharSet = CharSet.Auto)]
        public static extern int Shell_NotifyIcon(int message, NOTIFYICONDATA pnid);

        #endregion

        #region Malloc

        [ComImport, SuppressUnmanagedCodeSecurity, Guid("00000002-0000-0000-c000-000000000046"),
         InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IMalloc
        {
            [PreserveSig]
            IntPtr Alloc(int cb);

            [PreserveSig]
            IntPtr Realloc(IntPtr pv, int cb);

            [PreserveSig]
            void Free(IntPtr pv);

            [PreserveSig]
            int GetSize(IntPtr pv);

            [PreserveSig]
            int DidAlloc(IntPtr pv);

            [PreserveSig]
            void HeapMinimize();
        }

        [SecurityCritical]
        public static IMalloc GetSHMalloc()
        {
            IMalloc[] ppMalloc = new IMalloc[1];
            SHGetMalloc(ppMalloc);
            return ppMalloc[0];
        }

        [SecurityCritical, DllImport("shell32")]
        private static extern int SHGetMalloc([Out, MarshalAs(UnmanagedType.LPArray)] IMalloc[] ppMalloc);

        #endregion

        #region Folders

        [SecurityCritical, DllImport("shell32")]
        public static extern int SHGetFolderLocation(IntPtr hwndOwner, Int32 nFolder, IntPtr hToken, uint dwReserved,
            out IntPtr ppidl);

        [SecurityCritical, DllImport("shell32")]
        public static extern int SHParseDisplayName([MarshalAs(UnmanagedType.LPWStr)] string pszName, IntPtr pbc,
            out IntPtr ppidl, uint sfgaoIn, out uint psfgaoOut);

        [SecurityCritical, DllImport("shell32")]
        public static extern IntPtr SHBrowseForFolder(ref BROWSEINFO lbpi);

        [SecurityCritical, DllImport("shell32", CharSet = CharSet.Auto)]
        public static extern bool SHGetPathFromIDList(IntPtr pidl, IntPtr pszPath);

        #endregion

        #endregion

        #region Winmm

        #region Enums

        [Flags]
        private enum PlaySoundFlags
        {
            SND_ALIAS = 0x10000,
            SND_APPLICATION = 0x80,
            SND_ASYNC = 1,
            SND_FILENAME = 0x20000,
            SND_LOOP = 8,
            SND_MEMORY = 4,
            SND_NODEFAULT = 2,
            SND_NOSTOP = 0x10,
            SND_NOWAIT = 0x2000,
            SND_PURGE = 0x40,
            SND_RESOURCE = 0x40000,
            SND_SYNC = 0,
        }

        #endregion

        [SecurityCritical, SuppressUnmanagedCodeSecurity, DllImport("winmm", CharSet = CharSet.Unicode)]
        private static extern bool PlaySound(string soundName, IntPtr hmod, PlaySoundFlags soundFlags);


        #endregion

        #region Helpers

        public static int IntPtrToInt32(IntPtr intPtr)
        {
            return (int)intPtr.ToInt64();
        }

        public delegate IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [SecurityCritical]
        public static IntPtr GetHIcon(ImageSource source)
        {
            BitmapFrame frame = source as BitmapFrame;

            if (frame != null && frame.Decoder.Frames.Count > 0)
            {
                frame = frame.Decoder.Frames[0];

                int width = frame.PixelWidth;
                int height = frame.PixelHeight;
                int stride = width * ((frame.Format.BitsPerPixel + 7) / 8);

                byte[] bits = new byte[height * stride];

                frame.CopyPixels(bits, stride, 0);

                // pin the bytes in memory (avoids using unsafe context)
                GCHandle gcHandle = GCHandle.Alloc(bits, GCHandleType.Pinned);

                Bitmap bitmap = new Bitmap(
                    width,
                    height,
                    stride,
                    System.Drawing.Imaging.PixelFormat.Format32bppPArgb,
                    gcHandle.AddrOfPinnedObject());

                IntPtr hIcon = bitmap.GetHicon();

                gcHandle.Free();

                return hIcon;
            }

            return IntPtr.Zero;
        }

        #endregion
    }

    /// <summary>
    ///     Encapsulates methods for method arguments validation.
    /// </summary>
    internal static class ArgumentValidator
    {
        /// <summary>
        ///     Checks a string argument to ensure it isn't null or empty
        /// </summary>
        /// <param name="argumentValue">The argument value to check.</param>
        /// <param name="argumentName">The name of the argument.</param>
        public static void NotNullOrEmptyString(string argumentValue, string argumentName)
        {
            NotNull(argumentValue, argumentName);

            if (argumentValue.Length == 0)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture,
                    "", argumentName));
            }
        }

        /// <summary>
        ///     Checks an argument to ensure it isn't null
        /// </summary>
        /// <param name="argumentValue">The argument value to check.</param>
        /// <param name="argumentName">The name of the argument.</param>
        public static void NotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        ///     Checks an Enum argument to ensure that its value is defined by the specified Enum type.
        /// </summary>
        /// <param name="enumType">The Enum type the value should correspond to.</param>
        /// <param name="value">The value to check for.</param>
        /// <param name="argumentName">The name of the argument holding the value.</param>
        public static void EnumValueIsDefined(Type enumType, object value, string argumentName)
        {
            if (Enum.IsDefined(enumType, value) == false)
            {
                throw new ArgumentException(String.Format(CultureInfo.CurrentCulture,
                    "",
                    argumentName, enumType));
            }
        }

        /// <summary>
        ///     Verifies that an argument type is assignable from the provided type (meaning
        ///     interfaces are implemented, or classes exist in the base class hierarchy).
        /// </summary>
        /// <param name="assignee">The argument type.</param>
        /// <param name="providedType">The type it must be assignable from.</param>
        /// <param name="argumentName">The argument name.</param>
        public static void TypeIsAssignableFromType(Type assignee, Type providedType, string argumentName)
        {
            if (!providedType.IsAssignableFrom(assignee))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture,
                   "", assignee, providedType), argumentName);
            }
        }
    }
}
