// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Component
{
    public abstract class Action : ActionBase, ISerialize
    {
        public void DeSerialize(SerializeData serializeData)
        {
            PropertyInfo[] ps = this.GetType().GetProperties();

            foreach (PropertyInfo p in ps)
            {
                if (!p.CanRead) continue;

                if (!p.CanWrite) continue;

                XmlIgnoreAttribute ignore = p.GetCustomAttribute<XmlIgnoreAttribute>();

                if (ignore != null) continue;

                SerializeDataProperty find = serializeData.Propertys.FirstOrDefault(l => l.Name == p.Name);

                if (find == null) continue;

                if (p.PropertyType.IsPrimitive)
                {
                    object v = Convert.ChangeType(find.Value, p.PropertyType);

                    p.SetValue(this, v);
                }

                if (p.PropertyType.IsEnum)
                {
                    object v = Enum.Parse(p.PropertyType, find.Value);

                    p.SetValue(this, v);
                }

                if (p.PropertyType == typeof(string))
                {
                    p.SetValue(this, find.Value);
                }

                if (p.PropertyType == typeof(DateTime))
                {
                    p.SetValue(this, DateTime.Parse(find.Value));
                }
            }
        }

        public virtual SerializeData Serialize()
        {
            SerializeData result = new SerializeData();

            Type type = this.GetType();

            result.Dll = type.Assembly.FullName;

            result.Component = type.Assembly.GetName().Name;

            result.Class = type.Name;

            result.FullName = type.FullName;

            PropertyInfo[] ps = type.GetProperties();

            foreach (PropertyInfo p in ps)
            {
                if (!p.CanRead) continue;

                if (!p.CanWrite) continue;

                XmlIgnoreAttribute ignore = p.GetCustomAttribute<XmlIgnoreAttribute>();

                if (ignore != null) continue;

                SerializeDataProperty property = new SerializeDataProperty();

                property.Name = p.Name;

                object value = p.GetValue(this);

                property.Value = value?.ToString();

                result.Propertys.Add(property);
            }

            return result;
        }
    }
}
