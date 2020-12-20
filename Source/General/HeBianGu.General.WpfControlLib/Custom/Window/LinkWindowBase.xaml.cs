using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary> 链接主窗口 </summary>
    public partial class LinkWindowBase : MainWindowBase
    { 
        public ObservableCollection<TabLink> TabLinks
        {
            get { return (ObservableCollection<TabLink>)GetValue(TabLinksProperty); }
            set { SetValue(TabLinksProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TabLinksProperty =
            DependencyProperty.Register("TabLinks", typeof(ObservableCollection<TabLink>), typeof(LinkWindowBase), new PropertyMetadata(new ObservableCollection<TabLink>(), (d, e) =>
            {
                WindowBase control = d as WindowBase;

                if (control == null) return;

                ObservableCollection<TabLink> config = e.NewValue as ObservableCollection<TabLink>;

            }));

        //public LinkGroup SettingLinks
        //{
        //    get { return (LinkGroup)GetValue(SettingLinksProperty); }
        //    set { SetValue(SettingLinksProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty SettingLinksProperty =
        //    DependencyProperty.Register("SettingLinks", typeof(LinkGroup), typeof(LinkWindowBase), new PropertyMetadata(new LinkGroup(), (d, e) =>
        //    {
        //        WindowBase control = d as WindowBase;

        //        if (control == null) return;

        //        ObservableCollection<Link> config = e.NewValue as ObservableCollection<Link>;

        //    }));



        public static readonly DependencyProperty LogoProperty = DependencyProperty.Register("Logo", typeof(ImageSource), typeof(LinkWindowBase), new PropertyMetadata(null));

        public ImageSource Logo
        {
            get { return (ImageSource)GetValue(LogoProperty); }
            set { SetValue(LogoProperty, value); }
        }



        public TabLink CurrentLink
        {
            get { return (TabLink)GetValue(CurrentLinkProperty); }
            set { SetValue(CurrentLinkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentLinkProperty =
            DependencyProperty.Register("CurrentLink", typeof(TabLink), typeof(LinkWindowBase), new PropertyMetadata(default(TabLink), (d, e) =>
            {
                LinkWindowBase control = d as LinkWindowBase;

                if (control == null) return;

                TabLink config = e.NewValue as TabLink;

            }));
    }

    /// <summary> 连接绑定对象 </summary>
    public class TabLink : LinkAction
    {

        private LinkAction _centerLink;
        /// <summary> 左侧控件地址 </summary>
        public LinkAction CenterLink
        {
            get { return this._centerLink; }
            set
            {
                if (this._centerLink != value)
                {
                    this._centerLink = value;
                    RaisePropertyChanged("CenterLink");
                }
            }
        }

        private LinkAction _leftLink;
        /// <summary> 左侧控件地址 </summary>
        public LinkAction LeftLink
        {
            get { return this._leftLink; }
            set
            {
                if (this._leftLink != value)
                {
                    this._leftLink = value;
                    RaisePropertyChanged("LeftLink");
                }
            }
        }

        private LinkAction _rightLink;
        /// <summary> 右侧控件地址  </summary>
        public LinkAction RightLink
        {
            get { return _rightLink; }
            set
            {
                _rightLink = value;
                RaisePropertyChanged("RightLink");
            }
        }

        private LinkAction _topLink;
        /// <summary> 上侧控件地址  </summary>
        public LinkAction TopLink
        {
            get { return _topLink; }
            set
            {
                _topLink = value;
                RaisePropertyChanged("TopLink");
            }
        }

        private LinkAction _bottomLink;
        /// <summary> 下侧控件地址  </summary>
        public LinkAction BottomLink
        {
            get { return _bottomLink; }
            set
            {
                _bottomLink = value;
                RaisePropertyChanged("BottomLink");
            }
        }
    }

    public partial class PhoneWindowBase:LinkWindowBase
    {
        public PhoneWindowBase()
        {
            this.MouseDown +=(l, k) =>
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
            get { return (object)GetValue(CustomContentProperty); }
            set { SetValue(CustomContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CustomContentProperty =
            DependencyProperty.Register("CustomContent", typeof(object), typeof(PhoneWindowBase), new PropertyMetadata(default(object), (d, e) =>
            {
                PhoneWindowBase control = d as PhoneWindowBase;

                if (control == null) return;

                object config = e.NewValue as object;

            }));
    }
}
