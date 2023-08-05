// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;
using HeBianGu.Window.Message;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Window.Link
{
    public partial class LinkWindowBase : MessageWindowBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(LinkWindowBase), "S.Window.Link.Default");
        public static new ComponentResourceKey SingleKey => new ComponentResourceKey(typeof(LinkWindowBase), "S.Window.Link.Single");
        public static ComponentResourceKey DiskKey => new ComponentResourceKey(typeof(LinkWindowBase), "S.Window.Link.Disk");
        public static ComponentResourceKey DownLoadKey => new ComponentResourceKey(typeof(LinkWindowBase), "S.Window.Link.DownLoad");
        public static ComponentResourceKey MusicKey => new ComponentResourceKey(typeof(LinkWindowBase), "S.Window.Link.Music");

        static LinkWindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinkWindowBase), new FrameworkPropertyMetadata(typeof(LinkWindowBase)));
        }


        public ObservableCollection<ILinkAction> Links
        {
            get { return (ObservableCollection<ILinkAction>)GetValue(LinksProperty); }
            set { SetValue(LinksProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinksProperty =
            DependencyProperty.Register("Links", typeof(ObservableCollection<ILinkAction>), typeof(LinkWindowBase), new PropertyMetadata(new ObservableCollection<ILinkAction>(), (d, e) =>
            {
                WindowBase control = d as WindowBase;

                if (control == null) return;

                ObservableCollection<ILinkAction> config = e.NewValue as ObservableCollection<ILinkAction>;

            }));

        public static readonly DependencyProperty LogoProperty = DependencyProperty.Register("Logo", typeof(ImageSource), typeof(LinkWindowBase), new PropertyMetadata(null));

        public ImageSource Logo
        {
            get { return (ImageSource)GetValue(LogoProperty); }
            set { SetValue(LogoProperty, value); }
        }

        public ILinkAction CurrentLink
        {
            get { return (ILinkAction)GetValue(CurrentLinkProperty); }
            set { SetValue(CurrentLinkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentLinkProperty =
            DependencyProperty.Register("CurrentLink", typeof(ILinkAction), typeof(LinkWindowBase), new PropertyMetadata(default(ILinkAction), (d, e) =>
            {
                LinkWindowBase control = d as LinkWindowBase;

                if (control == null) return;

                ILinkAction config = e.NewValue as ILinkAction;

            }));


        public object CaptionCustomContent
        {
            get { return GetValue(CaptionCustomContentProperty); }
            set { SetValue(CaptionCustomContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaptionCustomContentProperty =
            DependencyProperty.Register("CaptionCustomContent", typeof(object), typeof(LinkWindowBase), new FrameworkPropertyMetadata(default(object), (d, e) =>
             {
                 LinkWindowBase control = d as LinkWindowBase;

                 if (control == null) return;

                 if (e.OldValue is object o)
                 {

                 }

                 if (e.NewValue is object n)
                 {

                 }

             }));
    }
}
