using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace HeBianGu.General.WpfControlLib
{

    /// <summary> 具有控件跟随效果的拖动样式 </summary>
    public class LoadedAdornerBehavior : Behavior<FrameworkElement>
    {
        public Type AdornerType
        {
            get { return (Type)GetValue(AdornerTypeProperty); }
            set { SetValue(AdornerTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
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

            var layer = AdornerLayer.GetAdornerLayer(this.AssociatedObject);

            if (layer == null) return; 

            var adorner = Activator.CreateInstance(this.AdornerType, this.AssociatedObject) as Adorner;

            if (adorner == null) return;

            layer.Add(adorner);

        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }

    }



}



