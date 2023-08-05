// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Window.Link
{

    /// <summary> 链接主窗口 </summary>
    public partial class CustomManagerWindowBase : ManagerWindowBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(CustomManagerWindowBase), "S.Window.Custom.Default");

        static CustomManagerWindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomManagerWindowBase), new FrameworkPropertyMetadata(typeof(CustomManagerWindowBase)));
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        public object LeftContent
        {
            get { return GetValue(LeftContentProperty); }
            set { SetValue(LeftContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftContentProperty =
            DependencyProperty.Register("LeftContent", typeof(object), typeof(ManagerWindowBase), new PropertyMetadata(default(object), (d, e) =>
            {
                ManagerWindowBase control = d as ManagerWindowBase;

                if (control == null) return;

                object config = e.NewValue;

            }));

    }
}
