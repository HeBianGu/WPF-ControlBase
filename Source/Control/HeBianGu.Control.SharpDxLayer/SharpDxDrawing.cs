using HeBianGu.Base.WpfBase;
using HeBianGu.Control.LayerBox;
using HeBianGu.Control.SharpDx;
using System;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.Control.SharpDxLayer
{
    public class SharpDxDrawing : IDrawing
    {
        private readonly SharpDX.DirectWrite.Factory _factoryDWrite = null;
        public SharpDxDrawing()
        {
            _factoryDWrite = new SharpDX.DirectWrite.Factory();
        }
        public SharpDX.Direct2D1.RenderTarget RenderTarget { get; set; }
        public LayerView LayerView { get; set; }

        public virtual double DPI => this.LayerView.Coord.DPI;

        public virtual double BlueprintDPI => this.DPI;

        private BlueprintLayerBase GetBlueprintLayer()
        {
            if (LayerView == null)
                return null;
            BlueprintLayerBase imageLayer = LayerView.Layers.OfType<BlueprintLayerBase>()?.FirstOrDefault();
            return imageLayer;
        }

        private double GetBlueprintDpi()
        {
            var imageLayer = GetBlueprintLayer();
            return LayerView.Coord.DPI * imageLayer.DPI;
        }

        public void DrawDrawing(System.Windows.Media.Drawing drawing)
        {
            //var brush = new BitmapBrush(_d2dRenderTarget, bitmap, bitmapBrushProperties, brushProperties);

            //SharpDX.Direct2D1.Bitmap dxbmp = new SharpDX.Direct2D1.Bitmap(this.RenderTarget, new SharpDX.Size2(500, 500), 
            //    new SharpDX.Direct2D1.BitmapProperties(this.RenderTarget.PixelFormat)); 
            //dxbmp.CopyFromMemory(bmpBits, bmpWidth * 4);
            //this.RenderTarget.DrawBitmap(dxbmp, 1F, SharpDX.Direct2D1.BitmapInterpolationMode.Linear); 
            //dxbmp.Dispose();
            //SharpDX.Direct2D1.Bitmap.FromWicBitmap(_renderTarget, _wicBitmap);



            SharpDX.Direct2D1.Bitmap bitmap = new SharpDX.Direct2D1.Bitmap(this.RenderTarget, new SharpDX.Size2(500, 500));
            this.RenderTarget.DrawBitmap(bitmap, 1.0f, SharpDX.Direct2D1.BitmapInterpolationMode.NearestNeighbor);
        }

        public void DrawEllipse(Brush brush, Pen pen, Point center, double radiusX, double radiusY)
        {
            var ellipse = new SharpDX.Direct2D1.Ellipse(center.ToPoint(), (float)(radiusX / this.DPI), (float)(radiusY / this.DPI));
            using (var b = brush.ToSolidColorBrush(this.RenderTarget))
                this.RenderTarget.DrawEllipse(ellipse, b);
        }

        public void DrawEllipse(Brush brush, Pen pen, Point center, AnimationClock centerAnimations, double radiusX, AnimationClock radiusXAnimations, double radiusY, AnimationClock radiusYAnimations)
        {
            //this.DrawingContext.DrawEllipse(brush, pen, center, centerAnimations, radiusY, radiusXAnimations, radiusY, radiusYAnimations);
        }

        public void DrawGeometry(Brush brush, Pen pen, Geometry geometry)
        {
            //if (geometry is EllipseGeometry ellipse)
            //{
            //    SharpDX.Direct2D1.Ellipse ellipse1 = new SharpDX.Direct2D1.Ellipse(ellipse.Center.ToPoint(), (float)(ellipse.RadiusX / this.DPI), (float)(ellipse.RadiusY / this.DPI));
            //    SharpDX.Direct2D1.EllipseGeometry ellipseGeometry = new SharpDX.Direct2D1.EllipseGeometry(this.DrawingContext.Factory, ellipse1);

            //    using (var b = Brushes.Red.ToSolidColorBrush(this.DrawingContext))
            //    {
            //        this.DrawingContext.DrawGeometry(ellipseGeometry, b);
            //        this.DrawingContext.FillGeometry(ellipseGeometry, b);
            //    }
            //}
        }

        public void DrawGlyphRun(Brush foregroundBrush, GlyphRun glyphRun)
        {
            //this.DrawingContext.DrawGlyphRun(foregroundBrush, glyphRun);
        }

        public void DrawImage(ImageSource imageSource, Rect rectangle)
        {
            //this.DrawingContext.DrawImage(imageSource, rectangle);
        }

        public void DrawImage(ImageSource imageSource, Rect rectangle, AnimationClock rectangleAnimations)
        {
            //this.DrawingContext.DrawImage(imageSource, rectangle, rectangleAnimations);
        }

        public void DrawLine(Pen pen, Point point0, AnimationClock point0Animations, Point point1, AnimationClock point1Animations)
        {
            //this.DrawingContext.DrawLine(pen, point0, point0Animations, point1, point1Animations);
        }

        public void DrawPolyLine(Pen pen, Point[] points)
        {
            if (points.Length < 2)
                return;

            SharpDX.Direct2D1.PathGeometry geometry = new SharpDX.Direct2D1.PathGeometry(this.RenderTarget.Factory);
            {
                //var styleProperties = new SharpDX.Direct2D1.StrokeStyleProperties()
                //{
                //    StartCap = SharpDX.Direct2D1.CapStyle.Round,
                //    EndCap = SharpDX.Direct2D1.CapStyle.Round
                //};
                //var strokeStyle = new SharpDX.Direct2D1.StrokeStyle(this.DrawingContext.Factory, styleProperties);
                {
                    var sink = geometry.Open();
                    {
                        sink.BeginFigure(points.FirstOrDefault().ToPoint(), SharpDX.Direct2D1.FigureBegin.Hollow);
                        sink.AddLines(points.Select(x => x.ToPoint()).ToArray());
                        sink.EndFigure(SharpDX.Direct2D1.FigureEnd.Open);
                    }
                    sink.Close();
                    using (var brush = pen.Brush.ToSolidColorBrush(this.RenderTarget))
                    {
                        this.RenderTarget.DrawGeometry(geometry, pen.Brush.ToSolidColorBrush(this.RenderTarget), (float)pen.Thickness);
                    }
                }
            }
        }

        public void DrawLine(Pen pen, Point point0, Point point1)
        {
            //this.DrawingContext.DrawLine(pen, point0, point1);

            //var brush1 = new SharpDX.Direct2D1.SolidColorBrush(this.DrawingContext, new SharpDX.Mathematics.Interop.RawColor4(1.0f, 1.0f, 1.0f, 1.0f));
            //this.DrawingContext.DrawLine(new SharpDX.Mathematics.Interop.RawVector2() { X = (float)point0.X, Y = (float)point0.Y }, new SharpDX.Mathematics.Interop.RawVector2() { X = (float)point1.X, Y = (float)point1.Y }, brush1);

            var styleProperties = new SharpDX.Direct2D1.StrokeStyleProperties()
            {
                StartCap = SharpDX.Direct2D1.CapStyle.Round,
                EndCap = SharpDX.Direct2D1.CapStyle.Round
            };
            var strokeStyle = new SharpDX.Direct2D1.StrokeStyle(this.RenderTarget.Factory, styleProperties);
            this.RenderTarget.DrawLine(point0.ToPoint(), point1.ToPoint(), pen.Brush.ToSolidColorBrush(this.RenderTarget), (float)pen.Thickness, strokeStyle);

        }

        public void DrawRectangle(Brush brush, Pen pen, Rect rectangle)
        {
            this.RenderTarget.FillRectangle(rectangle.ToRawRectangleF(), brush.ToSolidColorBrush(this.RenderTarget));
            if (pen != null)
                this.RenderTarget.DrawRectangle(rectangle.ToRawRectangleF(), pen.Brush.ToSolidColorBrush(this.RenderTarget), (float)pen.Thickness);

        }

        public void DrawRectangle(Brush brush, Pen pen, Rect rectangle, AnimationClock rectangleAnimations)
        {
            //this.DrawingContext.DrawRectangle(brush, pen, rectangle, rectangleAnimations);
        }

        public void DrawRoundedRectangle(Brush brush, Pen pen, Rect rectangle, AnimationClock rectangleAnimations, double radiusX, AnimationClock radiusXAnimations, double radiusY, AnimationClock radiusYAnimations)
        {
            //this.DrawingContext.DrawRoundedRectangle(brush, pen, rectangle, rectangleAnimations, radiusX, radiusXAnimations, radiusY, radiusYAnimations);
        }

        public void DrawRoundedRectangle(Brush brush, Pen pen, Rect rectangle, double radiusX, double radiusY)
        {
            //this.DrawingContext.DrawRoundedRectangle(brush, pen, rectangle, radiusY, radiusY);
        }

        public void DrawText(FormattedText formattedText, Point origin)
        {
            //this.DrawingContext.DrawText(formattedText, origin);

            //var format= new TextFormat(this.DrawingContext.Factory, "微软雅黑", 30);

            //var transform = this.DrawingContext.Transform;

            // set a 90 degree rotation around the (100,100); 
            //RenderTarget2D.SetTransform(Matrix3x2F.Rotation(90, Point2F(100, 100)));
            //this.DrawingContext.Transform = new SharpDX.Mathematics.Interop.RawMatrix3x2(transform.M11, transform.M12, transform.M21, -transform.M22, transform.M31, transform.M32);
            var size = (float)(12 / this.DPI);
            size = Math.Max(size, 5);
            var textFormat = new SharpDX.DirectWrite.TextFormat(_factoryDWrite, "微软雅黑", size);
            textFormat.TextAlignment = SharpDX.DirectWrite.TextAlignment.Center;
            textFormat.ParagraphAlignment = SharpDX.DirectWrite.ParagraphAlignment.Center;
            textFormat.FlowDirection = SharpDX.DirectWrite.FlowDirection.BottomToTop;
            this.RenderTarget.DrawText(formattedText.Text, textFormat, new SharpDX.Mathematics.Interop.RawRectangleF((float)origin.X, (float)origin.Y, (float)origin.X + 100, (float)origin.Y + 100), Brushes.Red.ToSolidColorBrush(this.RenderTarget));
            //this.DrawingContext.Transform = transform;
        }



        public void DrawTextCenterTo(FormattedText formattedText, Point from, Point to)
        {
            //this.DrawingContext.DrawTextCenterTo(formattedText, from, to);
        }

        public void DrawGeometry(Brush brush, Pen pen, Geometry geometry, Rect target)
        {
            //geometry.Transform = geometry.Bounds.TransformToRect(target);
            //this.DrawingContext.DrawGeometry(brush, pen, geometry);
        }
        static GeometryConverter converter = new GeometryConverter();
        public void DrawGeometry(Brush brush, Pen pen, string geometry, Rect target)
        {
            //Geometry geo = converter.ConvertFromString(geometry) as Geometry;
            //geo.Transform = geo.Bounds.TransformToRect(target);
            //this.DrawingContext.DrawGeometry(brush, pen, geo);
        }

        public void DrawGeometry(Brush brush, Pen pen, string geometry)
        {
            //Geometry geo = converter.ConvertFromString(geometry) as Geometry;
            //this.DrawingContext.DrawGeometry(brush, pen, geo);
        }

        public void PushTransform(Transform transform)
        {
            //this.DrawingContext.PushTransform(transform);
        }

        public void Pop()
        {
            //this.DrawingContext.Pop();
            this.RenderTarget.PopAxisAlignedClip();
            //this.RenderTarget.PopLayer();
        }

        public void DrawText(string text, Point point, Brush foreground, double fontSize = 15)
        {
            //#if NETCOREAPP
            //            this.DrawingContext.DrawText(new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("微软雅黑"), fontSize, foreground, 30), point);
            //#endif

            if (string.IsNullOrEmpty(text))
                return;
            var FactoryDWrite = new SharpDX.DirectWrite.Factory();
            var textFormat = new SharpDX.DirectWrite.TextFormat(FactoryDWrite, "微软雅黑", (float)(fontSize));
            this.RenderTarget.DrawText(text, textFormat, new SharpDX.Mathematics.Interop.RawRectangleF((float)point.X, (float)point.Y, (float)point.X + (float)(this.LayerView.ActualWidth / this.DPI), (float)point.Y + (float)(this.LayerView.ActualHeight / this.DPI)), foreground.ToSolidColorBrush(this.RenderTarget));

        }

        public void DrawDrawing(byte[] data, int pitch, int width, int height)
        {
            //SharpDX.Direct2D1.Bitmap dxbmp = new SharpDX.Direct2D1.Bitmap(this.RenderTarget, new SharpDX.Size2(width, height), new SharpDX.Direct2D1.BitmapProperties(new SharpDX.Direct2D1.PixelFormat(SharpDX.DXGI.Format.Unknown, SharpDX.Direct2D1.AlphaMode.Unknown)));

            //SharpDX.Direct2D1.Bitmap dxbmp = new SharpDX.Direct2D1.Bitmap(this.RenderTarget, new SharpDX.Size2(width, height));
            SharpDX.Direct2D1.Bitmap dxbmp = new SharpDX.Direct2D1.Bitmap(this.RenderTarget, new SharpDX.Size2(width, height), new SharpDX.Direct2D1.BitmapProperties(this.RenderTarget.PixelFormat));
            dxbmp.CopyFromMemory(data, pitch);
            this.RenderTarget.DrawBitmap(dxbmp, 1.0f, SharpDX.Direct2D1.BitmapInterpolationMode.Linear);
            dxbmp.Dispose();
        }

        public void PushClip(Geometry clipGeometry)
        {
            if (clipGeometry is RectangleGeometry geometry)
            {
                var rect = geometry.Rect.ToRawRectangleF();
                this.RenderTarget.PushAxisAlignedClip(rect, SharpDX.Direct2D1.AntialiasMode.PerPrimitive); 
            }
        }
    }

}
