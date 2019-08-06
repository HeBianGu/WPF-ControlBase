using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    public enum ProgressBarState
    {
        None,
        Error,
        Wait
    }

    public class MetroProgressBar : ProgressBar
    {

        public ProgressBarState ProgressBarState
        {
            get { return (ProgressBarState)GetValue(ProgressBarStateProperty); }
            set { SetValue(ProgressBarStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressBarStateProperty =
            DependencyProperty.Register("ProgressBarState", typeof(ProgressBarState), typeof(MetroProgressBar), new PropertyMetadata(default(ProgressBarState), (d, e) =>
             {
                 MetroProgressBar control = d as MetroProgressBar;

                 if (control == null) return;

                 //ProgressBarState config = e.NewValue as ProgressBarState;

             }));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(MetroProgressBar), new PropertyMetadata(default(CornerRadius), (d, e) =>
             {
                 MetroProgressBar control = d as MetroProgressBar;

                 if (control == null) return;

                 //CornerRadius config = e.NewValue as CornerRadius;

             }));


        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(MetroProgressBar), new PropertyMetadata(default(string), (d, e) =>
             {
                 MetroProgressBar control = d as MetroProgressBar;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HintProperty =
            DependencyProperty.Register("Hint", typeof(string), typeof(MetroProgressBar), new PropertyMetadata(default(string), (d, e) =>
             {
                 MetroProgressBar control = d as MetroProgressBar;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public double ProgressBarHeight
        {
            get { return (double)GetValue(ProgressBarHeightProperty); }
            set { SetValue(ProgressBarHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressBarHeightProperty =
            DependencyProperty.Register("ProgressBarHeight", typeof(double), typeof(MetroProgressBar), new PropertyMetadata(0.0, (d, e) =>
             {
                 MetroProgressBar control = d as MetroProgressBar;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));


        public HorizontalAlignment TextHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(TextHorizontalAlignmentProperty); }
            set { SetValue(TextHorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextHorizontalAlignmentProperty =
            DependencyProperty.Register("TextHorizontalAlignment", typeof(HorizontalAlignment), typeof(MetroProgressBar), new PropertyMetadata(default(HorizontalAlignment), (d, e) =>
             {
                 MetroProgressBar control = d as MetroProgressBar;

                 if (control == null) return;

                 //HorizontalAlignment config = e.NewValue as HorizontalAlignment;

             }));



        //public static readonly DependencyProperty ProgressBarStateProperty = ElementBase.Property<MetroProgressBar, ProgressBarState>(nameof(ProgressBarStateProperty));
        //public static readonly DependencyProperty CornerRadiusProperty = ElementBase.Property<MetroProgressBar, CornerRadius>(nameof(CornerRadiusProperty));
        //public static readonly DependencyProperty TitleProperty = ElementBase.Property<MetroProgressBar, string>(nameof(TitleProperty));
        //public static readonly DependencyProperty HintProperty = ElementBase.Property<MetroProgressBar, string>(nameof(HintProperty));
        //public static readonly DependencyProperty ProgressBarHeightProperty = ElementBase.Property<MetroProgressBar, double>(nameof(ProgressBarHeightProperty));
        //public static readonly DependencyProperty TextHorizontalAlignmentProperty = ElementBase.Property<MetroProgressBar, HorizontalAlignment>(nameof(TextHorizontalAlignmentProperty));

        //public ProgressBarState ProgressBarState { get { return (ProgressBarState)GetValue(ProgressBarStateProperty); } set { SetValue(ProgressBarStateProperty, value); } }
        //public CornerRadius CornerRadius { get { return (CornerRadius)GetValue(CornerRadiusProperty); } set { SetValue(CornerRadiusProperty, value); } }
        //public string Title { get { return (string)GetValue(TitleProperty); } set { SetValue(TitleProperty, value); } }
        //public string Hint { get { return (string)GetValue(HintProperty); } set { SetValue(HintProperty, value); } }
        //public double ProgressBarHeight { get { return (double)GetValue(ProgressBarHeightProperty); } set { SetValue(ProgressBarHeightProperty, value); } }
        //public HorizontalAlignment TextHorizontalAlignment { get { return (HorizontalAlignment)GetValue(TextHorizontalAlignmentProperty); } set { SetValue(TextHorizontalAlignmentProperty, value); } }

        public MetroProgressBar()
        {
            //Utility.Refresh(this);

            ValueChanged += delegate
            {
                if (Hint == null || Hint.EndsWith(" %"))
                {
                    Hint = ((int)(Value / Maximum * 100)).ToString() + " %";
                }
            };
        }

        static MetroProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroProgressBar), new FrameworkPropertyMetadata(typeof(MetroProgressBar)));

        }
    }
}
