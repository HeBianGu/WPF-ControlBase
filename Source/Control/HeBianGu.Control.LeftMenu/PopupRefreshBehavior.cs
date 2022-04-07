// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace HeBianGu.Control.LeftMenu
{
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
            System.Collections.Generic.IEnumerable<RadioButton> rs = AssociatedObject.GetElements<RadioButton>();

            if (rs == null) return;

            System.Collections.Generic.List<RadioButton> sss = rs.ToList();

            sss.ForEach(l => l.IsChecked = false);

            RadioButton find = sss.FirstOrDefault(l => l.Content == this.LeftMenu?.SelectedLink);

            if (find != null)
                find.IsChecked = true;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Opened -= AssociatedObject_Opened;
        }
    }
}
