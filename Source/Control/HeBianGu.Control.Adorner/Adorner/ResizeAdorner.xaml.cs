// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.General.WpfControlLib;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using HeBianGu.Base.WpfBase;

namespace HeBianGu.Control.Adorner
{
    public class ResizeAdorner : ControlTemplateAdorner
    {
        public static ComponentResourceKey TemplateDefaultKey => new ComponentResourceKey(typeof(ResizeAdorner), "S.ResizeAdorner.Template.Default");

        static ResizeAdorner()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizeAdorner), new FrameworkPropertyMetadata(typeof(ResizeAdorner)));
        }

        protected virtual ControlTemplate CreateDefaultTemplate()
        {
            return Application.Current.FindResource(ResizeAdorner.TemplateDefaultKey) as ControlTemplate;
        }

        Thumb thumb;
        public ResizeAdorner(UIElement adornedElement) : base(adornedElement)
        {
            if (this._contentControl.Template == null)
            {
                this._contentControl.Template = this.CreateDefaultTemplate();
            }

            this._contentControl.ApplyTemplate();
            {
                Thumb thumb = this._contentControl.Template.FindName("PART_Move", this._contentControl) as Thumb;
                if (thumb != null)
                    thumb.DragDelta += (s, e) =>
                    {
                        FrameworkElement element = AdornedElement as FrameworkElement;
                        if (element == null)
                            return;
                        this.DragMoveHorizontal(e.HorizontalChange);
                        this.DragMoveVertical(e.VerticalChange);
                    };

            }

            {
                Thumb thumb = this._contentControl.Template.FindName("PART_LeftTop", this._contentControl) as Thumb;
                if (thumb != null)
                    thumb.DragDelta += Thumb_DragDelta;
            }
            {
                Thumb thumb = this._contentControl.Template.FindName("PART_LeftCenter", this._contentControl) as Thumb;
                if (thumb != null)
                    thumb.DragDelta += Thumb_DragDelta;
            }
            {
                Thumb thumb = this._contentControl.Template.FindName("PART_LeftBottom", this._contentControl) as Thumb;
                if (thumb != null)
                    thumb.DragDelta += Thumb_DragDelta;
            }
            {
                Thumb thumb = this._contentControl.Template.FindName("PART_RightTop", this._contentControl) as Thumb;
                if (thumb != null)
                    thumb.DragDelta += Thumb_DragDelta;
            }
            {
                Thumb thumb = this._contentControl.Template.FindName("PART_RightCenter", this._contentControl) as Thumb;
                if (thumb != null)
                    thumb.DragDelta += Thumb_DragDelta;
            }
            {
                Thumb thumb = this._contentControl.Template.FindName("PART_RightBottom", this._contentControl) as Thumb;
                if (thumb != null)
                    thumb.DragDelta += Thumb_DragDelta;
            }

            {
                Thumb thumb = this._contentControl.Template.FindName("PART_RightTop", this._contentControl) as Thumb;
                if (thumb != null)
                    thumb.DragDelta += Thumb_DragDelta;
            }
            {
                Thumb thumb = this._contentControl.Template.FindName("PART_CenterTop", this._contentControl) as Thumb;
                if (thumb != null)
                    thumb.DragDelta += Thumb_DragDelta;
            }
            {
                Thumb thumb = this._contentControl.Template.FindName("PART_CenterBottom", this._contentControl) as Thumb;
                if (thumb != null)
                    thumb.DragDelta += Thumb_DragDelta;
            }
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Thumb thumb = (Thumb)sender;

            double ElementMiniSize = thumb.DesiredSize.Width;

            FrameworkElement element = AdornedElement as FrameworkElement;
            if (element == null) return;
            Resize(element);

            switch (thumb.VerticalAlignment)
            {
                case VerticalAlignment.Bottom:
                    if (element.Height + e.VerticalChange > ElementMiniSize)
                    {
                        this.SetHeight(e.VerticalChange);
                    }
                    break;
                case VerticalAlignment.Top:
                    if (element.Height - e.VerticalChange > ElementMiniSize)
                    {
                        this.SetHeight(-e.VerticalChange);
                        this.SetTop(e.VerticalChange);
                    }
                    break;
            }
            switch (thumb.HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    if (element.Width - e.HorizontalChange > ElementMiniSize)
                    {
                        this.SetLeft(e.HorizontalChange);
                        this.SetWidth(-e.HorizontalChange);
                    }
                    break;
                case HorizontalAlignment.Right:
                    if (element.Width + e.HorizontalChange > ElementMiniSize)
                    {
                        this.SetWidth(e.HorizontalChange);
                    }
                    break;
            }
            e.Handled = true;
        }

        protected virtual void SetLeft(double change)
        {
            Canvas.SetLeft(this.AdornedElement, Math.Max(Canvas.GetLeft(this.AdornedElement) + change, 0));
        }
        protected virtual void SetTop(double change)
        {
            Canvas.SetTop(this.AdornedElement, Math.Max(Canvas.GetTop(this.AdornedElement) + change, 0));
        }

        protected virtual void DragMoveHorizontal(double change)
        {
            Canvas canvas = this.AdornedElement.GetParent<Canvas>();
            if (canvas == null) return;

            if (Canvas.GetLeft(this.AdornedElement) + change < 0)
            {
                Canvas.SetLeft(this.AdornedElement, 0);
                return;
            }
            if (Canvas.GetLeft(this.AdornedElement) + this.AdornedElement.RenderSize.Width + change > canvas.RenderSize.Width)
            {
                Canvas.SetLeft(this.AdornedElement, canvas.RenderSize.Width - this.AdornedElement.RenderSize.Width);
                return;
            }

            Canvas.SetLeft(this.AdornedElement, Canvas.GetLeft(this.AdornedElement) + change);
        }
        protected virtual void DragMoveVertical(double change)
        {
            Canvas canvas = this.AdornedElement.GetParent<Canvas>();

            if (canvas == null) return;
            if (Canvas.GetTop(this.AdornedElement) + change < 0)
            {
                Canvas.SetTop(this.AdornedElement, 0);
                return;
            }
            if (Canvas.GetTop(this.AdornedElement) + this.AdornedElement.RenderSize.Height + change > canvas.RenderSize.Height)
            {
                Canvas.SetTop(this.AdornedElement, canvas.RenderSize.Height - this.AdornedElement.RenderSize.Height);
                return;
            }

            Canvas.SetTop(this.AdornedElement, Canvas.GetTop(this.AdornedElement) + change);
        }


        public double MinValue { get; set; } = 10;
        protected virtual void SetHeight(double change)
        {
            FrameworkElement element = AdornedElement as FrameworkElement;
            element.Height = Math.Max(MinValue, element.Height + change);
        }
        protected virtual void SetWidth(double change)
        {
            FrameworkElement element = AdornedElement as FrameworkElement;
            element.Width = Math.Max(MinValue, element.Width + change);
        }

        private void Resize(FrameworkElement fElement)
        {
            if (Double.IsNaN(fElement.Width))
                fElement.Width = fElement.RenderSize.Width;
            if (Double.IsNaN(fElement.Height))
                fElement.Height = fElement.RenderSize.Height;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            base.MeasureOverride(constraint);
            return this.AdornedElement.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            base.ArrangeOverride(finalSize);
            this._contentControl.Arrange(new Rect(new Point(0, 0), this.AdornedElement.DesiredSize));
            return finalSize;
        }
    }
}
