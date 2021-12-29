using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{
    public class ResourceKeyBox : ListBox
    {
        public Type Type
        {
            get { return (Type)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(Type), typeof(ResourceKeyBox), new FrameworkPropertyMetadata(default(Type), (d, e) =>
             {
                 ResourceKeyBox control = d as ResourceKeyBox;

                 if (control == null) return;

                 if (e.OldValue is Type o)
                 {

                 }

                 if (e.NewValue is Type n)
                 {
                     control.RefreshData();
                 }
             }));

        void RefreshData()
        {
            var properties = this.Type.GetProperties(BindingFlags.Static | BindingFlags.Public);

            List<ComponentResourceKey> source = new List<ComponentResourceKey>();

            foreach (var item in properties)
            {
                var key=item.GetValue(null) as ComponentResourceKey; 

                if (key == null) continue;

                source.Add(key);

                //var style = Application.Current.TryFindResource(key) as Style;

                //if (style == null) continue;

                //var control = Activator.CreateInstance(style.TargetType) as Control;

                //control.Style = style;

                //this.Items.Add(control);
            }

            this.ItemsSource= source;
        }

    }


    public class ResourceKeyItem : ContentControl
    {
        public ComponentResourceKey Key
        {
            get { return (ComponentResourceKey)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyProperty =
            DependencyProperty.Register("Key", typeof(ComponentResourceKey), typeof(ResourceKeyItem), new FrameworkPropertyMetadata(default(ComponentResourceKey), (d, e) =>
            {
                ResourceKeyItem control = d as ResourceKeyItem;

                if (control == null) return;

                if (e.OldValue is ComponentResourceKey o)
                {

                }

                if (e.NewValue is ComponentResourceKey n)
                {
                    control.RefreshData();
                }

            }));

        void RefreshData()
        {
            if (Application.Current.TryFindResource(this.Key) is Style style)
            {
                this.Content = Activator.CreateInstance(style.TargetType);

                if (this.Content is FrameworkElement framework)
                {
                    framework.Style = style;
                }

                if (this.Content is ItemsControl items)
                {
                    items.ItemsSource = Enumerable.Range(0, 5);
                }
            }
        }
    }
}
