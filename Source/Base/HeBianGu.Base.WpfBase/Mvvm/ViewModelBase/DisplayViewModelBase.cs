// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using System.Xml.Serialization;

namespace HeBianGu.Base.WpfBase
{
    public interface IDisplayer
    {
        void LoadDefault();
    }

    public class DisplayerViewModelBase : NotifyPropertyChangedBase, IDisplayer
    {
        public DisplayerViewModelBase()
        {
            this.ID = this.GetType().Name;
            this.Name = this.GetType().Name;
            {
                var display = this.GetType().GetCustomAttribute<DisplayerAttribute>(true);
                if (display != null)
                {
                    this.ID = display.ID ?? this.ID;
                    this.Name = display.Name ?? this.Name;
                    this.GroupName = display.GroupName;
                    this.Description = display.Description;
                    this.Order = display.Order;
                    this.Icon = display.Icon;
                    this.TabName = display.TabName;
                }
            }

            var cmdps = this.GetType().GetProperties().Where(x => typeof(ICommand).IsAssignableFrom(x.PropertyType));
            foreach (var cmdp in cmdps)
            {
                if (cmdp.CanRead == false)
                    continue;
                if (cmdp.GetCustomAttribute<BrowsableAttribute>()?.Browsable == false)
                    continue;
                ICommand command = cmdp.GetValue(this) as ICommand;
                this.Commands.Add(command);
            }

            this.LoadDefault();
        }
        [Browsable(false)]
        [XmlIgnore]
        public ObservableCollection<ICommand> Commands { get; } = new ObservableCollection<ICommand>();

        [Browsable(false)]
        [XmlIgnore]
        public RelayCommand LoadedCommand => new RelayCommand(Loaded);

        [Browsable(false)]
        [XmlIgnore]
        public bool IsLoaded { get; set; }
        protected virtual void Loaded(object obj)
        {
            IsLoaded = true;
        }
        //[XmlIgnore]
        //[Browsable(false)]
        //public virtual string ID { get; set; }

        private string _id;
        [Browsable(false)]
        public virtual string ID
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged();
            }
        }

        private string _name;
        [XmlIgnore]
        [Browsable(false)]
        public virtual string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }


        private string _icon;
        [XmlIgnore]
        [Browsable(false)]
        public virtual string Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                RaisePropertyChanged();
            }
        }

        private string _shortName;
        [XmlIgnore]
        [Browsable(false)]
        public virtual string ShortName
        {
            get { return _shortName; }
            set
            {
                _shortName = value;
                RaisePropertyChanged();
            }
        }

        private string _groupName;
        [XmlIgnore]
        [Browsable(false)]
        public virtual string GroupName
        {
            get { return _groupName; }
            set
            {
                _groupName = value;
                RaisePropertyChanged();
            }
        }

        private string _description;
        [XmlIgnore]
        [Browsable(false)]
        public virtual string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged();
            }
        }


        private int _order;
        [XmlIgnore]
        [Browsable(false)]
        public virtual int Order
        {
            get { return _order; }
            set
            {
                _order = value;
                RaisePropertyChanged();
            }
        }


        private string _tabName;
        [XmlIgnore]
        [Browsable(false)]
        public virtual string TabName
        {
            get { return _tabName; }
            set
            {
                _tabName = value;
                RaisePropertyChanged();
            }
        }

        [XmlIgnore]
        [Display(Name = "恢复默认")]
        [Browsable(false)]
        public virtual RelayCommand LoadDefaultCommand => new RelayCommand((s, e) =>
        {
            this.LoadDefault();
        });

        public virtual void LoadDefault()
        {

            System.Diagnostics.Debug.WriteLine("DisplayerViewModelBase.LoadDefault");

            PropertyInfo[] ps = this.GetType().GetProperties();

            foreach (PropertyInfo p in ps)
            {
                DefaultValueAttribute d = p.GetCustomAttribute<DefaultValueAttribute>();
                if (d == null) continue;
                if (typeof(IConvertible).IsAssignableFrom(p.PropertyType))
                {
                    object value = Convert.ChangeType(d.Value, p.PropertyType);
                    p.SetValue(this, value);
                }
                else
                {
                    p.SetValue(this, d.Value);
                }
            }
        }
    }

    public class DisplayerAttribute : Attribute
    {
        public int Order { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }
        public string Icon { get; set; }
        public string TabName { get; set; }
    }
}
