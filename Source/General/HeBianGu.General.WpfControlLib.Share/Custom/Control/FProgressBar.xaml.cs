// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
{
    public enum ProgressBarState
    {
        None,
        Error,
        Wait
    }

    public class FProgressBar : ProgressBar
    {

        public ProgressBarState ProgressBarState
        {
            get { return (ProgressBarState)GetValue(ProgressBarStateProperty); }
            set { SetValue(ProgressBarStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressBarStateProperty =
            DependencyProperty.Register("ProgressBarState", typeof(ProgressBarState), typeof(FProgressBar), new PropertyMetadata(default(ProgressBarState), (d, e) =>
             {
                 FProgressBar control = d as FProgressBar;

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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(FProgressBar), new PropertyMetadata(default(CornerRadius), (d, e) =>
             {
                 FProgressBar control = d as FProgressBar;

                 if (control == null) return;

                 //CornerRadius config = e.NewValue as CornerRadius;

             }));


        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HintProperty =
            DependencyProperty.Register("Hint", typeof(string), typeof(FProgressBar), new PropertyMetadata(default(string), (d, e) =>
             {
                 FProgressBar control = d as FProgressBar;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public HorizontalAlignment TextHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(TextHorizontalAlignmentProperty); }
            set { SetValue(TextHorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextHorizontalAlignmentProperty =
            DependencyProperty.Register("TextHorizontalAlignment", typeof(HorizontalAlignment), typeof(FProgressBar), new PropertyMetadata(default(HorizontalAlignment), (d, e) =>
             {
                 FProgressBar control = d as FProgressBar;

                 if (control == null) return;

                 //HorizontalAlignment config = e.NewValue as HorizontalAlignment;

             }));


        public FProgressBar()
        {
            ValueChanged += delegate
            {
                if (Hint == null || Hint.EndsWith(" %"))
                {
                    Hint = ((int)(Value / Maximum * 100)).ToString() + " %";
                }
            };
        }

        static FProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FProgressBar), new FrameworkPropertyMetadata(typeof(FProgressBar)));

        }
    }
}
