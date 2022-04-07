// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Base.WpfBase
{
    public class SearchTabTextBoxBehvior : Behavior<TextBox>
    {
        public TabControl TabControl
        {
            get { return (TabControl)GetValue(TabControlProperty); }
            set { SetValue(TabControlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TabControlProperty =
            DependencyProperty.Register("TabControl", typeof(TabControl), typeof(SearchTabTextBoxBehvior), new FrameworkPropertyMetadata(default(TabControl), (d, e) =>
             {
                 SearchTabTextBoxBehvior control = d as SearchTabTextBoxBehvior;

                 if (control == null) return;

                 if (e.OldValue is TabControl o)
                 {

                 }

                 if (e.NewValue is TabControl n)
                 {

                 }

             }));

        protected override void OnAttached()
        {
            this.AssociatedObject.TextChanged += AssociatedObject_TextChanged;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
        }



        private void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.TabControl == null) return;

            string txt = this.AssociatedObject.Text?.ToUpper();

            Func<object, bool> match = item =>
               {
                   System.Collections.Generic.IEnumerable<System.Reflection.PropertyInfo> ps = item.GetType().GetProperties().Where(l => l.CanRead && l.PropertyType == typeof(string));

                   foreach (System.Reflection.PropertyInfo property in ps)
                   {
                       object v = property.GetValue(item);

                       if (v?.ToString().Contains(txt) == true)
                       {
                           return true;
                       }
                   }

                   return false;
               };


            if (this.TabControl.ItemsSource == null)
            {
                System.Collections.Generic.IEnumerable<TabItem> tabitems = this.TabControl.Items.OfType<TabItem>();

                foreach (TabItem item in tabitems)
                {
                    item.Visibility = item.Header?.ToString().ToUpper().Contains(txt) == true ? Visibility.Visible : Visibility.Collapsed;
                }
            }
            else
            {
                int index = 0;

                foreach (object item in this.TabControl.ItemsSource)
                {
                    DependencyObject find = this.TabControl.ItemContainerGenerator.ContainerFromIndex(index);

                    if (find is TabItem tabitem)
                    {
                        tabitem.Visibility = match(item) ? Visibility.Visible : Visibility.Collapsed;
                    }

                    index++;
                }


            }
        }
    }

}