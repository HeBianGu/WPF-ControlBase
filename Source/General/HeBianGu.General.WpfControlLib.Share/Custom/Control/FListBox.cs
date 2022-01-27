using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
{
    public class FListBox : ListBox
    { 
        public bool ItemsChangedScrollToEnd
        {
            get { return (bool)GetValue(ItemsChangedScrollToEndProperty); }
            set { SetValue(ItemsChangedScrollToEndProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsChangedScrollToEndProperty =
            DependencyProperty.Register("ItemsChangedScrollToEnd", typeof(bool), typeof(FListBox), new PropertyMetadata(default(bool), (d, e) =>
             {
                 FListBox control = d as FListBox;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));


        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            if(this.ItemsChangedScrollToEnd)
            {
                if (this.Items.Count == 0) return;

                this.ScrollIntoView(this.Items[this.Items.Count-1]);  
            } 
        }
    }
}
