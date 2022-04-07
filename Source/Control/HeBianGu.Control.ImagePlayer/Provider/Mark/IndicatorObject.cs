// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.ImagePlayer
{
    internal class IndicatorObject : Canvas
    {
        private MaskCanvas canvasOwner;

        public IndicatorObject(MaskCanvas canvasOwner)
        {
            this.canvasOwner = canvasOwner;
        }

        static IndicatorObject()
        {
            System.Type ownerType = typeof(IndicatorObject);

            FocusVisualStyleProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(null));
            DefaultStyleKeyProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(ownerType));
            MinWidthProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(5.0));
            MinHeightProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(5.0));
        }

        public void Move(System.Windows.Point offset)
        {
            double x = Canvas.GetLeft(this) + offset.X;
            double y = Canvas.GetTop(this) + offset.Y;



            //if (x<0)
            //{
            //    this.Width = Math.Max(this.Width + x,0);
            //}

            //if (y < 0)
            //{
            //    this.Height = Math.Max(this.Height + x, 0);
            //}

            //x = x < 0 ? 0 : x;
            //y = y < 0 ? 0 : y;

            //x = Math.Min(x, this.canvasOwner.Width - this.Width);
            //y = Math.Min(y, this.canvasOwner.Height - this.Height);

            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);

            canvasOwner.UpdateSelectionRegion(new Rect(x, y, Width, Height), true);
        }


        public void MoveCenter(System.Windows.Point offset)
        {
            double x = offset.X - this.Width / 2;
            double y = offset.Y - this.Height / 2;

            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);

            canvasOwner.UpdateSelectionRegion(new Rect(x, y, Width, Height), true);
        }

        public double WidthPercent
        {
            get
            {
                return this.Width / this.canvasOwner.Width;
            }
        }

        public double HeightPercent
        {
            get
            {
                return this.Height / this.canvasOwner.Height;
            }
        }

    }
}
