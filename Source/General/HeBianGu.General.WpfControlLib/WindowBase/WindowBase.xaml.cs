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

    public interface IWindowBase
    {
        /// <summary> 输出消息 </summary>
        void AddSnackMessage(string message);

        /// <summary> 输出消息和操作按钮 </summary>
        void AddSnackMessage(string message, object actionContent, Action actionHandler);

        /// <summary> 输出消息、按钮和参数 </summary>
        void AddSnackMessage<TArgument>(string message, object actionContent, Action<TArgument> actionHandler,
           TArgument actionArgument);

        /// <summary> 显示蒙版 </summary>
        void ShowWithLayer(Uri uri, int layerIndex = 0);

        /// <summary> 关闭蒙版 </summary>
        void CloseWithLayer(int layerIndex = 0);

    }

    /// <summary>
    /// WindowBase.xaml 的交互逻辑
    /// </summary>
    public partial class WindowBase : Window
    {

        #region - 依赖属性 -

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


        public bool IsClose
        {
            get { return (bool)GetValue(IsCloseProperty); }
            set { SetValue(IsCloseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCloseProperty =
            DependencyProperty.Register("IsClose", typeof(bool), typeof(WindowBase), new PropertyMetadata(default(bool), (d, e) =>
            {
                WindowBase control = d as WindowBase;

                if (control == null) return;

                bool config = (bool)e.NewValue;

                if (config)
                {
                    control.DialogResult = true;

                    CloseStoryService.Instance.UoToDownClose(control);
                }

            }));



        #endregion


        #region top drawer

        public static readonly DependencyProperty TopDrawerContentProperty = DependencyProperty.Register(
            nameof(TopDrawerContent), typeof(object), typeof(WindowBase), new PropertyMetadata(default(object)));

        public object TopDrawerContent
        {
            get { return (object)GetValue(TopDrawerContentProperty); }
            set { SetValue(TopDrawerContentProperty, value); }
        }

        public static readonly DependencyProperty TopDrawerContentTemplateProperty = DependencyProperty.Register(
            nameof(TopDrawerContentTemplate), typeof(DataTemplate), typeof(WindowBase), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate TopDrawerContentTemplate
        {
            get { return (DataTemplate)GetValue(TopDrawerContentTemplateProperty); }
            set { SetValue(TopDrawerContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty TopDrawerContentTemplateSelectorProperty = DependencyProperty.Register(
            nameof(TopDrawerContentTemplateSelector), typeof(DataTemplateSelector), typeof(WindowBase), new PropertyMetadata(default(DataTemplateSelector)));

        public DataTemplateSelector TopDrawerContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(TopDrawerContentTemplateSelectorProperty); }
            set { SetValue(TopDrawerContentTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty TopDrawerContentStringFormatProperty = DependencyProperty.Register(
            nameof(TopDrawerContentStringFormat), typeof(string), typeof(WindowBase), new PropertyMetadata(default(string)));

        public string TopDrawerContentStringFormat
        {
            get { return (string)GetValue(TopDrawerContentStringFormatProperty); }
            set { SetValue(TopDrawerContentStringFormatProperty, value); }
        }

        public static readonly DependencyProperty TopDrawerBackgroundProperty = DependencyProperty.Register(
            nameof(TopDrawerBackground), typeof(Brush), typeof(WindowBase), new PropertyMetadata(default(Brush)));

        public Brush TopDrawerBackground
        {
            get { return (Brush)GetValue(TopDrawerBackgroundProperty); }
            set { SetValue(TopDrawerBackgroundProperty, value); }
        }

        public static readonly DependencyProperty IsTopDrawerOpenProperty = DependencyProperty.Register(
            nameof(IsTopDrawerOpen), typeof(bool), typeof(WindowBase), new FrameworkPropertyMetadata(default(bool)));

        public bool IsTopDrawerOpen
        {
            get { return (bool)GetValue(IsTopDrawerOpenProperty); }
            set { SetValue(IsTopDrawerOpenProperty, value); }
        }

        private static readonly DependencyPropertyKey TopDrawerZIndexPropertyKey =
                                    DependencyProperty.RegisterReadOnly(
                                    "TopDrawerZIndex", typeof(int), typeof(WindowBase),
                                    new PropertyMetadata(4));

        public static readonly DependencyProperty TopDrawerZIndexProperty = TopDrawerZIndexPropertyKey.DependencyProperty;

        public int TopDrawerZIndex
        {
            get { return (int)GetValue(TopDrawerZIndexProperty); }
            private set { SetValue(TopDrawerZIndexPropertyKey, value); }
        }

        #endregion

        #region left drawer

        public static readonly DependencyProperty LeftDrawerContentProperty = DependencyProperty.Register(
            nameof(LeftDrawerContent), typeof(object), typeof(WindowBase), new PropertyMetadata(default(object)));

        public object LeftDrawerContent
        {
            get { return (object)GetValue(LeftDrawerContentProperty); }
            set { SetValue(LeftDrawerContentProperty, value); }
        }

        public static readonly DependencyProperty LeftDrawerContentTemplateProperty = DependencyProperty.Register(
            nameof(LeftDrawerContentTemplate), typeof(DataTemplate), typeof(WindowBase), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate LeftDrawerContentTemplate
        {
            get { return (DataTemplate)GetValue(LeftDrawerContentTemplateProperty); }
            set { SetValue(LeftDrawerContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty LeftDrawerContentTemplateSelectorProperty = DependencyProperty.Register(
            nameof(LeftDrawerContentTemplateSelector), typeof(DataTemplateSelector), typeof(WindowBase), new PropertyMetadata(default(DataTemplateSelector)));

        public DataTemplateSelector LeftDrawerContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(LeftDrawerContentTemplateSelectorProperty); }
            set { SetValue(LeftDrawerContentTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty LeftDrawerContentStringFormatProperty = DependencyProperty.Register(
            nameof(LeftDrawerContentStringFormat), typeof(string), typeof(WindowBase), new PropertyMetadata(default(string)));

        public string LeftDrawerContentStringFormat
        {
            get { return (string)GetValue(LeftDrawerContentStringFormatProperty); }
            set { SetValue(LeftDrawerContentStringFormatProperty, value); }
        }

        public static readonly DependencyProperty LeftDrawerBackgroundProperty = DependencyProperty.Register(
            nameof(LeftDrawerBackground), typeof(Brush), typeof(WindowBase), new PropertyMetadata(default(Brush)));

        public Brush LeftDrawerBackground
        {
            get { return (Brush)GetValue(LeftDrawerBackgroundProperty); }
            set { SetValue(LeftDrawerBackgroundProperty, value); }
        }

        public static readonly DependencyProperty IsLeftDrawerOpenProperty = DependencyProperty.Register(
            nameof(IsLeftDrawerOpen), typeof(bool), typeof(WindowBase), new FrameworkPropertyMetadata(default(bool)));

        public bool IsLeftDrawerOpen
        {
            get { return (bool)GetValue(IsLeftDrawerOpenProperty); }
            set { SetValue(IsLeftDrawerOpenProperty, value); }
        }

        private static readonly DependencyPropertyKey LeftDrawerZIndexPropertyKey =
                                            DependencyProperty.RegisterReadOnly(
                                            "LeftDrawerZIndex", typeof(int), typeof(WindowBase),
                                            new PropertyMetadata(2));

        public static readonly DependencyProperty LeftDrawerZIndexProperty = LeftDrawerZIndexPropertyKey.DependencyProperty;

        public int LeftDrawerZIndex
        {
            get { return (int)GetValue(LeftDrawerZIndexProperty); }
            private set { SetValue(LeftDrawerZIndexPropertyKey, value); }
        }

        #endregion

        #region right drawer

        public static readonly DependencyProperty RightDrawerContentProperty = DependencyProperty.Register(
            nameof(RightDrawerContent), typeof(object), typeof(WindowBase), new PropertyMetadata(default(object)));

        public object RightDrawerContent
        {
            get { return (object)GetValue(RightDrawerContentProperty); }
            set { SetValue(RightDrawerContentProperty, value); }
        }

        public static readonly DependencyProperty RightDrawerContentTemplateProperty = DependencyProperty.Register(
            nameof(RightDrawerContentTemplate), typeof(DataTemplate), typeof(WindowBase), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate RightDrawerContentTemplate
        {
            get { return (DataTemplate)GetValue(RightDrawerContentTemplateProperty); }
            set { SetValue(RightDrawerContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty RightDrawerContentTemplateSelectorProperty = DependencyProperty.Register(
            nameof(RightDrawerContentTemplateSelector), typeof(DataTemplateSelector), typeof(WindowBase), new PropertyMetadata(default(DataTemplateSelector)));

        public DataTemplateSelector RightDrawerContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(RightDrawerContentTemplateSelectorProperty); }
            set { SetValue(RightDrawerContentTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty RightDrawerContentStringFormatProperty = DependencyProperty.Register(
            nameof(RightDrawerContentStringFormat), typeof(string), typeof(WindowBase), new PropertyMetadata(default(string)));

        public string RightDrawerContentStringFormat
        {
            get { return (string)GetValue(RightDrawerContentStringFormatProperty); }
            set { SetValue(RightDrawerContentStringFormatProperty, value); }
        }

        public static readonly DependencyProperty RightDrawerBackgroundProperty = DependencyProperty.Register(
            nameof(RightDrawerBackground), typeof(Brush), typeof(WindowBase), new PropertyMetadata(default(Brush)));

        public Brush RightDrawerBackground
        {
            get { return (Brush)GetValue(RightDrawerBackgroundProperty); }
            set { SetValue(RightDrawerBackgroundProperty, value); }
        }

        public static readonly DependencyProperty IsRightDrawerOpenProperty = DependencyProperty.Register(
            nameof(IsRightDrawerOpen), typeof(bool), typeof(WindowBase), new FrameworkPropertyMetadata(default(bool)));

        public bool IsRightDrawerOpen
        {
            get { return (bool)GetValue(IsRightDrawerOpenProperty); }
            set { SetValue(IsRightDrawerOpenProperty, value); }
        }

        private static readonly DependencyPropertyKey RightDrawerZIndexPropertyKey =
                                    DependencyProperty.RegisterReadOnly(
                                    "RightDrawerZIndex", typeof(int), typeof(WindowBase),
                                    new PropertyMetadata(1));

        public static readonly DependencyProperty RightDrawerZIndexProperty = RightDrawerZIndexPropertyKey.DependencyProperty;

        public int RightDrawerZIndex
        {
            get { return (int)GetValue(RightDrawerZIndexProperty); }
            private set { SetValue(RightDrawerZIndexPropertyKey, value); }
        }

        #endregion

        #region bottom drawer

        public static readonly DependencyProperty BottomDrawerContentProperty = DependencyProperty.Register(
            nameof(BottomDrawerContent), typeof(object), typeof(WindowBase), new PropertyMetadata(default(object)));

        public object BottomDrawerContent
        {
            get { return (object)GetValue(BottomDrawerContentProperty); }
            set { SetValue(BottomDrawerContentProperty, value); }
        }

        public static readonly DependencyProperty BottomDrawerContentTemplateProperty = DependencyProperty.Register(
            nameof(BottomDrawerContentTemplate), typeof(DataTemplate), typeof(WindowBase), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate BottomDrawerContentTemplate
        {
            get { return (DataTemplate)GetValue(BottomDrawerContentTemplateProperty); }
            set { SetValue(BottomDrawerContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty BottomDrawerContentTemplateSelectorProperty = DependencyProperty.Register(
            nameof(BottomDrawerContentTemplateSelector), typeof(DataTemplateSelector), typeof(WindowBase), new PropertyMetadata(default(DataTemplateSelector)));

        public DataTemplateSelector BottomDrawerContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(BottomDrawerContentTemplateSelectorProperty); }
            set { SetValue(BottomDrawerContentTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty BottomDrawerContentStringFormatProperty = DependencyProperty.Register(
            nameof(BottomDrawerContentStringFormat), typeof(string), typeof(WindowBase), new PropertyMetadata(default(string)));

        public string BottomDrawerContentStringFormat
        {
            get { return (string)GetValue(BottomDrawerContentStringFormatProperty); }
            set { SetValue(BottomDrawerContentStringFormatProperty, value); }
        }

        public static readonly DependencyProperty BottomDrawerBackgroundProperty = DependencyProperty.Register(
            nameof(BottomDrawerBackground), typeof(Brush), typeof(WindowBase), new PropertyMetadata(default(Brush)));

        public Brush BottomDrawerBackground
        {
            get { return (Brush)GetValue(BottomDrawerBackgroundProperty); }
            set { SetValue(BottomDrawerBackgroundProperty, value); }
        }

        public static readonly DependencyProperty IsBottomDrawerOpenProperty = DependencyProperty.Register(
            nameof(IsBottomDrawerOpen), typeof(bool), typeof(WindowBase), new FrameworkPropertyMetadata(default(bool)));

        public bool IsBottomDrawerOpen
        {
            get { return (bool)GetValue(IsBottomDrawerOpenProperty); }
            set { SetValue(IsBottomDrawerOpenProperty, value); }
        }

        private static readonly DependencyPropertyKey BottomDrawerZIndexPropertyKey =
                                    DependencyProperty.RegisterReadOnly(
                                    "BottomDrawerZIndex", typeof(int), typeof(WindowBase),
                                    new PropertyMetadata(3));

        public static readonly DependencyProperty BottomDrawerZIndexProperty = BottomDrawerZIndexPropertyKey.DependencyProperty;

        public int BottomDrawerZIndex
        {
            get { return (int)GetValue(BottomDrawerZIndexProperty); }
            private set { SetValue(BottomDrawerZIndexPropertyKey, value); }
        }

        #endregion
        #endregion

        #region - 绑定命令 -
        public ICommand CloseWindowCommand { get; protected set; }
        public ICommand MaximizeWindowCommand { get; protected set; }
        public ICommand MinimizeWindowCommand { get; protected set; }

        public ICommand SettimgWindowCommand { get; protected set; }

        private void CloseCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            Action<object, DialogClosingEventArgs> action = (l, k) =>
            {
                if ((bool)k.Parameter)
                {
                    this.BegionStoryClose();
                }
            };

            if (Application.Current.MainWindow == this)
            {
                MessageService.ShowResultMessge("确认要退出系统?", action);
            }
            else
            {
                this.BegionStoryClose();
            }
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
            this.DragMove();

            base.OnMouseLeftButtonDown(e);
        }

        #endregion

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
            this.OpacityMask = this.FindResource("S.WindowOpMack.LoadBrush") as Brush;

            this.MaxHeight = SystemParameters.WorkArea.Height + 12 + 2;
            //bind command
            this.CloseWindowCommand = new RoutedUICommand();
            this.MaximizeWindowCommand = new RoutedUICommand();
            this.MinimizeWindowCommand = new RoutedUICommand();
            this.SettimgWindowCommand = new RoutedUICommand();
            this.BindCommand(CloseWindowCommand, this.CloseCommand_Execute);
            this.BindCommand(MaximizeWindowCommand, this.MaxCommand_Execute);
            this.BindCommand(MinimizeWindowCommand, this.MinCommand_Execute);
            this.BindCommand(SettimgWindowCommand, this.SettimgCommand_Execute);

        }
        private void SettimgCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            this.ShowWithLayer(e.Parameter as Uri);
        }



    }


    [TemplatePart(Name = "PART_SnackBar", Type = typeof(Snackbar))]
    [TemplatePart(Name = "PART_SettingFrame", Type = typeof(ModernFrame))]
    partial class WindowBase : IWindowBase
    {
        Snackbar _snackbar;
        ModernFrame _settingFrame;
        public override void OnApplyTemplate()
        {

            base.OnApplyTemplate();

            this._snackbar = Template.FindName("PART_SnackBar", this) as Snackbar;
            this._settingFrame = Template.FindName("PART_SettingFrame", this) as ModernFrame;
        }
        /// <summary> 输出消息 </summary>
        public void AddSnackMessage(string message)
        {
            SnackbarMessageQueue queue = _snackbar.MessageQueue;

            Task.Factory.StartNew(() => queue.Enqueue(message));
        }

        /// <summary> 输出消息和操作按钮 </summary>
        public void AddSnackMessage(string message, object actionContent, Action actionHandler)
        {
            SnackbarMessageQueue queue = _snackbar.MessageQueue;

            Task.Factory.StartNew(() => queue.Enqueue(message, actionContent, actionHandler));
        }

        /// <summary> 输出消息、按钮和参数 </summary>
        public void AddSnackMessage<TArgument>(string message, object actionContent, Action<TArgument> actionHandler,
            TArgument actionArgument)
        {
            SnackbarMessageQueue queue = _snackbar.MessageQueue;

            Task.Factory.StartNew(() => queue.Enqueue(message, actionContent, actionHandler, actionArgument));
        }


        public void ShowWithLayer(Uri uri, int layerIndex = 0)
        {
            _settingFrame.Source = uri as Uri;

            _settingFrame.Visibility = Visibility.Visible;
        }

        public void CloseWithLayer(int layerIndex = 0)
        {
            _settingFrame.Visibility = Visibility.Collapsed;
        }
    }

}
