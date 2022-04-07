// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace HeBianGu.Base.WpfBase
{
    public interface ISetting
    {
        string Name { get; }

        string Group { get; }

        void Load();

        bool Save(out string message);

        void LoadDefault();
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]

    public class SettingAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SettingConfigAttribute : Attribute
    {

        public string Name { get; set; }

        public string Group { get; set; }

        public int Order { get; set; }
    }

    public abstract class SettingBase : NotifyPropertyChangedBase, ISetting
    {
        public SettingBase()
        {
            this.LoadDefault();

            SettingConfigAttribute config = this.GetType().GetCustomAttribute<SettingConfigAttribute>();

            this.Name = config?.Name;
            this.Group = config?.Group;
            this.ID = this.GetType().Name;
        }

        private string _name;
        [XmlIgnore]
        [Browsable(false)]
        public string Name
        {
            get { return _name; }
            private set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }


        private string _group;
        [XmlIgnore]
        [Browsable(false)]
        public string Group
        {
            get { return _group; }
            private set
            {
                _group = value;
                RaisePropertyChanged();
            }
        }


        public void LoadDefault()
        {
            PropertyInfo[] ps = this.GetType().GetProperties();

            foreach (PropertyInfo p in ps)
            {
                DefaultValueAttribute d = p.GetCustomAttribute<DefaultValueAttribute>();

                if (d == null) continue;

                object value = Convert.ChangeType(d.Value, p.PropertyType);

                p.SetValue(this, value);
            }
        }

        [Browsable(false)]
        private ISerializerService XmlSerializer => ServiceRegistry.Instance.GetInstance<ISerializerService>();

        protected virtual string GetPath()
        {
            if (SettingPath.Instance == null)
            {
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.ID + ".xml");
            }
            return System.IO.Path.Combine(SettingPath.Instance.GetSetting(), this.ID + SettingPath.Instance.GetConfigExtention());
        }

        [XmlIgnore]
        [Browsable(false)]
        public string ID { get; set; }

        public virtual bool Save(out string message)
        {
            message = null;

            XmlSerializer?.Save(this.GetPath(), this);

            return true;
        }

        public virtual void Load()
        {
            object load = XmlSerializer?.Load(this.GetPath(), this.GetType());

            if (load == null) return;

            PropertyInfo[] ps = this.GetType().GetProperties();

            foreach (PropertyInfo p in ps)
            {
                if (p.CanRead && p.CanWrite)
                {
                    p.SetValue(this, p.GetValue(load));
                }
            }
        }

        public bool IsInit()
        {
            return !File.Exists(this.GetPath());
        }
    }


    public abstract class LazySettingInstance<T> : SettingBase where T : new()
    {
        public static T Instance = new Lazy<T>(() => new T()).Value;
    }
}