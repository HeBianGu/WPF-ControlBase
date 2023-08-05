// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Xml.Serialization;

namespace HeBianGu.Control.PropertyGrid
{
    public interface ILoadDefaultPropertyItem
    {
        void LoadDefault();
    }

    public class StylesPropertyItem : ItemsSourcePropertyItem<StyleHost>, ILoadDefaultPropertyItem
    {
        public StylesPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
            this.LoadDefault();
        }

        public string FilterFormat { get; set; } = "S.{0}.";

        public virtual void LoadDefault()
        {
            return;

            string filter = this.PropertyInfo.GetCustomAttribute<StyleSettingFilterAttribute>()?.Filter;

            string format = string.Format(this.FilterFormat, filter);

            System.Collections.Generic.List<System.Collections.DictionaryEntry> entries = Application.Current.Resources.FindResources(l => l.Key?.ToString()?.StartsWith(format) == true);

            foreach (System.Collections.DictionaryEntry entriey in entries)
            {
                if (entriey.Value is Style style)
                {
                    StyleHost host = new StyleHost();
                    host.Style = style;
                    host.Name = entriey.Key?.ToString();
                    this.Collection.Add(host);
                }
            }

            if (this.Value == null)
            {
                this.Value = this.Collection?.FirstOrDefault();
            }
            else
            {
                if (this.Value.Style == null)
                {
                    this.Value.Style = this.Collection?.FirstOrDefault(l => l.Name == this.Value.Name)?.Style;

                }
                this.Collection.Replace(this.Value, l => l.Name == this.Value.Name);
            }
        }
    }

    public class ViewStylesPropertyItem : StylesPropertyItem
    {
        public ViewStylesPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }
    }


    public class KeyStylesPropertyItem : ViewStylesPropertyItem
    {
        public KeyStylesPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        public override void LoadDefault()
        {
            Type keyType = this.PropertyInfo.GetCustomAttribute<KeyStyleSettingFilterAttribute>()?.KeyType;

            PropertyInfo[] properties = keyType.GetProperties(BindingFlags.Static | BindingFlags.Public);


            foreach (PropertyInfo item in properties)
            {
                ComponentResourceKey key = item.GetValue(null) as ComponentResourceKey;

                if (key == null) continue;

                Style style = Application.Current.TryFindResource(key) as Style;

                StyleHost host = new StyleHost();
                host.Style = style;
                host.Name = key.ResourceId?.ToString();
                this.Collection.Add(host);
            }

            if (this.Value == null)
            {
                this.Value = this.Collection?.FirstOrDefault();
            }
            else
            {
                if (this.Value.Style == null)
                {
                    this.Value.Style = this.Collection?.FirstOrDefault(l => l.Name == this.Value.Name)?.Style;

                }
                this.Collection.Replace(this.Value, l => l.Name == this.Value.Name);
            }
        }
    }

    public class StyleHost : NotifyPropertyChangedBase
    {
        public string Name { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public Style Style { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class StyleSettingFilterAttribute : Attribute
    {
        public string Filter { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class KeyStyleSettingFilterAttribute : Attribute
    {
        public Type KeyType { get; set; }
    }

    public abstract class RefreshDefaultSettingInstance<T> : LazySettingInstance<T> where T : new()
    {
        public override void Load()
        {
            base.Load();

            PropertyInfo[] ps = this.GetType().GetProperties();

            foreach (PropertyInfo p in ps)
            {
                PropertyItemTypeAttribute pa = p.GetCustomAttribute<PropertyItemTypeAttribute>();

                if (pa?.Type == null) continue;

                object instance = Activator.CreateInstance(pa?.Type, p, this);

                if (instance is ILoadDefaultPropertyItem refreshDefault)
                {
                    refreshDefault.LoadDefault();
                }
            }
        }
    }
}
