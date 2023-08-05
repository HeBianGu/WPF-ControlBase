// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace HeBianGu.Control.PropertyGrid
{
    public class PropertyService : LazyInstance<PropertyService>
    {
        public List<IPropertyItem> GetPropertyItems(object obj, Func<IPropertyItem, bool> filter = null)
        {
            if (obj == null) return null;

            List<PropertyInfo> propertys = obj.GetType().GetProperties().OrderBy(l => l.GetCustomAttribute<DisplayAttribute>()?.Order).ToList();

            List<IPropertyItem> propertyItems = new List<IPropertyItem>();

            foreach (PropertyInfo item in propertys)
            {
                BrowsableAttribute browsable = item.GetCustomAttribute<BrowsableAttribute>();

                if (browsable?.Browsable == false) continue;

                IPropertyItem from = item.Create(obj);

                if (filter?.Invoke(from) == false) continue;

                propertyItems.Add(from);
            }

            return propertyItems;
        }


        public List<PropertyTabItem> GetPropertyTabItems(object obj, Func<IPropertyItem, bool> filter)
        {
            List<IPropertyItem> propertyItems = this.GetPropertyItems(obj, filter);

            return this.GetPropertyTabItems(propertyItems);
        }

        public List<PropertyTabItem> GetPropertyTabItems(object obj)
        {
            Func<IPropertyItem, bool> filter = l => l.PropertyInfo.GetCustomAttribute<PropertyItemTypeAttribute>() != null;

            //         Func<IPropertyItem, bool> filter = l => l.PropertyInfo.GetCustomAttribute<CommandAttribute>() != null
            //|| l.PropertyInfo.GetCustomAttribute<PropertyItemTypeAttribute>() != null;


            List<IPropertyItem> propertyItems = this.GetPropertyItems(obj, filter);

            return this.GetPropertyTabItems(propertyItems);
        }

        public List<PropertyTabItem> GetPropertyTabItems(List<IPropertyItem> propertyItems)
        {
            List<PropertyTabItem> result = new List<PropertyTabItem>();

            IEnumerable<IGrouping<string, IPropertyItem>> tabs = propertyItems.GroupBy(l => l.TabGroup);

            foreach (IGrouping<string, IPropertyItem> tab in tabs)
            {
                PropertyTabItem tabItem = new PropertyTabItem();
                tabItem.Name = tab.Key;

                IEnumerable<IGrouping<string, IPropertyItem>> groups = tab.GroupBy(l => l.GroupName);

                foreach (IGrouping<string, IPropertyItem> group in groups)
                {
                    PropertyGroupItem groupItem = new PropertyGroupItem();
                    groupItem.Name = group.Key;
                    groupItem.PropertyItems = group.ToObservable();
                    tabItem.Groups.Add(groupItem);
                }

                result.Add(tabItem);
            }

            return result;
        }
    }
}
