using HeBianGu.Base.WpfBase;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using static HeBianGu.General.WpfControlLib.BlurWindowExtensions;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// WindowBase.xaml 的交互逻辑
    /// </summary>
    public class WindowBase : Window
    {
        #region 默认Header：窗体字体图标FIcon

        public static readonly DependencyProperty FIconProperty =
            DependencyProperty.Register("FIcon", typeof(string), typeof(WindowBase), new PropertyMetadata("\ue62e"));

        /// <summary>
        /// 按钮字体图标编码
        /// </summary>
        public string FIcon
        {
            get { return (string)GetValue(FIconProperty); }
            set { SetValue(FIconProperty, value); }
        }

        #endregion

        #region  默认Header：窗体字体图标大小

        public static readonly DependencyProperty FIconSizeProperty =
            DependencyProperty.Register("FIconSize", typeof(double), typeof(WindowBase), new PropertyMetadata(20D));

        /// <summary>
        /// 按钮字体图标大小
        /// </summary>
        public double FIconSize
        {
            get { return (double)GetValue(FIconSizeProperty); }
            set { SetValue(FIconSizeProperty, value); }
        }

        #endregion

        #region CaptionHeight 标题栏高度

        public static readonly DependencyProperty CaptionHeightProperty =
            DependencyProperty.Register("CaptionHeight", typeof(double), typeof(WindowBase), new PropertyMetadata(26D));

        /// <summary>
        /// 标题高度
        /// </summary>
        public double CaptionHeight
        {
            get { return (double)GetValue(CaptionHeightProperty); }
            set
            {
                SetValue(CaptionHeightProperty, value);
                //this._WC.CaptionHeight = value;
            }
        }

        #endregion

        #region CaptionBackground 标题栏背景色

        public static readonly DependencyProperty CaptionBackgroundProperty = DependencyProperty.Register(
            "CaptionBackground", typeof(Brush), typeof(WindowBase), new PropertyMetadata(null));

        public Brush CaptionBackground
        {
            get { return (Brush)GetValue(CaptionBackgroundProperty); }
            set { SetValue(CaptionBackgroundProperty, value); }
        }

        #endregion

        #region CaptionForeground 标题栏前景景色

        public static readonly DependencyProperty CaptionForegroundProperty = DependencyProperty.Register(
            "CaptionForeground", typeof(Brush), typeof(WindowBase), new PropertyMetadata(null));

        public Brush CaptionForeground
        {
            get { return (Brush)GetValue(CaptionForegroundProperty); }
            set { SetValue(CaptionForegroundProperty, value); }
        }

        #endregion

        #region Header 标题栏内容模板，以提高默认模板，可自定义

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header", typeof(ControlTemplate), typeof(WindowBase), new PropertyMetadata(null));

        public ControlTemplate Header
        {
            get { return (ControlTemplate)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        #endregion

        #region MaxboxEnable 是否显示最大化按钮

        public static readonly DependencyProperty MaxboxEnableProperty = DependencyProperty.Register(
            "MaxboxEnable", typeof(bool), typeof(WindowBase), new PropertyMetadata(true));

        public bool MaxboxEnable
        {
            get { return (bool)GetValue(MaxboxEnableProperty); }
            set { SetValue(MaxboxEnableProperty, value); }
        }

        #endregion

        #region MinboxEnable 是否显示最小化按钮

        public static readonly DependencyProperty MinboxEnableProperty = DependencyProperty.Register(
            "MinboxEnable", typeof(bool), typeof(WindowBase), new PropertyMetadata(true));

        public bool MinboxEnable
        {
            get { return (bool)GetValue(MinboxEnableProperty); }
            set { SetValue(MinboxEnableProperty, value); }
        }

        #endregion

        #region MinboxEnable 是否显示设置按钮

        public static readonly DependencyProperty SetboxEnableProperty = DependencyProperty.Register(
            " SetboxEnable", typeof(bool), typeof(WindowBase), new PropertyMetadata(true));

        public bool SetboxEnable
        {
            get { return (bool)GetValue(SetboxEnableProperty); }
            set { SetValue(SetboxEnableProperty, value); }
        }

        #endregion

        /****************** commands ******************/
        public ICommand CloseWindowCommand { get; protected set; }
        public ICommand MaximizeWindowCommand { get; protected set; }
        public ICommand MinimizeWindowCommand { get; protected set; }

        public WindowBase()
        {
            this.WindowStyle = WindowStyle.None;
            this.AllowsTransparency = true;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Todo ：初始化动画变量 
            TransformGroup group = new TransformGroup();
            ScaleTransform scale = new ScaleTransform();
            SkewTransform skew = new SkewTransform();
            RotateTransform rotate = new RotateTransform();
            TranslateTransform translate = new TranslateTransform();
            group.Children.Add(scale);
            group.Children.Add(skew);
            group.Children.Add(rotate);
            group.Children.Add(translate);
            this.RenderTransform = group;


            // Todo ：初始化淡出初始效果 
            this.OpacityMask = this.FindResource("WindowOpMack") as Brush;

            //this.Style = this.FindResource("DefaultWindowStyle") as Style;
            //this.Icon = Images.CreateImageSourceFromImage(Properties.Resources.logo);
            //12=6+6//Margin=6,Border.Effect.BlueRadius=6
            this.MaxHeight = SystemParameters.WorkArea.Height + 12 + 2;
            //bind command
            this.CloseWindowCommand = new RoutedUICommand();
            this.MaximizeWindowCommand = new RoutedUICommand();
            this.MinimizeWindowCommand = new RoutedUICommand();
            this.BindCommand(CloseWindowCommand, this.CloseCommand_Execute);
            this.BindCommand(MaximizeWindowCommand, this.MaxCommand_Execute);
            this.BindCommand(MinimizeWindowCommand, this.MinCommand_Execute);

            this.Loaded += WindowBase_Loaded;


        }

        private void WindowBase_Loaded(object sender, RoutedEventArgs e)
        {
            this.EnableBlur();
        }

        private void CloseCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            this.BegionStoryClose();
        }

        /// <summary> 用于重写关闭到那个花 </summary>
        public virtual void BegionStoryClose()
        {
            CloseStoryService.Instance.DownToUpOpsClose(this);
        }

        private void MaxCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            e.Handled = true;
        }

        private void MinCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            e.Handled = true;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }


    }

    internal static class BlurWindowExtensions
    {
        internal static class NativeMethods
        {
            [DllImport("user32.dll")]
            internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttribData data);


            [StructLayout(LayoutKind.Sequential)]
            internal struct WindowCompositionAttribData
            {
                public WindowCompositionAttribute Attribute;
                public IntPtr Data;
                public int SizeOfData;
            }


            [StructLayout(LayoutKind.Sequential)]
            internal struct AccentPolicy
            {
                public AccentState AccentState;
                public AccentFlags AccentFlags;
                public int GradientColor;
                public int AnimationId;
            }


            [Flags]
            internal enum AccentFlags
            {
                // ... 
                DrawLeftBorder = 0x20,
                DrawTopBorder = 0x40,
                DrawRightBorder = 0x80,
                DrawBottomBorder = 0x100,
                DrawAllBorders = (DrawLeftBorder | DrawTopBorder | DrawRightBorder | DrawBottomBorder),
                DrawNoBorder = 0
                // ... 
            }


            internal enum WindowCompositionAttribute
            {
                // ... 
                WCA_ACCENT_POLICY = 19
                // ... 
            }


            internal enum AccentState
            {
                ACCENT_DISABLED = 0,
                ACCENT_ENABLE_GRADIENT = 1,
                ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
                ACCENT_ENABLE_BLURBEHIND = 3,
                ACCENT_INVALID_STATE = 4
            }
        }


        public static void EnableBlur(this Window window)
        {
            if (SystemParameters.HighContrast)
            {
                return; // Blur is not useful in high contrast mode 
            }

            SetAccentPolicy(window, NativeMethods.AccentState.ACCENT_ENABLE_BLURBEHIND);
        }


        public static void DisableBlur(this Window window)
        {
            SetAccentPolicy(window, NativeMethods.AccentState.ACCENT_DISABLED);
        }


        private static void SetAccentPolicy(Window window, NativeMethods.AccentState accentState)
        {
            var windowHelper = new WindowInteropHelper(window);


            var accent = new NativeMethods.AccentPolicy
            {
                AccentState = accentState,
                AccentFlags = GetAccentFlagsForTaskbarPosition()
            };


            var accentStructSize = Marshal.SizeOf(accent);


            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);


            var data = new NativeMethods.WindowCompositionAttribData
            {
                Attribute = NativeMethods.WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };

            NativeMethods.SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }


        private static NativeMethods.AccentFlags GetAccentFlagsForTaskbarPosition()
        {
            return NativeMethods.AccentFlags.DrawNoBorder;
        }
    }
}
