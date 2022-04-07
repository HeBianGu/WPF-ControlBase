// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Windows;
using System.Windows.Input;

namespace HeBianGu.Control.ImagePlayer
{
    /// <summary> 但点击当前控件时ListBoxItem项值也选中 </summary>
    public class ImageBaseMouseDragBehavior : Behavior<ImageCore>
    {
        //public ImageBaseMouseDragBehavior(ImageCore imageBase) : base(imageBase)
        //{

        //}
        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.grid_Mouse_drag.PreviewMouseDown += control_MouseDown;
            AssociatedObject.grid_Mouse_drag.PreviewMouseUp += control_MouseUp;
            AssociatedObject.grid_Mouse_drag.PreviewMouseMove += control_MouseMove;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.grid_Mouse_drag.PreviewMouseDown -= control_MouseDown;
            AssociatedObject.grid_Mouse_drag.PreviewMouseUp -= control_MouseUp;
            AssociatedObject.grid_Mouse_drag.PreviewMouseMove -= control_MouseMove;

            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }

        #region - 鸟撖图操作 -

        private bool mouseDown;
        private Point mouseXY;

        protected void control_MouseMove(object sender, MouseEventArgs e)
        {
            if ((AssociatedObject.OperateType != OperateType.Default) && e.MiddleButton == MouseButtonState.Released)
            {
                return;
            }

            if (AssociatedObject.imgWidth == 0 || AssociatedObject.imgHeight == 0)
                return;

            UIElement img = sender as UIElement;
            if (img == null)
            {
                return;
            }
            if (mouseDown)
            {
                Domousemove(img, e);
            }
        }

        private void Domousemove(UIElement img, MouseEventArgs e)
        {
            if ((AssociatedObject.OperateType != OperateType.Default) && e.MiddleButton == MouseButtonState.Released)
            {
                return;
            }

            Point position = e.GetPosition(img);
            double X = mouseXY.X - position.X;
            double Y = mouseXY.Y - position.Y;
            mouseXY = position;
            if (X != 0)
                AssociatedObject._svDefault.ScrollToHorizontalOffset(AssociatedObject._svDefault.HorizontalOffset + X);
            if (Y != 0)
                AssociatedObject._svDefault.ScrollToVerticalOffset(AssociatedObject._svDefault.VerticalOffset + Y);

            AssociatedObject.GetOffSetRate();
        }

        protected void control_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if ((AssociatedObject.OperateType != OperateType.Default && e.ChangedButton != MouseButton.Middle))
            {
                return;
            }

            UIElement img = sender as UIElement;


            if (img == null)
            {
                return;
            }
            img.ReleaseMouseCapture();

            mouseDown = false;


            //this.RefreshCursor();
        }

        protected void control_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((AssociatedObject.OperateType != OperateType.Default && e.ChangedButton != MouseButton.Middle))
            {
                return;
            }

            AssociatedObject.Cursor = Cursors.Hand;

            //if (e.ChangedButton == MouseButton.Middle)
            //{
            //    AssociatedObject.Cursor = Cursors.Hand;
            //}
            //else
            //{
            //    if ((AssociatedObject.OperateType != OperateType.Default)) return;
            //}

            UIElement img = sender as UIElement;

            if (img == null)
            {
                return;
            }

            img.CaptureMouse();

            mouseDown = true;

            mouseXY = e.GetPosition(img);

        }

        #endregion
    }

    public class ImageBaseMouseRightDragBehavior : ImageBaseMouseDragBehavior
    {
        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.grid_Mouse_drag.PreviewMouseRightButtonDown += control_MouseDown;
            AssociatedObject.grid_Mouse_drag.PreviewMouseRightButtonUp += control_MouseUp;
            AssociatedObject.grid_Mouse_drag.PreviewMouseMove += control_MouseMove;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.grid_Mouse_drag.PreviewMouseRightButtonDown -= control_MouseDown;
            AssociatedObject.grid_Mouse_drag.PreviewMouseRightButtonUp -= control_MouseUp;
            AssociatedObject.grid_Mouse_drag.PreviewMouseMove -= control_MouseMove;

            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }
    }
}
