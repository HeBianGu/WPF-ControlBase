// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace HeBianGu.Control.LeftMenu
{

    public class LinkGroupExpander : Selector, ICommandSource
    {
        public static ComponentResourceKey ListBoxItemKey => new ComponentResourceKey(typeof(LinkGroupExpander), "S.LinkGrouExpander.ListBoxItem.Default");
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(LinkGroupExpander), "S.LinkGroupExpander.Default");
        public static ComponentResourceKey AccentKey => new ComponentResourceKey(typeof(LinkGroupExpander), "S.LinkGroupExpander.Accent");

        static LinkGroupExpander()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinkGroupExpander), new FrameworkPropertyMetadata(typeof(LinkGroupExpander)));
        }

        public ICommand Command { get; set; }

        public object CommandParameter { get; set; }

        public IInputElement CommandTarget { get; set; }

        public IAction SelectedLink
        {
            get { return (IAction)GetValue(SelectedLinkProperty); }
            set { SetValue(SelectedLinkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedLinkProperty =
            DependencyProperty.Register("SelectedLink", typeof(IAction), typeof(LinkGroupExpander), new PropertyMetadata(new NofoundAction(), (d, e) =>
             {
                 LinkGroupExpander control = d as LinkGroupExpander;

                 if (control == null) return;

                 control.SelectedItem = e.NewValue;

                 control.Command?.Execute(control.CommandParameter);

             }), ValidateValue);

        //验证
        private static bool ValidateValue(object obj)
        {
            if (obj == null) return false;

            return true;
        }

        public Brush SelectItemBackground
        {
            get { return (Brush)GetValue(SelectItemBackgroundProperty); }
            set { SetValue(SelectItemBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectItemBackgroundProperty =
            DependencyProperty.Register("SelectItemBackground", typeof(Brush), typeof(LinkGroupExpander), new PropertyMetadata(default(Brush), (d, e) =>
             {
                 LinkGroupExpander control = d as LinkGroupExpander;

                 if (control == null) return;

                 Brush config = e.NewValue as Brush;

             }));


        public Brush MouseOverBackground
        {
            get { return (Brush)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverBackgroundProperty =
            DependencyProperty.Register("MouseOverBackground", typeof(Brush), typeof(LinkGroupExpander), new PropertyMetadata(default(Brush), (d, e) =>
             {
                 LinkGroupExpander control = d as LinkGroupExpander;

                 if (control == null) return;

                 Brush config = e.NewValue as Brush;

             }));


        public Brush SelectItemFlagForeground
        {
            get { return (Brush)GetValue(SelectItemFlagForegroundProperty); }
            set { SetValue(SelectItemFlagForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectItemFlagForegroundProperty =
            DependencyProperty.Register("SelectItemFlagForeground", typeof(Brush), typeof(LinkGroupExpander), new PropertyMetadata(default(Brush), (d, e) =>
             {
                 LinkGroupExpander control = d as LinkGroupExpander;

                 if (control == null) return;

                 Brush config = e.NewValue as Brush;

             }));


    }

}
