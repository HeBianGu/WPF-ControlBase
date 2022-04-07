// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Service.Animation
{
    public class LinearOpacityMaskTransition : TransitionBase
    {
        public LinearOpacityMaskTransition()
        {
            RectangleGeometry geometry = new RectangleGeometry();

            geometry.Rect = new Rect() { X = 0.01, Y = 0.01, Width = 0.9, Height = 0.9 };

            this.Geometry = geometry;
            this.Viewport = new Rect() { X = 0.1, Y = 0.2, Height = 0.1, Width = 0.1 };

            this.StartPoint = new Point(0, 0);

            this.EndPoint = new Point(1, 1);
        }

        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            base.BeginCurrent(element);

            element.OpacityMask = null;
        }

        public Rect Viewport { get; set; }

        public Geometry Geometry { get; set; }

        public Point StartPoint { get; set; }

        public Point EndPoint { get; set; }

        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            base.BeginPrevious(element);

            Action action = () =>
              {
                  element.Visibility = HiddenOrCollapsed;
                  complate?.Invoke();
              };

            this.Begin(element, 1, 0, "OpacityMask.(DrawingBrush.Drawing).(GeometryDrawing.Brush).(LinearGradientBrush.GradientStops)[1].Offset", action);

        }

        public override void BeginVisible(UIElement element, Action complate = null)
        {
            base.BeginVisible(element);

            this.Begin(element, 0, 1, "OpacityMask.(DrawingBrush.Drawing).(GeometryDrawing.Brush).(LinearGradientBrush.GradientStops)[0].Offset", complate);
        }

        private void Begin(UIElement element, double from, double to, string propertyPath, Action complate = null)
        {
            DrawingBrush brush = new DrawingBrush();

            brush.TileMode = TileMode.Tile;

            brush.Viewport = Viewport;

            brush.ViewboxUnits = BrushMappingMode.RelativeToBoundingBox;

            GeometryDrawing drawing = new GeometryDrawing();

            LinearGradientBrush linear = new LinearGradientBrush();
            linear.StartPoint = this.StartPoint;
            linear.EndPoint = this.EndPoint;
            linear.GradientStops.Add(new GradientStop() { Offset = 0, Color = Colors.Black });
            linear.GradientStops.Add(new GradientStop() { Offset = 1, Color = Colors.Transparent });
            drawing.Geometry = this.Geometry;
            drawing.Brush = linear;
            brush.Drawing = drawing;
            element.OpacityMask = brush;

            element.BegionDoubleStoryBoard(from, to, this.HideDuration / 1000.0, propertyPath, l =>
                {
                    element.OpacityMask = null;
                    complate?.Invoke();
                }, l =>
                 {
                     l.Easing = this.VisibleEasing;
                 });
        }
    }
}
