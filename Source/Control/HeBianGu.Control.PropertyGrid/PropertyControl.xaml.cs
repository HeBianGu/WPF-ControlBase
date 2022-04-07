// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;

namespace HeBianGu.Control.PropertyGrid
{

    /// <summary> 自定义导航框架 </summary> 
    public partial class PropertyControl : PropertyGrid
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(PropertyControl), "S.PropertyControl.Default");
        public static new ComponentResourceKey SingleKey => new ComponentResourceKey(typeof(PropertyControl), "S.PropertyControl.Single");
        public static new ComponentResourceKey SumitKey => new ComponentResourceKey(typeof(PropertyControl), "S.PropertyControl.Default.WithSumit");
        public static new ComponentResourceKey SumitCloseKey => new ComponentResourceKey(typeof(PropertyControl), "S.PropertyControl.Default.WithSumitClose");

        static PropertyControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyControl), new FrameworkPropertyMetadata(typeof(PropertyControl)));

            StyleLoader.Instance?.LoadDefault(typeof(PropertyControl));
        }

        public bool IsFilterBindable
        {
            get { return (bool)GetValue(IsFilterBindableProperty); }
            set { SetValue(IsFilterBindableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFilterBindableProperty =
            DependencyProperty.Register("IsFilterBindable", typeof(bool), typeof(PropertyControl), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 PropertyControl control = d as PropertyControl;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

                 control.RefreshObject(control.SelectObject);

             }));


        protected override void RefreshObject(object o)
        {
            if (o == null) return;

            Type type = o.GetType();

            List<PropertyInfo> propertys = new List<PropertyInfo>();

            Action<Type> action = null;

            action = l =>
            {
                if (l.BaseType != typeof(object))
                {
                    action(l.BaseType);
                }

                PropertyInfo[] ps = l.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                if (ps != null)
                    propertys.AddRange(ps);


            };

            if (this.DeclaredOnly)
            {
                PropertyInfo[] ps = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                if (ps != null)
                    propertys.AddRange(ps);
            }
            else
            {
                action.Invoke(type);
            }


            //var propertys = type.GetProperties().OrderBy(l => l.DeclaringType).ToList();

            List<IPropertyItem> items = new List<IPropertyItem>();

            foreach (PropertyInfo item in propertys)
            {
                //  Do ：筛选条件
                if (this.Filter != null && !this.Filter.Invoke(item))
                {
                    continue;
                }

                if (this.IsFilterBindable)
                {
                    BindableAttribute bindable = item.GetCustomAttribute<BindableAttribute>();

                    if (bindable == null || bindable.Bindable == false) continue;
                }

                BrowsableAttribute browsable = item.GetCustomAttribute<BrowsableAttribute>();

                if (browsable != null && browsable.Browsable == false) continue;

                IPropertyItem from = PropertyExtention.Create(item, o);

                if (from is ObjectPropertyItem obj)
                {
                    obj.ValueChanged = l =>
                    {
                        this.OnValueChanged(Tuple.Create(from, l));
                    };
                }

                items.Add(from);
            }

            this.ItemsSource = items;
        }
    }
}
