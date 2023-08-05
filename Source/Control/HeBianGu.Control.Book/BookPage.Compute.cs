// Copyright ? 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.Book
{
    public partial class BookPage
    {
        private static PageParameters ResetPage(UIElement source, CornerOrigin origin)
        {
            PageParameters _parameters = new PageParameters(source.RenderSize);
            _parameters.Page0ShadowOpacity = 0;
            _parameters.Page1ClippingFigure = new PathFigure();
            _parameters.Page1ReflectionStartPoint = new Point(0, 0);
            _parameters.Page1ReflectionEndPoint = new Point(0, 0);
            _parameters.Page1RotateAngle = 0;
            _parameters.Page1RotateCenterX = 0;
            _parameters.Page1RotateCenterY = 0;
            _parameters.Page1TranslateX = 0;
            _parameters.Page1TranslateY = 0;
            _parameters.Page2ClippingFigure = new PathFigure();
            _parameters.Page0ShadowStartPoint = new Point(0, 0);
            _parameters.Page0ShadowEndPoint = new Point(0, 0);

            return _parameters;
        }

        private static double epsilon = 0.001;

        private static PageParameters? ComputePage(UIElement source, Point p, CornerOrigin origin)
        {
            CheckParams(ref source, ref p, origin);

            PageParameters _parameters = new PageParameters(source.RenderSize);

            double ratio = ComputeProgressRatio(source, p, origin);
            if (ratio > 1.5)
                ratio = (2 - ratio) / 0.5;
            else
                ratio = 1;
            _parameters.Page0ShadowOpacity = ratio;

            double xc = source.RenderSize.Width;
            double yc = source.RenderSize.Height;

            switch (origin)
            {
                case CornerOrigin.TopLeft:
                    p.X = xc - p.X;
                    p.Y = yc - p.Y;
                    break;
                case CornerOrigin.TopRight:
                    p.Y = yc - p.Y;
                    break;
                case CornerOrigin.BottomLeft:
                    p.X = xc - p.X;
                    break;
            }

            if (p.X >= xc)
                return null;

            // x = a * y + b
            double a = -(p.Y - yc) / (p.X - xc);
            double b = (xc + p.X) / 2 - a * ((yc + p.Y) / 2);

            double h1 = (xc - b) / a;
            double l1 = a * yc + b;

            double angle = (Math.Atan((xc - p.X) / (h1 - p.Y))) * 180 / Math.PI;
            if ((a < 0) && (p.Y < h1))
                angle = angle - 180;

            switch (origin)
            {
                case CornerOrigin.BottomRight:
                    _parameters.Page1RotateAngle = -angle;
                    _parameters.Page1RotateCenterX = p.X;
                    _parameters.Page1RotateCenterY = p.Y;
                    _parameters.Page1TranslateX = p.X;
                    _parameters.Page1TranslateY = p.Y - yc;
                    break;
                case CornerOrigin.TopLeft:
                    _parameters.Page1RotateAngle = -angle;
                    _parameters.Page1RotateCenterX = xc - p.X;
                    _parameters.Page1RotateCenterY = yc - p.Y;
                    _parameters.Page1TranslateX = -p.X;
                    _parameters.Page1TranslateY = yc - p.Y;
                    break;
                case CornerOrigin.TopRight:
                    _parameters.Page1RotateAngle = angle;
                    _parameters.Page1RotateCenterX = p.X;
                    _parameters.Page1RotateCenterY = yc - p.Y;
                    _parameters.Page1TranslateX = p.X;
                    _parameters.Page1TranslateY = yc - p.Y;
                    break;
                case CornerOrigin.BottomLeft:
                    _parameters.Page1RotateAngle = angle;
                    _parameters.Page1RotateCenterX = xc - p.X;
                    _parameters.Page1RotateCenterY = p.Y;
                    _parameters.Page1TranslateX = -p.X;
                    _parameters.Page1TranslateY = p.Y - yc;
                    break;
            }

            switch (origin)
            {
                case CornerOrigin.BottomRight:
                    if (angle < 0)
                    {
                        _parameters.Page1ClippingFigure.StartPoint = new Point(0, yc);
                        _parameters.Page1ClippingFigure.Segments.Clear();
                        _parameters.Page1ClippingFigure.Segments.Add(new LineSegment(new Point(xc - l1, yc), false));
                        _parameters.Page1ClippingFigure.Segments.Add(new LineSegment(new Point(0, h1), false));
                    }
                    else
                    {
                        _parameters.Page1ClippingFigure.StartPoint = new Point(0, 0);
                        _parameters.Page1ClippingFigure.Segments.Clear();
                        _parameters.Page1ClippingFigure.Segments.Add(new LineSegment(new Point(xc - b, 0), false));
                        _parameters.Page1ClippingFigure.Segments.Add(new LineSegment(new Point(0, h1), false));
                    }
                    break;
                case CornerOrigin.TopLeft:
                    if (angle < 0)
                    {
                        _parameters.Page1ClippingFigure.StartPoint = new Point(xc, 0);
                        _parameters.Page1ClippingFigure.Segments.Clear();
                        _parameters.Page1ClippingFigure.Segments.Add(new LineSegment(new Point(l1, 0), false));
                        _parameters.Page1ClippingFigure.Segments.Add(new LineSegment(new Point(xc, yc - h1), false));
                    }
                    else
                    {
                        _parameters.Page1ClippingFigure.StartPoint = new Point(xc, yc);
                        _parameters.Page1ClippingFigure.Segments.Clear();
                        _parameters.Page1ClippingFigure.Segments.Add(new LineSegment(new Point(b, yc), false));
                        _parameters.Page1ClippingFigure.Segments.Add(new LineSegment(new Point(xc, yc - h1), false));
                    }
                    break;
                case CornerOrigin.BottomLeft:
                    if (angle < 0)
                    {
                        _parameters.Page1ClippingFigure.StartPoint = new Point(xc, yc);
                        _parameters.Page1ClippingFigure.Segments.Clear();
                        _parameters.Page1ClippingFigure.Segments.Add(new LineSegment(new Point(l1, yc), false));
                        _parameters.Page1ClippingFigure.Segments.Add(new LineSegment(new Point(xc, h1), false));
                    }
                    else
                    {
                        _parameters.Page1ClippingFigure.StartPoint = new Point(xc, 0);
                        _parameters.Page1ClippingFigure.Segments.Clear();
                        _parameters.Page1ClippingFigure.Segments.Add(new LineSegment(new Point(b, 0), false));
                        _parameters.Page1ClippingFigure.Segments.Add(new LineSegment(new Point(xc, h1), false));
                    }
                    break;
                case CornerOrigin.TopRight:
                    if (angle < 0)
                    {
                        _parameters.Page1ClippingFigure.StartPoint = new Point(0, 0);
                        _parameters.Page1ClippingFigure.Segments.Clear();
                        _parameters.Page1ClippingFigure.Segments.Add(new LineSegment(new Point(xc - l1, 0), false));
                        _parameters.Page1ClippingFigure.Segments.Add(new LineSegment(new Point(0, yc - h1), false));
                    }
                    else
                    {
                        _parameters.Page1ClippingFigure.StartPoint = new Point(0, yc);
                        _parameters.Page1ClippingFigure.Segments.Clear();
                        _parameters.Page1ClippingFigure.Segments.Add(new LineSegment(new Point(xc - b, yc), false));
                        _parameters.Page1ClippingFigure.Segments.Add(new LineSegment(new Point(0, yc - h1), false));
                    }
                    break;
            }

            _parameters.Page2ClippingFigure.StartPoint = new Point(xc - _parameters.Page1ClippingFigure.StartPoint.X, _parameters.Page1ClippingFigure.StartPoint.Y);

            _parameters.Page2ClippingFigure.Segments = _parameters.Page1ClippingFigure.Segments.Clone();
            ((LineSegment)_parameters.Page2ClippingFigure.Segments[0]).Point
                = new Point(xc - ((LineSegment)_parameters.Page2ClippingFigure.Segments[0]).Point.X,
                    ((LineSegment)_parameters.Page2ClippingFigure.Segments[0]).Point.Y);
            ((LineSegment)_parameters.Page2ClippingFigure.Segments[1]).Point
                = new Point(xc - ((LineSegment)_parameters.Page2ClippingFigure.Segments[1]).Point.X,
                    ((LineSegment)_parameters.Page2ClippingFigure.Segments[1]).Point.Y);

            Point refletStartPoint;
            Point refletEndPoint;

            CornerOrigin oppositeOrigin = CornerOrigin.TopLeft;
            switch (origin)
            {
                case CornerOrigin.BottomLeft:
                    oppositeOrigin = CornerOrigin.BottomRight;
                    break;
                case CornerOrigin.BottomRight:
                    oppositeOrigin = CornerOrigin.BottomLeft;
                    break;
                case CornerOrigin.TopLeft:
                    oppositeOrigin = CornerOrigin.TopRight;
                    break;
                case CornerOrigin.TopRight:
                    oppositeOrigin = CornerOrigin.TopLeft;
                    break;
            }

            LinearGradientHelper.ComputePointsFromTop(xc, yc, xc - l1, yc - h1, 20, 20,
                oppositeOrigin,
                out refletStartPoint,
                out refletEndPoint);

            _parameters.Page1ReflectionStartPoint = refletStartPoint;
            _parameters.Page1ReflectionEndPoint = refletEndPoint; //new Point(1, 1 / Math.Tan((90 - angleClipping) * Math.PI / 180));

            Point startPoint;
            Point endPoint;

            double d = Math.Sqrt(Math.Pow(p.X - xc, 2) + Math.Pow(p.Y - yc, 2));

            double r1 = d / 10;
            double r2 = d / 10;

            LinearGradientHelper.ComputePoints(xc, yc, xc - l1, yc - h1, r1, r2,
                origin, out startPoint, out endPoint);

            _parameters.Page0ShadowStartPoint = startPoint;
            _parameters.Page0ShadowEndPoint = endPoint;

            return _parameters;
        }

        private static void CheckParams(ref UIElement source, ref Point p, CornerOrigin origin)
        {
            switch (origin)
            {
                case CornerOrigin.TopRight:
                    if (origin == CornerOrigin.TopRight)
                    {
                        if (p.Y == 0)
                            p.Y = epsilon;
                        if (p.X == 0)
                            p.X = epsilon;
                        if (p.Y > 0)
                        {
                            double d = Math.Sqrt(p.X * p.X + p.Y * p.Y);
                            if (d > source.RenderSize.Width)
                            {
                                double x = p.X * source.RenderSize.Width / d;
                                double y = p.Y * source.RenderSize.Width / d;
                                p.X = x;
                                p.Y = y;
                            }
                        }
                        else
                        {
                            double d = Math.Sqrt(p.X * p.X + p.Y * p.Y);
                            double R = Math.Sqrt(source.RenderSize.Width * source.RenderSize.Width + source.RenderSize.Height * source.RenderSize.Height);

                            double a = (p.Y * p.Y) / (p.X * p.X) + 1;
                            double b = -2 * p.Y * source.RenderSize.Height / p.X;
                            double c = source.RenderSize.Height * source.RenderSize.Height - R * R;
                            double delta = b * b - 4 * a * c;

                            double x = 0;
                            if (p.X > 0)
                                x = (-b + Math.Sqrt(delta)) / (2 * a);
                            else
                                x = (-b - Math.Sqrt(delta)) / (2 * a);

                            double y = p.Y * x / p.X;

                            if (Math.Abs(x) < Math.Abs(p.X))
                            {
                                p.X = x;
                                p.Y = y;
                            }
                        }
                        if (source.RenderSize.Width - p.X == p.Y)
                            p.X += epsilon;
                    }
                    break;

                case CornerOrigin.TopLeft:
                    p.X = source.RenderSize.Width - p.X;
                    CheckParams(ref source, ref p, CornerOrigin.TopRight);
                    p.X = source.RenderSize.Width - p.X;
                    break;
                case CornerOrigin.BottomRight:
                    p.Y = source.RenderSize.Height - p.Y;
                    CheckParams(ref source, ref p, CornerOrigin.TopRight);
                    p.Y = source.RenderSize.Height - p.Y;
                    break;
                case CornerOrigin.BottomLeft:
                    p.Y = source.RenderSize.Height - p.Y;
                    p.X = source.RenderSize.Width - p.X;
                    CheckParams(ref source, ref p, CornerOrigin.TopRight);
                    p.Y = source.RenderSize.Height - p.Y;
                    p.X = source.RenderSize.Width - p.X;
                    break;
            }
        }
    }
}
