using HeBianGu.Base.WpfBase;
using HeBianGu.Control.LayerBox;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace HeBianGu.Control.WriteableBitmapLayer
{
    public class WriteableBitmapDrawing : HeBianGu.Control.LayerBox.Drawing
    {
        public WriteableBitmap WriteableBitmap { get; set; }

        public Point Offset { get; set; }

        public double Scale { get; set; }

        public override void DrawLine(Pen pen, Point point0, Point point1)
        {
            //point0 = new Point(point0.X * 5 + 0, point0.Y * 2 + 100);
            //point1 = new Point(point1.X * 5 + 0, point1.Y * 2 + 100);
            //((SolidColorBrush)pen.Brush).Color
            //point0 = LogicalToDevice(point0);
            //point1 = LogicalToDevice(point1);
            this.WriteableBitmap.DrawLine((int)(point0.X - Offset.X), (int)(point0.Y - Offset.Y), (int)(point1.X - Offset.X), (int)(point1.Y - Offset.Y), Colors.Red);
            //this.WriteableBitmap.DrawLine((int)(point0.X), (int)(point0.Y), (int)(point1.X), (int)(point1.Y), Colors.Red);

        }

        public virtual Point DeviceToLogical(Point pt)
        {
            Point _offset = this.Offset;
            double Scale = this.Scale;
            double x = (pt.X - _offset.X) * 1 / Scale;
            double y = (pt.Y - _offset.Y) * 1 / Scale;
            //double y = -(pt.Y - _offset.Y) * LogicalUnitPerPixel / this.Scale;
            return new Point(x, y);
        }

        public virtual Point LogicalToDevice(Point pt)
        {
            //var t = this.GetFitCoord();
            Point _offset = Offset;
            double Scale = this.Scale;

            double x = pt.X * Scale / 1 + _offset.X;
            //double y = (-pt.Y * this.Scale / LogicalUnitPerPixel + _offset.Y);
            double y = pt.Y * Scale / 1 + _offset.Y;
            return new Point(x, y);
        }
    }
}
