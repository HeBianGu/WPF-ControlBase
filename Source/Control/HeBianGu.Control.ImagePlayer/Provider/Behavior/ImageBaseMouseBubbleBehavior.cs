// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.Control.ImagePlayer
{
    /// <summary> 但点击当前控件时ListBoxItem项值也选中 </summary>
    public class ImageBaseMouseBubbleBehavior : Behavior<ImageCore>
    {
        //public ImageBaseMouseBubbleBehavior(ImageCore imageBase):base(imageBase)
        //{

        //}

        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject._centerCanvas.MouseMove += InkCanvas_MouseMove;
            AssociatedObject._centerCanvas.MouseEnter += _centerCanvas_MouseEnter;
            AssociatedObject._centerCanvas.MouseLeave += _centerCanvas_MouseLeave;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;

            AssociatedObject._centerCanvas.MouseMove -= InkCanvas_MouseMove;
            AssociatedObject._centerCanvas.MouseEnter -= _centerCanvas_MouseEnter;
            AssociatedObject._centerCanvas.MouseLeave -= _centerCanvas_MouseLeave;
        }

        #region - 放大镜效果 -


        private void InkCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (AssociatedObject.OperateType != OperateType.Bubble) return;

            FrameworkElement element = sender as FrameworkElement;

            // 计算鼠标在X轴的移动距离
            double deltaV = e.GetPosition(element).Y;
            //计算鼠标在Y轴的移动距离
            double deltaH = e.GetPosition(element).X;

            double newTop = deltaV - AssociatedObject.MoveRect.ActualHeight / 2 <= 0 ? 0 : deltaV - AssociatedObject.MoveRect.ActualHeight / 2;
            double newLeft = deltaH - AssociatedObject.MoveRect.ActualWidth / 2 <= 0 ? 0 : deltaH - AssociatedObject.MoveRect.ActualWidth / 2;

            newTop = deltaV + AssociatedObject.MoveRect.ActualHeight / 2 > AssociatedObject._centerCanvas.ActualHeight ? AssociatedObject._centerCanvas.ActualHeight - AssociatedObject.MoveRect.ActualHeight : newTop;
            newLeft = deltaH + AssociatedObject.MoveRect.ActualWidth / 2 > AssociatedObject._centerCanvas.ActualWidth ? AssociatedObject._centerCanvas.ActualWidth - AssociatedObject.MoveRect.ActualWidth : newLeft;

            AssociatedObject.MoveRect.SetValue(InkCanvas.TopProperty, newTop);
            AssociatedObject.MoveRect.SetValue(InkCanvas.LeftProperty, newLeft);

            AdjustBigImage();

            Point position2 = Mouse.GetPosition(AssociatedObject.grid_all);

            AssociatedObject.popup.Margin = new Thickness(position2.X - AssociatedObject.popup.Width / 2, position2.Y - AssociatedObject.popup.Height / 2, 0, 0);

            //if (this.start.X <= 0 || this.start.Y <= 0) { return; }

            return;

        }


        private void _centerCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            if (AssociatedObject.OperateType != OperateType.Bubble) return;

            AssociatedObject.popup.Visibility = Visibility.Collapsed;
        }

        private void _centerCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            if (AssociatedObject.OperateType != OperateType.Bubble) return;
            AssociatedObject.popup.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 调整右侧大图的位置
        /// </summary>
        private void AdjustBigImage()
        {
            //获取右侧大图框与透明矩形框的尺寸比率
            double n = AssociatedObject.BigBox.Width / AssociatedObject.MoveRect.Width;

            //获取半透明矩形框在左侧小图中的位置
            double left = (double)AssociatedObject.MoveRect.GetValue(InkCanvas.LeftProperty);
            double top = (double)AssociatedObject.MoveRect.GetValue(InkCanvas.TopProperty);

            //计算和设置原图在右侧大图框中的Canvas.Left 和 Canvas.Top
            double newLeft = -left * n;
            double newTop = -top * n;
            AssociatedObject.bigImg.SetValue(Canvas.LeftProperty, newLeft);
            AssociatedObject.bigImg.SetValue(Canvas.TopProperty, newTop);
        }

        #endregion
    }


    //public abstract class Behavior<T>: IBehavior
    //{
    //    public T AssociatedObject { get; set; }

    //    public Behavior(T t)
    //    {
    //        AssociatedObject = t;
    //    }

    //    protected abstract void OnAttached();

    //    protected abstract void OnDetaching();

    //    public void RegisterBehavior()
    //    {
    //        this.OnAttached();
    //    }
    //}

    //public interface IBehavior
    //{
    //    void RegisterBehavior();
    //}
}
