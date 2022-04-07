// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HeBianGu.Control.ImagePlayer
{
    public class MaskCanvas : Canvas
    {
        public MaskCanvas()
        {
            //Loaded += OnLoaded;

            maskRectLeft.Fill = maskRectRight.Fill = maskRectTop.Fill = maskRectBottom.Fill = MaskWindowBackground;

            SetLeft(maskRectLeft, 0);
            SetTop(maskRectLeft, 0);
            SetRight(maskRectRight, 0);
            SetTop(maskRectRight, 0);
            SetTop(maskRectTop, 0);
            SetBottom(maskRectBottom, 0);
            maskRectLeft.Height = ActualHeight;

            Children.Add(maskRectLeft);
            Children.Add(maskRectRight);
            Children.Add(maskRectTop);
            Children.Add(maskRectBottom);

            selectionBorder.Stroke = SelectionBorderBrush;
            selectionBorder.StrokeThickness = 1;

            Children.Add(selectionBorder);

            Indicator = new IndicatorObject(this);
            Indicator.Visibility = System.Windows.Visibility.Hidden;
            Indicator.Background = Brushes.Transparent;
            Children.Add(Indicator);

            CompositionTarget.Rendering += OnCompositionTargetRendering;
        }

        public System.Windows.Media.Brush SelectionBorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 255, 255));
        public Thickness SelectionBorderThickness = new Thickness(1);

        //public System.Windows.Media.Brush MaskWindowBackground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(5, 0, 0, 0));

        public System.Windows.Media.Brush MaskWindowBackground = new SolidColorBrush(Colors.Black) { Opacity = 0.5 };

        public event EventHandler<LoactionArgs> LoationChanged;

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            maskRectLeft.Fill = maskRectRight.Fill = maskRectTop.Fill = maskRectBottom.Fill = MaskWindowBackground;

            SetLeft(maskRectLeft, 0);
            SetTop(maskRectLeft, 0);
            SetRight(maskRectRight, 0);
            SetTop(maskRectRight, 0);
            SetTop(maskRectTop, 0);
            SetBottom(maskRectBottom, 0);
            maskRectLeft.Height = ActualHeight;

            Children.Add(maskRectLeft);
            Children.Add(maskRectRight);
            Children.Add(maskRectTop);
            Children.Add(maskRectBottom);

            selectionBorder.Stroke = SelectionBorderBrush;
            selectionBorder.StrokeThickness = 1;

            Children.Add(selectionBorder);

            Indicator = new IndicatorObject(this);
            Indicator.Visibility = System.Windows.Visibility.Hidden;
            Indicator.Background = Brushes.Transparent;
            Children.Add(Indicator);

            CompositionTarget.Rendering += OnCompositionTargetRendering;



        }

        private void UpdateSelectionBorderLayout()
        {
            if (!selectionRegion.IsEmpty)
            {
                SetLeft(selectionBorder, selectionRegion.Left);
                SetTop(selectionBorder, selectionRegion.Top);
                selectionBorder.Width = selectionRegion.Width;
                selectionBorder.Height = selectionRegion.Height;
            }
        }

        private void UpdateMaskRectanglesLayout()
        {
            double actualHeight = ActualHeight;
            double actualWidth = ActualWidth;

            if (selectionRegion.IsEmpty)
            {
                SetLeft(maskRectLeft, 0);
                SetTop(maskRectLeft, 0);
                maskRectLeft.Width = actualWidth;
                maskRectLeft.Height = actualHeight;

                maskRectRight.Width = maskRectRight.Height = maskRectTop.Width = maskRectTop.Height = maskRectBottom.Width = maskRectBottom.Height = 0;
            }
            else
            {
                double temp = selectionRegion.Left;

                SetLeft(maskRectLeft, 0);
                SetTop(maskRectLeft, 0);
                maskRectLeft.Width = actualWidth;
                maskRectLeft.Height = actualHeight;
                //maskRectRight.Width = maskRectRight.Height = maskRectTop.Width = maskRectTop.Height = maskRectBottom.Width = maskRectBottom.Height = 0;


                if (maskRectLeft.Width != temp)
                {
                    maskRectLeft.Width = temp < 0 ? 0 : temp; //Math.Max(0, selectionRegion.Left);
                }

                temp = ActualWidth - selectionRegion.Right;
                if (maskRectRight.Width != temp)
                {
                    maskRectRight.Width = temp < 0 ? 0 : temp; //Math.Max(0, ActualWidth - selectionRegion.Right);
                }

                if (maskRectRight.Height != actualHeight)
                {
                    maskRectRight.Height = actualHeight;
                }

                SetLeft(maskRectTop, maskRectLeft.Width);
                SetLeft(maskRectBottom, maskRectLeft.Width);

                temp = actualWidth - maskRectLeft.Width - maskRectRight.Width + 0.2;
                maskRectTop.Margin = new Thickness(-0.1, 0, 0, 0);
                if (maskRectTop.Width != temp)
                {
                    maskRectTop.Width = temp < 0 ? 0 : temp; //Math.Max(0, ActualWidth - maskRectLeft.Width - maskRectRight.Width);

                }

                temp = selectionRegion.Top;

                if (maskRectTop.Height != temp)
                {
                    maskRectTop.Height = temp < 0 ? 0 : temp; //Math.Max(0, selectionRegion.Top);
                }

                maskRectBottom.Width = maskRectTop.Width;

                temp = actualHeight - selectionRegion.Bottom + 0.2;
                maskRectBottom.Margin = new Thickness(-0.1, 0, 0, 0);
                if (maskRectBottom.Height != temp)
                {
                    maskRectBottom.Height = temp < 0 ? 0 : temp; //Math.Max(0, ActualHeight - selectionRegion.Bottom);

                }
            }
        }


        #region Fileds & Props
        private Rect selectionRegion = Rect.Empty;
        private bool isMaskDraging;
        public bool MoveState = false;
        private IndicatorObject indicator;
        private System.Windows.Point? selectionStartPoint;
        private System.Windows.Point? selectionEndPoint;

        private readonly System.Windows.Shapes.Rectangle selectionBorder = new System.Windows.Shapes.Rectangle();

        private readonly System.Windows.Shapes.Rectangle maskRectLeft = new System.Windows.Shapes.Rectangle();
        private readonly System.Windows.Shapes.Rectangle maskRectRight = new System.Windows.Shapes.Rectangle();
        private readonly System.Windows.Shapes.Rectangle maskRectTop = new System.Windows.Shapes.Rectangle();
        private readonly System.Windows.Shapes.Rectangle maskRectBottom = new System.Windows.Shapes.Rectangle();


        public System.Drawing.Size? DefaultSize
        {
            get;
            set;
        }
        internal IndicatorObject Indicator { get => indicator; set => indicator = value; }
        #endregion

        #region Mouse Managment

        private bool IsMouseOnThis(RoutedEventArgs e)
        {
            return e.Source.Equals(this) || e.Source.Equals(maskRectLeft) || e.Source.Equals(maskRectRight) || e.Source.Equals(maskRectTop) || e.Source.Equals(maskRectBottom);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            Indicator.Visibility = System.Windows.Visibility.Visible;

            if (e.Source.Equals(Indicator))
            {
                HandleIndicatorMouseDown(e);
            }
            else
            {
                Point mouse = e.GetPosition(this);

                this.Indicator.MoveCenter(mouse);
            }
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {

            System.Diagnostics.Debug.WriteLine(DateTime.Now + "OnMouseMove");

            if (IsMouseOnThis(e))
            {
                UpdateSelectionRegion(e, UpdateMaskType.ForMouseMoving);

                e.Handled = true;
            }

            this.UpdateDynimicRect();

            base.OnMouseMove(e);

        }

        private void UpdateDynimicRect()
        {
            //  Message：移动区域
            if (this.mouseXY != null && this.MoveState)
            {
                Point position = Mouse.GetPosition(this);
                Point transform = new Point();
                transform.X -= mouseXY.X - position.X;
                transform.Y -= mouseXY.Y - position.Y;
                mouseXY = position;

                this.Indicator.Move(transform);
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            HandleIndicatorMouseUp(e);

            System.Diagnostics.Debug.WriteLine("OnMouseLeave");

            base.OnMouseLeave(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (IsMouseOnThis(e))
            {
                UpdateSelectionRegion(e, UpdateMaskType.ForMouseLeftButtonUp);

                FinishShowMask();


            }

            HandleIndicatorMouseUp(e);

            base.OnMouseLeftButtonUp(e);
        }

        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        {
            Indicator.Visibility = Visibility.Collapsed;
            selectionRegion = Rect.Empty;
            selectionBorder.Width = selectionBorder.Height = 0;
            // ClearSelectionData();
            UpdateMaskRectanglesLayout();

            base.OnMouseRightButtonUp(e);
        }


        internal void HandleIndicatorMouseDown(MouseButtonEventArgs e)
        {
            MoveState = true;

            mouseXY = e.GetPosition(this);


            System.Diagnostics.Debug.WriteLine("HandleIndicatorMouseDown");

        }

        internal void HandleIndicatorMouseUp(MouseEventArgs e)
        {
            MoveState = false;

            System.Diagnostics.Debug.WriteLine("HandleIndicatorMouseUp");
        }

        private void PrepareShowMask(System.Drawing.Point mouseLoc)
        {
            Indicator.Visibility = Visibility.Collapsed;
            selectionBorder.Visibility = Visibility.Visible;

        }

        private void UpdateSelectionRegion()
        {
            System.Drawing.Point startPoint = new System.Drawing.Point(0, 0);
            System.Drawing.Point endPoint = new System.Drawing.Point(190, 130);
            int sX = startPoint.X;
            int sY = startPoint.Y;
            int eX = endPoint.X;
            int eY = endPoint.Y;

            int deltaX = eX - sX;
            int deltaY = eY - sY;

            if (Math.Abs(deltaX) >= SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(deltaX) >= SystemParameters.MinimumVerticalDragDistance)
            {

                double x = sX < eX ? sX : eX;//Math.Min(sX, eX);
                double y = sY < eY ? sY : eY;//Math.Min(sY, eY);
                double w = deltaX < 0 ? -deltaX : deltaX;//Math.Abs(deltaX);
                double h = deltaY < 0 ? -deltaY : deltaY;//Math.Abs(deltaY);

                selectionRegion = new Rect(x, y, w, h);
            }
            else
            {
                selectionRegion = new Rect(startPoint.X, startPoint.Y, DefaultSize.Value.Width, DefaultSize.Value.Height);
            }
        }

        private Point mouseXY;
        private void UpdateSelectionRegion(MouseEventArgs e, UpdateMaskType updateType)
        {
            if (updateType == UpdateMaskType.ForMouseMoving && e.LeftButton != MouseButtonState.Pressed)
            {
                selectionStartPoint = null;
            }

            if (selectionStartPoint.HasValue)
            {
                selectionEndPoint = e.GetPosition(this);

                Point startPoint = (System.Windows.Point)selectionEndPoint;
                Point endPoint = (System.Windows.Point)selectionStartPoint;
                double sX = startPoint.X;
                double sY = startPoint.Y;
                double eX = endPoint.X;
                double eY = endPoint.Y;

                double deltaX = eX - sX;
                double deltaY = eY - sY;

                if (Math.Abs(deltaX) >= SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(deltaX) >= SystemParameters.MinimumVerticalDragDistance)
                {
                    isMaskDraging = true;

                    double x = sX < eX ? sX : eX;//Math.Min(sX, eX);
                    double y = sY < eY ? sY : eY;//Math.Min(sY, eY);
                    double w = deltaX < 0 ? -deltaX : deltaX;//Math.Abs(deltaX);
                    double h = deltaY < 0 ? -deltaY : deltaY;//Math.Abs(deltaY);

                    selectionRegion = new Rect(x, y, w, h);
                }
                else
                {
                    if (DefaultSize.HasValue && updateType == UpdateMaskType.ForMouseLeftButtonUp)
                    {
                        isMaskDraging = true;

                        selectionRegion = new Rect(startPoint.X, startPoint.Y, DefaultSize.Value.Width, DefaultSize.Value.Height);
                    }
                    else
                    {
                        isMaskDraging = false;
                    }
                }
            }

            UpdateIndicator(selectionRegion);
        }

        internal void UpdateSelectionRegion(Rect region, bool flag = false)
        {
            if (!flag && this.MoveState) return;

            selectionRegion = region;

            UpdateIndicator(selectionRegion);

            if (LoationChanged != null && flag)
            {


                LoationChanged(this, new LoactionArgs(region.Left / this.Width, region.Top / this.Height));
            }
        }


        private void FinishShowMask()
        {
            if (IsMouseCaptured)
            {
                ReleaseMouseCapture();
            }

            if (isMaskDraging)
            {

                UpdateIndicator(selectionRegion);

                ClearSelectionData();
            }
        }

        private void ClearSelectionData()
        {
            isMaskDraging = false;
            selectionBorder.Visibility = Visibility.Collapsed;
            selectionStartPoint = null;
            selectionEndPoint = null;
        }

        private void UpdateIndicator(Rect region)
        {

            //System.Diagnostics.Debug.WriteLine(region.Width);
            //System.Diagnostics.Debug.WriteLine(region.Height);

            if (Indicator == null)
                return;

            if (region.Width < Indicator.MinWidth || region.Height < Indicator.MinHeight)
            {
                return;
            }

            if (double.IsInfinity(region.Width) || double.IsInfinity(region.Height)) return;

            Indicator.Visibility = Visibility.Visible;
            Indicator.Width = region.Width;
            Indicator.Height = region.Height;
            SetLeft(Indicator, region.Left);
            SetTop(Indicator, region.Top);


        }

        private Rect GetIndicatorRegion()
        {
            return new Rect(GetLeft(Indicator), GetTop(Indicator), Indicator.ActualWidth, Indicator.ActualHeight);
        }

        #endregion

        #region Render

        private void OnCompositionTargetRendering(object sender, EventArgs e)
        {
            UpdateSelectionBorderLayout();
            UpdateMaskRectanglesLayout();
        }

        #endregion

        #region inner types

        private enum UpdateMaskType
        {
            ForMouseMoving,
            ForMouseLeftButtonUp
        }

        #endregion

    }

}
