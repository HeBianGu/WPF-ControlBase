using HeBianGu.Base.WpfBase;
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
    [TemplatePart(Name = "PART_Center", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_Show", Type = typeof(Grid))]
    public class FDrawer : ContentControl
    {
        public FDrawer()
        {
            //  Do ：显示
            {
                CommandBinding command = new CommandBinding(CommandService.Open, (l, k) =>
                {
                    this.Open();
                });

                this.CommandBindings.Add(command);
            }

            //  Do ：隐藏
            {
                CommandBinding command = new CommandBinding(CommandService.Close, (l, k) =>
                {
                    this.Close();
                });

                this.CommandBindings.Add(command);
            }

            this.Loaded += (l, k) =>
            {
                this.RefreshState(this.IsExpanded);
            };

        }


        void Open()
        {
            if (this._center == null || this._show == null) return;

            Cattach.SetIsVisible(this._center, true);

            if (this.UseShowAction)
                Cattach.SetIsVisible(this._show, false);

            Panel.SetZIndex(this, 1);
        }

        void Close()
        {
            if (this._center == null || this._show == null) return;

            Cattach.SetIsVisible(this._center, false);

            if (this.UseShowAction)
                Cattach.SetIsVisible(this._show, true);


            Panel.SetZIndex(this, 0);
        }

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(FDrawer), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 FDrawer control = d as FDrawer;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

                 if (e.NewValue is bool b)
                 {
                     control.RefreshState(b);
                 }
             }));

        void RefreshState(bool operate)
        {
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
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(FDrawer), new PropertyMetadata(default(object), (d, e) =>
             {
                 FDrawer control = d as FDrawer;

                 if (control == null) return;

                 object config = e.NewValue as object;

             }));


        public VerticalAlignment VerticalOpenContentAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalOpenContentAlignmentProperty); }
            set { SetValue(VerticalOpenContentAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalOpenContentAlignmentProperty =
            DependencyProperty.Register("VerticalOpenContentAlignment", typeof(VerticalAlignment), typeof(FDrawer), new PropertyMetadata(default(VerticalAlignment), (d, e) =>
             {
                 FDrawer control = d as FDrawer;

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
            DependencyProperty.Register("HorizontalOpenContentAlignment", typeof(HorizontalAlignment), typeof(FDrawer), new PropertyMetadata(default(HorizontalAlignment), (d, e) =>
             {
                 FDrawer control = d as FDrawer;

                 if (control == null) return;

                 //HorizontalAlignment config = e.NewValue as HorizontalAlignment;

             }));


        Grid _center;

        Grid _show;

        public override void OnApplyTemplate()
        {

            base.OnApplyTemplate();

            this._center = Template.FindName("PART_Center", this) as Grid;

            this._show = Template.FindName("PART_Show", this) as Grid;
        }


        public bool UseShowAction
        {
            get { return (bool)GetValue(UseShowActionProperty); }
            set { SetValue(UseShowActionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseShowActionProperty =
            DependencyProperty.Register("UseShowAction", typeof(bool), typeof(FDrawer), new PropertyMetadata(true, (d, e) =>
             {
                 FDrawer control = d as FDrawer;

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
            DependencyProperty.Register("OpenContent", typeof(object), typeof(FDrawer), new PropertyMetadata(default(ControlTemplate), (d, e) =>
             {
                 FDrawer control = d as FDrawer;

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
            DependencyProperty.Register("CloseContent", typeof(ControlTemplate), typeof(FDrawer), new PropertyMetadata(default(ControlTemplate), (d, e) =>
             {
                 FDrawer control = d as FDrawer;

                 if (control == null) return;

                 ControlTemplate config = e.NewValue as ControlTemplate;

             }));
    }
}
