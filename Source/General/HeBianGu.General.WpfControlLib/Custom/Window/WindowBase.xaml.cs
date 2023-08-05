// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace HeBianGu.General.WpfControlLib
{
    public abstract partial class WindowBase : Window, IWindowBase
    {
        public static ComponentResourceKey DynamicKey => new ComponentResourceKey(typeof(WindowBase), "S.WindowBase.Dynamic");
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(WindowBase), "S.WindowBase.Default");
        public static ComponentResourceKey SingleKey => new ComponentResourceKey(typeof(WindowBase), "S.WindowBase.Single");
        public static ComponentResourceKey AccentKey => new ComponentResourceKey(typeof(WindowBase), "S.WindowBase.Accent");
        public static ComponentResourceKey ClearKey => new ComponentResourceKey(typeof(WindowBase), "S.WindowBase.Clear");

        static WindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowBase), new FrameworkPropertyMetadata(typeof(WindowBase)));
        }

        public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register("IconSize", typeof(double), typeof(WindowBase), new PropertyMetadata(20D));
        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }
        public static readonly DependencyProperty CaptionHeightProperty = DependencyProperty.Register("CaptionHeight", typeof(double), typeof(WindowBase), new PropertyMetadata(26D));
        public double CaptionHeight
        {
            get { return (double)GetValue(CaptionHeightProperty); }
            set
            {
                SetValue(CaptionHeightProperty, value);
            }
        }

        public static readonly DependencyProperty CaptionBackgroundProperty = DependencyProperty.Register("CaptionBackground", typeof(Brush), typeof(WindowBase), new PropertyMetadata(null));

        public Brush CaptionBackground
        {
            get { return (Brush)GetValue(CaptionBackgroundProperty); }
            set { SetValue(CaptionBackgroundProperty, value); }
        }

        public CornerRadius CaptionCornerRadius
        {
            get { return (CornerRadius)GetValue(CaptionCornerRadiusProperty); }
            set { SetValue(CaptionCornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CaptionCornerRadiusProperty =
            DependencyProperty.Register("CaptionCornerRadius", typeof(CornerRadius), typeof(WindowBase), new FrameworkPropertyMetadata(default(CornerRadius), (d, e) =>
             {
                 WindowBase control = d as WindowBase;

                 if (control == null) return;

                 if (e.OldValue is CornerRadius o)
                 {

                 }

                 if (e.NewValue is CornerRadius n)
                 {

                 }

             }));


        public static readonly DependencyProperty CaptionForegroundProperty = DependencyProperty.Register("CaptionForeground", typeof(Brush), typeof(WindowBase), new PropertyMetadata(null));

        public Brush CaptionForeground
        {
            get { return (Brush)GetValue(CaptionForegroundProperty); }
            set { SetValue(CaptionForegroundProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(ControlTemplate), typeof(WindowBase), new PropertyMetadata(null));
        public ControlTemplate Header
        {
            get { return (ControlTemplate)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty MaxboxEnableProperty = DependencyProperty.Register("MaxboxEnable", typeof(bool), typeof(WindowBase), new PropertyMetadata(true));
        public bool MaxboxEnable
        {
            get { return (bool)GetValue(MaxboxEnableProperty); }
            set { SetValue(MaxboxEnableProperty, value); }
        }

        public static readonly DependencyProperty MinboxEnableProperty = DependencyProperty.Register("MinboxEnable", typeof(bool), typeof(WindowBase), new PropertyMetadata(true));

        public bool MinboxEnable
        {
            get { return (bool)GetValue(MinboxEnableProperty); }
            set { SetValue(MinboxEnableProperty, value); }
        }
        public bool IsClose
        {
            get { return (bool)GetValue(IsCloseProperty); }
            set { SetValue(IsCloseProperty, value); }
        }

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

        public static readonly DependencyProperty IsUseDragProperty =
            DependencyProperty.Register("IsUseDrag", typeof(bool), typeof(WindowBase), new PropertyMetadata(default(bool), (d, e) =>
             {
                 WindowBase control = d as WindowBase;

                 if (control == null) return;

             }));

        public Effect AdornerDecoratorEffect
        {
            get { return (Effect)GetValue(AdornerDecoratorEffectProperty); }
            set { SetValue(AdornerDecoratorEffectProperty, value); }
        }

        public static readonly DependencyProperty AdornerDecoratorEffectProperty =
            DependencyProperty.Register("AdornerDecoratorEffect", typeof(Effect), typeof(WindowBase), new PropertyMetadata(default(Effect), (d, e) =>
            {
                WindowBase control = d as WindowBase;

                if (control == null) return;

                Effect config = e.NewValue as Effect;

            }));

        public BlurEffect DefaultBlurEffect
        {
            get { return (BlurEffect)GetValue(DefaultBlurEffectProperty); }
            set { SetValue(DefaultBlurEffectProperty, value); }
        }

        public static readonly DependencyProperty DefaultBlurEffectProperty =
            DependencyProperty.Register("DefaultBlurEffect", typeof(BlurEffect), typeof(WindowBase), new PropertyMetadata(new BlurEffect(), (d, e) =>
            {
                MainWindowBase control = d as MainWindowBase;

                if (control == null) return;

                BlurEffect config = e.NewValue as BlurEffect;

            }));

        public Action<IWindowBase> ShowAnimation
        {
            get { return (Action<IWindowBase>)GetValue(ShowAnimationProperty); }
            set { SetValue(ShowAnimationProperty, value); }
        }

        public static readonly DependencyProperty ShowAnimationProperty =
            DependencyProperty.Register("ShowAnimation", typeof(Action<IWindowBase>), typeof(WindowBase), new PropertyMetadata(default(Action<WindowBase>), (d, e) =>
             {
                 IWindowBase control = d as IWindowBase;

                 if (control == null) return;

                 Action<IWindowBase> config = e.NewValue as Action<IWindowBase>;

             }));

        public Action<IWindowBase> CloseAnimation
        {
            get { return (Action<IWindowBase>)GetValue(CloseAnimationProperty); }
            set { SetValue(CloseAnimationProperty, value); }
        }

        public static readonly DependencyProperty CloseAnimationProperty =
            DependencyProperty.Register("CloseAnimation", typeof(Action<IWindowBase>), typeof(WindowBase), new PropertyMetadata(default(Action<WindowBase>), (d, e) =>
             {
                 WindowBase control = d as WindowBase;

                 if (control == null) return;

                 Action<IWindowBase> config = e.NewValue as Action<IWindowBase>;

             }));

        public ICommand CloseWindowCommand { get; protected set; }
        public ICommand MaximizeWindowCommand { get; protected set; }
        public ICommand MinimizeWindowCommand { get; protected set; }

        private void CloseCommand_Execute(object sender, ExecutedRoutedEventArgs e)
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
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed && this.IsUseDrag)
                this.DragMove();

            base.OnMouseLeftButtonDown(e);
        }

        public double TopTemp { get; set; }
        public double LeftTemp { get; set; }
        public WindowBase()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //this.MaxHeight = SystemParameters.WorkArea.Height;
            this.CloseWindowCommand = new RoutedUICommand();
            this.MaximizeWindowCommand = new RoutedUICommand();
            this.MinimizeWindowCommand = new RoutedUICommand();

            this.BindCommand(CloseWindowCommand, this.CloseCommand_Execute);
            this.BindCommand(MaximizeWindowCommand, this.MaxCommand_Execute);
            this.BindCommand(MinimizeWindowCommand, this.MinCommand_Execute);

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
            };
        }

        public new bool? ShowDialog()
        {
            return base.ShowDialog();
        }

        public new void Show()
        {
            base.Show();
        }

        public void BeginClose()
        {
            this.CloseAnimation?.Invoke(this);
        }

        public virtual void RefreshHide()
        {

        }

        public virtual void Show(bool value)
        {
            IWindowAnimationService animation = ServiceRegistry.Instance.GetInstance<IWindowAnimationService>();

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