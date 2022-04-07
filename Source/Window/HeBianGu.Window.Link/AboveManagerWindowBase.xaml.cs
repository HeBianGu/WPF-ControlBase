// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Window.Link
{
    /// <summary> 遮盖窗口 </summary>
    public partial class AboveManagerWindowBase : LinkGroupsManagerWindowBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(AboveManagerWindowBase), "S.Window.Above.Default");

        static AboveManagerWindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AboveManagerWindowBase), new FrameworkPropertyMetadata(typeof(AboveManagerWindowBase)));
        }
        /// <summary> 环绕的布局容器 </summary>
        public ItemsPanelTemplate ItemsPanel
        {
            get { return (ItemsPanelTemplate)GetValue(ItemsPanelProperty); }
            set { SetValue(ItemsPanelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsPanelProperty =
            DependencyProperty.Register("ItemsPanel", typeof(ItemsPanelTemplate), typeof(AboveManagerWindowBase), new PropertyMetadata(default(ItemsPanelTemplate), (d, e) =>
            {
                AboveManagerWindowBase control = d as AboveManagerWindowBase;

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
            DependencyProperty.Register("LinkActionDataTemplate", typeof(DataTemplate), typeof(AboveManagerWindowBase), new PropertyMetadata(default(DataTemplate), (d, e) =>
            {
                AboveManagerWindowBase control = d as AboveManagerWindowBase;

                if (control == null) return;

                DataTemplate config = e.NewValue as DataTemplate;

            }));


        public bool IsboveState
        {
            get { return (bool)GetValue(IsboveStateProperty); }
            set { SetValue(IsboveStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsboveStateProperty =
            DependencyProperty.Register("IsboveState", typeof(bool), typeof(AboveManagerWindowBase), new PropertyMetadata(default(bool), (d, e) =>
             {
                 AboveManagerWindowBase control = d as AboveManagerWindowBase;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));

        protected override void OnLoaded()
        {
            return;
        }
    }
}
