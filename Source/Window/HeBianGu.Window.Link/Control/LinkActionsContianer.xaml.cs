// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Markup;

namespace HeBianGu.Window.Link
{
    /// <summary> 带有数据集选项的导航框架 </summary>
    [ContentProperty("LinkActions")]
    public class LinkActionsContianer : LinkActionFrame
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(LinkActionsContianer), "S.LinkActionsContianer.Default");
        public static ComponentResourceKey RightBottomKey => new ComponentResourceKey(typeof(LinkActionsContianer), "S.LinkActionsContianer.Expander.RightBottom");
        public static ComponentResourceKey ExpanderTopKey => new ComponentResourceKey(typeof(LinkActionsContianer), "S.LinkActionsContianer.Expander.Top");
        public static ComponentResourceKey ExpanderBottomKey => new ComponentResourceKey(typeof(LinkActionsContianer), "S.LinkActionsContianer.Expander.Bottom");
        public static ComponentResourceKey ExpanderLeftKey => new ComponentResourceKey(typeof(LinkActionsContianer), "S.LinkActionsContianer.Expander.Left");
        public static ComponentResourceKey ExpanderRightKey => new ComponentResourceKey(typeof(LinkActionsContianer), "S.LinkActionsContianer.Expander.Right");
        public static new ComponentResourceKey SingleKey => new ComponentResourceKey(typeof(LinkActionsContianer), "S.LinkActionsContianer.Single");
        public static ComponentResourceKey SingleHorizontalRightTopKey => new ComponentResourceKey(typeof(LinkActionsContianer), "S.LinkActionsContianer.Single.Horizontal.RightTop");
        public static ComponentResourceKey SingleHorizontalRightBottomKey => new ComponentResourceKey(typeof(LinkActionsContianer), "S.LinkActionsContianer.Single.Horizontal.RightBottom");
        public static ComponentResourceKey SingleHorizontalLeftBottomKey => new ComponentResourceKey(typeof(LinkActionsContianer), "S.LinkActionsContianer.Single.Horizontal.LeftBottom");
        public static ComponentResourceKey SingleHorizontalLeftTopKey => new ComponentResourceKey(typeof(LinkActionsContianer), "S.LinkActionsContianer.Single.Horizontal.LeftTop");
        public static ComponentResourceKey SingleVerticalRightTopKey => new ComponentResourceKey(typeof(LinkActionsContianer), "S.LinkActionsContianer.Single.Vertical.RightTop");
        public static ComponentResourceKey SingleVerticalRightBottomKey => new ComponentResourceKey(typeof(LinkActionsContianer), "S.LinkActionsContianer.Single.Vertical.RightBottom");
        public static ComponentResourceKey SingleVerticalLeftBottomKey => new ComponentResourceKey(typeof(LinkActionsContianer), "S.LinkActionsContianer.Single.Vertical.LeftBottom");
        public static ComponentResourceKey ToggleExpanderSingleKey => new ComponentResourceKey(typeof(LinkActionsContianer), "S.ToggleExpander.LinkActionsContianer.Single");
        public static ComponentResourceKey ToggleExpanderCenterKey => new ComponentResourceKey(typeof(LinkActionsContianer), "S.ToggleExpander.Center");
        public static ComponentResourceKey ToggleExpanderKey => new ComponentResourceKey(typeof(LinkActionsContianer), "S.ToggleExpander.LinkActionsContianer");

        public LinkActionCollection LinkActions
        {
            get { return (LinkActionCollection)GetValue(LinkActionsProperty); }
            set { SetValue(LinkActionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkActionsProperty =
            DependencyProperty.Register("LinkActions", typeof(LinkActionCollection), typeof(LinkActionsContianer), new PropertyMetadata(new LinkActionCollection(), (d, e) =>
             {
                 LinkActionsContianer control = d as LinkActionsContianer;

                 if (control == null) return;

                 LinkActionCollection config = e.NewValue as LinkActionCollection;

             }));



        public Style ListStyle
        {
            get { return (Style)GetValue(ListStyleProperty); }
            set { SetValue(ListStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListStyleProperty =
            DependencyProperty.Register("ListStyle", typeof(Style), typeof(LinkActionsContianer), new PropertyMetadata(default(Style), (d, e) =>
             {
                 LinkActionsContianer control = d as LinkActionsContianer;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));

    }



}
