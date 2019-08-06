using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// MultiComboBox.xaml 的交互逻辑
    /// </summary>
    [TemplatePart(Name = "PART_ListBox", Type = typeof(ListBox))]
    public partial class MultiComboBox : ComboBox
    {
        /// <summary>
        /// 获取选择项集合
        /// </summary>
        public IList SelectedItems
        {
            get { return this._ListBox.SelectedItems; }
        }

        /// <summary>
        /// 设置或获取选择项
        /// </summary>
        public new int SelectedIndex
        {
            get { return this._ListBox.SelectedIndex; }
            set { this._ListBox.SelectedIndex = value; }
        }

        private ListBox _ListBox;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this._ListBox = Template.FindName("PART_ListBox", this) as ListBox;

            this.RefreshList();

            this._ListBox.SelectionChanged += _ListBox_SelectionChanged;

        }

        void _ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            string str=string.Empty;

            foreach (var item in this.SelectedItems)
            {
                sb.Append(item.ToString()).Append(this.SplitChar);

                str += item.ToString();

            }
            this.Text = sb.ToString().Trim(this.SplitChar.ToCharArray()[0]);

            this.SelectText = this.Text;
        }


        static MultiComboBox()
        {
            //OverridesDefaultStyleProperty.OverrideMetadata(typeof(MultiComboBox), new FrameworkPropertyMetadata(typeof(MultiComboBox)));
        }

        public MultiComboBox()
        {
            ListBox ls = new ListBox();
            //ls.SelectedItems
        }

        public void SelectAll()
        {
            this._ListBox.SelectAll();
        }

        public void UnselectAll()
        {
            this._ListBox.UnselectAll();
        }

        public string SelectText
        {
            get { return (string)GetValue(_selectTextProperty); }
            set { SetValue(_selectTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for _selectText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty _selectTextProperty =
            DependencyProperty.Register("SelectText", typeof(string), typeof(MultiComboBox), new PropertyMetadata(null, PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as MultiComboBox;

            if (e.NewValue == null) return;

            control.SelectText = e.NewValue.ToString();

            control.Text = e.NewValue.ToString();

            control.RefreshList();
        
        }


        void RefreshList()
        {
            var collection = this.Text.Split(this.SplitChar.ToCharArray()[0]);

            if (this._ListBox == null) return;

            foreach (var item in collection)
            {
                this._ListBox.SelectedItems.Add(item);
            }
        }


        public string SplitChar
        {
            get { return (string)GetValue(SplitCharProperty); }
            set { SetValue(SplitCharProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SplitChar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitCharProperty =
            DependencyProperty.Register("SplitChar", typeof(string), typeof(MultiComboBox), new PropertyMetadata("\\"));



    }
}
