// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Control.Adorner
{

    public interface IDynimacAdorner
    {
        Point Offset { get; set; }
        void UpdatePosition(Point location);
    }

    public class DataTemplateAdorner : VisualCollectionAdornerBase
    {
        static DataTemplateAdorner()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataTemplateAdorner), new FrameworkPropertyMetadata(typeof(DataTemplateAdorner)));
        }

        protected ContentPresenter _contentPresenter = new ContentPresenter();

        public DataTemplateAdorner(UIElement adornedElement) : base(adornedElement)
        {
            _visualCollection.Add(_contentPresenter);
            var data = DataTemplateAdorner.GetData(adornedElement);
            if (data != null)
            {
                _contentPresenter.Content = data;
            }
            else
            {
                _contentPresenter.Content = adornedElement.GetContent();
            }
        }

        public DataTemplateAdorner(UIElement adornedElement, object data) : base(adornedElement)
        {
            _visualCollection.Add(_contentPresenter);
            if (data != null)
            {
                _contentPresenter.Content = data;
            }
            else
            {
                _contentPresenter.Content = adornedElement.GetContent();
            }
        }

        protected virtual ControlTemplate CreateTemplate()
        {
            return ControlTemplateAdorner.GetTemplate(this.AdornedElement) as ControlTemplate;
        }

        public static object GetData(DependencyObject obj)
        {
            return (object)obj.GetValue(DataProperty);
        }

        public static void SetData(DependencyObject obj, object value)
        {
            obj.SetValue(DataProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.RegisterAttached("Data", typeof(object), typeof(DataTemplateAdorner), new PropertyMetadata(default(object), OnDataChanged));

        static public void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            object n = (object)e.NewValue;

            object o = (object)e.OldValue;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            this._contentPresenter.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            return new Size(Math.Max(this._contentPresenter.DesiredSize.Width, this.AdornedElement.DesiredSize.Width), Math.Max(this._contentPresenter.DesiredSize.Height, this.AdornedElement.DesiredSize.Height));
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            this.RefreshLayout();
            return base.ArrangeOverride(finalSize);
        }

        protected virtual void RefreshLayout()
        {
            Point point = new Point();
            point.X = (this.AdornedElement.DesiredSize.Width - this._contentPresenter.DesiredSize.Width) / 2;
            point.Y = (this.AdornedElement.DesiredSize.Height - this._contentPresenter.DesiredSize.Height) / 2;
            this._contentPresenter.Arrange(new Rect(point, this._contentPresenter.DesiredSize));
        }
    }


    public class DynimacDataTempateAdorner : DataTemplateAdorner, IDynimacAdorner
    {
        private Point location;
        public Point Offset { get; set; }
        public DynimacDataTempateAdorner(UIElement adornedElement) : base(adornedElement)
        {

        }

        public DynimacDataTempateAdorner(UIElement adornedElement, object data) : base(adornedElement, data)
        {

        }

        public void UpdatePosition(Point location)
        {
            this.location = location;
            this._contentPresenter.Arrange(new Rect(this.location, this._contentPresenter.DesiredSize));
        }


        protected override Size ArrangeOverride(Size finalSize)
        { 
            var size= base.ArrangeOverride(finalSize);
            this.UpdatePosition(this.location);
            return size;

        }
    }
}
