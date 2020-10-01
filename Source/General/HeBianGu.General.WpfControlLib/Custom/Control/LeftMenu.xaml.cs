using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
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
    public class LeftMenu : ListBox, ICommandSource
    {
        //static LeftMenu()
        //{
        //    DefaultStyleKeyProperty.OverrideMetadata(typeof(LeftMenu), new FrameworkPropertyMetadata(typeof(LeftMenu)));
        //}

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
            DependencyProperty.Register("IsExpended", typeof(bool), typeof(LeftMenu), new PropertyMetadata(default(bool), (d, e) =>
             {
                 LeftMenu control = d as LeftMenu;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));

        public double FIconSize
        {
            get { return (double)GetValue(FIconSizeProperty); }
            set { SetValue(FIconSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FIconSizeProperty =
            DependencyProperty.Register("FIconSize", typeof(double), typeof(LeftMenu), new PropertyMetadata(default(double), (d, e) =>
             {
                 LeftMenu control = d as LeftMenu;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));

        public LinkAction SelectedLink
        {
            get { return (LinkAction)GetValue(SelectedLinkProperty); }
            set { SetValue(SelectedLinkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedLinkProperty =
            DependencyProperty.Register("SelectedLink", typeof(LinkAction), typeof(LeftMenu), new PropertyMetadata(new LinkAction(), (d, e) =>
            {
                LeftMenu control = d as LeftMenu;

                if (control == null) return;

                control.SelectedItem = e.NewValue;

                control.Command?.Execute(control.CommandParameter);

                Debug.WriteLine(e.NewValue);

            }), ValidateValue);


        public LeftMenu()
        {
            //this.SelectionChanged +=(l, k) =>
            // {
            //     var ssss = this.SelectedItem;
            // };

        }
        //验证
        static bool ValidateValue(object obj)
        {
            if (obj == null) return false;

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



    }


    /// <summary> 当点击容器中一项时，将该项目置于容器最顶端 </summary>
    public class PopupRefreshBehavior : Behavior<Popup>
    {

        public LeftMenu LeftMenu
        {
            get { return (LeftMenu)GetValue(LeftMenuProperty); }
            set { SetValue(LeftMenuProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftMenuProperty =
            DependencyProperty.Register("LeftMenu", typeof(LeftMenu), typeof(PopupRefreshBehavior), new PropertyMetadata(default(LeftMenu), (d, e) =>
             {
                 PopupRefreshBehavior control = d as PopupRefreshBehavior;

                 if (control == null) return;

                 LeftMenu config = e.NewValue as LeftMenu;

             }));


        protected override void OnAttached()
        {
            AssociatedObject.Opened += AssociatedObject_Opened;
        }

        private void AssociatedObject_Opened(object sender, EventArgs e)
        {
            var rs = AssociatedObject.GetElements<RadioButton>();

            if (rs == null) return;

            var sss = rs.ToList();

            sss.ForEach(l => l.IsChecked = false);

            var find = sss.FirstOrDefault(l => l.Content == this.LeftMenu?.SelectedLink);

            if (find != null)
                find.IsChecked = true;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Opened -= AssociatedObject_Opened;
        }
    }
}
