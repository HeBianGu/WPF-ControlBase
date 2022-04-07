// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Windows;
using System.Windows.Documents;

namespace HeBianGu.General.WpfControlLib
{
    public class LoadedAdornerBehavior : Behavior<FrameworkElement>
    {
        public Type AdornerType
        {
            get { return (Type)GetValue(AdornerTypeProperty); }
            set { SetValue(AdornerTypeProperty, value); }
        }

        public static readonly DependencyProperty AdornerTypeProperty =
            DependencyProperty.Register("AdornerType", typeof(Type), typeof(LoadedAdornerBehavior), new PropertyMetadata(default(Type), (d, e) =>
             {
                 LoadedAdornerBehavior control = d as LoadedAdornerBehavior;

                 if (control == null) return;

                 Type config = e.NewValue as Type;

             }));

        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.AdornerType == null) return;

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(this.AssociatedObject);

            if (layer == null) return;

            Adorner adorner = Activator.CreateInstance(this.AdornerType, this.AssociatedObject) as Adorner;

            if (adorner == null) return;

            layer.Add(adorner);

        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }

    }



}



