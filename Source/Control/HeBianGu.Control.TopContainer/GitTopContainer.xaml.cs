// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace HeBianGu.Control.TopContainer
{
    public partial class GitTopContainer : ListBox
    {
        static GitTopContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GitTopContainer), new FrameworkPropertyMetadata(typeof(GitTopContainer)));
        }

        public static RoutedUICommand CoverHideCommand = new RoutedUICommand() { Text = "隐藏面板" };
        private Border boder;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            boder = Template.FindName("PART_COVER", this) as Border;

            boder.MouseDown += (l, k) =>
              {
                  this.HideAll();
              };

        }
        public GitTopContainer()
        {
            CommandBinding binding = new CommandBinding(CoverHideCommand, (l, k) =>
               {
                   this.HideAll();
               });

            this.CommandBindings.Add(binding);
        }

        private void HideAll()
        {
            System.Collections.Generic.List<ToggleButton> tbs = this.GetChildren<ToggleButton>()?.ToList();

            tbs.ForEach(l => l.IsChecked = false);
        }

        public Visibility CoverVisibility
        {
            get { return (Visibility)GetValue(CoverVisibilityProperty); }
            set { SetValue(CoverVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CoverVisibilityProperty =
            DependencyProperty.Register("CoverVisibility", typeof(Visibility), typeof(GitTopContainer), new PropertyMetadata(default(Visibility), (d, e) =>
             {
                 GitTopContainer control = d as GitTopContainer;

                 if (control == null) return;

                 //Visibility config = e.NewValue as Visibility;

             }));


        public object CenterContent
        {
            get { return GetValue(CenterContentProperty); }
            set { SetValue(CenterContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CenterContentProperty =
            DependencyProperty.Register("CenterContent", typeof(object), typeof(GitTopContainer), new PropertyMetadata(default(object), (d, e) =>
             {
                 GitTopContainer control = d as GitTopContainer;

                 if (control == null) return;

                 object config = e.NewValue;

             }));


        public double HeaderHeight
        {
            get { return (double)GetValue(HeaderHeightProperty); }
            set { SetValue(HeaderHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderHeightProperty =
            DependencyProperty.Register("HeaderHeight", typeof(double), typeof(GitTopContainer), new PropertyMetadata(50.0, (d, e) =>
             {
                 GitTopContainer control = d as GitTopContainer;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));
    }

    public partial class GitTopContainer
    {
        public static ComponentResourceKey DynamicKey => new ComponentResourceKey(typeof(GitTopContainer), "S.GitTopContainer.Dynamic");
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(GitTopContainer), "S.GitTopContainer.Default");
        public static ComponentResourceKey ToggleKey => new ComponentResourceKey(typeof(GitTopContainer), "S.GitTopContainer.Toggle");
    }

    public class GitTopItem : ToggleButton
    {
        public static ComponentResourceKey DynamicKey => new ComponentResourceKey(typeof(GitTopItem), "S.GitTopItem.Dynamic");
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(GitTopItem), "S.GitTopItem.Default");
        public static ComponentResourceKey CircleKey => new ComponentResourceKey(typeof(GitTopItem), "S.GitTopItem.Circle");

        static GitTopItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GitTopItem), new FrameworkPropertyMetadata(typeof(GitTopItem)));
        }

        public object HeaderContent
        {
            get { return GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderContentProperty =
            DependencyProperty.Register("HeaderContent", typeof(object), typeof(GitTopItem), new PropertyMetadata(default(object), (d, e) =>
             {
                 GitTopItem control = d as GitTopItem;

                 if (control == null) return;

                 object config = e.NewValue;

             }));



        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(GitTopItem), new PropertyMetadata(default(DataTemplate), (d, e) =>
             {
                 GitTopItem control = d as GitTopItem;

                 if (control == null) return;

                 DataTemplate config = e.NewValue as DataTemplate;

             }));



        public double HeaderHeight
        {
            get { return (double)GetValue(HeaderHeightProperty); }
            set { SetValue(HeaderHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderHeightProperty =
            DependencyProperty.Register("HeaderHeight", typeof(double), typeof(GitTopItem), new PropertyMetadata(50.0, (d, e) =>
             {
                 GitTopItem control = d as GitTopItem;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));

    }
}
