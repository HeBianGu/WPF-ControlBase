// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.PropertyGrid
{
    public partial class PropertyTabControl : TabControl
    {
        public static ComponentResourceKey OfficeKey => new ComponentResourceKey(typeof(PropertyTabControl), "S.PropertyTabControl.Office");


        static PropertyTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyTabControl), new FrameworkPropertyMetadata(typeof(PropertyTabControl)));
        }

        public PropertyTabControl()
        {
            this.Mapper = (l, k) => l.Create(k);
        }

        public PropertyTabItem SelectedTabItem
        {
            get { return (PropertyTabItem)GetValue(SelectedTabItemProperty); }
            set { SetValue(SelectedTabItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedTabItemProperty =
            DependencyProperty.Register("SelectedTabItem", typeof(PropertyTabItem), typeof(PropertyTabControl), new PropertyMetadata(default(PropertyTabItem), (d, e) =>
            {
                PropertyTabControl control = d as PropertyTabControl;

                if (control == null) return;

                Predicate<PropertyInfo> config = e.NewValue as Predicate<PropertyInfo>;
            }));


        public object SelectObject
        {
            get { return GetValue(SelectObjectProperty); }
            set { SetValue(SelectObjectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectObjectProperty =
            DependencyProperty.Register("SelectObject", typeof(object), typeof(PropertyTabControl), new FrameworkPropertyMetadata(default(object), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 PropertyTabControl control = d as PropertyTabControl;

                 if (control == null) return;

                 if (e.OldValue is object o)
                 {

                 }

                 if (e.NewValue is object n)
                 {
                     control.RefreshObject(n);
                 }

             }));

        public Predicate<PropertyInfo> Filter
        {
            get { return (Predicate<PropertyInfo>)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(Predicate<PropertyInfo>), typeof(PropertyTabControl), new PropertyMetadata(default(Predicate<PropertyInfo>), (d, e) =>
            {
                PropertyGrid control = d as PropertyGrid;

                if (control == null) return;

                Predicate<PropertyInfo> config = e.NewValue as Predicate<PropertyInfo>;

            }));


        public Func<PropertyInfo, object, IPropertyItem> Mapper
        {
            get { return (Func<PropertyInfo, object, ObjectPropertyItem>)GetValue(MapperProperty); }
            set { SetValue(MapperProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapperProperty =
            DependencyProperty.Register("Mapper", typeof(Func<PropertyInfo, object, IPropertyItem>), typeof(PropertyTabControl), new FrameworkPropertyMetadata(default(Func<PropertyInfo, object, IPropertyItem>), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
            {
                PropertyGrid control = d as PropertyGrid;

                if (control == null) return;

                if (e.OldValue is Func<PropertyInfo, object, IPropertyItem> o)
                {

                }

                if (e.NewValue is Func<PropertyInfo, object, IPropertyItem> n)
                {

                }

            }));


        protected void RefreshObject(object o)
        {
            if (o == null) return;

            List<PropertyInfo> propertys = o.GetType().GetProperties().ToList();

            List<IPropertyItem> propertyItems = new List<IPropertyItem>();

            foreach (PropertyInfo item in propertys)
            {
                //  Do ：筛选条件
                if (this.Filter != null && !this.Filter.Invoke(item))
                {
                    continue;
                }

                BrowsableAttribute browsable = item.GetCustomAttribute<BrowsableAttribute>();

                if (browsable?.Browsable != true) continue;

                IPropertyItem from = this.Mapper?.Invoke(item, o);

                propertyItems.Add(from);
            }

            this.ItemsSource = PropertyService.Instance.GetPropertyTabItems(propertyItems).ToObservable();
        }

    }




    public class PropertyTabItem : NotifyPropertyChangedBase
    {
        public string Name { get; set; }

        private ObservableCollection<PropertyGroupItem> _groups = new ObservableCollection<PropertyGroupItem>();
        /// <summary> 说明  </summary>
        public ObservableCollection<PropertyGroupItem> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                RaisePropertyChanged("Groups");
            }
        }
    }


    public class PropertyGroupItem : NotifyPropertyChangedBase
    {
        public string Name { get; set; }

        private ObservableCollection<IPropertyItem> _propertyItems = new ObservableCollection<IPropertyItem>();
        /// <summary> 说明  </summary>
        public ObservableCollection<IPropertyItem> PropertyItems
        {
            get { return _propertyItems; }
            set
            {
                _propertyItems = value;
                RaisePropertyChanged("PropertyItems");
            }
        }

    }

}
