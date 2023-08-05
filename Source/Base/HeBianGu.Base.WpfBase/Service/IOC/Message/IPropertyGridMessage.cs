// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Base.WpfBase
{
    public interface IPropertyGridMessage
    {
        Task<bool> ShowEdit<T>(T value, Predicate<T> match = null, string title = "编辑", Action<IPropertyGridOption> builder = null, ComponentResourceKey key = null);
        Task<bool> ShowView<T>(T value, Predicate<T> match = null, string title = "详情", Action<IPropertyGridOption> builder = null, ComponentResourceKey key = null);
        Task<bool> ShowEdits(string title = "编辑", params object[] value);
    }

    public interface IPropertyGridOption
    {
        string ExceptPropertyNames { get; set; }
        Predicate<PropertyInfo> Filter { get; set; }
        double MessageWidth { get; set; }
        object SelectObject { get; set; }
        string Title { get; set; }
        bool UseArray { get; set; }
        bool UseAsync { get; set; }
        bool UseBoolen { get; set; }
        bool UseClass { get; set; }
        bool UseCommand { get; set; }
        bool UseCommandOnly { get; set; }
        bool UseDateTime { get; set; }
        bool UseDeclaredOnly { get; set; }
        bool UseDisplayer { get; set; }
        bool UseEnum { get; set; }
        bool UseEnumerator { get; set; }
        bool UseGroup { get; set; }
        string UseGroupNames { get; set; }
        bool UseInterface { get; set; }
        bool UseNull { get; set; }
        bool UseOrder { get; set; }
        bool UseOrderByName { get; set; }
        bool UseOrderByType { get; set; }
        bool UsePresenter { get; set; }
        bool UsePrimitive { get; set; }
        string UsePropertyNames { get; set; }
        bool UsePropertyView { get; set; }
        bool UseString { get; set; }
        bool UseTypeConverter { get; set; }
        bool UseTypeConverterOnly { get; set; }
        double MinWidth { get; set; }
        double Width { get; set; }
        Thickness Margin { get; set; }
        Thickness Padding { get; set; }
        HorizontalAlignment HorizontalAlignment { get; set; }
        VerticalAlignment VerticalAlignment { get; set; }
    }

}
