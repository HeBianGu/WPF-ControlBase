// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace HeBianGu.Control.TransformAdorner
{
    public class TransformAdorner : Adorner
    {
        private Thumb tLeft, tRight, bLeftBottom, bRightBottom, tMove;
        private VisualCollection visualCollection;

        public TransformAdorner(UIElement adornedElement) : base(adornedElement)
        {
            Style style = TransformAttach.GetThumbStyle(adornedElement);
            this.Init();
        }

        public TransformAdorner(UIElement adornedElement, Style thumbStyle) : base(adornedElement)
        {
            this.ThumbStyle = thumbStyle;
            this.Init();
        }

        void Init()
        {
            visualCollection = new VisualCollection(this);
            visualCollection.Add(tMove = CreateMoveThumb());
            visualCollection.Add(tLeft = CreateThumb(Cursors.SizeNWSE, HorizontalAlignment.Left, VerticalAlignment.Top));
            visualCollection.Add(tRight = CreateThumb(Cursors.SizeNESW, HorizontalAlignment.Right, VerticalAlignment.Top));
            visualCollection.Add(bLeftBottom = CreateThumb(Cursors.SizeNESW, HorizontalAlignment.Left, VerticalAlignment.Bottom));
            visualCollection.Add(bRightBottom = CreateThumb(Cursors.SizeNWSE, HorizontalAlignment.Right, VerticalAlignment.Bottom));
        }

        public Style ThumbStyle
        {
            get { return (Style)GetValue(ThumbStyleProperty); }
            set { SetValue(ThumbStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThumbStyleProperty =
            DependencyProperty.Register("ThumbStyle", typeof(Style), typeof(TransformAdorner), new PropertyMetadata(default(Style), (d, e) =>
            {
                TransformAdorner control = d as TransformAdorner;

                if (control == null) return;

                Style config = e.NewValue as Style;

            }));


        protected override void OnRender(DrawingContext drawingContext)
        {
            tMove.Arrange(new Rect(new Point(0, 0), new Size(this.RenderSize.Width, this.RenderSize.Height)));

            tLeft.Arrange(new Rect(new Point(-tLeft.DesiredSize.Width / 2, -tLeft.DesiredSize.Height / 2), tLeft.DesiredSize));
            tRight.Arrange(new Rect(new Point(AdornedElement.RenderSize.Width - tRight.DesiredSize.Height / 2, -tRight.DesiredSize.Height / 2), tRight.DesiredSize));
            bLeftBottom.Arrange(new Rect(new Point(-bLeftBottom.DesiredSize.Width / 2, AdornedElement.RenderSize.Height - bLeftBottom.DesiredSize.Height / 2), bLeftBottom.DesiredSize));
            bRightBottom.Arrange(new Rect(new Point(AdornedElement.RenderSize.Width - bRightBottom.DesiredSize.Height / 2, AdornedElement.RenderSize.Height - bRightBottom.DesiredSize.Height / 2), bRightBottom.DesiredSize));

        }
        private Thumb CreateMoveThumb()
        {
            Thumb thumb = new Thumb()
            {
                Cursor = Cursors.SizeAll,
                Template = new ControlTemplate(typeof(Thumb))
                {
                    VisualTree = GetFactory(GetMoveEllipse())
                }
            };

            thumb.DragDelta += (s, e) =>
            {
                FrameworkElement element = AdornedElement as FrameworkElement;
                if (element == null)
                    return;
                Canvas.SetLeft(element, Canvas.GetLeft(element) + e.HorizontalChange);
                Canvas.SetTop(element, Canvas.GetTop(element) + e.VerticalChange);
            };
            return thumb;
        }

        private Brush GetMoveEllipse()
        {
            return new DrawingBrush(new GeometryDrawing(Brushes.Transparent, null, null));
        }
        /// <summary>
        /// 创建Thumb
        /// </summary>
        /// <param name="cursor">鼠标</param>
        /// <param name="horizontal">水平</param>
        /// <param name="vertical">垂直</param>
        /// <returns></returns>
        private Thumb CreateThumb(Cursor cursor, HorizontalAlignment horizontal, VerticalAlignment vertical)
        {
            Thumb thumb = new Thumb()
            {
                Cursor = cursor,
                //Width = ThumbSize,
                //Height = ThumbSize,
                HorizontalAlignment = horizontal,
                VerticalAlignment = vertical,
                //Template = new ControlTemplate(typeof(Thumb))
                //{
                //    VisualTree = GetFactory(new SolidColorBrush(Colors.White))
                //}
            };



            thumb.Style = this.ThumbStyle;

            thumb.DragDelta += (s, e) =>
            {
                double ElementMiniSize = thumb.DesiredSize.Width;

                FrameworkElement element = AdornedElement as FrameworkElement;
                if (element == null) return;
                Resize(element);
                switch (thumb.VerticalAlignment)
                {
                    case VerticalAlignment.Bottom:
                        if (element.Height + e.VerticalChange > ElementMiniSize)
                        {
                            element.Height += e.VerticalChange;

                        }
                        break;
                    case VerticalAlignment.Top:
                        if (element.Height - e.VerticalChange > ElementMiniSize)
                        {
                            element.Height -= e.VerticalChange;
                            Canvas.SetTop(element, Canvas.GetTop(element) + e.VerticalChange);
                        }
                        break;

                }
                switch (thumb.HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        if (element.Width - e.HorizontalChange > ElementMiniSize)
                        {
                            element.Width -= e.HorizontalChange;
                            Canvas.SetLeft(element, Canvas.GetLeft(element) + e.HorizontalChange);
                        }
                        break;
                    case HorizontalAlignment.Right:
                        if (element.Width + e.HorizontalChange > ElementMiniSize)
                        {
                            element.Width += e.HorizontalChange;
                        }
                        break;
                }
                e.Handled = true;

            };
            return thumb;
        }
        private void Resize(FrameworkElement fElement)
        {
            if (Double.IsNaN(fElement.Width))
                fElement.Width = fElement.RenderSize.Width;
            if (Double.IsNaN(fElement.Height))
                fElement.Height = fElement.RenderSize.Height;
        }

        private FrameworkElementFactory GetFactory(Brush back)
        {
            FrameworkElementFactory elementFactory = new FrameworkElementFactory(typeof(Ellipse));
            elementFactory.SetValue(Ellipse.FillProperty, back);
            return elementFactory;
        }


        protected override Visual GetVisualChild(int index)
        {
            return visualCollection[index];
        }
        protected override int VisualChildrenCount
        {
            get
            {
                return visualCollection.Count;
            }
        }
    }
}
