// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.Control.ImagePlayer
{
    /// <summary> 但点击当前控件时ListBoxItem项值也选中 </summary>
    public class ImageBaseMouseWheelBehavior : Behavior<ImageCore>
    {
        //public ImageBaseMouseWheelBehavior(ImageCore imageBase) : base(imageBase)
        //{

        //}
        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            ////gridMouse.MouseWheel += svImg_MouseWheel;

            AssociatedObject._svDefault.ScrollChanged += svImg_ScrollChanged;

            AssociatedObject.rootGrid.MouseWheel += svImg_MouseWheel;

            AssociatedObject.mask.LoationChanged += Mask_LoationChanged;
        }

        protected override void OnDetaching()
        {

            AssociatedObject._svDefault.ScrollChanged -= svImg_ScrollChanged;

            AssociatedObject.rootGrid.MouseWheel -= svImg_MouseWheel;

            AssociatedObject.mask.LoationChanged -= Mask_LoationChanged;


            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }

        private void Mask_LoationChanged(object sender, LoactionArgs e)
        {
            if (AssociatedObject.OperateType == OperateType.Bubble) return;

            Tuple<double, double> result = GetScrollWidthAndHeight();

            double xleft = (AssociatedObject.rootGrid.ActualWidth - AssociatedObject.vb.ActualWidth) / 2;
            double ytop = (AssociatedObject.rootGrid.ActualHeight - AssociatedObject.vb.ActualHeight) / 2;

            if (double.IsNaN(e.Left)) return;
            if (double.IsNaN(e.Top)) return;

            AssociatedObject._svDefault.ScrollToHorizontalOffset(e.Left * AssociatedObject._svDefault.ExtentWidth * result.Item1 + xleft);
            AssociatedObject._svDefault.ScrollToVerticalOffset(e.Top * AssociatedObject._svDefault.ExtentHeight * result.Item2 + ytop);
        }


        /// <summary> 滚动条放大缩小和新方法 </summary>
        private void svImg_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (AssociatedObject.OperateType == OperateType.Bubble) return;

            if (AssociatedObject.imgWidth == 0 || AssociatedObject.imgHeight == 0) return;

            //  Do：找出ViewBox的左边距和上边距
            double x_viewbox_margrn = (AssociatedObject.rootGrid.ActualWidth - AssociatedObject.vb.ActualWidth) / 2;
            double y_viewbox_margrn = (AssociatedObject.rootGrid.ActualHeight - AssociatedObject.vb.ActualHeight) / 2;

            //  Do：计算边距百分比
            double x_viewbox_margrn_percent = x_viewbox_margrn / AssociatedObject.rootGrid.ActualWidth;
            double y_viewbox_margrn_percent = y_viewbox_margrn / AssociatedObject.rootGrid.ActualHeight;

            //  Do：获取鼠标在绘图区域Canvas的位置的百分比
            Point position_canvas = e.GetPosition(AssociatedObject._centerCanvas);

            double x_position_canvas_percent = position_canvas.X / AssociatedObject._centerCanvas.ActualWidth;
            double y_position_canvas_percent = position_canvas.Y / AssociatedObject._centerCanvas.ActualHeight;

            //  Do：获取鼠标站显示窗口GridMouse中的位置
            Point position_gridMouse = e.GetPosition(AssociatedObject.grid_Mouse_drag);

            //  Message：设置图片的缩放 
            this.ChangeScale(e.Delta > 0 ? AssociatedObject.WheelScale : -AssociatedObject.WheelScale);

            //  Message：根据百分比计算放大后滚轮滚动的位置
            double xvalue = x_viewbox_margrn_percent * AssociatedObject.rootGrid.ActualWidth + x_position_canvas_percent * AssociatedObject.vb.ActualWidth - position_gridMouse.X;
            double yvalue = y_viewbox_margrn_percent * AssociatedObject.rootGrid.ActualHeight + y_position_canvas_percent * AssociatedObject.vb.ActualHeight - position_gridMouse.Y;

            AssociatedObject._svDefault.ScrollToHorizontalOffset(xvalue);
            AssociatedObject._svDefault.ScrollToVerticalOffset(yvalue);
        }

        private int thumbWidth;
        private int thumbHeight;

        private void svImg_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (AssociatedObject.OperateType == OperateType.Bubble) return;

            if (AssociatedObject.imgWidth == 0 || AssociatedObject.imgHeight == 0)
                return;

            thumbWidth = (int)AssociatedObject.controlmask.ActualWidth;
            thumbHeight = (int)AssociatedObject.controlmask.ActualHeight;

            double xleft = (AssociatedObject.rootGrid.ActualWidth - AssociatedObject.vb.ActualWidth) / 2;
            double ytop = (AssociatedObject.rootGrid.ActualHeight - AssociatedObject.vb.ActualHeight) / 2;


            Tuple<double, double> result = this.GetScrollWidthAndHeight();

            double scroll_width = AssociatedObject._svDefault.ViewportWidth + AssociatedObject._svDefault.ScrollableWidth;

            double timeH = AssociatedObject._svDefault.ViewportHeight / (AssociatedObject._svDefault.ViewportHeight + AssociatedObject._svDefault.ScrollableHeight);
            double timeW = AssociatedObject._svDefault.ViewportWidth / (scroll_width - 2 * xleft);

            double w = thumbWidth * timeW;
            double h = thumbHeight * timeH;

            double offsetx = 0;
            double offsety = 0;

            if (AssociatedObject._svDefault.ScrollableWidth == 0)
            {
                offsetx = 0;
            }
            else
            {
                offsetx = (w - thumbWidth) / AssociatedObject._svDefault.ScrollableWidth * (AssociatedObject._svDefault.HorizontalOffset);
            }

            offsetx = -(AssociatedObject._svDefault.HorizontalOffset - xleft) / (scroll_width - xleft * 2) * thumbWidth;

            if (AssociatedObject._svDefault.ScrollableHeight == 0)
            {
                offsety = 0;
            }
            else
            {
                offsety = (h - thumbHeight) / AssociatedObject._svDefault.ScrollableHeight * AssociatedObject._svDefault.VerticalOffset;
            }

            Rect rect = new Rect(-offsetx, -offsety, w, h);

            AssociatedObject.mask.UpdateSelectionRegion(rect);
        }

        private Tuple<double, double> GetScrollWidthAndHeight()
        {
            double xleft = 1 - (AssociatedObject.rootGrid.ActualWidth - AssociatedObject.vb.ActualWidth) / AssociatedObject.rootGrid.ActualWidth;
            double ytop = 1 - (AssociatedObject.rootGrid.ActualHeight - AssociatedObject.vb.ActualHeight) / AssociatedObject.rootGrid.ActualHeight;

            return Tuple.Create(xleft, ytop);
        }

        private double matchscale;

        private bool ChangeScale(double value)
        {
            matchscale = AssociatedObject.GetFullScale();

            AssociatedObject.Scale = AssociatedObject.Scale + value;

            //SetbtnActualsizeEnable();

            Debug.WriteLine(AssociatedObject.Scale);
            Debug.WriteLine(matchscale);

            //btnNarrow.IsEnabled = Scale > matchscale;

            //btnEnlarge.IsEnabled = Scale < MaxScale;

            AssociatedObject.IsMaxScaled = !(AssociatedObject.Scale < AssociatedObject.MaxScale);

            AssociatedObject.IsMinScaled = !(AssociatedObject.Scale > matchscale);

            //this.txtZoom.Text = ((int)(Scale * 100)).ToString() + "%";

            AssociatedObject.OnNoticeMessaged(((int)(AssociatedObject.Scale * 100)).ToString() + "%");

            //if (sb_Tip != null)
            //    sb_Tip.Begin();

            if (AssociatedObject.Scale < matchscale)
            {
                AssociatedObject.Scale = matchscale;

                AssociatedObject.OnNoticeMessaged("已最小");

                AssociatedObject._svDefault.IsEnabled = false;
            }

            if (AssociatedObject.Scale > AssociatedObject.MaxScale)
            {
                AssociatedObject.Scale = AssociatedObject.MaxScale;

                AssociatedObject._svDefault.IsEnabled = false;

                AssociatedObject.OnNoticeMessaged("已最大");
            }
            AssociatedObject._svDefault.IsEnabled = true;

            AssociatedObject.RefreshImageByScale();

            return true;
        }
    }
}
