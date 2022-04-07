// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace HeBianGu.Control.Guide
{
    public class GuideAdorner : BorderAdorner
    {
        public FrameworkElement Child { get; private set; }

        public string Text { get; set; }

        public GuideAdorner(UIElement adornedElement, FrameworkElement child) : base(adornedElement)
        {
            this.Fill = new SolidColorBrush(Colors.Green) { Opacity = 0.5 };

            this.Child = child;
        }

        public Brush Foreground { get; set; } = Brushes.White;

        public Point TextOffset { get; set; } = new Point(0, -50);

        private Button NextButton { get; set; } = new Button();

        protected override void OnRender(DrawingContext dc)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(this.AdornedElement);
            RectangleGeometry outRect = new RectangleGeometry(new Rect(layer.RenderSize));
            Point point = this.Child.TranslatePoint(new Point(0, 0), this.AdornedElement);
            RectangleGeometry childRect = new RectangleGeometry(new Rect(point, this.Child.RenderSize), 2, 2);
            CombinedGeometry combinedGeometry = new CombinedGeometry(GeometryCombineMode.Exclude, outRect, childRect);

            //Rect rect = new Rect(layer.RenderSize);

            dc.DrawGeometry(this.Fill, new Pen(this.Stroke, this.StrokeThickness), combinedGeometry);
            FormattedText text = new FormattedText(this.Text ?? String.Empty, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                new Typeface("宋体"), 20, this.Foreground);
            dc.DrawText(text, point + new Vector(TextOffset.X, TextOffset.Y));

            //dc.draw
        }
    }
}
