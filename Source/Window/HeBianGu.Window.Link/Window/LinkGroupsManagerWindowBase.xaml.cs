// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Linq;
using System.Windows;

namespace HeBianGu.Window.Link
{
    /// <summary> 链接主窗口 </summary>
    public partial class LinkGroupsManagerWindowBase : ManagerWindowBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(LinkGroupsManagerWindowBase), "S.LinkGroupsManagerWindowBase.Default");
        public static ComponentResourceKey LeftMenuKey => new ComponentResourceKey(typeof(LinkGroupsManagerWindowBase), "S.LinkGroupsManagerWindowBase.LeftMenu");

        static LinkGroupsManagerWindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinkGroupsManagerWindowBase), new FrameworkPropertyMetadata(typeof(LinkGroupsManagerWindowBase)));
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            LinkActionGroup find = this.LinkActionGroups.FirstOrDefault(f => f.IsExpanded) ?? this.LinkActionGroups.FirstOrDefault();

            if (this.CurrentLink != null) return;

            this.CurrentLink = find?.Links?.FirstOrDefault();
        }


        public LinkActionGroups LinkActionGroups
        {
            get { return (LinkActionGroups)GetValue(LinkActionGroupsProperty); }
            set { SetValue(LinkActionGroupsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkActionGroupsProperty =
            DependencyProperty.Register("LinkActionGroups", typeof(LinkActionGroups), typeof(ManagerWindowBase), new PropertyMetadata(new LinkActionGroups(true), (d, e) =>
            {
                ManagerWindowBase control = d as ManagerWindowBase;

                if (control == null) return;

                LinkActionGroups config = e.NewValue as LinkActionGroups;

                if (config == null) return;

            }));
    }
}
