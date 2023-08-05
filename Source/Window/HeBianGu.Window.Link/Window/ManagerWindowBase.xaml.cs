// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Window.Message;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Window.Link
{
    public partial class ManagerWindowBase : MessageWindowBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ManagerWindowBase), "S.Window.Manager.Default");

        static ManagerWindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ManagerWindowBase), new FrameworkPropertyMetadata(typeof(ManagerWindowBase)));
        }


        public static readonly DependencyProperty LogoProperty = DependencyProperty.Register("Logo", typeof(ImageSource), typeof(ManagerWindowBase), new PropertyMetadata(null));

        public ImageSource Logo
        {
            get { return (ImageSource)GetValue(LogoProperty); }
            set { SetValue(LogoProperty, value); }
        }

        public IAction CurrentLink
        {
            get { return (IAction)GetValue(CurrentLinkProperty); }
            set { SetValue(CurrentLinkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentLinkProperty =
            DependencyProperty.Register("CurrentLink", typeof(IAction), typeof(ManagerWindowBase), new PropertyMetadata(default(IAction), (d, e) =>
            {
                ManagerWindowBase control = d as ManagerWindowBase;

                if (control == null) return;

                IAction config = e.NewValue as IAction;

            }));


        public object CustomContent
        {
            get { return GetValue(CustomContentProperty); }
            set { SetValue(CustomContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CustomContentProperty =
            DependencyProperty.Register("CustomContent", typeof(object), typeof(ManagerWindowBase), new PropertyMetadata(default(object), (d, e) =>
            {
                ManagerWindowBase control = d as ManagerWindowBase;

                if (control == null) return;

                object config = e.NewValue;

            }));

        public ManagerWindowBase()
        {
            this.Loaded += (l, k) =>
              {
                  OnLoaded();
              };

        }

        protected virtual void OnLoaded()
        {

        }


        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(ManagerWindowBase), new FrameworkPropertyMetadata(true, (d, e) =>
             {
                 ManagerWindowBase control = d as ManagerWindowBase;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));


        public Thickness ActionFrameMargin
        {
            get { return (Thickness)GetValue(ActionFrameMarginProperty); }
            set { SetValue(ActionFrameMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActionFrameMarginProperty =
            DependencyProperty.Register("ActionFrameMargin", typeof(Thickness), typeof(ManagerWindowBase), new FrameworkPropertyMetadata(default(Thickness), (d, e) =>
             {
                 ManagerWindowBase control = d as ManagerWindowBase;

                 if (control == null) return;

                 if (e.OldValue is Thickness o)
                 {

                 }

                 if (e.NewValue is Thickness n)
                 {

                 }

             }));


    }
}
