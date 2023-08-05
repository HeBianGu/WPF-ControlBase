// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Control.Adorner
{
    public class DragAdorner : AdornerBase, IDragAdorner
    {
        public static ComponentResourceKey ListBoxItemAllowDropKey => new ComponentResourceKey(typeof(DragAdorner), "S.ListBoxItem.AllowDrop");
        public static ComponentResourceKey ListBoxItemAllowDropBothKey => new ComponentResourceKey(typeof(DragAdorner), "S.ListBoxItem.AllowDrop.Both");

        private Brush vbrush;
        private Point location;
        public Point Offset { get; set; }
        public DrapAdornerMode DropAdornerMode { get; set; }

        public DragAdorner(UIElement adornedElement, Point offset) : base(adornedElement)
        {
            this.Offset = offset;
            vbrush = new VisualBrush(AdornedElement);
            vbrush.Opacity = AdornerSetting.Instance.DragAornerOpacity;
        }

        public void UpdatePosition(Point location)
        {
            this.location = location;
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc)
        {
            Point p = location;
            if (this.DropAdornerMode == DrapAdornerMode.OnlyY)
            {
                p = new Point(0, p.Y);
                p.Offset(0, -Offset.Y);
            }
            else if (this.DropAdornerMode == DrapAdornerMode.OnlyX)
            {
                p = new Point(p.X, 0);
                p.Offset(-Offset.X, 0);
            }
            else
            {
                p.Offset(-Offset.X, -Offset.Y);
            }

            dc.DrawRectangle(vbrush, null, new Rect(p, this.RenderSize));
        }
        public object GetData()
        {
            return this.AdornedElement.GetDataContext();
        }
    }

    public enum DrapAdornerMode
    {
        Both = 0, OnlyX, OnlyY,
    }

    public class DragDataTemplateAdorner : DataTemplateAdorner, IDragAdorner
    {
        //private Brush vbrush;
        private Point location;
        public Point Offset { get; set; }
        public DrapAdornerMode DropAdornerMode { get; set; }
        public DragDataTemplateAdorner(UIElement adornedElement, Point offset) : base(adornedElement)
        {
            this.Offset = offset;
        }

        public void UpdatePosition(Point location)
        {
            this.location = location;
            this.InvalidateArrange();
        }

        protected override void RefreshLayout()
        {
            Point point = new Point();
            point.X = this.location.X + (this.AdornedElement.DesiredSize.Width - this._contentPresenter.DesiredSize.Width) / 2;
            point.Y = this.location.Y + (this.AdornedElement.DesiredSize.Height - this._contentPresenter.DesiredSize.Height) / 2;
            this._contentPresenter.Arrange(new Rect(point, this._contentPresenter.DesiredSize));
        }

        public object GetData()
        {
            return this._contentPresenter.Content;
        }
    }

    public class DragControlTemplateAdorner : ControlTemplateAdorner, IDragAdorner
    {
        //private Brush vbrush;
        private Point location;
        public Point Offset { get; set; }
        public DrapAdornerMode DropAdornerMode { get; set; }
        public DragControlTemplateAdorner(UIElement adornedElement, Point offset) : base(adornedElement)
        {
            this.Offset = offset;

        }

        public void UpdatePosition(Point location)
        {
            this.location = location;
            this.InvalidateArrange();
        }

        protected override void RefreshLayout()
        {
            Point point = new Point();
            point.X = this.location.X + (this.AdornedElement.DesiredSize.Width - this._contentControl.DesiredSize.Width) / 2;
            point.Y = this.location.Y + (this.AdornedElement.DesiredSize.Height - this._contentControl.DesiredSize.Height) / 2;
            this._contentControl.Arrange(new Rect(point, this._contentControl.DesiredSize));
        }

        public object GetData()
        {
            return this.AdornedElement.GetDataContext();
        }
    }
}
