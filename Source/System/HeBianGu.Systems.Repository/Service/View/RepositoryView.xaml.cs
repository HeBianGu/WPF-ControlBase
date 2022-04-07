// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Systems.Repository
{
    public partial class RepositoryView : ContentControl
    {
        static RepositoryView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RepositoryView), new FrameworkPropertyMetadata(typeof(RepositoryView)));
        }

        public ItemsPanelTemplateDisplay ItemsPanel
        {
            get { return (ItemsPanelTemplateDisplay)GetValue(ItemsPanelProperty); }
            set { SetValue(ItemsPanelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsPanelProperty =
            DependencyProperty.Register("ItemsPanel", typeof(ItemsPanelTemplateDisplay), typeof(RepositoryView), new FrameworkPropertyMetadata(default(ItemsPanelTemplateDisplay), (d, e) =>
            {
                RespositoryBox control = d as RespositoryBox;

                if (control == null) return;

                if (e.OldValue is ItemsPanelTemplateDisplay o)
                {

                }

                if (e.NewValue is ItemsPanelTemplateDisplay n)
                {

                }

            }));

        [TypeConverter(typeof(ArrayConverter))]
        public ItemsPanelTemplateDisplay[] ItemsPanelSelectSource
        {
            get { return (ItemsPanelTemplateDisplay[])GetValue(ItemsPanelSelectSourceProperty); }
            set { SetValue(ItemsPanelSelectSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsPanelSelectSourceProperty =
            DependencyProperty.Register("ItemsPanelSelectSource", typeof(ItemsPanelTemplateDisplay[]), typeof(RepositoryView), new FrameworkPropertyMetadata(default(ItemsPanelTemplateDisplay[]), (d, e) =>
            {
                RespositoryBox control = d as RespositoryBox;

                if (control == null) return;

                if (e.OldValue is ItemsPanelTemplateDisplay[] o)
                {

                }

                if (e.NewValue is ItemsPanelTemplateDisplay[] n)
                {
                    control.ItemsPanel = n?.FirstOrDefault();
                }

            }));

        public DiplayDataTemplate[] ItemTemplateSelectSource
        {
            get { return (DiplayDataTemplate[])GetValue(ItemTemplateSelectSourceProperty); }
            set { SetValue(ItemTemplateSelectSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateSelectSourceProperty =
            DependencyProperty.Register("ItemTemplateSelectSource", typeof(DiplayDataTemplate[]), typeof(RepositoryView), new FrameworkPropertyMetadata(default(DiplayDataTemplate[]), (d, e) =>
            {
                RespositoryBox control = d as RespositoryBox;

                if (control == null) return;

                if (e.OldValue is DiplayDataTemplate[] o)
                {

                }

                if (e.NewValue is DiplayDataTemplate[] n)
                {
                    control.ItemTemplate = n?.FirstOrDefault();
                }

            }));


        public Style PagedDataGridStyle
        {
            get { return (Style)GetValue(PagedDataGridStyleProperty); }
            set { SetValue(PagedDataGridStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PagedDataGridStyleProperty =
            DependencyProperty.Register("PagedDataGridStyle", typeof(Style), typeof(RepositoryView), new FrameworkPropertyMetadata(default(Style), (d, e) =>
             {
                 RepositoryView control = d as RepositoryView;

                 if (control == null) return;

                 if (e.OldValue is Style o)
                 {

                 }

                 if (e.NewValue is Style n)
                 {

                 }

             }));


    }
}
