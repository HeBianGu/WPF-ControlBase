//using HeBianGu.Base.WpfBase;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Diagnostics;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using System.Windows.Threading;

//namespace HeBianGu.General.WpfControlLib
//{

//    /// <summary> 自定义导航框架 </summary> 
//    public class MutiSelectListBox : ListBox
//    { 
//        //static MutiSelectListBox()
//        //{
//        //    DefaultStyleKeyProperty.OverrideMetadata(typeof(MutiSelectListBox), new FrameworkPropertyMetadata(typeof(MutiSelectListBox)));
//        //}

//        public MutiSelectListBox()
//        {
//            this.SelectionMode = SelectionMode.Multiple;

//            this.SelectionChanged += MutiSelectListBox_SelectionChanged;
//        } 

//        public IList MutiSelectedItems
//        {
//            get { return (IList)GetValue(MutiSelectedItemsProperty); }
//            set { SetValue(MutiSelectedItemsProperty, value); }
//        }

//        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
//        public static readonly DependencyProperty MutiSelectedItemsProperty =
//            DependencyProperty.Register("MutiSelectedItems", typeof(IList), typeof(MutiSelectListBox), new PropertyMetadata(default(IList), (d, e) =>
//             {
//                 MutiSelectListBox control = d as MutiSelectListBox;

//                 if (control == null) return;

//                 IList config = e.NewValue as IList;

//             }));

//        private void MutiSelectListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
//        {
//            foreach (var item in e.AddedItems)
//            {
//                this.MutiSelectedItems.Add(item);
//            }
            
//            foreach (var item in e.RemovedItems)
//            {
//                this.MutiSelectedItems.Remove(item);
//            }
//        }
//    }
//}
