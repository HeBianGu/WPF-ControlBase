using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary> 链接主窗口 </summary>
    public partial class LinkWindowBase : WindowBase
    {

        public LinkGroup TabLinks
        {
            get { return (LinkGroup)GetValue(TabLinksProperty); }
            set { SetValue(TabLinksProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TabLinksProperty =
            DependencyProperty.Register("TabLinks", typeof(LinkGroup), typeof(LinkWindowBase), new PropertyMetadata(new LinkGroup(), (d, e) =>
            {
                WindowBase control = d as WindowBase;

                if (control == null) return;

                ObservableCollection<Link> config = e.NewValue as ObservableCollection<Link>;

            }));


        public LinkGroup SettingLinks
        {
            get { return (LinkGroup)GetValue(SettingLinksProperty); }
            set { SetValue(SettingLinksProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SettingLinksProperty =
            DependencyProperty.Register("SettingLinks", typeof(LinkGroup), typeof(LinkWindowBase), new PropertyMetadata(new LinkGroup(), (d, e) =>
            {
                WindowBase control = d as WindowBase;

                if (control == null) return;

                ObservableCollection<Link> config = e.NewValue as ObservableCollection<Link>;

            }));



        public static readonly DependencyProperty LogoProperty = DependencyProperty.Register("Logo", typeof(ImageSource), typeof(LinkWindowBase), new PropertyMetadata(null));

        public ImageSource Logo
        {
            get { return (ImageSource)GetValue(LogoProperty); }
            set { SetValue(LogoProperty, value); }
        }

    }
}
