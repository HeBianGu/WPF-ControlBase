// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
{
    public class TupleGroup : ContentControl
    {
        static TupleGroup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TupleGroup), new FrameworkPropertyMetadata(typeof(TupleGroup)));
        }



        public object LeftItem1
        {
            get { return GetValue(LeftItem1Property); }
            set { SetValue(LeftItem1Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftItem1Property =
            DependencyProperty.Register("LeftItem1", typeof(object), typeof(TupleGroup), new PropertyMetadata(default(object), (d, e) =>
             {
                 TupleGroup control = d as TupleGroup;

                 if (control == null) return;

                 object config = e.NewValue;

             }));


        public Style LeftItemStyle1
        {
            get { return (Style)GetValue(LeftItemStyle1Property); }
            set { SetValue(LeftItemStyle1Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftItemStyle1Property =
            DependencyProperty.Register("LeftItemStyle1", typeof(Style), typeof(TupleGroup), new PropertyMetadata(default(Style), (d, e) =>
             {
                 TupleGroup control = d as TupleGroup;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));


        public Style LeftItemStyle2
        {
            get { return (Style)GetValue(LeftItemStyle2Property); }
            set { SetValue(LeftItemStyle2Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftItemStyle2Property =
            DependencyProperty.Register("LeftItemStyle2", typeof(Style), typeof(TupleGroup), new PropertyMetadata(default(Style), (d, e) =>
             {
                 TupleGroup control = d as TupleGroup;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));


        public Style LeftItemStyle3
        {
            get { return (Style)GetValue(LeftItemStyle3Property); }
            set { SetValue(LeftItemStyle3Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftItemStyle3Property =
            DependencyProperty.Register("LeftItemStyle3", typeof(Style), typeof(TupleGroup), new PropertyMetadata(default(Style), (d, e) =>
             {
                 TupleGroup control = d as TupleGroup;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));


        public Style LeftItemStyle4
        {
            get { return (Style)GetValue(LeftItemStyle4Property); }
            set { SetValue(LeftItemStyle4Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftItemStyle4Property =
            DependencyProperty.Register("LeftItemStyle4", typeof(Style), typeof(TupleGroup), new PropertyMetadata(default(Style), (d, e) =>
             {
                 TupleGroup control = d as TupleGroup;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));


        public Style RightItemStyle1
        {
            get { return (Style)GetValue(RightItemStyle1Property); }
            set { SetValue(RightItemStyle1Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightItemStyle1Property =
            DependencyProperty.Register("RightItemStyle1", typeof(Style), typeof(TupleGroup), new PropertyMetadata(default(Style), (d, e) =>
             {
                 TupleGroup control = d as TupleGroup;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));


        public Style RightItemStyle2
        {
            get { return (Style)GetValue(RightItemStyle2Property); }
            set { SetValue(RightItemStyle2Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightItemStyle2Property =
            DependencyProperty.Register("RightItemStyle2", typeof(Style), typeof(TupleGroup), new PropertyMetadata(default(Style), (d, e) =>
             {
                 TupleGroup control = d as TupleGroup;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));


        public Style RightItemStyle3
        {
            get { return (Style)GetValue(RightItemStyle3Property); }
            set { SetValue(RightItemStyle3Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightItemStyle3Property =
            DependencyProperty.Register("RightItemStyle3", typeof(Style), typeof(TupleGroup), new PropertyMetadata(default(Style), (d, e) =>
             {
                 TupleGroup control = d as TupleGroup;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));


        public Style RightItemStyle4
        {
            get { return (Style)GetValue(RightItemStyle4Property); }
            set { SetValue(RightItemStyle4Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightItemStyle4Property =
            DependencyProperty.Register("RightItemStyle4", typeof(Style), typeof(TupleGroup), new PropertyMetadata(default(Style), (d, e) =>
             {
                 TupleGroup control = d as TupleGroup;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));


        public object LeftItem2
        {
            get { return GetValue(LeftItem2Property); }
            set { SetValue(LeftItem2Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftItem2Property =
            DependencyProperty.Register("LeftItem2", typeof(object), typeof(TupleGroup), new PropertyMetadata(default(object), (d, e) =>
             {
                 TupleGroup control = d as TupleGroup;

                 if (control == null) return;

                 object config = e.NewValue;

             }));


        public object LeftItem3
        {
            get { return GetValue(LeftItem3Property); }
            set { SetValue(LeftItem3Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftItem3Property =
            DependencyProperty.Register("LeftItem3", typeof(object), typeof(TupleGroup), new PropertyMetadata(default(object), (d, e) =>
             {
                 TupleGroup control = d as TupleGroup;

                 if (control == null) return;

                 object config = e.NewValue;

             }));


        public object LeftItem4
        {
            get { return GetValue(LeftItem4Property); }
            set { SetValue(LeftItem4Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftItem4Property =
            DependencyProperty.Register("LeftItem4", typeof(object), typeof(TupleGroup), new PropertyMetadata(default(object), (d, e) =>
             {
                 TupleGroup control = d as TupleGroup;

                 if (control == null) return;

                 object config = e.NewValue;

             }));


        public object RightItem1
        {
            get { return GetValue(RightItem1Property); }
            set { SetValue(RightItem1Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightItem1Property =
            DependencyProperty.Register("RightItem1", typeof(object), typeof(TupleGroup), new PropertyMetadata(default(object), (d, e) =>
             {
                 TupleGroup control = d as TupleGroup;

                 if (control == null) return;

                 object config = e.NewValue;

             }));


        public object RightItem2
        {
            get { return GetValue(RightItem2Property); }
            set { SetValue(RightItem2Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightItem2Property =
            DependencyProperty.Register("RightItem2", typeof(object), typeof(TupleGroup), new PropertyMetadata(default(object), (d, e) =>
             {
                 TupleGroup control = d as TupleGroup;

                 if (control == null) return;

                 object config = e.NewValue;

             }));


        public object RightItem3
        {
            get { return GetValue(RightItem3Property); }
            set { SetValue(RightItem3Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightItem3Property =
            DependencyProperty.Register("RightItem3", typeof(object), typeof(TupleGroup), new PropertyMetadata(default(object), (d, e) =>
             {
                 TupleGroup control = d as TupleGroup;

                 if (control == null) return;

                 object config = e.NewValue;

             }));


        public object RightItem4
        {
            get { return GetValue(RightItem4Property); }
            set { SetValue(RightItem4Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightItem4Property =
            DependencyProperty.Register("RightItem4", typeof(object), typeof(TupleGroup), new PropertyMetadata(default(object), (d, e) =>
             {
                 TupleGroup control = d as TupleGroup;

                 if (control == null) return;

                 object config = e.NewValue;

             }));

    }


}
