// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HeBianGu.Base.WpfBase
{
    //public interface IDataGridColumn
    //{
    //    DataGridColumn GetDataGridColumn(PropertyInfo propertyInfo);
    //    string PropertyPath { get; set; }
    //}

    public interface IDataGridColumn
    {
        string PropertyPath { get; set; }
        Type Template { get; set; }
        DataGridLength Width { get; set; }

        DataGridColumn GetDataGridColumn(PropertyInfo propertyInfo);
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class DataGridColumnAttribute : Attribute, IDataGridColumn
    {
        public DataGridColumnAttribute(string width)
        {
            DataGridLengthConverter converter = new DataGridLengthConverter();
            this.Width = (DataGridLength)converter.ConvertFromString(width);
        }
        public DataGridColumnAttribute()
        {

        }
        public DataGridLength Width { get; set; } = DataGridLength.Auto;
        public Type Template { get; set; } = typeof(DataGridTextColumn);
        /// <summary>
        /// "{0}.Property"
        /// </summary>
        public string PropertyPath { get; set; } = "{0}";
        public virtual DataGridColumn GetDataGridColumn(PropertyInfo propertyInfo)
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

    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class ForegroundDataGridColumnAttribute : DataGridColumnAttribute
    {
        public override DataGridColumn GetDataGridColumn(PropertyInfo propertyInfo)
        {
            DataGridTemplateColumn column = new DataGridTemplateColumn() { Width = this.Width, IsReadOnly = false };
            DataTemplate dataTemplate = new DataTemplate(propertyInfo.PropertyType);
            FrameworkElementFactory elementFactory = new FrameworkElementFactory(typeof(TextBlock));
            var binding = this.GetForegroundBinding(propertyInfo);
            elementFactory.SetBinding(TextBlock.ForegroundProperty, binding);
            elementFactory.SetBinding(TextBlock.TextProperty, new Binding(string.Format(this.PropertyPath, propertyInfo.Name)));
            dataTemplate.VisualTree = elementFactory;
            column.CellTemplate = dataTemplate;
            return column;
        }

        protected virtual Binding GetForegroundBinding(PropertyInfo propertyInfo)
        {
            Binding binding = new Binding(string.Format(this.PropertyPath, propertyInfo.Name));
            binding.Converter = Converter.GetStateBrush;
            return binding;
        }
    }

    [System.AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public sealed class VipAttribute : Attribute
    {
        public VipAttribute(int level)
        {
            Level = level;
        }

        public int Level { get; set; }
    }
}
