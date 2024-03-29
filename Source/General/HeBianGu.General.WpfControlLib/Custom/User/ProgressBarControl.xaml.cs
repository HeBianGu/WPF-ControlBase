﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// 进度条
    /// </summary>
    [Obsolete("效果不佳 不用了")]
    public partial class ProgressBarControl : UserControl
    {
        private DispatcherTimer animationTimer;

        private ProgressBarDataModel _dataModel;

        #region 构造方法与加载

        public ProgressBarControl()
        {
            InitializeComponent();

        }
        /// <summary>
        /// 加载后刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressBarControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            animationTimer = new DispatcherTimer(DispatcherPriority.ContextIdle, Dispatcher);

            //指定时间间隔
            animationTimer.Interval = new TimeSpan(0, 0, 0, 0, TimeSpan);

            if (EllipseCount < 1)
            {
                EllipseCount = 12;
            }

            ProgressBarCanvas.Children.Clear();

            for (int i = 0; i < EllipseCount; i++)
            {
                ProgressBarCanvas.Children.Add(new Ellipse());
            }
            ProgressBarDataModel dataModel = new ProgressBarDataModel()
            {
                CanvasSize = CanvasSize,
                EclipseSize = EllipseSize
            };
            _dataModel = dataModel;
            this.DataContext = dataModel;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置圆圈数量
        /// 默认12
        /// </summary>
        public double EllipseCount
        {
            get { return (double)GetValue(EllipseCountProperty); }
            set { SetValue(EllipseCountProperty, value); }
        }
        public static readonly DependencyProperty EllipseCountProperty =
            DependencyProperty.Register("EllipseCount", typeof(double), typeof(ProgressBarControl),
            new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 获取或设置圆圈大小
        /// 默认10
        /// </summary>
        public double EllipseSize
        {
            get { return (double)GetValue(EllipseSizeProperty); }
            set { SetValue(EllipseSizeProperty, value); }
        }
        public static readonly DependencyProperty EllipseSizeProperty =
            DependencyProperty.Register("EllipseSize", typeof(double), typeof(ProgressBarControl),
            new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 获取或设置面板大小
        /// 默认80
        /// </summary>
        public double CanvasSize
        {
            get { return (double)GetValue(CanvasSizeProperty); }
            set { SetValue(CanvasSizeProperty, value); }
        }
        public static readonly DependencyProperty CanvasSizeProperty =
            DependencyProperty.Register("CanvasSize", typeof(double), typeof(ProgressBarControl),
            new FrameworkPropertyMetadata(80.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 获取或设置每次旋转角度
        /// 默认10.0
        /// </summary>
        public double StepAngle
        {
            get { return (double)GetValue(StepAngleProperty); }
            set { SetValue(StepAngleProperty, value); }
        }

        public static readonly DependencyProperty StepAngleProperty =
            DependencyProperty.Register("StepAngle", typeof(double), typeof(ProgressBarControl),
            new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender));
        /// <summary>
        /// 获取或设置每次旋转间隔时间（毫秒）
        /// 默认100毫秒
        /// </summary>
        public int TimeSpan
        {
            get { return (int)GetValue(TimeSpanProperty); }
            set { SetValue(TimeSpanProperty, value); }
        }
        public static readonly DependencyProperty TimeSpanProperty =
            DependencyProperty.Register("TimeSpan", typeof(int), typeof(ProgressBarControl),
            new FrameworkPropertyMetadata(100, FrameworkPropertyMetadataOptions.AffectsRender));

        #endregion

        #region 方法
        /// <summary>
        /// Canvas加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleLoaded(object sender, RoutedEventArgs e)
        {
            //设置设置圆的位置和旋转角度
            SetEclipsePosition(_dataModel);
            //DesignerProperties   提供用于与设计器进行通信的附加属性。
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                if (this.Visibility == System.Windows.Visibility.Visible)
                {
                    //超过计时器间隔时发生。
                    animationTimer.Tick += HandleAnimationTick;
                    animationTimer.Start();
                }
            }
        }

        /// <summary>
        /// 设置圆的位置和旋转角度
        /// </summary>
        private void SetEclipsePosition(ProgressBarDataModel dataModel)
        {
            //圆周长就是：C = π * d 或者C=2*π*r（其中d是圆的直径，r是圆的半径）
            double r = dataModel.R;

            UIElementCollection children = ProgressBarCanvas.Children;
            int count = children.Count;
            double step = (Math.PI * 2) / count;

            //根据圆中正弦、余弦计算距离
            int index = 0;
            foreach (object element in children)
            {
                Ellipse ellipse = element as Ellipse;
                //透明度
                double opacity = Convert.ToDouble(index) / (count - 1);
                ellipse.SetValue(UIElement.OpacityProperty, opacity < 0.05 ? 0.05 : opacity);
                //距离
                double left = r + Math.Sin(step * index) * r;
                ellipse.SetValue(Canvas.LeftProperty, left);
                double top = r - Math.Cos(step * index) * r;
                ellipse.SetValue(Canvas.TopProperty, top);

                index++;
            }
        }

        /// <summary>  设置透明度  </summary>
        private void SetEclipseOpacity(int param)
        {
            UIElementCollection children = ProgressBarCanvas.Children;
            int count = children.Count;
            //根据圆中正弦、余弦计算距离
            int index = 0;

            foreach (object element in children)
            {
                Ellipse ellipse = element as Ellipse;

                int c = (param + index) % (count - 1);

                //透明度
                double opacity = Convert.ToDouble(c) / (count - 1);

                ellipse.SetValue(UIElement.OpacityProperty, opacity < 0.05 ? 0.05 : opacity);

                index++;
            }
        }

        /// <summary>
        /// Canvas卸载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleUnloaded(object sender, RoutedEventArgs e)
        {
            if (animationTimer == null) return;

            animationTimer.Stop();
            //除去委托
            animationTimer.Tick -= HandleAnimationTick;
        }

        private int _current = 0;
        /// <summary>
        /// 超过计时器间隔时发生。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleAnimationTick(object sender, EventArgs e)
        {
            ////设置旋转角度
            //SpinnerRotate.Angle = (SpinnerRotate.Angle + StepAngle) % 360;

            _current++;

            int count = ProgressBarCanvas.Children.Count;

            _current = _current % count;

            this.SetEclipseOpacity(_current);
        }
        #endregion
    }

    /// <summary>
    /// 进度条Model类
    /// </summary>
    public class ProgressBarDataModel
    {
        public double EclipseSize { get; set; }
        public double CanvasSize { get; set; }
        public double ViewBoxSize
        {
            get
            {
                double length = Convert.ToDouble(CanvasSize) - Convert.ToDouble(EclipseSize);
                return length;
            }
        }
        public double EclipseLeftLength
        {
            get
            {
                double length = Convert.ToDouble(CanvasSize) / 2;
                return length;
            }
        }
        public double R
        {
            get
            {
                double length = (Convert.ToDouble(CanvasSize) - Convert.ToDouble(EclipseSize)) / 2;
                return length;
            }
        }
    }
}
