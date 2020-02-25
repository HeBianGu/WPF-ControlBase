using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary> 自定义按钮控件 </summary>
    public partial class FButton : Button
    {
        public static readonly DependencyProperty PressedBackgroundProperty =
            DependencyProperty.Register("PressedBackground", typeof(Brush), typeof(FButton), new PropertyMetadata(Brushes.DarkBlue));
        /// <summary> 鼠标按下背景样式 </summary>
        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty PressedForegroundProperty =
            DependencyProperty.Register("PressedForeground", typeof(Brush), typeof(FButton), new PropertyMetadata(Brushes.White));
        /// <summary> 鼠标按下前景样式（图标、文字） </summary>
        public Brush PressedForeground
        {
            get { return (Brush)GetValue(PressedForegroundProperty); }
            set { SetValue(PressedForegroundProperty, value); }
        }

        public static readonly DependencyProperty PressedBorderBrushProperty = DependencyProperty.Register("PressedBorderBrush", typeof(Brush), typeof(FButton), new PropertyMetadata(Brushes.Transparent));
        /// <summary> 鼠标按下边框样式 </summary>
        public Brush PressedBorderBrush
        {
            get { return (Brush)GetValue(PressedBorderBrushProperty); }
            set { SetValue(PressedBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty MouseOverBackgroundProperty =
            DependencyProperty.Register("MouseOverBackground", typeof(Brush), typeof(FButton), new PropertyMetadata(Brushes.RoyalBlue));
        /// <summary> 鼠标进入背景样式 </summary>
        public Brush MouseOverBackground
        {
            get { return (Brush)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }

        public static readonly DependencyProperty MouseOverForegroundProperty =
            DependencyProperty.Register("MouseOverForeground", typeof(Brush), typeof(FButton), new PropertyMetadata(Brushes.White));
        /// <summary> 鼠标进入前景样式 </summary>
        public Brush MouseOverForeground
        {
            get { return (Brush)GetValue(MouseOverForegroundProperty); }
            set { SetValue(MouseOverForegroundProperty, value); }
        }

        public static readonly DependencyProperty MouseOverBorderBrushProperty =
    DependencyProperty.Register("MouseOverBorderBrush", typeof(Brush), typeof(FButton), new PropertyMetadata(Brushes.Transparent));
        /// <summary> 鼠标进入边框样式 </summary>
        public Brush MouseOverBorderBrush
        {
            get { return (Brush)GetValue(MouseOverBorderBrushProperty); }
            set { SetValue(MouseOverBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty FIconProperty =
            DependencyProperty.Register("FIcon", typeof(string), typeof(FButton), new PropertyMetadata("\xe607"));
        /// <summary> 按钮字体图标编码 </summary>
        public string FIcon
        {
            get { return (string)GetValue(FIconProperty); }
            set { SetValue(FIconProperty, value); }
        }

        public static readonly DependencyProperty FIconSizeProperty =
            DependencyProperty.Register("FIconSize", typeof(int), typeof(FButton), new PropertyMetadata(20));
        /// <summary> 按钮字体图标大小 </summary>
        public int FIconSize
        {
            get { return (int)GetValue(FIconSizeProperty); }
            set { SetValue(FIconSizeProperty, value); }
        }

        public static readonly DependencyProperty FIconMarginProperty = DependencyProperty.Register(
            "FIconMargin", typeof(Thickness), typeof(FButton), new PropertyMetadata(new Thickness(0, 1, 3, 1)));
        /// <summary> 字体图标间距 </summary>
        public Thickness FIconMargin
        {
            get { return (Thickness)GetValue(FIconMarginProperty); }
            set { SetValue(FIconMarginProperty, value); }
        }

        public static readonly DependencyProperty AllowsAnimationProperty = DependencyProperty.Register(
            "AllowsAnimation", typeof(bool), typeof(FButton), new PropertyMetadata(true));
        /// <summary> 是否启用Ficon动画 </summary>
        public bool AllowsAnimation
        {
            get { return (bool)GetValue(AllowsAnimationProperty); }
            set { SetValue(AllowsAnimationProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(FButton), new PropertyMetadata(new CornerRadius(2)));
        /// <summary> 按钮圆角大小,左上，右上，右下，左下 </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty ContentDecorationsProperty = DependencyProperty.Register(
            "ContentDecorations", typeof(TextDecorationCollection), typeof(FButton), new PropertyMetadata(null));
        public TextDecorationCollection ContentDecorations
        {
            get { return (TextDecorationCollection)GetValue(ContentDecorationsProperty); }
            set { SetValue(ContentDecorationsProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(FButton), new PropertyMetadata(null));

        /// <summary> 图标和文字的布局方式 </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(ContentDecorationsProperty); }
            set { SetValue(ContentDecorationsProperty, value); }
        }

        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register("IsChecked", typeof(bool), typeof(FButton), new PropertyMetadata(false));
        /// <summary> 按钮是否被选中 </summary>
        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }


        public static readonly DependencyProperty IconFontWeightProperty =
      DependencyProperty.Register("IconFontWeight", typeof(FontWeight), typeof(FButton), new PropertyMetadata(null));
        /// <summary> 按钮字体图标编码 </summary>
        public FontWeight IconFontWeight
        {
            get { return (FontWeight)GetValue(IconFontWeightProperty); }
            set { SetValue(IconFontWeightProperty, value); }
        }

        static FButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FButton), new FrameworkPropertyMetadata(typeof(FButton)));
        }


    }

    /// <summary> 带有倒计时效果的按钮控件 </summary>
    public class FButtonCountDown : FButton
    {
        /// <summary> 开始或停止倒计时 </summary>
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(FButtonCountDown), new PropertyMetadata(default(bool), (d, e) =>
            {
                FButtonCountDown control = d as FButtonCountDown;

                if (control == null) return;

                if ((bool)e.NewValue)
                {
                    control.count = control.Count;

                    control.StopAll();

                    control.StartOne();
                }
                else
                {
                    control.StopAll();
                }
            }));

        TaskQueue _taskEngines = new TaskQueue();

        public void StopAll()
        {
            _taskEngines.StopAll();
        }

       

        public void StartOne()
        {
            TaskEngine engine = new TaskEngine();

            Action startOne = () =>
             {
                 while (true)
                 {
                     //  Do ：调用取消退出循环
                     if (engine.IsCancellationRequested)
                     {
                         break;
                     }

                     Thread.Sleep(1000);

                     Application.Current.Dispatcher.Invoke(() =>
                     {
                         this.Content = this.Content?.ToString().Split('(')[0] + $"({count = count - 1})";

                         if (count <= 0)
                         {
                             //  Do ：结束触发点击事件
                             this.Content = this.Content?.ToString().Split('(')[0];

                             this.Command?.Execute(this.CommandParameter);

                             this.IsActive = false;

                             //  Do ：结束触发点击事件
                             this.Content = this.Content?.ToString().Split('(')[0];

                         }
                     });
                 }

                 Application.Current.Dispatcher.Invoke(() =>
                 {
                     //  Do ：结束触发点击事件
                     this.Content = this.Content?.ToString().Split('(')[0];
                 });


             };

            _taskEngines.Enqueue(engine.Start(startOne));
        }

        int count = -1;

        public int Count
        {
            get
            {
                return this.Dispatcher.Invoke(() =>
                {
                    return (int)GetValue(CountProperty);
                });
            }
            set { SetValue(CountProperty, value); }
        }

        public static readonly DependencyProperty CountProperty =
            DependencyProperty.Register("Count", typeof(int), typeof(FButtonCountDown), new PropertyMetadata(5, (d, e) =>
            {
                FButtonCountDown control = d as FButtonCountDown;

                if (control == null) return;

            }));


        public FButtonCountDown()
        {
            //  Message：点击取消
            this.Click += (l, k) =>
            {
                this.IsActive = false;
            };

        }
    }

    public class TaskQueue : Queue<TaskEngine>
    {
        /// <summary> 停止所有任务 </summary>
        public void StopAll()
        {
            while (this.Count > 0)
            {
                this.Dequeue().Cancel();
            }
        }
    }

    public class TaskEngine
    {
        public Task Task { get; set; }

        CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        ManualResetEvent resetEvent = new ManualResetEvent(true);

        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
        }

        public bool IsCancellationRequested { get => _cancellationTokenSource.IsCancellationRequested; }

        public TaskEngine Start(Action action)
        {

            Task.Run(action, _cancellationTokenSource.Token);

            // 初始化为true时执行WaitOne不阻塞
            resetEvent.WaitOne();

            return this;
        }

        /// <summary> 暂停 </summary>
        public void Sleep()
        {
            resetEvent.Reset();
        }

        /// <summary> 继续 </summary>
        public void Continue()
        {
            resetEvent.Set();
        }
    }

   

}
