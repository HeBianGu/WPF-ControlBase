using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
    public class MetroTabItem : TabItem
    {

        public HorizontalAlignment TextHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(TextHorizontalAlignmentProperty); }
            set { SetValue(TextHorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextHorizontalAlignmentProperty =
            DependencyProperty.Register("TextHorizontalAlignment", typeof(HorizontalAlignment), typeof(MetroTabItem), new PropertyMetadata(HorizontalAlignment.Right, (d, e) =>
             {
                 MetroTabItem control = d as MetroTabItem;

                 if (control == null) return;

                 //HorizontalAlignment config = e.NewValue as HorizontalAlignment;

             }));


        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(MetroTabItem), new PropertyMetadata(null, (d, e) =>
             {
                 MetroTabItem control = d as MetroTabItem;
                 if (control == null) return;
                 //ImageSource config = e.NewValue as ImageSource;

             }));




        public double IconFontSize
        {
            get { return (double)GetValue(IconFontSizeProperty); }
            set { SetValue(IconFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconFontSizeProperty =
            DependencyProperty.Register("IconFontSize", typeof(double), typeof(MetroTabItem), new PropertyMetadata(28.0, (d, e) =>
             {
                 MetroTabItem control = d as MetroTabItem;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));



        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconMarginProperty =
            DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(MetroTabItem), new PropertyMetadata(default(Thickness), (d, e) =>
             {
                 MetroTabItem control = d as MetroTabItem;

                 if (control == null) return;

                 //Thickness config = e.NewValue as Thickness;

             }));




        static MetroTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroTabItem), new FrameworkPropertyMetadata(typeof(MetroTabItem)));
        }
    }
}
