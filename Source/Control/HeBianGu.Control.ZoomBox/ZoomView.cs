using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace HeBianGu.Control.ZoomBox
{
    public partial class ZoomView : ContentControl
    {
        Point? _start;

        public ZoomView()
        {
            this.PreviewMouseWheel += (l, e) =>
            {
                Point ptc = e.GetPosition(this);
                this.Zoom(ptc, e.Delta > 0);
            };

            this.MouseDown += (l, e) =>
            {
                this._start = e.GetPosition(this);
            };

            this.MouseMove += (l, e) =>
            {
                Point pt1 = e.GetPosition(this);
                if (this._start == null)
                    return;

                if (e.MiddleButton == MouseButtonState.Pressed)
                {
                    Point pt2 = this._start.Value;
                    Vector vector = pt1 - pt2;
                    this.Coord.Offset += vector;
                    this._start = pt1;
                }
                else
                {
                    this._start = null;
                }
            };

            this.MouseUp += (l, k) =>
            {
                this._start = null;
            };

            this.Coord.CoordinateChanged += Coord_CoordinateChanged;

            this.Loaded += (l, k) =>
            {
                this.RefreshZoom();
            };

            this.SizeChanged += (l, k) =>
            {
                this.RefreshZoom();
            };

            this.InitCommandBindings();
        }
        private void Coord_CoordinateChanged(object sender, EventArgs e)
        {
            this.RefreshZoom();
            this.OnCoordinateChanged();
        }

        public Coordinate Coord
        {
            get { return (Coordinate)GetValue(CoordProperty); }
            //set { SetValue(CoordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CoordProperty =
            DependencyProperty.Register("Coord", typeof(Coordinate), typeof(ZoomView), new FrameworkPropertyMetadata(new Coordinate(), (d, e) =>
            {
                ZoomView control = d as ZoomView;

                if (control == null) return;

                if (e.OldValue is Coordinate o)
                {
                    o.CoordinateChanged -= control.Coord_CoordinateChanged;
                }

                if (e.NewValue is Coordinate n)
                {
                    n.CoordinateChanged -= control.Coord_CoordinateChanged;
                    n.CoordinateChanged += control.Coord_CoordinateChanged;
                }
            }));

        //声明和注册路由事件
        public static readonly RoutedEvent CoordinateChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("CoordinateChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ZoomView));
        //CLR事件包装
        public event RoutedEventHandler CoordinateChanged
        {
            add { this.AddHandler(CoordinateChangedRoutedEvent, value); }
            remove { this.RemoveHandler(CoordinateChangedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnCoordinateChanged()
        {
            RoutedEventArgs args = new RoutedEventArgs(CoordinateChangedRoutedEvent, this);
            this.RaiseEvent(args);
        }


        public void Zoom(Point center, bool zoomIn)
        {
            Point pt = Coord.DeviceToLogical(center);
            double scale;
            scale = Math.Round(Coord.Scale * (zoomIn ? 1.1 : 1 / 1.1), 1);

            if (zoomIn)
                scale = Coord.Scale + 0.1;
            if (!zoomIn)
                scale = Coord.Scale - 0.1;

            Coord.Scale = scale;
            Point newPtc = Coord.LogicalToDevice(pt);
            Coord.Offset += center - newPtc;
            System.Diagnostics.Debug.WriteLine($"Scale:{Coord.Scale} Offset:{Coord.Offset}");
        }

        void RefreshZoom()
        {
            var matrix = new MatrixTransform(this.Coord.CoordMatrix);
            UIElement child = this.Content as UIElement;
            if (child == null)
                return;
            child.RenderTransform = matrix;
        }
    }
    public partial class ZoomView
    {
        void InitCommandBindings()
        {
            {
                CommandBinding binding = new CommandBinding(ZoomCommands.ZoomAll);
                binding.Executed += (l, k) => this.PanZoomToAll();
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(ZoomCommands.ZoomIn);
                binding.Executed += (l, k) => this.Zoom(new Point(this.ActualWidth / 2, this.ActualHeight / 2), true); ;
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(ZoomCommands.ZoomOut);
                binding.Executed += (l, k) => this.Zoom(new Point(this.ActualWidth / 2, this.ActualHeight / 2), false);
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(ZoomCommands.CenterTo);
                binding.Executed += (l, k) => this.PanCenterTo(new Point(this.ActualWidth / 2, this.ActualHeight / 2));
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(ZoomCommands.ZoomToHori);
                binding.Executed += (l, k) =>
                {
                    Rect rc = this.GetBounds();
                    Rect rcView = new Rect(rc.Left, (rc.Top + rc.Bottom) / 2, rc.Width, 0);
                    this.PanZoomToView(rcView);
                };
                this.CommandBindings.Add(binding);
            }


            {
                CommandBinding binding = new CommandBinding(ZoomCommands.ZoomToVert);
                binding.Executed += (l, k) =>
                {
                    Rect rc = this.GetBounds();
                    Rect rcView = new Rect((rc.Left + rc.Right) / 2, rc.Top, 0, rc.Height);
                    this.PanZoomToView(rcView);
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(ZoomCommands.ZoomToInit);
                binding.Executed += (l, k) =>
                {
                    this.Coord.Offset = new Point(0, 0);
                    this.Coord.Scale = 1;
                };
                this.CommandBindings.Add(binding);
            }
        }

        public void PanZoomToView(Rect rc, double pixelMarginX = 2, double pixelMaringY = 2, bool zoomIn = true)
        {
            double lcWidth = Coordinate.LogicalToDeviceNormalLength(rc.Width);
            double lcHeight = Coordinate.LogicalToDeviceNormalLength(rc.Height);

            double scale = (this.ActualWidth - pixelMarginX * 2) / lcWidth;
            scale = Math.Min(scale, (this.ActualHeight - pixelMaringY * 2) / lcHeight);
            if (zoomIn || scale < Coord.Scale)
                Coord.Scale = scale;

            PanCenterTo(new Point((rc.Left + rc.Right) / 2, (rc.Top + rc.Bottom) / 2));
        }

        public void PanZoomToAll(double pixelMargin = 5)
        {
            double w = this.ActualWidth;
            double h = this.ActualHeight;
            Rect rc = new Rect(w / 4, h / 6, w / 3, h / 4);

            rc = this.GetBounds();
            this.PanZoomToView(rc, pixelMargin, pixelMargin, true);
        }

        public void PanCenterTo(Point ptCenter)
        {
            Point ptc = Coord.LogicalToDevice(ptCenter);
            ptc.X -= Coord.Offset.X;
            ptc.Y -= Coord.Offset.Y;
            Point ptOffset = new Point(this.ActualWidth / 2 - ptc.X, this.ActualHeight / 2 - ptc.Y);
            Coord.Offset = ptOffset;
        }

        public Rect GetBounds()
        {
            //var bounds = this.Layers.Select(x => x.ContentBounds);
            //return bounds.Aggregate((x, y) => Rect.Union(x, y));

            //UIElement child = this.Content as UIElement;
            return new Rect(this.RenderSize);
        }
    }

    public class ZoomCommands
    {
        public static ICommand ZoomAll = new RoutedUICommand() { Text = "ZoomAll" };
        public static ICommand ZoomIn = new RoutedUICommand() { Text = "ZoomIn" };
        public static ICommand ZoomOut = new RoutedUICommand() { Text = "ZoomOut" };
        public static ICommand CenterTo = new RoutedUICommand() { Text = "CenterTo" };
        public static ICommand ZoomToVert = new RoutedUICommand() { Text = "ZoomToVert" };
        public static ICommand ZoomToHori = new RoutedUICommand() { Text = "ZoomToHori" };
        public static ICommand ZoomToInit = new RoutedUICommand() { Text = "1:1" };
        public static ICommand ZoomToVisual = new RoutedUICommand() { Text = "缩放到控件" };
        public static ICommand CenterToVisual = new RoutedUICommand() { Text = "定位到控件" };
    }
}
