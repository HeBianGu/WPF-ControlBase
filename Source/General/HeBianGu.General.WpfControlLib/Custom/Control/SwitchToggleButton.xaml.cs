// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls.Primitives;

namespace HeBianGu.General.WpfControlLib
{
    public class SwitchToggleButton : ToggleButton
    {

        public HorizontalAlignment TextHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(TextHorizontalAlignmentProperty); }
            set { SetValue(TextHorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextHorizontalAlignmentProperty =
            DependencyProperty.Register("TextHorizontalAlignment", typeof(HorizontalAlignment), typeof(SwitchToggleButton), new PropertyMetadata(HorizontalAlignment.Left, (d, e) =>
             {
                 SwitchToggleButton control = d as SwitchToggleButton;

                 if (control == null) return;

                 //HorizontalAlignment config = e.NewValue as HorizontalAlignment;

             }));


        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(SwitchToggleButton), new PropertyMetadata(default(CornerRadius), (d, e) =>
             {
                 SwitchToggleButton control = d as SwitchToggleButton;

                 if (control == null) return;

                 //CornerRadius config = e.NewValue as CornerRadius;

             }));


        public CornerRadius BorderCornerRadius
        {
            get { return (CornerRadius)GetValue(BorderCornerRadiusProperty); }
            set { SetValue(BorderCornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderCornerRadiusProperty =
            DependencyProperty.Register("BorderCornerRadius", typeof(CornerRadius), typeof(SwitchToggleButton), new PropertyMetadata(default(CornerRadius), (d, e) =>
             {
                 SwitchToggleButton control = d as SwitchToggleButton;

                 if (control == null) return;

                 //CornerRadius config = e.NewValue as CornerRadius;

             }));


        //public static readonly DependencyProperty TextHorizontalAlignmentProperty = ElementBase.Property<MetroSwitch, HorizontalAlignment>(nameof(TextHorizontalAlignmentProperty), HorizontalAlignment.Left);
        //public static readonly DependencyProperty CornerRadiusProperty = ElementBase.Property<MetroSwitch, CornerRadius>(nameof(CornerRadiusProperty), new CornerRadius(10));
        //public static readonly DependencyProperty BorderCornerRadiusProperty = ElementBase.Property<MetroSwitch, CornerRadius>(nameof(BorderCornerRadiusProperty), new CornerRadius(12));

        //public HorizontalAlignment TextHorizontalAlignment { get { return (HorizontalAlignment)GetValue(TextHorizontalAlignmentProperty); } set { SetValue(TextHorizontalAlignmentProperty, value); } }
        //public CornerRadius CornerRadius { get { return (CornerRadius)GetValue(CornerRadiusProperty); } set { SetValue(CornerRadiusProperty, value); } }
        //public CornerRadius BorderCornerRadius { get { return (CornerRadius)GetValue(BorderCornerRadiusProperty); } set { SetValue(BorderCornerRadiusProperty, value); } }

        public SwitchToggleButton()
        {
            Loaded += delegate { this.GoToState((bool)IsChecked ? "OpenLoaded" : "CloseLoaded"); };
        }

        protected override void OnChecked(RoutedEventArgs e)
        {
            base.OnChecked(e);
            this.GoToState("Open");
        }

        protected override void OnUnchecked(RoutedEventArgs e)
        {
            base.OnChecked(e);
            this.GoToState("Close");
        }

        static SwitchToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SwitchToggleButton), new FrameworkPropertyMetadata(typeof(SwitchToggleButton)));

        }

        private void GoToState(string value)
        {
            VisualStateManager.GoToState(this, value, false);
        }
    }
}
