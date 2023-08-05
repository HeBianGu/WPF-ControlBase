// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.PagedDataGrid
{
    public class TextBoxDataGridColumn : DataGridBoundColumn
    {
        public TextBoxDataGridColumn()
        {
            this.IsReadOnly = false;
        }
        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            TextBox textBox = new TextBox() { HorizontalAlignment = HorizontalAlignment.Stretch };
            if (this.Binding != null)
                textBox.SetBinding(TextBox.TextProperty, this.Binding);
            return textBox;
        }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            //var textBox = new TextBlock();
            //textBox.SetBinding(TextBlock.TextProperty, this.Binding);
            //return textBox;
            cell.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            TextBox textBox = new TextBox() { HorizontalAlignment = HorizontalAlignment.Stretch };
            if (this.Binding != null)
                textBox.SetBinding(TextBox.TextProperty, this.Binding);
            return textBox;

        }
    }
}
