// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.Control.Host
{
    public class MouseHost : ItemsHost
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(MouseHost), "S.MouseHost.Default");

        static MouseHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MouseHost), new FrameworkPropertyMetadata(typeof(MouseHost)));
        }

        public MouseHost()
        {
            this.PreviewMouseDown += (l, k) =>
              {
                  Point p = Mouse.GetPosition(this);
                  PointHitTestParameters parameters = new PointHitTestParameters(p);
                  VisualTreeHelper.HitTest(this, HitTestFilter, HitTestCallBack, parameters);
              };
        }

        private HitTestResultBehavior HitTestCallBack(HitTestResult result)
        {
            return HitTestResultBehavior.Continue;
        }

        private HitTestFilterBehavior HitTestFilter(DependencyObject obj)
        {
            object data = MouseHost.GetData(obj);
            if (data == null)
            {
                return HitTestFilterBehavior.Continue;
            }

            if (data is FrameworkElement element)
            {
                element.Measure(this.DesiredSize);
                //element.Arrange(new Rect(0, 0, element.DesiredSize.Width, element.DesiredSize.Height));
                this.Container.Width = element.DesiredSize.Width;
                this.Container.Height = element.DesiredSize.Height;
            }
            //  Do ：连续点击停止上一次动画
            storyboard?.Stop();
            //  Do ：触发loaded
            this.Container.Content = null;
            this.Container.Content = data;
            Point p = Mouse.GetPosition(this);
            this.Location(p);
            this.Begin();

            return HitTestFilterBehavior.Stop;
        }

        private Storyboard storyboard;
        public void Begin()
        {
            storyboard = StoryboardFactory.Create();
            storyboard.Completed += (l, k) =>
            {
                //  Do ：移除控件
                this.Container.Content = null;
            };
            ObjectAnimationUsingKeyFrames frames = new ObjectAnimationUsingKeyFrames();
            DiscreteObjectKeyFrame key1 = new DiscreteObjectKeyFrame(Visibility.Visible, KeyTime.FromTimeSpan(TimeSpan.Zero));
            frames.KeyFrames.Add(key1);
            DiscreteObjectKeyFrame key2 = new DiscreteObjectKeyFrame(Visibility.Hidden, KeyTime.FromTimeSpan(this.Duration.TimeSpan));
            frames.KeyFrames.Add(key2);
            Storyboard.SetTarget(frames, this.Container);
            Storyboard.SetTargetProperty(frames, new PropertyPath(FrameworkElement.VisibilityProperty));
            storyboard.Children.Add(frames);
            storyboard.FillBehavior = FillBehavior.HoldEnd;
            storyboard.Begin();
        }

        public static object GetData(DependencyObject obj)
        {
            return obj.GetValue(DataProperty);
        }

        public static void SetData(DependencyObject obj, object value)
        {
            obj.SetValue(DataProperty, value);
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.RegisterAttached("Data", typeof(object), typeof(MouseHost), new PropertyMetadata(null, OnDataChanged));
        public static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;
            object n = e.NewValue;
            object o = e.OldValue;
        }

    }
}
