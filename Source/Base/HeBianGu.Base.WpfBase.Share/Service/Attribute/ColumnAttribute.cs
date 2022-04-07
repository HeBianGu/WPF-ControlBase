// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Reflection;
using System.Windows.Controls;

namespace HeBianGu.Base.WpfBase
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class DataGridColumnAttribute : Attribute
    {
        public DataGridColumnAttribute(string width)
        {
            DataGridLengthConverter converter = new DataGridLengthConverter();

            this.Width = (DataGridLength)converter.ConvertFromString(width);
        }
        public DataGridLength Width { get; set; } = new DataGridLength(1, DataGridLengthUnitType.Star);

        public Type Template { get; set; } = typeof(DataGridTextColumn);

        /// <summary>
        /// "{0}.Property"
        /// </summary>
        public string PropertyPath { get; set; } = "{0}";


        public DataGridColumn GetDataGridColumn(PropertyInfo propertyInfo)
        {
            DataGridColumn dataGridColumn = Activator.CreateInstance(this.Template) as DataGridColumn;

            if (dataGridColumn == null)
            {
                if (propertyInfo.PropertyType == typeof(bool))
                {
                    return new DataGridCheckBoxColumn() { Width = this.Width, IsReadOnly = false };
                }
                else if (propertyInfo.PropertyType.IsEnum)
                {
                    return new DataGridComboBoxColumn() { Width = this.Width, IsReadOnly = false };
                }
                else
                {
                    return new DataGridTextColumn() { Width = this.Width, IsReadOnly = false };
                }
            }

            dataGridColumn.Width = this.Width;

            return dataGridColumn;
        }

    }
}
