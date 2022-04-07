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
}
