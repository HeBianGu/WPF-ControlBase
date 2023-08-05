// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Animation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.Control.Drawer
{
    [TemplatePart(Name = "PART_Center", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_Show", Type = typeof(Grid))]
    public class Drawer : ContentControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Drawer), "S.Drawer.Default");
        public static ComponentResourceKey SingleKey => new ComponentResourceKey(typeof(Drawer), "S.Drawer.Single");
        public static ComponentResourceKey ClearKey => new ComponentResourceKey(typeof(Drawer), "S.Drawer.Clear");
        public static ComponentResourceKey GeomotryKey => new ComponentResourceKey(typeof(Drawer), "S.Drawer.Geomotry.Default");
        public static ComponentResourceKey CenterKey => new ComponentResourceKey(typeof(Drawer), "S.Drawer.Center.Default");
        public static ComponentResourceKey RightKey => new ComponentResourceKey(typeof(Drawer), "S.Drawer.Right.Default");
        public static ComponentResourceKey RightSingleKey => new ComponentResourceKey(typeof(Drawer), "S.Drawer.Right.Single");
        public static ComponentResourceKey RightClearKey => new ComponentResourceKey(typeof(Drawer), "S.Drawer.Right.Clear");

        public static ComponentResourceKey LeftKey => new ComponentResourceKey(typeof(Drawer), "S.Drawer.Left.Default");
        public static ComponentResourceKey LeftSingleKey => new ComponentResourceKey(typeof(Drawer), "S.Drawer.Left.Single");
        public static ComponentResourceKey LeftClearKey => new ComponentResourceKey(typeof(Drawer), "S.Drawer.Left.Clear");

        public static ComponentResourceKey BottomKey => new ComponentResourceKey(typeof(Drawer), "S.Drawer.Bottom.Default");
        public static ComponentResourceKey TopKey => new ComponentResourceKey(typeof(Drawer), "S.Drawer.Top.Default");

        static Drawer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Drawer), new FrameworkPropertyMetadata(typeof(Drawer)));
        }

        public Drawer()
        {
            //  Do ：显示
            {
                CommandBinding command = new CommandBinding(Commander.Open, (l, k) =>
                {
                    this.IsExpanded = true;
                });

                this.CommandBindings.Add(command);
            }

            //  Do ：隐藏
            {
                CommandBinding command = new CommandBinding(Commander.Close, (l, k) =>
                {
                    this.IsExpanded = false;
                });

                this.CommandBindings.Add(command);
            }

            this.Loaded += (l, k) =>
            {
                this.RefreshState(this.IsExpanded);
            };
        }

        private void Open()
        {
            TransitionService.SetIsVisible(this._center, true);

            if (this.UseShowAction)
                TransitionService.SetIsVisible(this._show, false);
        }

        private void Close()
        {
            TransitionService.SetIsVisible(this._center, false);
            bool b = TransitionService.GetIsVisible(this._show);
            if (this.UseShowAction)
                TransitionService.SetIsVisible(this._show, true);
        }

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(Drawer), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 Drawer control = d as Drawer;

                 if (control == null) return;

                 if (e.NewValue is bool b)
                 {
                     control.RefreshState(b);
                 }
             }));

        private void RefreshState(bool operate)
        {
            if (this._center == null || this._show == null) return;

            if (operate)
            {
                this.Open();
            }
            else
            {
                this.Close();
            }
        }

        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(Drawer), new PropertyMetadata(default(object), (d, e) =>
             {
                 Drawer control = d as Drawer;

                 if (control == null) return;

                 object config = e.NewValue;

             }));


        public VerticalAlignment VerticalOpenContentAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalOpenContentAlignmentProperty); }
            set { SetValue(VerticalOpenContentAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalOpenContentAlignmentProperty =
            DependencyProperty.Register("VerticalOpenContentAlignment", typeof(VerticalAlignment), typeof(Drawer), new PropertyMetadata(default(VerticalAlignment), (d, e) =>
             {
                 Drawer control = d as Drawer;

                 if (control == null) return;

                 //VerticalAlignment config = e.NewValue as VerticalAlignment;

             }));


        public HorizontalAlignment HorizontalOpenContentAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalOpenContentAlignmentProperty); }
            set { SetValue(HorizontalOpenContentAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalOpenContentAlignmentProperty =
            DependencyProperty.Register("HorizontalOpenContentAlignment", typeof(HorizontalAlignment), typeof(Drawer), new PropertyMetadata(default(HorizontalAlignment), (d, e) =>
             {
                 Drawer control = d as Drawer;

                 if (control == null) return;

                 //HorizontalAlignment config = e.NewValue as HorizontalAlignment;

             }));
        private Grid _center;
        private Grid _show;

        public override void OnApplyTemplate()
        {

            base.OnApplyTemplate();

            this._center = Template.FindName("PART_Center", this) as Grid;

            this._show = Template.FindName("PART_Show", this) as Grid;

            this.RefreshState(this.IsExpanded);
        }


        public bool UseShowAction
        {
            get { return (bool)GetValue(UseShowActionProperty); }
            set { SetValue(UseShowActionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseShowActionProperty =
            DependencyProperty.Register("UseShowAction", typeof(bool), typeof(Drawer), new PropertyMetadata(true, (d, e) =>
             {
                 Drawer control = d as Drawer;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));


        public ControlTemplate OpenContent
        {
            get { return (ControlTemplate)GetValue(OpenContentProperty); }
            set { SetValue(OpenContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpenContentProperty =
            DependencyProperty.Register("OpenContent", typeof(object), typeof(Drawer), new PropertyMetadata(default(ControlTemplate), (d, e) =>
             {
                 Drawer control = d as Drawer;

                 if (control == null) return;

                 ControlTemplate config = e.NewValue as ControlTemplate;

             }));


        public ControlTemplate CloseContent
        {
            get { return (ControlTemplate)GetValue(CloseContentProperty); }
            set { SetValue(CloseContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseContentProperty =
            DependencyProperty.Register("CloseContent", typeof(ControlTemplate), typeof(Drawer), new PropertyMetadata(default(ControlTemplate), (d, e) =>
             {
                 Drawer control = d as Drawer;

                 if (control == null) return;

                 ControlTemplate config = e.NewValue as ControlTemplate;

             }));


        public Style GroupStyle
        {
            get { return (Style)GetValue(GroupStyleProperty); }
            set { SetValue(GroupStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupStyleProperty =
            DependencyProperty.Register("GroupStyle", typeof(Style), typeof(Drawer), new FrameworkPropertyMetadata(default(Style), (d, e) =>
             {
                 Drawer control = d as Drawer;

                 if (control == null) return;

                 if (e.OldValue is Style o)
                 {

                 }

                 if (e.NewValue is Style n)
                 {

                 }

             }));

    }
}
