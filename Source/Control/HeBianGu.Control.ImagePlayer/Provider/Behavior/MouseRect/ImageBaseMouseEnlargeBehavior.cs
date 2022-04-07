// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.ImagePlayer
{
    /// <summary> 但点击当前控件时ListBoxItem项值也选中 </summary>
    public class ImageBaseMouseEnlargeBehavior : ImageBaseMouseDrawRectBehaviorBase
    {
        //public ImageBaseMouseEnlargeBehavior(ImageCore imageBase) : base(imageBase)
        //{

        //}
        #region - 绘制矩形框和相关操作 -

        private void ShowPartWithShape(RectangleShape rectangle)
        {
            RectangleGeometry rect = new RectangleGeometry(new Rect(0, 0, AssociatedObject._centerCanvas.ActualWidth, AssociatedObject._centerCanvas.ActualHeight));

            //  Do：设置覆盖的蒙版
            PathGeometry geo = Geometry.Combine(rect, new RectangleGeometry(rectangle.Rect), GeometryCombineMode.Exclude, null);

            DynamicShape shape = new DynamicShape(rectangle);

            AssociatedObject.ShowShape(rectangle.Rect);

            AssociatedObject._dynamic.Visibility = Visibility.Collapsed;
        }

        protected override void OnOverDrawRect(Rect rect)
        {
            AssociatedObject.ShowShape(rect);
        }

        protected override bool IsMatch()
        {
            return AssociatedObject.OperateType == OperateType.Enlarge;
        }

        #endregion
    }
}
