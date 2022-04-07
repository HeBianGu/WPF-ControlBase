// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.ToggleExpander
{
    public class ToggleExpander : ListBox
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ToggleExpander), "S.ToggleExpander.Default");

        public static ComponentResourceKey SingleKey => new ComponentResourceKey(typeof(ToggleExpander), "S.ToggleExpander.Single");

        static ToggleExpander()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleExpander), new FrameworkPropertyMetadata(typeof(ToggleExpander)));
        }

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(ToggleExpander), new PropertyMetadata(default(bool), (d, e) =>
             {
                 ToggleExpander control = d as ToggleExpander;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));


    }

    public class ElementExpander : ToggleExpander
    {
        public static ComponentResourceKey MouseEnterKey => new ComponentResourceKey(typeof(ElementExpander), "S.ElementExpander.MouseEnter");

        static ElementExpander()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ElementExpander), new FrameworkPropertyMetadata(typeof(ElementExpander)));
        }

        public object Content
        {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(ElementExpander), new PropertyMetadata(default(object), (d, e) =>
             {
                 ElementExpander control = d as ElementExpander;

                 if (control == null) return;

                 object config = e.NewValue;

             }));

    }
}
