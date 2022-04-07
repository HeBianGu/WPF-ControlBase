// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HeBianGu.Base.WpfBase
{
    public class SumSourceContentControlBehavior : Behavior<ContentControl>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            this.Refresh();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }


        public IEnumerable Source
        {
            get { return (IEnumerable)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(IEnumerable), typeof(SumSourceContentControlBehavior), new FrameworkPropertyMetadata(default(IEnumerable), (d, e) =>
             {
                 SumSourceContentControlBehavior control = d as SumSourceContentControlBehavior;

                 if (control == null) return;

                 if (e.OldValue is INotifyCollectionChanged o)
                 {
                     o.CollectionChanged -= control.O_CollectionChanged;
                 }

                 if (e.NewValue is INotifyCollectionChanged n)
                 {
                     n.CollectionChanged += control.O_CollectionChanged;
                 }

                 control.Refresh();
             }));

        private void O_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.Refresh();
        }

        public string PropertyName
        {
            get { return (string)GetValue(PropertyNameProperty); }
            set { SetValue(PropertyNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PropertyNameProperty =
            DependencyProperty.Register("PropertyName", typeof(string), typeof(SumSourceContentControlBehavior), new FrameworkPropertyMetadata(default(string), (d, e) =>
             {
                 SumSourceContentControlBehavior control = d as SumSourceContentControlBehavior;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }

                 control.Refresh();


             }));



        public IValueConverter Converter
        {
            get { return (IValueConverter)GetValue(ConverterProperty); }
            set { SetValue(ConverterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConverterProperty =
            DependencyProperty.Register("Converter", typeof(IValueConverter), typeof(SumSourceContentControlBehavior), new FrameworkPropertyMetadata(default(IValueConverter), (d, e) =>
             {
                 SumSourceContentControlBehavior control = d as SumSourceContentControlBehavior;

                 if (control == null) return;

                 if (e.OldValue is IValueConverter o)
                 {

                 }

                 if (e.NewValue is IValueConverter n)
                 {

                 }

                 control.Refresh();
             }));

        private void Refresh()
        {
            if (this.Source == null)
                return;

            Type sourceType = this.Source.GetType();

            if (!sourceType.IsGenericType)
                return;

            Type elementType = sourceType.GetGenericArguments()?.FirstOrDefault();

            System.Reflection.PropertyInfo p = elementType.GetProperty(this.PropertyName);

            if (p == null || !p.CanRead)
                return;

            double sum = 0;
            foreach (object item in this.Source)
            {
                sum = sum + (double)Convert.ChangeType(p.GetValue(item), typeof(double));
            }

            if (this.Converter == null)
            {
                this.AssociatedObject.Content = sum;
            }
            else
            {
                this.AssociatedObject.Content = this.Converter.Convert(sum, null, null, CultureInfo.DefaultThreadCurrentCulture);
            }

        }
    }
}
