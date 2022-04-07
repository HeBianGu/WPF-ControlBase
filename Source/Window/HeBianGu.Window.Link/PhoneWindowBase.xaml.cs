// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;

namespace HeBianGu.Window.Link
{
    public partial class PhoneWindowBase : LinkWindowBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(PhoneWindowBase), "S.Window.Link.Phone");

        static PhoneWindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PhoneWindowBase), new FrameworkPropertyMetadata(typeof(PhoneWindowBase)));
        }

        public PhoneWindowBase()
        {
            this.MouseDown += (l, k) =>
              {

                  try
                  {
                      this.DragMove();
                  }
                  catch (Exception)
                  {

                  }
              };
        }
        //  Do ：自定义标题区域
        public object CustomContent
        {
            get { return GetValue(CustomContentProperty); }
            set { SetValue(CustomContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CustomContentProperty =
            DependencyProperty.Register("CustomContent", typeof(object), typeof(PhoneWindowBase), new PropertyMetadata(default(object), (d, e) =>
            {
                PhoneWindowBase control = d as PhoneWindowBase;

                if (control == null) return;

                object config = e.NewValue;

            }));
    }
}
