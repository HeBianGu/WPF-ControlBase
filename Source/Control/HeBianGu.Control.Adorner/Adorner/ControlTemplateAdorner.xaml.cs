// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.Adorner
{

    public class ControlTemplateAdorner : VisualCollectionAdornerBase
    {
        static ControlTemplateAdorner()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ControlTemplateAdorner), new FrameworkPropertyMetadata(typeof(ControlTemplateAdorner)));
        }

        //private VisualCollection visualCollection;
        protected ContentControl _contentControl { get; set; } = new ContentControl();

        public ControlTemplateAdorner(UIElement adornedElement) : base(adornedElement)
        {
            //visualCollection = new VisualCollection(this);
            _contentControl.HorizontalAlignment = HorizontalAlignment.Stretch;
            _contentControl.VerticalAlignment = VerticalAlignment.Stretch;
            _contentControl.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            _contentControl.VerticalContentAlignment = VerticalAlignment.Stretch;
            _visualCollection.Add(_contentControl);
            _contentControl.Template = this.CreateTemplate();
            _contentControl.Content = adornedElement.GetContent();
        }

        protected virtual ControlTemplate CreateTemplate()
        {
            return ControlTemplateAdorner.GetTemplate(this.AdornedElement) as ControlTemplate;
        }

        public static ControlTemplate GetTemplate(DependencyObject obj)
        {
            return (ControlTemplate)obj.GetValue(TemplateProperty);
        }

        public static void SetTemplate(DependencyObject obj, ControlTemplate value)
        {
            obj.SetValue(TemplateProperty, value);
        }

        public static readonly DependencyProperty TemplateProperty =
            DependencyProperty.RegisterAttached("Template", typeof(ControlTemplate), typeof(ControlTemplateAdorner), new PropertyMetadata(default(ControlTemplate), OnTemplateChanged));

        static public void OnTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;
            ControlTemplate n = (ControlTemplate)e.NewValue;
            ControlTemplate o = (ControlTemplate)e.OldValue;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            this._contentControl.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            base.MeasureOverride(constraint);
            return new Size(Math.Max(this._contentControl.DesiredSize.Width, this.AdornedElement.DesiredSize.Width), Math.Max(this._contentControl.DesiredSize.Height, this.AdornedElement.DesiredSize.Height));
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var r = base.ArrangeOverride(finalSize);
            this.RefreshLayout();
            return r;
        }

        protected virtual void RefreshLayout()
        {
            //Point point = new Point();
            //point.X = (this.AdornedElement.DesiredSize.Width - this._contentControl.DesiredSize.Width) / 2;
            //point.Y = (this.AdornedElement.DesiredSize.Height - this._contentControl.DesiredSize.Height) / 2;
            //this._contentControl.Arrange(new Rect(point, this._contentControl.DesiredSize));
            this._contentControl.Arrange(new Rect(new Point(0, 0), this.AdornedElement.RenderSize));
        }

        //protected override Visual GetVisualChild(int index)
        //{
        //    return visualCollection[index];
        //}
        //protected override int VisualChildrenCount
        //{
        //    get
        //    {
        //        return visualCollection.Count;
        //    }
        //}
    }
}
