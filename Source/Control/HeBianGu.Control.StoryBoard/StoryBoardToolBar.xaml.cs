// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;


namespace HeBianGu.Control.StoryBoard
{
    [TemplatePart(Name = "PART_CENTER_THUMB", Type = typeof(ThumbBar))]

    [TemplatePart(Name = "PART_CENTER", Type = typeof(Canvas))]
    public class StoryBoardToolBar : ContentControl
    {
        static StoryBoardToolBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StoryBoardToolBar), new FrameworkPropertyMetadata(typeof(StoryBoardToolBar)));
        }

        public StoryBoardToolBar()
        {
            this.Loaded += (l, k) =>
              {
                  this.SetPosition();
              };
        }

        private ThumbBar _center_thumb = null;
        private Canvas _center = null;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this._center_thumb = Template.FindName("PART_CENTER_THUMB", this) as ThumbBar;

            this._center = Template.FindName("PART_CENTER", this) as Canvas;

            this._center_thumb.DragDeltaChanged += (l, k) =>
              {
                  ObjectRoutedEventArgs<ThumbType> from = k as ObjectRoutedEventArgs<ThumbType>;

                  this.RefreshData(from.Entity);

              };

            this._center_thumb.DragCompletedChanged += (l, k) =>
              {
                  this.OnValueChanged();

              };
        }

        public double LeftPercent
        {
            get { return (double)GetValue(LeftPercentProperty); }
            set { SetValue(LeftPercentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftPercent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftPercentProperty =
            DependencyProperty.Register("LeftPercent", typeof(double), typeof(StoryBoardToolBar), new PropertyMetadata(0.2, (l, e) =>
            {
                StoryBoardToolBar control = l as StoryBoardToolBar;

                //control.OnValueChanged();

                control.SetPosition();

            }));



        public double RightPercent
        {
            get { return (double)GetValue(RightPercentProperty); }
            set { SetValue(RightPercentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightPercent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightPercentProperty =
            DependencyProperty.Register("RightPercent", typeof(double), typeof(StoryBoardToolBar), new PropertyMetadata(0.8, (l, e) =>
            {
                StoryBoardToolBar control = l as StoryBoardToolBar;

                //control.OnValueChanged(); 

                control.SetPosition();
            }));

        //double paramValue = 500.0;

        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(StoryBoardToolBar), new PropertyMetadata(100.0, (d, e) =>
             {
                 StoryBoardToolBar control = d as StoryBoardToolBar;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));
        private double minSplite = 1.0;

        /// <summary> 更新数据 </summary>
        private void RefreshData(ThumbType thumb)
        {
            if (_flag)
            {
                _flag = false;

                Canvas parent = _center_thumb.Parent as Canvas;

                double left = Canvas.GetLeft(_center_thumb) / parent.ActualWidth;

                double right = (Canvas.GetLeft(_center_thumb) + this._center_thumb.ActualWidth) / parent.ActualWidth;

                if (thumb == ThumbType.Left)
                {
                    this.LeftPercent = (left * MaxValue - (left * MaxValue) % minSplite) / MaxValue;
                }
                else if (thumb == ThumbType.Right)
                {
                    this.RightPercent = (right * MaxValue - (right * MaxValue) % minSplite + minSplite) / MaxValue;
                }
                else
                {
                    //this.LeftPercent = Convert.ToDouble((int)(left * MaxValue)) / MaxValue;

                    //this.RightPercent = Convert.ToDouble((int)(right * MaxValue)) / MaxValue;

                    this.LeftPercent = (left * MaxValue - (left * MaxValue) % minSplite) / MaxValue;
                    this.RightPercent = (right * MaxValue - (right * MaxValue) % minSplite) / MaxValue;

                }


                Debug.WriteLine("左：" + this.LeftPercent + "   右：" + this.RightPercent);

                _flag = true;
            }
        }

        private bool _flag = true;

        /// <summary> 刷新位置 </summary>
        private void SetPosition()
        {
            if (this._center_thumb == null) return;

            if (_flag)
            {
                _flag = false;

                Canvas parent = _center_thumb.Parent as Canvas;

                Canvas.SetLeft(_center_thumb, this.LeftPercent * parent.ActualWidth);

                _center_thumb.Width = this.RightPercent * parent.ActualWidth - this.LeftPercent * parent.ActualWidth;

                _flag = true;
            }
        }


        //声明和注册路由事件
        public static readonly RoutedEvent ValueChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(StoryBoardToolBar));
        //CLR事件包装
        public event RoutedEventHandler ValueChanged
        {
            add { this.AddHandler(ValueChangedRoutedEvent, value); }
            remove { this.RemoveHandler(ValueChangedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnValueChanged()
        {
            RoutedEventArgs args = new RoutedEventArgs(ValueChangedRoutedEvent, this);
            this.RaiseEvent(args);
        }


        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
        }
    }

    [TemplatePart(Name = "PART_LEFT_THUMB", Type = typeof(Thumb))]
    [TemplatePart(Name = "PART_RIGHT_THUMB", Type = typeof(Thumb))]
    public class ThumbBar : Thumb
    {
        private Thumb _left_thumb = null;
        private Thumb _right_thumb = null;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this._left_thumb = this.Template.FindName("PART_LEFT_THUMB", this) as Thumb;
            this._right_thumb = this.Template.FindName("PART_RIGHT_THUMB", this) as Thumb;

            if (this._left_thumb == null || this._right_thumb == null) return;

            this._left_thumb.DragDelta += Thumb_Left_DragDelta;

            this._right_thumb.DragDelta += Thumb_Right_DragDelta;


            this._right_thumb.DragCompleted += (l, k) =>
              {
                  this.OnDragCompletedChanged();
              };

            this._left_thumb.DragCompleted += (l, k) =>
            {
                this.OnDragCompletedChanged();
            };


            this.DragDelta += Thumb_DragDelta;
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            if (this._left_thumb.IsMouseOver || this._right_thumb.IsMouseOver) return;

            Debug.WriteLine(Canvas.GetLeft(this));

            if ((Canvas.GetLeft(this) + e.HorizontalChange) < 0)
            {
                Canvas.SetLeft(this, 0);

                this.OnDragDeltaChanged();

                this.OnDragCompletedChanged();
                return;
            }

            Canvas parent = this.Parent as Canvas;

            if (parent.ActualWidth - Canvas.GetLeft(this) - this.Width - e.HorizontalChange < 0)
            {
                Canvas.SetLeft(this, parent.ActualWidth - this.Width);
                this.OnDragDeltaChanged();

                this.OnDragCompletedChanged();
                return;
            }

            Canvas.SetLeft(this, Canvas.GetLeft(this) + e.HorizontalChange);

            this.OnDragDeltaChanged();

            this.OnDragCompletedChanged();
        }

        private void Thumb_Left_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            if (this.Width < minValue && e.HorizontalChange > 0) return;

            if ((Canvas.GetLeft(this) + e.HorizontalChange) < 0)
            {
                Canvas.SetLeft(this, 0);
                return;
            }

            if (this.Width - e.HorizontalChange < 0) return;

            this.Width -= e.HorizontalChange;

            Canvas.SetLeft(this, Canvas.GetLeft(this) + e.HorizontalChange);

            e.Handled = true;

            this.OnDragDeltaChanged(ThumbType.Left);

        }

        private double minValue = 50;

        private void Thumb_Right_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {

            if (this.Width < minValue && e.HorizontalChange < 0) return;

            Canvas parent = this.Parent as Canvas;

            if (parent.ActualWidth - Canvas.GetLeft(this) - this.Width - e.HorizontalChange < 0)
            {
                Canvas.SetLeft(this, parent.ActualWidth - this.Width);
                return;
            }

            if (this.Width + e.HorizontalChange < 0) return;

            this.Width += e.HorizontalChange;

            e.Handled = true;

            this.OnDragDeltaChanged(ThumbType.Right);
        }


        //声明和注册路由事件
        public static readonly RoutedEvent DragDeltaChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("DragDeltaChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ThumbBar));
        //CLR事件包装
        public event RoutedEventHandler DragDeltaChanged
        {
            add { this.AddHandler(DragDeltaChangedRoutedEvent, value); }
            remove { this.RemoveHandler(DragDeltaChangedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnDragDeltaChanged(ThumbType thumbType = ThumbType.Center)
        {
            ObjectRoutedEventArgs<ThumbType> args = new ObjectRoutedEventArgs<ThumbType>(DragDeltaChangedRoutedEvent, this);


            args.Entity = thumbType;

            this.RaiseEvent(args);
        }


        //声明和注册路由事件
        public static readonly RoutedEvent DragCompletedChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("DragCompletedChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ThumbBar));
        //CLR事件包装
        public event RoutedEventHandler DragCompletedChanged
        {
            add { this.AddHandler(DragCompletedChangedRoutedEvent, value); }
            remove { this.RemoveHandler(DragCompletedChangedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnDragCompletedChanged()
        {
            RoutedEventArgs args = new RoutedEventArgs(DragCompletedChangedRoutedEvent, this);

            this.RaiseEvent(args);


            Debug.WriteLine("说明");

        }


    }

    public enum ThumbType
    {
        Left, Right, Center
    }





}