// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.Control.ImagePlayer
{
    /// <summary> 但点击当前控件时ListBoxItem项值也选中 </summary>
    public abstract class ImageBaseMouseDrawRectBehaviorBase : Behavior<ImageCore>
    {
        //public ImageBaseMouseDrawRectBehaviorBase(ImageCore imageBase) : base(imageBase)
        //{

        //}

        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject._centerCanvas.MouseMove += InkCanvas_MouseMove;
            AssociatedObject._centerCanvas.MouseDown += InkCanvas_MouseDown;
            AssociatedObject._centerCanvas.MouseUp += InkCanvas_MouseUp;
            AssociatedObject._centerCanvas.MouseLeave += InkCanvas_MouseLeave;

            //  Do ：刷新初始化定位
            AssociatedObject.ShowShape(AssociatedObject.Location);
        }


        protected override void OnDetaching()
        {
            AssociatedObject._centerCanvas.MouseMove -= InkCanvas_MouseMove;
            AssociatedObject._centerCanvas.MouseDown -= InkCanvas_MouseDown;
            AssociatedObject._centerCanvas.MouseUp -= InkCanvas_MouseUp;
            AssociatedObject._centerCanvas.MouseLeave -= InkCanvas_MouseLeave;

            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }

        #region - 绘制矩形框和相关操作 -

        private System.Windows.Point start;
        private void InkCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!this.IsMatch()) return;

            AssociatedObject._dynamic.BegionMatch(true);

            start = e.GetPosition(sender as InkCanvas);
        }
        private void InkCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.IsMatch()) return;

            if (e.LeftButton != MouseButtonState.Pressed) return;

            if (this.start.X <= 0) return;

            Point end = e.GetPosition(AssociatedObject._centerCanvas);

            //this._isMatch = Math.Abs(start.X - end.X) > 50 && Math.Abs(start.Y - end.Y) > 50;

            AssociatedObject._dynamic.Visibility = Visibility.Visible;

            AssociatedObject._dynamic.Refresh(start, end);

        }
        private void InkCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!this.IsMatch()) return;

            //  Do：检查选择区域是否可用
            if (!AssociatedObject._dynamic.IsMatch())
            {
                AssociatedObject._dynamic.Visibility = Visibility.Collapsed;
                return;
            };

            if (this.start.X <= 0) return;

            this.OnOverDrawRect(AssociatedObject._dynamic.Rect);

            AssociatedObject._dynamic.Visibility = Visibility.Collapsed;

            ////  Message：设置只允许放大一次
            //this.SetMarkType(MarkType.None);

            //  Do：将数据初始化
            start = new System.Windows.Point(-1, -1);


        }
        private void InkCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.start.X <= 0) return;

            if (e.LeftButton == MouseButtonState.Released) return;

            InkCanvas_MouseUp(sender, null);
        }

        #endregion

        protected abstract void OnOverDrawRect(Rect rect);

        protected abstract bool IsMatch();

    }
}
