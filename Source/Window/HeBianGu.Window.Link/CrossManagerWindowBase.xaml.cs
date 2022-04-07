// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Window.Link
{
    /// <summary> 环绕操作窗口 </summary>
    public partial class CrossManagerWindowBase : LinkActionsManagerWindowBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(CrossManagerWindowBase), "S.Window.Cross.Default");

        static CrossManagerWindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CrossManagerWindowBase), new FrameworkPropertyMetadata(typeof(CrossManagerWindowBase)));
        }
        //protected override void OnLoaded()
        //{
        //    if(LoadDefault)
        //    {
        //        base.OnLoaded();
        //    }
        //}

        //public bool LoadDefault { get; set; }

        /// <summary> 环绕的布局容器 </summary>
        public ItemsPanelTemplate ItemsPanel
        {
            get { return (ItemsPanelTemplate)GetValue(ItemsPanelProperty); }
            set { SetValue(ItemsPanelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsPanelProperty =
            DependencyProperty.Register("ItemsPanel", typeof(ItemsPanelTemplate), typeof(CrossManagerWindowBase), new PropertyMetadata(default(ItemsPanelTemplate), (d, e) =>
            {
                CrossManagerWindowBase control = d as CrossManagerWindowBase;

                if (control == null) return;

                ItemsPanelTemplate config = e.NewValue as ItemsPanelTemplate;

            }));


        public DataTemplate LinkActionDataTemplate
        {
            get { return (DataTemplate)GetValue(LinkActionDataTemplateProperty); }
            set { SetValue(LinkActionDataTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkActionDataTemplateProperty =
            DependencyProperty.Register("LinkActionDataTemplate", typeof(DataTemplate), typeof(CrossManagerWindowBase), new PropertyMetadata(default(DataTemplate), (d, e) =>
            {
                CrossManagerWindowBase control = d as CrossManagerWindowBase;

                if (control == null) return;

                DataTemplate config = e.NewValue as DataTemplate;

            }));


        public Geometry ContenClip
        {
            get { return (Geometry)GetValue(ContenClipProperty); }
            set { SetValue(ContenClipProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContenClipProperty =
            DependencyProperty.Register("ContenClip", typeof(Geometry), typeof(CrossManagerWindowBase), new PropertyMetadata(default(Geometry), (d, e) =>
            {
                CrossManagerWindowBase control = d as CrossManagerWindowBase;

                if (control == null) return;

                Geometry config = e.NewValue as Geometry;

            }));


    }
}
