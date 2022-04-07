// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace HeBianGu.Base.WpfBase
{
    /// <summary> ScrollViewer带有鼠标拖动和触摸拖动效果 </summary>
    public class ScrollViewMouseDragBehavior : Behavior<ScrollViewer>
    {
        private ScrollViewer scrollViewer;

        protected override void OnAttached()
        {

            scrollViewer = AssociatedObject;

            if (scrollViewer == null) return;


            scrollViewer.PreviewMouseDown += AssociatedObject_PreviewMouseDown;

            scrollViewer.PreviewMouseMove += AssociatedObject_PreviewMouseMove;

            scrollViewer.TouchDown += AssociatedObject_TouchDown;

            scrollViewer.PreviewTouchMove += AssociatedObject_PreviewTouchMove;
        }

        protected override void OnDetaching()
        {
            if (scrollViewer == null) return;

            scrollViewer.PreviewMouseDown -= AssociatedObject_PreviewMouseDown;

            scrollViewer.PreviewMouseMove -= AssociatedObject_PreviewMouseMove;


            scrollViewer.TouchDown -= AssociatedObject_TouchDown;

            scrollViewer.PreviewTouchMove -= AssociatedObject_PreviewTouchMove;
        }



        /// <summary> 是否到达顶部 </summary>
        public bool IsTopped
        {
            get { return (bool)GetValue(IsToppedProperty); }
            set { SetValue(IsToppedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsToppedProperty =
            DependencyProperty.Register("IsTopped", typeof(bool), typeof(ScrollViewMouseDragBehavior), new PropertyMetadata(default(bool), (d, e) =>
             {
                 ScrollViewMouseDragBehavior control = d as ScrollViewMouseDragBehavior;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));

        /// <summary> 是否到达底部 </summary>
        public bool IsBottomed
        {
            get { return (bool)GetValue(IsBottomedProperty); }
            set { SetValue(IsBottomedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBottomedProperty =
            DependencyProperty.Register("IsBottomed", typeof(bool), typeof(ScrollViewMouseDragBehavior), new PropertyMetadata(default(bool), (d, e) =>
             {
                 ScrollViewMouseDragBehavior control = d as ScrollViewMouseDragBehavior;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));

        private void AssociatedObject_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;

            this.ScrollMove(e.Source);
        }

        private void AssociatedObject_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.GetLast(e.Source);
        }


        private void AssociatedObject_PreviewTouchMove(object sender, TouchEventArgs e)
        {
            this.ScrollMove(e.Source);
        }

        private void AssociatedObject_TouchDown(object sender, TouchEventArgs e)
        {
            this.GetLast(e.Source);
        }

        private void GetLast(object source)
        {
            Point pp = Mouse.GetPosition(source as FrameworkElement);

            Point temp = (source as FrameworkElement).PointToScreen(pp);

            last = temp;
        }

        private Point last;

        private void ScrollMove(object source)
        {
            Point pp = Mouse.GetPosition(source as FrameworkElement);

            Point temp = (source as FrameworkElement).PointToScreen(pp);

            double y = temp.Y - last.Y;

            this.scrollViewer.ScrollToVerticalOffset(this.scrollViewer.VerticalOffset - y);

            last = temp;

            this.CheckPosition();


        }

        private void CheckPosition()
        {
            if (this.scrollViewer.VerticalOffset == 0)
            {
                this.IsTopped = true;
            }

            else if (IsVerticalScrollBarAtBottom(this.scrollViewer))
            {
                this.IsBottomed = true;
            }
            else
            {
                this.IsTopped = false;
                this.IsBottomed = false;
            }
        }

        public bool IsVerticalScrollBarAtBottom(ScrollViewer s)
        {
            bool isAtButtom = false;

            double dVer = s.VerticalOffset;

            double dViewport = s.ViewportHeight;

            double dExtent = s.ExtentHeight;

            if (dVer != 0)
            {
                if (dVer + dViewport == dExtent)
                {
                    isAtButtom = true;
                }
                else
                {
                    isAtButtom = false;
                }
            }
            else
            {
                isAtButtom = false;
            }
            return isAtButtom;
        }
    }
}