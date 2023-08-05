// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// SpaceSpliterUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class SpaceSpliterUserControl : UserControl
    {
        public SpaceSpliterUserControl()
        {
            InitializeComponent();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.SetPosition();
        }

        public double LeftPercent
        {
            get { return (double)GetValue(LeftPercentProperty); }
            set { SetValue(LeftPercentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftPercent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftPercentProperty =
            DependencyProperty.Register("LeftPercent", typeof(double), typeof(SpaceSpliterUserControl),
                new FrameworkPropertyMetadata(0.2, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback((l, value) =>
                    {
                        SpaceSpliterUserControl control = l as SpaceSpliterUserControl;
                        control.SetPosition();
                    }),
                    new CoerceValueCallback((l, value) =>
                    {
                        //  Do ：添加验证 如限制大小 if(value<0) return 0;
                        return value;
                    })),
                new ValidateValueCallback(value =>
                {
                    //  Do ：添加验证有效值,if(value<0) return false; 小于0的值无效
                    return true;
                }));



        public double RightPercent
        {
            get { return (double)GetValue(RightPercentProperty); }
            set { SetValue(RightPercentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightPercent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightPercentProperty =
            DependencyProperty.Register("RightPercent", typeof(double), typeof(SpaceSpliterUserControl), new PropertyMetadata(0.8, (l, e) =>
            {
                SpaceSpliterUserControl control = l as SpaceSpliterUserControl;
                control.SetPosition();
            }));



        private void DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            Debug.WriteLine(Canvas.GetLeft(thumb1));

            if ((Canvas.GetLeft(thumb1) + e.HorizontalChange) < 0)
            {
                Canvas.SetLeft(thumb1, 0);
                this.RefreshData();
                return;
            }

            Canvas parent = thumb1.Parent as Canvas;

            if (parent.ActualWidth - Canvas.GetLeft(thumb1) - thumb1.Width - e.HorizontalChange < 0)
            {
                Canvas.SetLeft(thumb1, parent.ActualWidth - thumb1.Width);
                this.RefreshData();
                return;
            }

            Canvas.SetLeft(thumb1, Canvas.GetLeft(thumb1) + e.HorizontalChange);

            this.RefreshData();
        }

        private void DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            //thumb1.Background = Brushes.Green;
        }

        private void DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            //thumb1.Background = Brushes.LightBlue;
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            if ((Canvas.GetLeft(thumb1) + e.HorizontalChange) < 0)
            {
                Canvas.SetLeft(thumb1, 0);
                return;
            }

            if (thumb1.Width - e.HorizontalChange < 0) return;

            thumb1.Width -= e.HorizontalChange;

            this.RefreshData();



        }

        private void Thumb_DragDelta_1(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {

            Canvas parent = thumb1.Parent as Canvas;

            if (parent.ActualWidth - Canvas.GetLeft(thumb1) - thumb1.Width - e.HorizontalChange < 0)
            {
                Canvas.SetLeft(thumb1, parent.ActualWidth - thumb1.Width);
                return;
            }

            if (thumb1.Width + e.HorizontalChange < 0) return;

            thumb1.Width += e.HorizontalChange;

            e.Handled = true;

            this.RefreshData();

        }


        /// <summary> 更新数据 </summary>
        private void RefreshData()
        {
            if (_flag)
            {
                _flag = false;

                Canvas parent = thumb1.Parent as Canvas;

                this.LeftPercent = Canvas.GetLeft(thumb1) / parent.ActualWidth;

                this.RightPercent = (Canvas.GetLeft(thumb1) + this.thumb1.Width) / parent.ActualWidth;

                Debug.WriteLine("左：" + this.LeftPercent + "   右：" + this.RightPercent);

                _flag = true;
            }

        }

        private bool _flag = true;

        /// <summary> 刷新位置 </summary>
        private void SetPosition()
        {
            if (_flag)
            {
                _flag = false;

                Canvas parent = thumb1.Parent as Canvas;

                Canvas.SetLeft(thumb1, this.LeftPercent * parent.ActualWidth);

                thumb1.Width = this.RightPercent * parent.ActualWidth - this.LeftPercent * parent.ActualWidth;

                _flag = true;
            }

        }

    }
}
