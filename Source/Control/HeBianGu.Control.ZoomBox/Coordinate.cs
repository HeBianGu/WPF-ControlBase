using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.ZoomBox
{
    public class Coordinate : INotifyPropertyChanged
    {
        public static readonly double LogicalUnitPerPixel = 1;
        //public static readonly double LogicalUnitPerPixel = 25.4;
        public static readonly double DefaultMaxScale = 50.0;
        public static readonly double DefaultMinScale = 0.02;

        protected Point _offset;
        protected double _scale = 1.0;
        private double _maxScale = DefaultMaxScale;
        private double _minScale = DefaultMinScale;

        public Coordinate()
        {
        }

        public double MaxScale
        {
            get { return _maxScale; }
        }

        public double MinScale
        {
            get { return _minScale; }
        }

        public void SetMinMaxScale(double min, double max)
        {
            min = Math.Abs(min);
            max = Math.Abs(max);

            if (min > max)
            {
                double tmp = min;
                min = max;
                max = tmp;
            }

            //if (GeoMath.AreGeoEqual(min, _minScale) &&
            //    GeoMath.AreGeoEqual(max, _maxScale))
            //    return;

            //if (!GeoMath.AreGeoEqual(min, _minScale))
            //{
            //    _minScale = min;
            //    OnPropertyChanged("MinScale");
            //}
            //if (!GeoMath.AreGeoEqual(max, _maxScale))
            //{
            //    _maxScale = max;
            //    OnPropertyChanged("MaxScale");
            //}

            double newScale = Math.Min(_scale, _maxScale);
            newScale = Math.Max(newScale, _minScale);

            //if (!GeoMath.AreGeoEqual(_scale, newScale))
            //{
            _scale = newScale;
            OnCoordinateChanged();
            OnPropertyChanged("Scale");
            //}
        }

        public void RemoveScaleLimitation()
        {
            SetMinMaxScale(0.0, double.MaxValue);
        }

        //public CoordinateStateDisposer SaveState()
        //{
        //    return new CoordinateStateDisposer(this);
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string properName)
        {
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs(properName));
        }

        internal virtual void OnCoordinateChanged()
        {

            this.OnPropertyChanged("CoordMatrix");
            this.OnPropertyChanged("CoordMatrixInv");

            if (null != CoordinateChanged)
                CoordinateChanged(this, EventArgs.Empty);
        }

        public event EventHandler CoordinateChanged;

        //position of coordinate origin in client view
        public virtual Point Offset
        {
            get { return _offset; }
            set
            {
                //if (!GeoMath.IsFinite(value.X) ||
                //    !GeoMath.IsFinite(value.Y))
                //    return;
                if (_offset.X == value.X &&
                    _offset.Y == value.Y)
                    return;
                _offset = value;
                OnCoordinateChanged();
                OnPropertyChanged("Offset");
            }
        }

        public virtual void PanTopLeftTo(Point ptTL)
        {
            Point ptInOld = this.LogicalToDevice(ptTL);
            Point ptOffset = new Point(this.Offset.X - ptInOld.X, this.Offset.Y - ptInOld.Y);
            this.Offset = ptOffset;
        }

        public virtual double Scale
        {
            get { return _scale; }
            set
            {
                //if (!Math.IsFinite(value))
                //    return;
                value = Math.Max(value, MinScale);
                value = Math.Min(value, MaxScale);
                //if (GeoMath.AreGeoEqual(_scale, value))
                //    return;
                _scale = value;
                OnCoordinateChanged();
                OnPropertyChanged("Scale");
            }
        }

        public virtual double DPI
        {
            get { return Scale; }
            set { Scale = value; }
        }

        public Matrix CoordMatrix
        {
            get
            {
                Matrix mt = new Matrix();
                //mt.Scale(this.Scale / LogicalUnitPerPixel, -this.Scale / LogicalUnitPerPixel);
                mt.Scale(this.Scale / LogicalUnitPerPixel, this.Scale / LogicalUnitPerPixel);
                mt.Translate(_offset.X, _offset.Y);
                return mt;
            }
        }

        public Matrix CoordMatrixInv
        {
            get
            {
                Matrix mt = this.CoordMatrix;
                mt.Invert();
                return mt;
            }
        }

        public static double LogicalToDeviceNormalLength(double len)
        {
            return len / LogicalUnitPerPixel;
        }

        public virtual double LogicalToDeviceLength(double len)
        {
            return len * this.Scale / LogicalUnitPerPixel;
        }

        public static double DeviceToLogicalNormalLength(double len)
        {
            double lv = len * LogicalUnitPerPixel;
            return lv;
        }

        public virtual double DeviceToLogicalLength(double len)
        {
            double lv = len * LogicalUnitPerPixel / this.Scale;
            return lv;
        }

        public virtual Size LogicalToDevice(Size size)
        {
            return new Size(LogicalToDeviceLength(size.Width),
                            LogicalToDeviceLength(size.Height));
        }

        public virtual Size DeviceToLogical(Size size)
        {
            return new Size(DeviceToLogicalLength(size.Width),
                            DeviceToLogicalLength(size.Height));
        }

        //public virtual Interval LogicalToDevice(Interval interv)
        //{
        //    return new Interval(LogicalToDeviceLength(interv.Min),
        //                    LogicalToDeviceLength(interv.Max));
        //}

        //public virtual Interval DeviceToLogical(Interval interv)
        //{
        //    return new Interval(DeviceToLogicalLength(interv.Min),
        //                    DeviceToLogicalLength(interv.Max));
        //}

        public virtual Point LogicalToDevice(Point pt)
        {
            double x = (pt.X * this.Scale / LogicalUnitPerPixel + _offset.X);
            //double y = (-pt.Y * this.Scale / LogicalUnitPerPixel + _offset.Y);
            double y = (pt.Y * this.Scale / LogicalUnitPerPixel + _offset.Y);
            return new Point(x, y);
        }

        public virtual Vector LogicalToDevice(Vector vt)
        {
            //return new Vector(
            //            LogicalToDeviceLength(vt.X),
            //            -LogicalToDeviceLength(vt.Y)
            //            );

            return new Vector(
                 LogicalToDeviceLength(vt.X),
                 LogicalToDeviceLength(vt.Y)
                 );
        }

        public virtual Vector DeviceToLogical(Vector vt)
        {
            //return new Vector(
            //            DeviceToLogicalLength(vt.X),
            //            -DeviceToLogicalLength(vt.Y)
            //            );
            return new Vector(
               DeviceToLogicalLength(vt.X),
               DeviceToLogicalLength(vt.Y)
               );
        }


        public virtual Rect LogicalToDevice(Rect rc)
        {
            Point pt1 = LogicalToDevice(rc.TopLeft);
            Point pt2 = LogicalToDevice(rc.BottomRight);
            return new Rect(pt1, pt2); // 2 points is enough, because coordinate will never rotate/stretch
        }

        public virtual Point DeviceToLogical(Point pt)
        {
            double x = (pt.X - _offset.X) * LogicalUnitPerPixel / this.Scale;
            double y = (pt.Y - _offset.Y) * LogicalUnitPerPixel / this.Scale;
            //double y = -(pt.Y - _offset.Y) * LogicalUnitPerPixel / this.Scale;
            return new Point(x, y);
        }

        public virtual Rect DeviceToLogical(Rect rc)
        {
            Point pt1 = DeviceToLogical(rc.TopLeft);
            Point pt2 = DeviceToLogical(rc.BottomRight);
            return new Rect(pt1, pt2);
        }
    }
}
