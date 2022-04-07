// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Window.Message;
using System.Windows;

namespace HeBianGu.Window.Menu
{
    public partial class MenuWindowBase : MessageWindowBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(MenuWindowBase), "S.Window.Menu.Default");

        static MenuWindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuWindowBase), new FrameworkPropertyMetadata(typeof(MenuWindowBase)));
        }
        public object MenuContent
        {
            get { return GetValue(MenuContentProperty); }
            set { SetValue(MenuContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuContentProperty =
            DependencyProperty.Register("MenuContent", typeof(object), typeof(MenuWindowBase), new PropertyMetadata(default(object), (d, e) =>
             {
                 MenuWindowBase control = d as MenuWindowBase;

                 if (control == null) return;

                 object config = e.NewValue;

             }));

    }
}
