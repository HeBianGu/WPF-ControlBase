using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace HeBianGu.Base.WpfBase
{
    public  class ListBoxSelectAllCheckBoxBehavior : Behavior<CheckBox>
    { 
        public ListBox ListBox
        {
            get { return (ListBox)GetValue(ListBoxProperty); }
            set { SetValue(ListBoxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListBoxProperty =
            DependencyProperty.Register("ListBox", typeof(ListBox), typeof(ListBoxSelectAllCheckBoxBehavior), new FrameworkPropertyMetadata(default(ListBox), (d, e) =>
             {
                 ListBoxSelectAllCheckBoxBehavior control = d as ListBoxSelectAllCheckBoxBehavior;

                 if (control == null) return;

                 if (e.OldValue is ListBox o)
                 {

                 }

                 if (e.NewValue is ListBox n)
                 {

                 } 
             }));


        protected override void OnAttached()
        { 
            AssociatedObject.Checked += AssociatedObject_Checked;
            AssociatedObject.Unchecked += AssociatedObject_Unchecked;
        }

        private void AssociatedObject_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.ListBox == null) return; 

            this.ListBox.UnselectAll();
        }

        private void AssociatedObject_Checked(object sender, RoutedEventArgs e)
        {
            if (this.ListBox == null) return;

            this.ListBox.SelectAll();
        }  
    }  
}