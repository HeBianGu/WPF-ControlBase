using HeBianGu.Base.WpfBase;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace HeBianGu.General.WpfControlLib
{
    public interface IWindowBase
    {
        Action<WindowBase> CloseAnimation { get; set; }
        Action<WindowBase> ShowAnimation { get; set; }

        void BeginClose();
        void RefreshHide();
        void Show();
        void Show(bool value);
        bool? ShowDialog();
    }

    /// <summary>
    /// WindowBase.xaml 的交互逻辑
    /// </summary>
    public abstract partial class WindowBase : Window, IWindowBase
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
                    control.Close();
                }

            }));


        public bool IsUseDrag
        {
            get { return (bool)GetValue(IsUseDragProperty); }
            set { SetValue(IsUseDragProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsUseDragProperty =
            DependencyProperty.Register("IsUseDrag", typeof(bool), typeof(WindowBase), new PropertyMetadata(default(bool), (d, e) =>
             {
                 WindowBase control = d as WindowBase;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));


        #endregion

        #region - 窗体内容区域Effect效果 -

        public Effect AdornerDecoratorEffect
        {
            get { return (Effect)GetValue(AdornerDecoratorEffectProperty); }
            set { SetValue(AdornerDecoratorEffectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AdornerDecoratorEffectProperty =
            DependencyProperty.Register("AdornerDecoratorEffect", typeof(Effect), typeof(WindowBase), new PropertyMetadata(default(Effect), (d, e) =>
            {
                WindowBase control = d as WindowBase;

                if (control == null) return;

                Effect config = e.NewValue as Effect;

            }));

        /// <summary> 默认磨砂效果 </summary>
        public BlurEffect DefaultBlurEffect
        {
            get { return (BlurEffect)GetValue(DefaultBlurEffectProperty); }
            set { SetValue(DefaultBlurEffectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultBlurEffectProperty =
            DependencyProperty.Register("DefaultBlurEffect", typeof(BlurEffect), typeof(WindowBase), new PropertyMetadata(new BlurEffect(), (d, e) =>
            {
                MainWindowBase control = d as MainWindowBase;

                if (control == null) return;

                BlurEffect config = e.NewValue as BlurEffect;

            }));
        #endregion

        ///// <summary> 是否启用磨砂效果 </summary>
        //public bool IsUseBlur
        //{
        //    get { return (bool)GetValue(IsUseBlurProperty); }
        //    set { SetValue(IsUseBlurProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty IsUseBlurProperty =
        //    DependencyProperty.Register("IsUseBlur", typeof(bool), typeof(WindowBase), new PropertyMetadata(default(bool), (d, e) =>
        //     {
        //         WindowBase control = d as WindowBase;

        //         if (control == null) return;

        //         //bool config = e.NewValue as bool;

        //     }));


        /// <summary> 显示时的动画效果 </summary>
        public Action<WindowBase> ShowAnimation
        {
            get { return (Action<WindowBase>)GetValue(ShowAnimationProperty); }
            set { SetValue(ShowAnimationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowAnimationProperty =
            DependencyProperty.Register("ShowAnimation", typeof(Action<WindowBase>), typeof(WindowBase), new PropertyMetadata(default(Action<WindowBase>), (d, e) =>
             {
                 WindowBase control = d as WindowBase;

                 if (control == null) return;

                 Action<WindowBase> config = e.NewValue as Action<WindowBase>;

             }));

        /// <summary> 关闭时的动画效果 </summary>

        public Action<WindowBase> CloseAnimation
        {
            get { return (Action<WindowBase>)GetValue(CloseAnimationProperty); }
            set { SetValue(CloseAnimationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseAnimationProperty =
            DependencyProperty.Register("CloseAnimation", typeof(Action<WindowBase>), typeof(WindowBase), new PropertyMetadata(default(Action<WindowBase>), (d, e) =>
             {
                 WindowBase control = d as WindowBase;

                 if (control == null) return;

                 Action<WindowBase> config = e.NewValue as Action<WindowBase>;

             }));

        #endregion

        #region - 绑定命令 -
        public ICommand CloseWindowCommand { get; protected set; }
        public ICommand MaximizeWindowCommand { get; protected set; }
        public ICommand MinimizeWindowCommand { get; protected set; }

        private async void CloseCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            this.OnCloseAnimation();
        }

        protected virtual void OnCloseAnimation()
        {
            this.CloseAnimation?.Invoke(this);
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

            ////  Do ：当点击任务栏，最小化时发生
            //var engine = DoubleStoryboardEngine.Create(this.Top, this.Top + 200, 0.2, Window.TopProperty.Name);
            //engine.CompletedEvent += (l, k) => this.WindowState = WindowState.Minimized;
            //DoubleStoryboardEngine.Create(1, 0, 0.3, UIElement.OpacityProperty.Name).Start(this);
            //engine.Start(this);
            //e.Handled = true;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed && this.IsUseDrag)
                this.DragMove();

            base.OnMouseLeftButtonDown(e);
        }

        #endregion

        public double TopTemp { get; set; }

        public double LeftTemp { get; set; }

        public WindowBase()
        {
            //this.WindowStyle = WindowStyle.None;
            //this.AllowsTransparency = true;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //// Todo ：初始化动画变量 
            //TransformGroup group = new TransformGroup();
            //ScaleTransform scale = new ScaleTransform();
            //SkewTransform skew = new SkewTransform();
            //RotateTransform rotate = new RotateTransform();
            //TranslateTransform translate = new TranslateTransform();
            //group.Children.Add(scale);
            //group.Children.Add(skew);
            //group.Children.Add(rotate);
            //group.Children.Add(translate);
            //this.RenderTransform = group;

            this.MaxHeight = SystemParameters.WorkArea.Height + 12 + 2;
            //bind command
            this.CloseWindowCommand = new RoutedUICommand();
            this.MaximizeWindowCommand = new RoutedUICommand();
            this.MinimizeWindowCommand = new RoutedUICommand();

            this.BindCommand(CloseWindowCommand, this.CloseCommand_Execute);
            this.BindCommand(MaximizeWindowCommand, this.MaxCommand_Execute);
            this.BindCommand(MinimizeWindowCommand, this.MinCommand_Execute);


            //this.ShowAnimation = l =>
            //  {
            //      // Todo ：初始化淡出初始效果 
            //      this.OpacityMask = this.FindResource("S.WindowOpMack.LoadBrush") as Brush;
            //  };

            //this.CloseAnimation = l =>
            // {
            //     this.BegionStoryClose();
            // };


            this.ShowAnimation = l =>
              {
                  this.Show(true);
              };

            this.CloseAnimation = l =>
             {
                 this.Show(false);
             };


            this.Loaded += (l, k) =>
            {
                this.ShowAnimation?.Invoke(this);


                //if (this.IsUseBlur)
                //{
                //    this.EnableBlur();
                //}
            };
        }


        /// <summary> 按照动画方式显示 </summary>
        public new bool? ShowDialog()
        {
            //this.ShowAnimation?.Invoke(this);
            //this.Loaded += (l, k) =>
            //  {
            //      this.ShowAnimation?.Invoke(this);

            //      if (this.IsUseBlur)
            //      {
            //          this.EnableBlur();
            //      }

            //  };


            return base.ShowDialog();
        }

        /// <summary> 按照动画方式显示 </summary>
        public new void Show()
        {
            //this.ShowAnimation?.Invoke(this);

            //this.Loaded += (l, k) =>
            //{
            //    this.ShowAnimation?.Invoke(this);


            //    if (this.IsUseBlur)
            //    {
            //        this.EnableBlur();
            //    }
            //};

            base.Show();

        }

        /// <summary> 按照动画方式管理 </summary>
        public void BeginClose()
        {
            this.CloseAnimation?.Invoke(this);
        }


        public virtual void RefreshHide()
        {

        }

        public virtual void Show(bool value)
        {
            var animation = ServiceRegistry.Instance.GetInstance<IWindowAnimationService>();

            if (animation == null)
            {
                if (value)
                {
                    this.Show();
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                if (value)
                {
                    animation?.ShowAnimation(this);
                }
                else
                {
                    animation?.CloseAnimation(this);
                }
            }

        }
    }
}


//partial class WindowBase
//{
//    private int WM_SYSCOMMAND = 0x112;
//    private long SC_MAXIMIZE = 0xF030;
//    private long SC_MINIMIZE = 0xF020;
//    private long SC_CLOSE = 0xF060;
//    private long SC_DESMINIMIZE = 0x0000f120;

//    protected override void OnSourceInitialized(EventArgs e)
//    {
//        base.OnSourceInitialized(e);
//        HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
//        source.AddHook(WndProc);
//    }



//    private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
//    {
//        //Debug.WriteLine(WM_SYSCOMMAND +                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              