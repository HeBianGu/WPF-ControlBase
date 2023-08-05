// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.Control.LeftMenu
{
    public class LeftMenu : ListBox, ICommandSource
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(LeftMenu), "S.LeftMenu.Default");
        public static ComponentResourceKey LinkActionsKey => new ComponentResourceKey(typeof(LeftMenu), "S.LeftMenu.LinkActions.Default");
        public static ComponentResourceKey LinkActionGroupKey => new ComponentResourceKey(typeof(LeftMenu), "S.LeftMenu.LinkActionGroup.Default");
        public static ComponentResourceKey ListBoxItemKey => new ComponentResourceKey(typeof(LeftMenu), "S.LeftMenu.ListBoxItem.Default");


        static LeftMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LeftMenu), new FrameworkPropertyMetadata(typeof(LeftMenu)));
        }

        public LeftMenu()
        {
            this.Loaded += (l, k) =>
              {
                  VisualStateManager.GoToState(this, this.IsExpended ? "Expanded" : "UnExpanded", false);
              };

        }
        public ICommand Command { get; set; }

        public object CommandParameter { get; set; }

        public IInputElement CommandTarget { get; set; }

        public bool IsExpended
        {
            get { return (bool)GetValue(IsExpendedProperty); }
            set { SetValue(IsExpendedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsExpendedProperty =
            DependencyProperty.Register("IsExpended", typeof(bool), typeof(LeftMenu), new PropertyMetadata(true, (d, e) =>
             {
                 LeftMenu control = d as LeftMenu;

                 if (control == null) return;

                 //bool config = e.NewValue as bool; 

                 VisualStateManager.GoToState(control, (bool)e.NewValue ? "Expanded" : "UnExpanded", false);

             }));

        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(double), typeof(LeftMenu), new PropertyMetadata(20.0, (d, e) =>
             {
                 LeftMenu control = d as LeftMenu;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));

        public IAction SelectedLink
        {
            get { return (IAction)GetValue(SelectedLinkProperty); }
            set { SetValue(SelectedLinkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedLinkProperty =
            DependencyProperty.Register("SelectedLink", typeof(IAction), typeof(LeftMenu), new PropertyMetadata(new NofoundAction(), (d, e) =>
            {
                LeftMenu control = d as LeftMenu;

                if (control == null) return;

                control.SelectedItem = e.NewValue;

                control.Command?.Execute(control.CommandParameter);

                Debug.WriteLine(e.NewValue);

            }), ValidateValue);

        //验证
        private static bool ValidateValue(object obj)
        {
            if (obj == null) 
                return false;
            return true;
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LeftMenu), new PropertyMetadata(default(CornerRadius), (d, e) =>
             {
                 LeftMenu control = d as LeftMenu;

                 if (control == null) return;

                 //CornerRadius config = e.NewValue as CornerRadius;

             }));


        public bool UseLogo
        {
            get { return (bool)GetValue(UseLogoProperty); }
            set { SetValue(UseLogoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseLogoProperty =
            DependencyProperty.Register("UseLogo", typeof(bool), typeof(LeftMenu), new FrameworkPropertyMetadata(true, (d, e) =>
             {
                 LeftMenu control = d as LeftMenu;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));


        public string Logo
        {
            get { return (string)GetValue(LogoProperty); }
            set { SetValue(LogoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LogoProperty =
            DependencyProperty.Register("Logo", typeof(string), typeof(LeftMenu), new FrameworkPropertyMetadata(default(string), (d, e) =>
             {
                 LeftMenu control = d as LeftMenu;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }

             }));


    }
}
