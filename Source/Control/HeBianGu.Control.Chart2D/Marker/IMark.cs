using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HeBianGu.Control.Chart2D
{
    public class ShapePointMarker : Shape
    {
        public double Size
        {
            get { return (double)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register(
              "Size",
              typeof(double),
              typeof(ShapePointMarker),
              new FrameworkPropertyMetadata(5.0));


        /// <summary>Pen to outline marker</summary>
        public Pen Pen
        {
            get { return (Pen)GetValue(PenProperty); }
            set { SetValue(PenProperty, value); }
        }

        public static readonly DependencyProperty PenProperty =
            DependencyProperty.Register(
              "Pen",
              typeof(Pen),
              typeof(ShapePointMarker),
              new FrameworkPropertyMetadata(null));

        public Point ScreenPoint
        {
            get { return (Point)GetValue(ScreenPointProperty); }
            set { SetValue(ScreenPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScreenPoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScreenPointProperty =
            DependencyProperty.Register("ScreenPoint", typeof(Point), typeof(ShapePointMarker), new PropertyMetadata(new Point(0, 0)));


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
              "Text",
              typeof(string),
              typeof(ShapePointMarker),
              new FrameworkPropertyMetadata(""));

        //public int FontZize
        //{
        //    get { return (int)GetValue(FontZizeProperty); }
        //    set { SetValue(FontZizeProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for FontZize.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty FontZizeProperty =
        //    DependencyProperty.Register("FontZize", typeof(int), typeof(ShapePointMarker), new PropertyMetadata(10));

        //public Brush FontColor
        //{
        //    get { return (Brush)GetValue(FontColorProperty); }
        //    set { SetValue(FontColorProperty, value); }
        //}

        /// <summary> 描绘形状 </summary>
        protected override Geometry DefiningGeometry
        {
            get
            {
                EllipseGeometry e = new EllipseGeometry(ScreenPoint, this.Size / 2, this.Size / 2);
                return e;
            }
        }

        //// Using a DependencyProperty as the backing store for FontColor.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty FontColorProperty =
        //    DependencyProperty.Register("FontColor", typeof(Brush), typeof(ShapePointMarker), new PropertyMetadata(Brushes.Black));


        /// <summary> 绘制形状 </summary>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            Pen p = new Pen(this.Fill, 1);

            drawingContext.DrawGeometry(Brushes.White, p, DefiningGeometry);
        }
    }

    #region - Tool -

    //public static class RectExtensions
    //{
    //    public static Point GetCenter(this Rect rect)
    //    {
    //        return new Point(rect.Left + rect.Width * 0.5, rect.Top + rect.Height * 0.5);
    //    }

    //    public static Rect FromCenterSize(Point center, Size size)
    //    {
    //        return FromCenterSize(center, size.Width, size.Height);
    //    }

    //    public static Rect FromCenterSize(Point center, double width, double height)
    //    {
    //        Rect res = new Rect(center.X - width / 2, center.Y - height / 2, width, height);
    //        return res;
    //    }

    //    public static Rect Zoom(this Rect rect, Point to, double ratio)
    //    {
    //        return CoordinateUtilities.RectZoom(rect, to, ratio);
    //    }

    //    public static Rect ZoomX(this Rect rect, Point to, double ratio)
    //    {
    //        return CoordinateUtilities.RectZoomX(rect, to, ratio);
    //    }

    //    public static Rect ZoomY(this Rect rect, Point to, double ratio)
    //    {
    //        return CoordinateUtilities.RectZoomY(rect, to, ratio);
    //    }

    //    public static Int32Rect ToInt32Rect(this Rect rect)
    //    {
    //        Int32Rect intRect = new Int32Rect(
    //            (int)rect.X,
    //            (int)rect.Y,
    //            (int)rect.Width,
    //            (int)rect.Height);

    //        return intRect;
    //    }

    //    [DebuggerStepThrough]
    //    public static DataRect ToDataRect(this Rect rect)
    //    {
    //        return new DataRect(rect);
    //    }
    //}

    //public static class CoordinateUtilities
    //{
    //    public static Rect RectZoom(Rect rect, double ratio)
    //    {
    //        return RectZoom(rect, rect.GetCenter(), ratio);
    //    }

    //    public static Rect RectZoom(Rect rect, double horizontalRatio, double verticalRatio)
    //    {
    //        return RectZoom(rect, rect.GetCenter(), horizontalRatio, verticalRatio);
    //    }

    //    public static Rect RectZoom(Rect rect, Point zoomCenter, double ratio)
    //    {
    //        return RectZoom(rect, zoomCenter, ratio, ratio);
    //    }

    //    public static Rect RectZoom(Rect rect, Point zoomCenter, double horizontalRatio, double verticalRatio)
    //    {
    //        Rect res = new Rect();
    //        res.X = zoomCenter.X - (zoomCenter.X - rect.X) * horizontalRatio;
    //        res.Y = zoomCenter.Y - (zoomCenter.Y - rect.Y) * verticalRatio;
    //        res.Width = rect.Width * horizontalRatio;
    //        res.Height = rect.Height * verticalRatio;
    //        return res;
    //    }

    //    public static Rect RectZoomX(Rect rect, Point zoomCenter, double ratio)
    //    {
    //        Rect res = rect;
    //        res.X = zoomCenter.X - (zoomCenter.X - rect.X) * ratio;
    //        res.Width = rect.Width * ratio;
    //        return res;
    //    }

    //    public static Rect RectZoomY(Rect rect, Point zoomCenter, double ratio)
    //    {
    //        Rect res = rect;
    //        res.Y = zoomCenter.Y - (zoomCenter.Y - rect.Y) * ratio;
    //        res.Height = rect.Height * ratio;
    //        return res;
    //    }
    //}

    //[DebuggerDisplay("{XMin};{YMin} → {Width}*{Height}")]
    //public struct DataRect : IEquatable<DataRect>
    //{
    //    #region Ctors

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="DataRect"/> struct.
    //    /// </summary>
    //    /// <param name="rect">Source rect.</param>
    //    public DataRect(Rect rect)
    //    {
    //        xMin = rect.X;
    //        yMin = rect.Y;
    //        width = rect.Width;
    //        height = rect.Height;
    //    }

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="DataRect"/> struct.
    //    /// </summary>
    //    /// <param name="size">The size.</param>
    //    public DataRect(Size size)
    //    {
    //        if (size.IsEmpty)
    //        {
    //            this = emptyRect;
    //        }
    //        else
    //        {
    //            xMin = yMin = 0.0;
    //            width = size.Width;
    //            height = size.Height;
    //        }
    //    }

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="DataRect"/> struct.
    //    /// </summary>
    //    /// <param name="location">The location.</param>
    //    /// <param name="size">The size.</param>
    //    public DataRect(Point location, Size size)
    //    {
    //        if (size.IsEmpty)
    //        {
    //            this = emptyRect;
    //        }
    //        else
    //        {
    //            xMin = location.X;
    //            yMin = location.Y;
    //            width = size.Width;
    //            height = size.Height;
    //        }
    //    }

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="DataRect"/> struct.
    //    /// </summary>
    //    /// <param name="point1">The point1.</param>
    //    /// <param name="point2">The point2.</param>
    //    public DataRect(Point point1, Point point2)
    //    {
    //        xMin = Math.Min(point1.X, point2.X);
    //        yMin = Math.Min(point1.Y, point2.Y);
    //        width = Math.Max((double)(Math.Max(point1.X, point2.X) - xMin), 0);
    //        height = Math.Max((double)(Math.Max(point1.Y, point2.Y) - yMin), 0);
    //    }

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="DataRect"/> struct.
    //    /// </summary>
    //    /// <param name="point">The point.</param>
    //    /// <param name="vector">The vector.</param>
    //    public DataRect(Point point, Vector vector)
    //        : this(point, point + vector)
    //    {
    //    }

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="DataRect"/> struct.
    //    /// </summary>
    //    /// <param name="x">The x.</param>
    //    /// <param name="y">The y.</param>
    //    /// <param name="width">The width.</param>
    //    /// <param name="height">The height.</param>
    //    public DataRect(double x, double y, double width, double height)
    //    {
    //        if ((width < 0) || (height < 0))
    //        {
    //            throw new ArgumentException();
    //        }
    //        this.xMin = x;
    //        this.yMin = y;
    //        this.width = width;
    //        this.height = height;
    //    }

    //    #endregion

    //    #region Static

    //    public static DataRect FromPoints(double x1, double y1, double x2, double y2)
    //    {
    //        return new DataRect(new Point(x1, y1), new Point(x2, y2));
    //    }

    //    public static implicit operator DataRect(Rect rect)
    //    {
    //        return rect.ToDataRect();
    //    }

    //    public static implicit operator Rect(DataRect rect)
    //    {
    //        return rect.ToRect();
    //    }

    //    #endregion

    //    public Rect ToRect()
    //    {
    //        return new Rect(xMin, yMin, width, height);
    //    }

    //    public DataRect Intersect(DataRect rect)
    //    {
    //        if (!IntersectsWith(rect))
    //            return DataRect.Empty;

    //        DataRect res = new DataRect();

    //        double x = Math.Max(this.XMin, rect.XMin);
    //        double y = Math.Max(this.YMin, rect.YMin);
    //        res.width = Math.Max((double)(Math.Min(this.XMax, rect.XMax) - x), 0.0);
    //        res.height = Math.Max((double)(Math.Min(this.YMax, rect.YMax) - y), 0.0);
    //        res.xMin = x;
    //        res.yMin = y;

    //        return res;
    //    }

    //    public bool IntersectsWith(DataRect rect)
    //    {
    //        if (IsEmpty || rect.IsEmpty)
    //            return false;

    //        return ((((rect.XMin <= this.XMax) && (rect.XMax >= this.XMin)) && (rect.YMax >= this.YMin)) && (rect.YMin <= this.YMax));
    //    }

    //    private double xMin;
    //    private double yMin;
    //    private double width;
    //    private double height;

    //    /// <summary>
    //    /// Gets a value indicating whether this instance is empty.
    //    /// </summary>
    //    /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
    //    public bool IsEmpty
    //    {
    //        get { return width < 0; }
    //    }

    //    /// <summary>
    //    /// Gets the bottom.
    //    /// </summary>
    //    /// <value>The bottom.</value>
    //    public double YMin
    //    {
    //        get { return yMin; }
    //    }

    //    /// <summary>
    //    /// Gets the top.
    //    /// </summary>
    //    /// <value>The top.</value>
    //    public double YMax
    //    {
    //        get
    //        {
    //            if (IsEmpty)
    //            {
    //                return Double.PositiveInfinity;
    //            }
    //            return yMin + height;
    //        }
    //    }

    //    /// <summary>
    //    /// Gets the left.
    //    /// </summary>
    //    /// <value>The left.</value>
    //    public double XMin
    //    {
    //        get { return xMin; }
    //    }

    //    /// <summary>
    //    /// Gets the right.
    //    /// </summary>
    //    /// <value>The right.</value>
    //    public double XMax
    //    {
    //        get
    //        {
    //            if (IsEmpty)
    //            {
    //                return Double.PositiveInfinity;
    //            }
    //            return xMin + width;
    //        }
    //    }

    //    /// <summary>
    //    /// Gets or sets the location.
    //    /// </summary>
    //    /// <value>The location.</value>
    //    public Point Location
    //    {
    //        get { return new Point(xMin, yMin); }
    //        set
    //        {
    //            if (IsEmpty)
    //            {
    //                throw new InvalidOperationException();
    //            }
    //            xMin = value.X;
    //            yMin = value.Y;
    //        }
    //    }

    //    /// <summary>
    //    /// Gets or sets the size.
    //    /// </summary>
    //    /// <value>The size.</value>
    //    public Size Size
    //    {
    //        get
    //        {
    //            if (IsEmpty)
    //            {
    //                return Size.Empty;
    //            }
    //            return new Size(width, height);
    //        }
    //        set
    //        {
    //            if (value.IsEmpty)
    //            {
    //                this = emptyRect;
    //            }
    //            else
    //            {
    //                if (IsEmpty)
    //                {
    //                    throw new InvalidOperationException();
    //                }
    //                width = value.Width;
    //                height = value.Height;
    //            }
    //        }
    //    }

    //    public double Width
    //    {
    //        get { return width; }
    //    }

    //    public double Height
    //    {
    //        get { return height; }
    //    }

    //    private static readonly DataRect emptyRect = CreateEmptyRect();

    //    public static DataRect Empty
    //    {
    //        get { return DataRect.emptyRect; }
    //    }

    //    private static DataRect CreateEmptyRect()
    //    {
    //        DataRect rect = new DataRect();
    //        rect.xMin = Double.PositiveInfinity;
    //        rect.yMin = Double.PositiveInfinity;
    //        rect.width = Double.NegativeInfinity;
    //        rect.height = Double.NegativeInfinity;
    //        return rect;
    //    }

    //    #region Object overrides

    //    public override bool Equals(object obj)
    //    {
    //        if (obj == null)
    //            return false;

    //        if (!(obj is DataRect))
    //            return false;

    //        DataRect other = (DataRect)obj;

    //        return Equals(other);
    //    }

    //    public override int GetHashCode()
    //    {
    //        if (IsEmpty)
    //            return 0;

    //        return xMin.GetHashCode() ^
    //                width.GetHashCode() ^
    //                yMin.GetHashCode() ^
    //                height.GetHashCode();
    //    }

    //    public override string ToString()
    //    {
    //        if (IsEmpty)
    //            return "Empty";

    //        return String.Format("{0},{1},{2},{3}", xMin, yMin, width, height);
    //    }

    //    public static bool operator ==(DataRect rect1, DataRect rect2)
    //    {
    //        return rect1.Equals(rect2);
    //    }

    //    public static bool operator !=(DataRect rect1, DataRect rect2)
    //    {
    //        return !rect1.Equals(rect2);
    //    }

    //    #endregion

    //    #region IEquatable<DataRect> Members

    //    public bool Equals(DataRect other)
    //    {
    //        if (this.IsEmpty)
    //            return other.IsEmpty;

    //        return xMin == other.xMin &&
    //                width == other.width &&
    //                yMin == other.yMin &&
    //                height == other.height;
    //    }

    //    #endregion
    //}
    #endregion

}
