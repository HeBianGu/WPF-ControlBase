// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace HeBianGu.General.WpfControlLib
{
    public class ComboSelector : ToggleButton
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ComboSelector), "S.ComboSelector.Default");

        static ComboSelector()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ComboSelector), new FrameworkPropertyMetadata(typeof(ComboSelector)));
        }

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(ComboSelector), new FrameworkPropertyMetadata(default(object), (d, e) =>
            {
                ComboSelector control = d as ComboSelector;

                if (control == null) return;

                if (e.OldValue is object o)
                {

                }

                if (e.NewValue is object n)
                {

                }

            }));


        public DataTemplate SelectedTemplate
        {
            get { return (DataTemplate)GetValue(SelectedTemplateProperty); }
            set { SetValue(SelectedTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedTemplateProperty =
            DependencyProperty.Register("SelectedTemplate", typeof(DataTemplate), typeof(ComboSelector), new FrameworkPropertyMetadata(default(DataTemplate), (d, e) =>
            {
                ComboSelector control = d as ComboSelector;

                if (control == null) return;

                if (e.OldValue is DataTemplate o)
                {

                }

                if (e.NewValue is DataTemplate n)
                {

                }

            }));



        public Control Control
        {
            get { return (Control)GetValue(ControlProperty); }
            set { SetValue(ControlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControlProperty =
            DependencyProperty.Register("Control", typeof(Control), typeof(ComboSelector), new FrameworkPropertyMetadata(default(Control), (d, e) =>
            {
                ComboSelector control = d as ComboSelector;

                if (control == null) return;

                if (e.OldValue is Control o)
                {

                }

                if (e.NewValue is Control n)
                {

                }

            }));

    }

}
