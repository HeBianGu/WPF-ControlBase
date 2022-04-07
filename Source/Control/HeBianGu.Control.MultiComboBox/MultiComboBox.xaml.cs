// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.MultiComboBox
{
    /// <summary>
    /// MultiComboBox.xaml 的交互逻辑
    /// </summary>
    [TemplatePart(Name = "PART_ListBox", Type = typeof(ListBox))]
    public partial class MultiComboBox : ComboBox
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(MultiComboBox), "S.MultiComboBox.Default");
        public static ComponentResourceKey ClearKey => new ComponentResourceKey(typeof(MultiComboBox), "S.MultiComboBox.Clear");
        public static ComponentResourceKey LabelKey => new ComponentResourceKey(typeof(MultiComboBox), "S.MultiComboBox.Label");
        public static ComponentResourceKey LabelClearKey => new ComponentResourceKey(typeof(MultiComboBox), "S.MultiComboBox.LabelClear");

        static MultiComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiComboBox), new FrameworkPropertyMetadata(typeof(MultiComboBox)));
        }

        public ItemsPanelTemplate SelectedItemsPanel
        {
            get { return (ItemsPanelTemplate)GetValue(SelectedItemsPanelProperty); }
            set { SetValue(SelectedItemsPanelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsPanelProperty =
            DependencyProperty.Register("SelectedItemsPanel", typeof(ItemsPanelTemplate), typeof(MultiComboBox), new PropertyMetadata(default(ItemsPanelTemplate), (d, e) =>
             {
                 MultiComboBox control = d as MultiComboBox;

                 if (control == null) return;

                 ItemsPanelTemplate config = e.NewValue as ItemsPanelTemplate;

             }));


        public IList SelectedItems
        {
            get { return (IList)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(IList), typeof(MultiComboBox), new PropertyMetadata(default(IList), (d, e) =>
             {
                 MultiComboBox control = d as MultiComboBox;

                 if (control == null) return;

                 IList config = e.NewValue as IList;

             }));

        private ListBox _listBox;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this._listBox = Template.FindName("PART_ListBox", this) as ListBox;

            this._listBox.SelectionChanged += _ListBox_SelectionChanged;

        }

        private void _ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SelectedItems = this._listBox.SelectedItems;
        }
    }
}
