// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Base.WpfBase
{
    public interface ISetting
    {
        bool IsVisibleInSetting { get; set; }

        int Order { get; }

        string Name { get; }

        string GroupName { get; }

        void Load();

        bool Save(out string message);

        void LoadDefault();
    }

    public class SystemDisplay : LazyNotifyInstance<SystemDisplay>
    {
        private ObservableCollection<ISetting> _settings = new ObservableCollection<ISetting>();
        public ObservableCollection<ISetting> Settings
        {
            get { return _settings; }
            set
            {
                _settings = value;
                RaisePropertyChanged();
            }
        }
    }

    public interface ISettingOption
    {
        string Name { get; set; }
        string GroupName { get; set; }
        string Description { get; set; }
        string Icon { get; set; }
        int Order { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]

    public class SettingAttribute : Attribute
    {

    }

    //[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    //public class DisplayAttribute : Attribute
    //{
    //    public string Name { get; set; }
    //    public string Icon { get; set; }
    //    public string Group { get; set; }
    //    public string Description { get; set; }
    //    public int Order { get; set; }
    //}

    //public abstract class AutoritySettingBase : SettingBase, IAuthority
    //{
    //    public AutoritySettingBase()
    //    {
    //        this.AuthorID = this.ID;
    //        this.AuthorName = this.Name;
    //        this.AuthorPredefinePath = this.GroupName;
    //        var authority = this.GetType().GetCustomAttribute<AuthorityAttribute>();
    //        if (authority != null)
    //        {
    //            this.AuthorID = authority.ID ?? this.AuthorID;
    //            this.AuthorName = authority.Name ?? this.AuthorName;
    //            this.AuthorPredefinePath = authority.PredefinePath;
    //        }

    //        var check = Loginner.Instance?.User?.Role?.Authors?.Any(x => x.ID == this.AuthorID);
    //        if (check == false)
    //            this.IsAuthority = false;
    //    }

    //    public override void LoadDefault()
    //    {
    //        base.LoadDefault();

    //        var ps = this.GetType().GetProperties().Where(x => typeof(IAuthority).IsAssignableFrom(x.PropertyType));
    //        foreach (PropertyInfo p in ps)
    //        {
    //            var d = p.GetCustomAttribute<AuthorityAttribute>();
    //            if (d == null) continue;
    //            var authority = p.GetValue(this) as IAuthority;
    //            if (authority == null) continue;
    //            authority.AuthorID = d.ID;
    //            authority.AuthorName = d.Name;
    //            authority.GroupName = d.GroupName;
    //            authority.AuthorPredefinePath = d.PredefinePath;
    //        }
    //    }

    //    [XmlIgnore]
    //    [Browsable(false)]
    //    public string AuthorID { get; set; }
    //    [XmlIgnore]
    //    [Browsable(false)]
    //    public string AuthorName { get; set; }
    //    [XmlIgnore]
    //    [Browsable(false)]
    //    public string AuthorPredefinePath { get; set; }
    //    private bool _isAuthority = true;
    //    [XmlIgnore]
    //    [Browsable(false)]
    //    public bool IsAuthority
    //    {
    //        get { return _isAuthority; }
    //        set
    //        {
    //            _isAuthority = value;
    //            RaisePropertyChanged();
    //        }
    //    }


    //}

    public abstract class SettingBase : DisplayerViewModelBase, ISetting
    {
        [Browsable(false)]
        public bool IsVisibleInSetting { get; set; } = true;

        public override void LoadDefault()
        {
            base.LoadDefault();
            //this.ConfigPath = this.GetDefaultPath();
        }

        protected virtual string GetDefaultPath()
        {
            if (SettingPath.Instance == null)
            {
                return System.IO.Path.Combine(this.GetDefaultFolder(), this.ID + ".xml");
            }
            return System.IO.Path.Combine(this.GetDefaultFolder(), this.ID + SettingPath.Instance.GetConfigExtention());
        }

        protected virtual string GetDefaultFolder()
        {
            if (SettingPath.Instance == null)
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
            return SettingPath.Instance.GetSetting();
        }

        public virtual bool Save(out string message)
        {
            message = null;
            string path = this.GetDefaultPath();
            string folder = Path.GetDirectoryName(path);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            this.GetSerializerService()?.Save(path, this);
            return true;
        }

        protected virtual ISerializerService GetSerializerService()
        {
            return XmlSerialize.Instance;
        }

        public virtual void Load()
        {
            this.Load(this.GetDefaultPath());
        }

        protected virtual void Load(string path)
        {
            object load = this.GetSerializerService()?.Load(path, this.GetType());
            if (load == null) return;
            PropertyInfo[] ps = this.GetType().GetProperties();
            foreach (PropertyInfo p in ps)
            {
                if (p.GetCustomAttribute<XmlIgnoreAttribute>() != null)
                    continue;
                if (p.CanRead && p.CanWrite)
                {
                    var v = p.GetValue(load);
                    p.SetValue(this, v);
                }
            }
        }

        public virtual bool IsInit()
        {
            return !File.Exists(this.GetDefaultPath());
        }
    }
}