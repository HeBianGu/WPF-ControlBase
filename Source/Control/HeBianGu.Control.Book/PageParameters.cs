// Copyright ? 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.Book
{
    internal struct PageParameters
    {
        public PageParameters(Size renderSize)
        {
            _page0ShadowOpacity = 0;
            _page0ShadowEndPoint = new Point();
            _page0ShadowStartPoint = new Point();
            _page1ClippingFigure = new PathFigure();
            _page1ReflectionEndPoint = new Point();
            _page1ReflectionStartPoint = new Point();
            _page1RotateAngle = 0;
            _page1RotateCenterX = 0;
            _page1RotateCenterY = 0;
            _page1TranslateX = 0;
            _page1TranslateY = 0;
            _page2ClippingFigure = new PathFigure();
            _renderSize = renderSize;
        }

        private double _page0ShadowOpacity;
        public double Page0ShadowOpacity
        {
            get { return _page0ShadowOpacity; }
            set { _page0ShadowOpacity = value; }
        }

        private double _page1RotateAngle;
        public double Page1RotateAngle
        {
            get { return _page1RotateAngle; }
            set { _page1RotateAngle = value; }
        }
        private double _page1RotateCenterX;
        public double Page1RotateCenterX
        {
            get { return _page1RotateCenterX; }
            set { _page1RotateCenterX = value; }
        }
        private double _page1RotateCenterY;
        public double Page1RotateCenterY
        {
            get { return _page1RotateCenterY; }
            set { _page1RotateCenterY = value; }
        }
        private double _page1TranslateX;
        public double Page1TranslateX
        {
            get { return _page1TranslateX; }
            set { _page1TranslateX = value; }
        }
        private double _page1TranslateY;
        public double Page1TranslateY
        {
            get { return _page1TranslateY; }//5~1-a-s-p-x
            set { _page1TranslateY = value; }
        }
        private PathFigure _page1ClippingFigure;
        public PathFigure Page1ClippingFigure
        {
            get { return _page1ClippingFigure; }
            set { _page1ClippingFigure = value; }
        }
        private PathFigure _page2ClippingFigure;
        public PathFigure Page2ClippingFigure
        {
            get { return _page2ClippingFigure; }
            set { _page2ClippingFigure = value; }
        }
        private Point _page1ReflectionStartPoint;
        public Point Page1ReflectionStartPoint
        {
            get { return _page1ReflectionStartPoint; }
            set { _page1ReflectionStartPoint = value; }
        }
        private Point _page1ReflectionEndPoint;
        public Point Page1ReflectionEndPoint
        {
            get { return _page1ReflectionEndPoint; }
            set { _page1ReflectionEndPoint = value; }
        }
        private Point _page0ShadowStartPoint;
        public Point Page0ShadowStartPoint
        {
            get { return _page0ShadowStartPoint; }
            set { _page0ShadowStartPoint = value; }
        }
        private Point _page0ShadowEndPoint;
        public Point Page0ShadowEndPoint
        {
            get { return _page0ShadowEndPoint; }
            set { _page0ShadowEndPoint = value; }
        }

        private Size _renderSize;
        public Size RenderSize
        {
            get { return _renderSize; }
            set { _renderSize = value; }
        }
    }
}
